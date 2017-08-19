using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace hsb.WPF.Utilities
{
    #region 【Extenstion Methods : MessageBoxResult】
    /// <summary>
    /// MessageBoxResult に対する拡張メソッド定義
    /// </summary>
    public static class MessageBoxResultExtensions
    {
        #region - IsPositive : 戻り値が肯定かどうか？
        /// <summary>
        /// 戻り値が肯定かどうか？
        /// </summary>
        /// <param name="result">this MessageBoxResult</param>
        /// <returns>True : 肯定 / False: 否定</returns>
        public static bool IsPositive(this MessageBoxResult result)
        {
            return (result == MessageBoxResult.OK || result == MessageBoxResult.Yes);
        }
        #endregion
    }
    #endregion
}
