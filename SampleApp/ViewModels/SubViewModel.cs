using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using hsb.WPF;

namespace SampleApp.ViewModels
{
    #region 【Class : SubViewModel】
    /// <summary>
    /// SubViewModel
    /// </summary>
    class SubViewModel : ViewModelBase
    {
        #region ■ Properties 

        #region - Book : 書籍
        /// <summary>
        /// 書籍
        /// </summary>
        public DataBindPropertyItem<Models.Book> BookProperty { get; private set; }
        public Models.Book Book
        {
            get { return BookProperty.Value; }
            set { BookProperty.Value = value; }
        }
        #endregion

        #endregion

        #region ■ Commands 

        #region - UpdateCommand : 更新コマンド
        /// <summary>
        /// 更新コマンド
        /// </summary>
        public DelegateCommand UpdateBookCommand { get; private set; }
        #endregion

        #endregion

        #region ■ Constructor 
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="book">書籍</param>
        public SubViewModel(Models.Book book)
        {
            Title = "書籍の編集";

            // プロパティ値の初期設定
            BookProperty = CreateDataBindProperty(nameof(Book), book ?? new Models.Book());

            // コマンドの初期設定
            UpdateBookCommand = CreateCommand((book == null) ? "登　録" : "更　新",
                                              UpdateBook,
                                              v => Book.IsChanged && !Book.HasError,
                                              (book == null) ? "書籍情報を登録します。" : "書籍情報を更新します。");
        }
        #endregion

        #region ■ Methods 

        #region - CanCloseView : ViewをCloseしても良いか？
        /// <summary>
        /// ViewをCloseしても良いか？
        /// </summary>
        /// <param name="arg">CanCloseViewArgs</param>
        /// <returns>True : OK / False : No</returns>
        public override bool CanCloseView(CanCloseViewArgs arg)
        {
            return arg.DialogResult == true ||
                   !Book.IsChanged ||
                   UserConfirm("確認", "変更内容を破棄しますか？", MessageBoxButton.YesNo, MessageBoxImage.Question);
        }
        #endregion

        #region - ClosedView : Viewが閉じられた
        /// <summary>
        /// Viewが閉じられた
        /// </summary>
        /// <param name="arg">ClosedViewArgs</param>
        public override void ClosedView(ClosedViewArgs arg)
        {
            // キャンセルされた場合、編集内容を破棄する
            if (arg.DialogResult != true)
                Book.Reset();
            base.ClosedView(arg);
        }
        #endregion

        #region - UpdateBook : 書籍情報を更新する
        /// <summary>
        /// 書籍情報を更新する
        /// </summary>
        /// <param name="v">コマンド引数</param>
        private void UpdateBook(object v)
        {
            if (Book.ValidationCheck())
            {
                if (UserConfirm("確認", "書籍情報を更新しますか？", MessageBoxButton.YesNo, MessageBoxImage.Question))
                {
                    // 更新処理
                    Book.Update();
                    InvokeCloseView(true);
                }
            }
        }
        #endregion

        #endregion
    }
    #endregion
}
