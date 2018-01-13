using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using hsb.WPF;

namespace SampleApp.ViewModels
{
    #region 【Class : ViewModel】
    /// <summary>
    /// 基底ビューモデル
    /// </summary>
    class ViewModel : ViewModelBase
    {
        #region Properties

        #region - App : アプリケーションオブジェクト
        /// <summary>
        /// アプリケーションオブジェクト
        /// </summary>
        public App App
        {
            get { return Application.Current as App; }
        }
        #endregion

        #endregion
    }
    #endregion
}
