using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace hsb.WPF.DataConverters
{
    #region 【Class : Bool2Visibility】
    /// <summary>
    /// bool <=> Visibility
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class Bool2Visibility : DataConverterBase<bool, Visibility>
    {
        public Bool2Visibility()
        {
            Convert = (v, o) => v ? Visibility.Visible : Visibility.Hidden;
            ConvertBack = (v, o) => (v == Visibility.Visible);
        }
    }
    #endregion
}
