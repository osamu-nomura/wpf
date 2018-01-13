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

    #endregion
}
