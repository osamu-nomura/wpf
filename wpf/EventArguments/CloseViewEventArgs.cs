﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.WPF.EventArguments
{
    #region 【Class : CloseViewEventArgs】
    /// <summary>
    /// Viewに対してのClose通知時イベントパラメーター
    /// </summary>
    public class CloseViewEventArgs : EventArgs
    {
        #region ■ Properties

        #region - DialogResult : Viewがモーダルであった場合に、戻り値として設定されるDialogResult値
        /// <summary>
        /// Viewに渡されるパラメーター
        /// Viewがモーダルであった場合に、戻り値として設定されるDialogResult値
        /// </summary>
        public bool DialogResult { get; private set; }
        #endregion

        #region - Success : Viewから返される結果値
        /// <summary>
        /// Viewから返される結果値
        ///     Viewが正常に閉じられた場合はTrueがセットされることを期待している。
        ///     ただし値のセットはView側のイベントハンドラにて行う必要がある。
        /// </summary>
        public bool Success { get; set; }
        #endregion

        #endregion

        #region ■ Constructor ■
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="dialogResult">Viewへのパラメータ : bool</param>
        public CloseViewEventArgs(bool dialogResult = true)
        {
            DialogResult = dialogResult;
            Success = true;
        }
        #endregion

    }
    #endregion

}
