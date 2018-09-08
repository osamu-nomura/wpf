using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using hsb.Extensions;

namespace hsb.WPF.DataConverters
{
    #region 【Class: StringOmission】
    /// <summary>
    /// string を指定した桁数に省略するするデータコンバーター。
    /// </summary>
    [ValueConversion(typeof(string), typeof(string))]
    public class StringOmission : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var length = ((string)parameter)?.ToInt() ?? 10; ;
            return ((string)value)?.Replace("\r\n", string.Empty).Omission(length) ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
    #endregion
}
