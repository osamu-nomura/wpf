using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using hsb.Extensions;

namespace hsb.WPF.DataConverters
{
    #region 【Class: ZeroSuppress】
    /// <summary>
    /// int→Visibility 
    ///     int が 0 なら Hidden
    /// </summary>
    [ValueConversion(typeof(int), typeof(Visibility))]
    public class ZeroSuppress : DataConverterBase<int, Visibility>
    {
        public ZeroSuppress()
        {
            Convert = (v, o) => (v == 0 ? Visibility.Hidden : Visibility.Visible);
        }
    }
    #endregion
}
