using System;
using System.Collections.Generic;
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
        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MainViewModel() 
        {
            CloseViewCommand.Name = "終了";
            CloseViewCommand.Description = "プログラムを終了します。";
        }
        #endregion
    }
    #endregion
}
