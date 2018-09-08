using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using hsb.Extensions;

namespace hsb.WPF.DataConverters
{
    #region 【Class: Int2String】
    /// <summary>
    /// int <=> string
    /// </summary>
    [ValueConversion(typeof(int), typeof(string))]
    public class Int2String : DataConverterBase<int, string>
    {
        public Int2String()
        {
            Convert = (v, o) => v.ToString("#");
            ConvertBack = (v, o) => v?.ToInt() ?? 0;
        }
    }
    #endregion
}
