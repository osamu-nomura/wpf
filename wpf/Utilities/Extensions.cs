using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace hsb.WPF.Utilities
{
    #region 【Extenstion Methods : MessageBoxResult】
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

    #region 【Extention Methods : Window】

    public static class WindowExtension
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
                var vm = window.DataContext as ViewModelBase;
                if (vm != null)
                {
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
}
