using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hsb.Utilities;
using hsb.Extensions;
using hsb.WPF.DataConverters;
using SampleApp.Models;

namespace SampleApp.Views
{
    #region 【Class : CaptionFormat】
    /// <summary>
    /// Caption -> [Caption]：
    /// </summary>
    class CaptionFormat : DataConverterBase<string, string>
    {
        public CaptionFormat()
        {
            Convert = (v, o) => $"{v}：";
        }
    }
    #endregion

    #region 【Class : BookCategories2DisplayName】
    /// <summary>
    /// BookCategories -> String
    /// </summary>
    class BookCategories2DisplayName : Enum2StingConverter<BookCategories?>
    {
        protected override string Enum2String(BookCategories? e)
        {
            return (e.HasValue) ? AttributeUtil.GetEnumFieldDisplayName(e.Value) : "";
        }
    }
    #endregion

    #region 【Class : ReviewPoint2Stars】
    /// <summary>
    /// ReviewPoint -> ★★★★
    /// </summary>
    class ReviewPoint2Stars : DataConverterBase<int, string>
    {
        public ReviewPoint2Stars()
        {
            Convert = (v, o) => new string('★', v);
            ConvertBack = (v, o) => (v != null) ? v.Length : 0;
        }
    }
    #endregion

    #region 【Class : Decimal2String】
    /// <summary>
    /// Decimal? -> string
    /// </summary>
    class Decimal2String : DataConverterBase<decimal?, string>
    {
        public Decimal2String()
        {
            Convert = (v, o) => v.HasValue ? v.Value.ToString() : null;
            ConvertBack = (v, o) => v.ToDecimal();
        }
    }
    #endregion
}
