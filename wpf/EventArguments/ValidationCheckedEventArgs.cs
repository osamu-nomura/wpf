using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using hsb;
using hsb.WPF.Utilities;

namespace hsb.WPF.EventArguments
{
    #region 【Class : ValidationCheckedEventArgs】
    /// <summary>
    /// バリデーションチェック完了時イベントパラメータ
    /// </summary>
    public class ValidationCheckedEventArgs : EventArgs
    {
        #region ■ Constructor 
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="result">Result</param>
        public ValidationCheckedEventArgs(Result result, object value)
        {
            ValidationResult = result;
            Value = value;
        }
        #endregion

        #region ■ Properties 

        #region - ValidationResult : バリデーションチェックの結果
        /// <summary>
        /// バリデーションチェックの結果
        /// </summary>
        public Result ValidationResult { get; private set; }
        #endregion

        #region - Value : 値
        /// <summary>
        /// 値
        /// </summary>
        public object Value { get; private set; }
        #endregion

        #endregion
    }
    #endregion

}
