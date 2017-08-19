using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;

namespace hsb.WPF
{
    #region 【Class : DelegateCommand】
    /// <summary>
    /// ICommand の実装
    /// 　実際の処理はデリゲートに移譲
    /// </summary>
    public class DelegateCommand : ICommand, INotifyPropertyChanged
    {
        #region ■ Members
        private string _Name = "";
        private string _Description = "";
        private bool _IsEnabled = true;
        #endregion

        #region ■ Properties

        #region - Name : 名称
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set
            {
                if (_Name != value)
                {
                    _Name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }
        #endregion

        #region - Description : 説明
        /// <summary>
        /// 説明
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    RaisePropertyChanged("Description");
                }
            }
        }
        #endregion

        #region - IsEnabled : コマンドは有効？
        /// <summary>
        /// コマンドは有効？
        /// </summary>
        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                if (_IsEnabled != value)
                {
                    _IsEnabled = value;
                    RaiseCanExecuteChanged();
                }
            }
        }
        #endregion

        #endregion

        #region ■ Event / Delegate

        #region - Command : ICommand.Executeのデリゲート
        /// <summary>
        /// ICommand.Executeのデリゲート
        /// </summary>
        private Action<object> Command;
        #endregion

        #region - CanExecute : ICommand.CanExecuteのデリゲート
        /// <summary>
        /// ICommand.CanExecuteのデリゲート
        ///     実行可能な場合、Trueを返す
        /// </summary>
        private Func<object, bool> CanExecute;
        #endregion

        #region - CanExecuteChanged : ICommand.CanExecuteChanged の実装 
        /// <summary>
        /// ICommand.CanExecuteChanged の実装 
        /// </summary>
        event EventHandler ICommand.CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        #endregion

        #region - PropertyChanged : INotifyPropertyChanged.PropertyChanged の実装 
        /// <summary>
        /// INotifyPropertyChanged.PropertyChanged の実装 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="command">Action : ICommand.Executeのデリゲート</param>
        /// <param name="canExecute">Func : ICommand.CanExecuteのデリゲート</param>
        public DelegateCommand(string name, Action<object> command, Func<object, bool> canExecute, string desc = null)
        {
            Name = name;
            Command = command ?? throw new ArgumentNullException();
            CanExecute = canExecute;
            Description = desc;
            IsEnabled = true;
        }
        #endregion

        #region ■ Methods

        #region - Execute : コマンドの実行
        /// <summary>
        /// コマンドの実行
        /// </summary>
        /// <param name="parameter">object : パラメータ</param>
        public void Execute(object parameter)
        {
            Command(parameter);
        }
        #endregion

        #region - ICommand.Execute : ICommand.Executeの実装 
        /// <summary>
        /// ICommand.Executeの実装 
        /// </summary>
        /// <param name="parameter">object : パラメータ</param>
        void ICommand.Execute(object parameter)
        {
            Command(parameter);
        }
        #endregion

        #region - ICommand.CanExecute : ICommand.Executeの実装 
        /// <summary>
        /// ICommand.Executeの実装 
        /// </summary>
        /// <param name="parameter">object : パラメータ</param>
        /// <returns>bool : 実行可否</returns>
        bool ICommand.CanExecute(object parameter)
        {
            if (IsEnabled)
                return CanExecute?.Invoke(parameter) ?? true;
            else
                return false;
        }
        #endregion

        #region - RaisePropertyChanged : INotifyPropertyChanged.PropertyChangedイベントを発生させる。 
        /// <summary>
        /// INotifyPropertyChanged.PropertyChangedイベントを発生させる。 
        /// </summary>
        /// <param name="propertyName">string : プロパティ名</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #endregion

        #region ■　Static Methods

        #region - RaiseCanExecuteChanged : CanExecuteの状態が変更されたことを通知する
        /// <summary>
        /// CanExecuteの状態が変更されたことを通知する
        /// </summary>
        public static void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
        #endregion

        #endregion
    }
    #endregion

}
