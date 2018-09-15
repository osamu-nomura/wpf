using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hsb.Utilities;


namespace hsb.WPF.Utilities
{
    #region 【Static Class : IOFilters】
    /// <summary>
    /// IOフィルタ
    /// </summary>
    public static class IOFilters
    {
        #region ■ Static Methods

        #region - ToUpper : 大文字変換
        /// <summary>
        /// 大文字変換
        /// </summary>
        /// <param name="s">入力文字列</param>
        /// <param name="direction">入出力方向</param>
        /// <returns>出力文字列</returns>
        public static string ToUpper(string s, IOFilterDirection direction)
            => direction.IsOutput() ? s : s.ToUpper();
        #endregion

        #region - ToLower : 小文字変換
        /// <summary>
        /// 小文字変換
        /// </summary>
        /// <param name="s">入力文字列</param>
        /// <param name="direction">入出力方向</param>
        /// <returns>出力文字列</returns>
        public static string ToLower(string s, IOFilterDirection direction)
            => direction.IsOutput() ? s : s.ToLower();
        #endregion

        #region - Trim : 先頭・末尾の空白文字を除去する
        /// <summary>
        /// 先頭・末尾の空白文字を除去する
        /// </summary>
        /// <param name="s">入力文字列</param>
        /// <param name="direction">入出力方向</param>
        /// <returns>出力文字列</returns>
        public static string Trim(string s, IOFilterDirection direction)
            => direction.IsOutput() ? s : s.Trim();
        #endregion

        #region - TrimStart : 先頭の空白文字を除去する
        /// <summary>
        /// 先頭の空白文字を除去する
        /// </summary>
        /// <param name="s">入力文字列</param>
        /// <param name="direction">入出力方向</param>
        /// <returns>出力文字列</returns>
        public static string TrimStart(string s, IOFilterDirection direction)
            => direction.IsOutput() ? s : s.TrimStart();
        #endregion

        #region - TrimEnd : 末尾の空白文字を除去する
        /// <summary>
        /// 末尾の空白文字を除去する
        /// </summary>
        /// <param name="s">入力文字列</param>
        /// <param name="direction">入出力方向</param>
        /// <returns>出力文字列</returns>
        public static string TrimEnd(string s, IOFilterDirection direction)
            => direction.IsOutput() ? s : s.TrimEnd();
        #endregion

        #region - Han2Zen : 半角文字を全角にする
        /// <summary>
        /// 半角文字を全角にする
        /// </summary>
        /// <param name="s">入力文字列</param>
        /// <param name="direction">入出力方向</param>
        /// <returns>出力文字列</returns>
        public static string Han2Zen(string s, IOFilterDirection direction)
            => direction.IsOutput() ? s : MapString.Han2Zen(s);
        #endregion

        #region - Zen2Han : 全角文字を半角にする
        /// <summary>
        /// 全角文字を半角にする
        /// </summary>
        /// <param name="s">入力文字列</param>
        /// <param name="direction">入出力方向</param>
        /// <returns>出力文字列</returns>
        public static string Zen2Han(string s, IOFilterDirection direction)
            => direction.IsOutput() ? s : MapString.Zen2Han(s);
        #endregion

        #endregion
    }
    #endregion
}
