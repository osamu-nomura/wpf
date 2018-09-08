using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using hsb.Extensions;

namespace hsb.WPF.DataConverters
{
    #region 【Class: NullableInt2String】
    /// <summary>
    /// int? <=> string
    /// </summary>
    [ValueConversion(typeof(int?), typeof(string))]
    public class NullableInt2String : DataConverterBase<int?, string>
    {
        public NullableInt2String()
        {
            Convert = (v, o) => (v.HasValue) ? v.Value.ToString() : "";
            ConvertBack = (v, o) => v?.ToInt();
        }
    }
    #endregion
}
