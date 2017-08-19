using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.WPF.EventArguments
{
    #region 【Class : PropertyItemEventArgs】
    /// <summary>
    /// プロパティアイテムイベントパラメータ
    /// </summary>
    public class PropertyItemEventArgs : EventArgs
    {
        #region ■ Properties

        #region *- Property : プロパティアイテム
        /// <summary>
        /// プロパティアイテム
        /// </summary>
        public PropertyItemBase Property { get; private set; }
        #endregion

        #endregion
        #region ■ Constructor 
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PropertyItemEventArgs(PropertyItemBase property)
        {
            Property = Property;
        }
        #endregion

    }
    #endregion

}
