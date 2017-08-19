using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hsb.WPF.Utilities;

namespace hsb.WPF.EventArguments
{
    #region 【Class : PropertyItemInvalidatedEventArgs】
    /// <summary>
    /// バリデーションエラー時イベントパラメータ
    /// </summary>
    public class PropertyItemInvalidatedEventArgs : PropertyItemEventArgs
    {
        #region ■ Properties ■

        #region - ValidationResult : バリデーションチェックの結果
        /// <summary>
        /// バリデーションチェックの結果
        /// </summary>
        public Result ValidationResult { get; private set; }
        #endregion

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PropertyItemInvalidatedEventArgs(PropertyItemBase property, Result result)
            : base(property)
        {
            ValidationResult = result;
        }
        #endregion

    }
    #endregion

}
