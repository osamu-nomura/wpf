using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hsb.Classes;
using hsb.Utilities;
using hsb.WPF.Utilities;
using SampleApp.Models;

namespace SampleApp.Views
{
    #region 【Class : BookCategoriesSource】
    /// <summary>
    /// BookCategories ID with Name List Provider
    /// </summary>
    class BookCategoriesSource : ListProvider<ValueWithName<BookCategories>>
    {
        public BookCategoriesSource()
        {
            Items = EnumUtil.GetEnumList<BookCategories>();
        }
    }
    #endregion

    #region 【Class : ReviewPointsSource】
    /// <summary>
    /// ReviewPoints with Name List Provider
    /// </summary>
    class ReviewPointsSource : ListProvider<string>
    {
        public ReviewPointsSource()
        {
            Items = new List<string> { "", "★", "★★", "★★★", "★★★★", "★★★★★" };
        }
    }
    #endregion
}
