using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace hsb.WPF.Utilities
{
    #region 【Extension Methods : MessageBoxResult】
    /// <summary>
    /// MessageBoxResult に対する拡張メソッド定義
    /// </summary>
    public static class MessageBoxResultExtensions
    {
        #region - IsPositive : 戻り値が肯定かどうか？
        /// <summary>
        /// 戻り値が肯定かどうか？
        /// </summary>
        /// <param name="result">this MessageBoxResult</param>
        /// <returns>True : 肯定 / False: 否定</returns>
        public static bool IsPositive(this MessageBoxResult result)
        {
            return (result == MessageBoxResult.OK || result == MessageBoxResult.Yes);
        }
        #endregion
    }
    #endregion

    #region 【Extension Methods : Window】

    public static class WindowExtensions
    {
        #region - Setup : Windowクラスに対する共通初期設定
        /// <summary>
        /// Windowクラスに対する共通初期設定
        /// 　Windowクラスのコンストラクタ内で、InitializeComponentより前に呼び出す。
        /// </summary>
        /// <param name="window">this : Window</param>
        /// <param name="options">初期設定オプションのセット : int</param>
        public static void Setup(this Window window, WindowSetupOptions options = WindowSetupOptions.SetAll)
        {
            // DataContext変更時のイベントハンドラを設定する
            window.DataContextChanged += (o, e) =>
            {
                // 以前のViewModelに切断通知を送る
                if (e.OldValue is ViewModelBase)
                {
                    var oldViewMolde = e.OldValue as ViewModelBase;
                    oldViewMolde?.DisconnectedView(window);
                }

                var vm = window.DataContext as ViewModelBase;
                if (vm != null)
                {
                    // GetViewデレゲートの設定
                    vm.GetView = () => { return window; };

                    // ShowMessageBoxイベントのハンドラを設定
                    if ((options & WindowSetupOptions.SetShowMessageBoxHandler) == WindowSetupOptions.SetShowMessageBoxHandler)
                    {
                        vm.ShowMessageBox += (sender, arg) =>
                        {
                            arg.Result = MessageBox.Show(arg.Message, arg.Caption, arg.Buttons, arg.Icon);
                        };
                    }

                    // CloseViewイベントのハンドラを設定
                    if ((options & WindowSetupOptions.SetCloseViewHandler) == WindowSetupOptions.SetCloseViewHandler)
                    {
                        vm.CloseView += (sender, arg) =>
                        {
                            // モーダルダイアログならDialogResultを設定、モードレスなら Closeメソッドを呼ぶ
                            if (ComponentDispatcher.IsThreadModal)
                                window.DialogResult = arg.DialogResult;
                            else
                                window.Close();
                        };
                    }

                    // ViewModelへ接続を通知する
                    vm.ConnectedView(window);
                }
            };

            // ClosingイベントでViewModelのCanCloseViewe()をコールする
            window.Closing += (o, e) =>
            {
                var vm = window.DataContext as ViewModelBase;
                if (vm != null)
                    e.Cancel = !vm.CanCloseView(new ViewModelBase.CanCloseViewArgs(ComponentDispatcher.IsThreadModal, window.DialogResult));
            };

            // ClosedイベントでViewModelのClosedView()をコールする
            window.Closed += (o, e) =>
            {
                var vm = window.DataContext as ViewModelBase;
                if (vm != null)
                {
                    vm.ClosedView(new ViewModelBase.ClosedViewArgs(ComponentDispatcher.IsThreadModal, window.DialogResult));
                }
            };
        }
        #endregion
    }
    #endregion

    #region 【Extension Methods : Canvas】
    /// <summary>
    /// Canvas に対する拡張メソッド定義
    /// </summary>
    public static class CanvasExtensions
    {
        #region - SaveBitmapFile : BMPファイルにCANVSの内容を出力する
        /// <summary>
        /// BMPファイルにCANVSの内容を出力する
        /// </summary>
        /// <param name="canvas">this キャンバス</param>
        /// <param name="path">ファイル出力先PATH</param>
        public static void SaveBitmapFile(this Canvas canvas, string path)
        {
            // レイアウトを再計算させる
            var size = new Size(canvas.Width, canvas.Height);
            canvas.Measure(size);
            canvas.Arrange(new Rect(size));

            // VisualObjectをBitmapに変換する
            var renderBitmap = new RenderTargetBitmap((int)size.Width,                  // 画像の幅
                                                      (int)size.Height,                 // 画像の高さ
                                                      96.0d,                            // 横96.0DPI
                                                      96.0d,                            // 縦96.0DPI
                                                      PixelFormats.Pbgra32);            // 32bit色
            renderBitmap.Render(canvas);

            // 出力用の FileStream を作成する
            using (var os = new FileStream(path, FileMode.Create))
            {
                // 変換したBitmapをエンコードしてFileStreamに保存する。
                var encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                encoder.Save(os);
            }
        }
        #endregion

    }
    #endregion

    #region 【Extension Methods : Visual】
    /// <summary>
    /// Visualに対する拡張メソッド定義
    /// </summary>
    public static class VisualExtensions
    {
        #region - GetDpiScaleFactor : スケーリングの倍率を取得する。
        /// <summary>
        /// スケーリングの倍率を取得する。
        /// </summary>
        /// <param name="visual">this</param>
        /// <returns>Point</returns>
        public static Point GetDpiScaleFactor(this Visual visual)
        {
            var source = PresentationSource.FromVisual(visual);
            if (source?.CompositionTarget != null)
            {
                return new Point(
                    source.CompositionTarget.TransformToDevice.M11,
                    source.CompositionTarget.TransformToDevice.M22);
            }
            return new Point(1.0, 1.0);
        }
        #endregion
    }
    #endregion

    #region 【Extension Methods : Control】
    /// <summary>
    /// Control に対する拡張メソッド定義
    /// </summary>
    public static class ControlExtensions
    {
        #region - GetParentWindow : 親ウィンドウを返す。
        /// <summary>
        /// 親ウィンドウを返す。
        /// </summary>
        /// <param name="ctrl">this</param>
        /// <returns>Window</returns>
        public static Window GetParentWindow(this Control ctrl)
        {
            var obj = ctrl.Parent;
            while (!(obj is Window))
            {
                if (obj is FrameworkElement)
                    obj = (obj as FrameworkElement).Parent;
                else
                    break;
            }
            return obj as Window;
        }
        #endregion
    }
    #endregion

    #region 【Extension Methods : DependencyObject】
    /// <summary>
    /// DependencyObject に対する拡張メソッド定義
    /// </summary>
    public static class DependencyObjectExtensions
    {
        #region - WalkInChildren : 子オブジェクトに対してデリゲートを実行する
        /// <summary>
        /// 子オブジェクトに対してデリゲートを実行する
        /// </summary>
        /// <param name="obj">this</param>
        /// <param name="action">デリゲート</param>
        public static void WalkInChildren(this DependencyObject obj, Action<DependencyObject> action)
        {
            void Walk(DependencyObject o)
            {
                foreach (var child in LogicalTreeHelper.GetChildren(o))
                {
                    if (child is DependencyObject)
                    {
                        action(child as DependencyObject);
                        Walk(child as DependencyObject);
                    }
                }
            }

            if (action == null)
                throw new ArgumentNullException();
            Walk(obj);
        }
        #endregion

        #region - FindChild : 子オブジェクトを取得する
        /// <summary>
        /// 子オブジェクトを取得する　
        /// </summary>
        /// <param name="obj">this</param>
        /// <param name="childType">型</param>
        /// <param name="childName">名称</param>
        /// <returns>DependencyObject</returns>
        public static DependencyObject FindChild(this DependencyObject obj, Type childType, string childName)
        {
            DependencyObject foundChild = null;
            if (obj != null)
            {

                var childrenCount = VisualTreeHelper.GetChildrenCount(obj);
                for (var i = 0; i < childrenCount; i++)
                {

                    var child = VisualTreeHelper.GetChild(obj, i);
                    if (child.GetType() != childType)
                    {
                        foundChild = FindChild(child, childType, childName);
                        if (foundChild != null) break;
                    }
                    else if (!string.IsNullOrEmpty(childName))
                    {
                        var frameworkElement = child as FrameworkElement;
                        if (frameworkElement != null && frameworkElement.Name == childName)
                        {
                            foundChild = child;
                            break;
                        }
                    }
                    else
                    {
                        foundChild = child;
                        break;
                    }
                }
            }
            return foundChild;
        }
        #endregion
    }
    #endregion

}
