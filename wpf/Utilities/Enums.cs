using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.WPF.Utilities
{
    #region 【Enums】

    #region - WindowSetupOptions : 初期設定オプション定義
    /// <summary>
    /// 初期設定オプション定義
    /// </summary>
    public enum WindowSetupOptions
    {
        SetShowMessageBoxHandler = 0x1,     // ViewModelからのメッセージボックス表示通知のハンドラーを設定する
        SetCloseViewHandler = 0x2,          // ViewModelからのClose要求通知のハンドラーを設定する
        SetAll = 0x3                        // すべてを設定する
    }
    #endregion

    #region - IOFilterDirection : IOフィルター入出力方向
    /// <summary>
    /// IOフィルター入出力方向
    /// </summary>
    public enum IOFilterDirection
    {
        Input,  // 入力
        Output  // 出力
    }
    public static class IOFilterDirectionExtesion
    {
        #region - IsInput : 入力？
        /// <summary>
        /// 入力？
        /// </summary>
        /// <param name="direction">IOフィルター入出力方向</param>
        /// <returns>True : 入力 / False : 出力</returns>
        public static bool IsInput(this IOFilterDirection direction)
        {
            return direction == IOFilterDirection.Input;
        }
        #endregion

        #region - IsOutput : 出力？
        /// <summary>
        /// 出力？
        /// </summary>
        /// <param name="direction">IOフィルター入出力方向</param>
        /// <returns>True : 出力 / False : 入力</returns>
        public static bool IsOutput(this IOFilterDirection direction)
        {
            return direction == IOFilterDirection.Output;
        }
        #endregion
    }
    #endregion

    #endregion
}
