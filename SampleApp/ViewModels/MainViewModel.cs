using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using hsb.WPF;

namespace SampleApp.ViewModels
{
    #region 【Class : MainViewModel】
    /// <summary>
    /// MainWindow ViewModel
    /// </summary>
    class MainViewModel : ViewModelBase
    {
        #region ■ Properties 

        #region - Application : アプリケーションオブジェクト
        /// <summary>
        /// アプリケーションオブジェクト
        /// </summary>
        public App App
        {
            get { return Application.Current as App; }
        }
        #endregion

        #region - Books : 書籍リスト
        /// <summary>
        /// 書籍リスト
        /// </summary>
        public ObservableCollection<Models.Book> Books { get; private set; }
        #endregion

        #region - SelectedBook : 選択中の書籍
        /// <summary>
        /// 選択中の書籍
        /// </summary>
        public DataBindPropertyItem<Models.Book> SelectedBookProperty { get; private set; }
        public Models.Book SelectedBook
        {
            get { return SelectedBookProperty.Value; }
            set { SelectedBookProperty.Value = value; }
        }
        #endregion

        #endregion

        #region ■ Commands 

        #region - RemoveBookCommand : 書籍を削除する
        /// <summary>
        /// 書籍を削除する
        /// </summary>
        public DelegateCommand RemoveBookCommand { get; private set; }
        #endregion

        #region - EditCommand : 書籍を追加する
        /// <summary>
        /// 書籍を追加する
        /// </summary>
        public DelegateCommand AddBookCommand { get; private set; }
        #endregion

        #region - EditBookCommand : 書籍を編集する
        /// <summary>
        /// 書籍を編集する
        /// </summary>
        public DelegateCommand EditBookCommand { get; private set; }
        #endregion

        #region - PrintBookListCommand : 蔵書リストの印刷
        /// <summary>
        /// 蔵書リストの印刷
        /// </summary>
        public DelegateCommand PrintBookListCommand { get; private set; }
        #endregion

        #region - PreviewBookListCommand : 蔵書リストのプレビュー表示
        /// <summary>
        /// 蔵書リストのプレビュー表示
        /// </summary>
        public DelegateCommand PreviewBookListCommand { get; private set; }
        #endregion

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainViewModel() 
        {
            // プロパティ値の設定
            Books = null;
            SelectedBookProperty = CreateDataBindProperty<Models.Book>("SelectedBook", null);

            // コマンドの設定
            AddBookCommand = CreateCommand("書籍を追加", AddBook, null, "新たな書籍を追加します。");
            EditBookCommand = CreateCommand("書籍を編集", EditBook, v => SelectedBook != null, "選択中の書籍を編集します。");
            RemoveBookCommand = CreateCommand("書籍を削除", RemoveBook, v => SelectedBook != null, "選択中の書籍を削除します。");
            PrintBookListCommand = CreateCommand("リストの印刷", PrintBookList, null, "蔵書リストを印刷します。");
            PreviewBookListCommand = CreateCommand("リストのプレビュー", PreviewBookList, null, "蔵書リストをプレビュー表示します。");

            CloseViewCommand.Name = "終了";
            CloseViewCommand.Description = "プログラムを終了します。";

            // 初期値となる書籍のリストを取得する
            Books = new ObservableCollection<Models.Book>(Models.Book.GetBookList());
            if (Books.Count > 0)
                SelectedBook = Books.First();
        }
        #endregion

        #region ■ Methods 

        #region - CanCloseView : Viewをクローズしてもよい？
        /// <summary>
        /// Viewをクローズしてもよい？
        /// </summary>
        /// <param name="arg">CanCloseViewArgs</param>
        /// <returns>True : OK / False : NO</returns>
        public override bool CanCloseView(CanCloseViewArgs arg)
        {
            if (CloseViewCommand.IsEnabled)
                return UserConfirm("確認", "終了しますか？", MessageBoxButton.YesNo, MessageBoxImage.Question);
            else
                return false;
        }
        #endregion

        #region - AddBook : 書籍を追加する
        /// <summary>
        /// 書籍を追加する
        /// </summary>
        /// <param name="o">コマンド引数</param>
        private void AddBook(object o)
        {
            var sub = new SubViewModel(null);
            if (App.Views.ShowModalView(sub, this))
            {
                if (sub.Book != null)
                    Books.Add(sub.Book);
            }
        }
        #endregion

        #region - EditBook : 書籍を編集する
        /// <summary>
        /// 書籍を編集する
        /// </summary>
        /// <param name="o">コマンド引数</param>
        private void EditBook(object o)
        {
            if (SelectedBook != null)
            {
                var sub = new SubViewModel(SelectedBook);
                App.Views.ShowModalView(sub, this);
            }
        }
        #endregion

        #region - RemoveBook : 選択中の書籍を削除する
        /// <summary>
        /// 選択中の書籍を削除する
        /// </summary>
        /// <param name="o">コマンド引数</param>
        private void RemoveBook(object o)
        {
            if (SelectedBook != null && UserConfirm("確認", "選択した書籍を削除しますか？", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            {
                SelectedBook.Delete();
                Books.Remove(SelectedBook);
                SelectedBook = null;
            }
        }
        #endregion

        #region - PrintBookList : 蔵書リストの出力
        /// <summary>
        /// 蔵書リストの出力
        /// </summary>
        /// <param name="o">コマンド引数</param>
        private async void PrintBookList(object o)
        {
            if (UserConfirm("確認", "蔵書リストの印刷を実行しますか？", MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                // 別スレッドで実行
                CloseViewCommand.IsEnabled = false;
                await Task.Run(() =>
                {
                    // UIスレッドで生成した書籍オブジェクトをそのまま利用するとエラーになるので、
                    // 複製したものをレポートオブジェクトに渡す。
                    var report = new Reports.BookListReport(Books.Select(v => v.Clone()));
                    report.Print();
                });
                CloseViewCommand.IsEnabled = true;
            }
        }
        #endregion

        #region - PreviewBookList : 蔵書リストのプレビュー表示
        /// <summary>
        /// 蔵書リストのプレビュー表示
        /// </summary>
        /// <param name="o">コマンド引数</param>
        private void PreviewBookList(object o)
        {
            var report = new Reports.BookListReport(Books);
            var uri = "pack://book_list.xps";
            var doc = report.CreateXpsDocumet(uri);
            if (doc != null)
            {
                try
                {
                    var preview = new PreviewViewModel(doc);
                    App.Views.ShowModalView(preview, this);
                }
                finally
                {
                    Report.RemoveDocument(uri);
                }
            }
        }
        #endregion

        #endregion

    }
    #endregion
}
