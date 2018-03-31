using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hsb;
using hsb.Extensions;

namespace Calc.Model
{
    #region 【Class : StackCalculator】
    /// <summary>
    /// StackCalculator
    /// </summary>
    public class StackCalculator
    {
        private enum Operator { Add, Sub, Mul, Div };


        #region ■ Properties

        #region - Stacks : スタックリスト
        /// <summary>
        /// スタックリスト
        /// </summary>
        private List<Stack<decimal>> Stacks { get; set; }
        #endregion

        #region - CurrentStack : カレントスタック
        /// <summary>
        /// カレントスタック
        /// </summary>
        private Stack<decimal> CurrentStack { get; set; }
        #endregion

        #region - Value : 値
        /// <summary>
        /// 値 - カレントスタックの上端の値
        /// </summary>
        public decimal Value
        {
            get { return CurrentStack.Peek(); }
        }
        #endregion

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public StackCalculator(int? capacity = null)
        {
            // 0 ～ 9 までのスタックを生成
            Stacks = new List<Stack<decimal>>
            {
                new Stack<decimal>(capacity ?? 100),
                new Stack<decimal>(capacity ?? 100),
                new Stack<decimal>(capacity ?? 100),
                new Stack<decimal>(capacity ?? 100),
                new Stack<decimal>(capacity ?? 100),
                new Stack<decimal>(capacity ?? 100),
                new Stack<decimal>(capacity ?? 100),
                new Stack<decimal>(capacity ?? 100),
                new Stack<decimal>(capacity ?? 100)
            };
            CurrentStack = Stacks.First();
        }
        #endregion

        #region ■ Methods

        #region - Push : 値をスタックにセットする
        /// <summary>
        /// 値をスタックにセットする
        /// </summary>
        /// <param name="values">値</param>
        public decimal Push(IEnumerable<decimal> values)
        {
            values.ForEach(v => CurrentStack.Push(v));
            return CurrentStack.Peek();
        }
        #endregion

        #region - Push : 値をスタックにセットする
        /// <summary>
        /// 値をスタックにセットする
        /// </summary>
        /// <param name="values">値</param>
        public decimal Push(params decimal[] values)
            => Push((IEnumerable<decimal>)values);
        #endregion

        private decimal Calc(Operator op, decimal v1, decimal v2)
        {
            switch (op)
            {
                case Operator.Add: return Push(v1 + v2);
                case Operator.Sub: return Push(v1 - v2);
                case Operator.Mul: return Push(v1 * v2);
                case Operator.Div: return Push(v1 / v2);
                default:
                    throw new ApplicationException("Invalid Operator");
            }
        }

        public decimal Sum()
        {
            var sum = CurrentStack.Sum();
            CurrentStack.Clear();
            return Push(sum);
        }

        #region - Add : 値を加算した結果をスタックに積む
        /// <summary>
        /// 値を加算した結果をスタックに積む
        /// </summary>
        /// <param name="v1">値1</param>
        /// <param name="v2">値2</param>
        /// <returns>計算結果値</returns>
        public decimal Add(decimal v1, decimal v2)
        {
            return Calc(Operator.Add, v1, v2);
        }
        #endregion

        #region - Sub : 値を減算した結果をスタックに積む
        /// <summary>
        /// 値を減算した結果をスタックに積む
        /// </summary>
        /// <param name="v1">値1</param>
        /// <param name="v2">値2</param>
        /// <returns>計算結果</returns>
        public decimal Sub(decimal v1, decimal v2)
        {
            return Calc(Operator.Sub, v1, v2);
        }

        public decimal Mul(decimal v1, decimal v2)
        {
            return Calc(Operator.Mul, v1, v2);
        }

        public decimal Div(decimal v1, decimal v2)
        {
            return Calc(Operator.Div, v1, v2);
        }

        #endregion
    }
    #endregion
}
