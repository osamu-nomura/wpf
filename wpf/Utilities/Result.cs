using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.WPF.Utilities
{
    #region 【Class : Result】
    /// <summary>
    /// 正否とエラー時のメッセージを保持するクラス
    /// </summary>
    public class Result
    {
        #region ■ Private Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="result">bool : 結果値</param>
        /// <param name="description">string : エラーメッセージ</param>
        private Result(bool result, string errorMessage)
        {
            Value = result;
            ErrorMessage = errorMessage;
        }
        #endregion

        #region ■ Properties

        #region - Value : 正否の値
        /// <summary>
        /// 正否の値
        /// </summary>
        private bool Value { get; set; }
        #endregion

        #region - Successful : 成功した
        /// <summary>
        /// 成功した
        /// </summary>
        public bool Successful { get { return Value; } }
        #endregion

        #region - Failed : 失敗した
        /// <summary>
        /// 失敗した
        /// </summary>
        public bool Failed { get { return !Value; } }
        #endregion

        #region - ErrorMessage : エラーメッセージ
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ErrorMessage { get; private set; }
        #endregion

        #endregion

        #region ■ Private Static Members

        #region - _Success : 成功値
        /// <summary>
        /// 成功値（成功値は常にこのインスタンスを使いまわす）
        /// </summary>
        private static Result _Success = new Result(true, null);
        #endregion

        #endregion

        #region ■ Static Methods 

        #region - Success : 成功値を返す
        /// <summary>
        /// 成功値を返す
        /// </summary>
        /// <returns>Result</returns>
        public static Result Success()
        {
            return _Success;
        }
        #endregion

        #region - Fail : 失敗値を返す
        /// <summary>
        /// 失敗値を返す
        /// </summary>
        /// <param name="description">string : 説明文</param>
        /// <returns>Result</returns>
        public static Result Fail(string description)
        {
            return new Result(false, description);
        }
        #endregion

        #region - Create : Resultクラスを返す
        /// <summary>
        /// Resultクラスを返す
        /// </summary>
        /// <param name="result">bool : 結果値</param>
        /// <param name="errorMessage">string : エラーメッセージ</param>
        /// <returns>Result</returns>
        public static Result Create(bool result, string errorMessage)
        {
            return result ? _Success : Fail(errorMessage);
        }
        #endregion

        #endregion
    }
    #endregion

}
