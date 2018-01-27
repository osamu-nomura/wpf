﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;

namespace hsb.WPF.DataConverters
{
    #region 【Class : Enum2StingConverter】
    /// <summary>
    /// Enum用のデーターコンバーター
    /// </summary>
    /// <typeparam name="T">型</typeparam>
    public abstract class Enum2StingConverter<T> : IValueConverter
    {
        #region ■ Portected Methods 

        #region - Enum2String : Enumを文字列にする
        /// <summary>
        /// Enumを文字列にする
        /// </summary>
        /// <param name="e">T : Enum型</param>
        /// <returns>string</returns>
        protected virtual string Enum2String(T e)
        {
            return e.ToString();
        }
        #endregion

        #endregion

        #region ■ Methods

        #region - Convert : T -> string
        /// <summary>
        /// T -> string
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="targetType">型</param>
        /// <param name="parameter">引数</param>
        /// <param name="culture">カルチャ情報</param>
        /// <returns>object</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Enum2String((T)value);
        }
        #endregion

        #region - ConvertBack : string -> T
        /// <summary>
        /// string -> T
        /// </summary>
        /// <param name="value">値</param>
        /// <param name="targetType">型</param>
        /// <param name="parameter">引数</param>
        /// <param name="culture">Cカルチャ情報</param>
        /// <returns>object</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var n in Enum.GetValues(typeof(T)))
            {
                if (Enum2String((T)n) == (string)value)
                    return (T)n;
            }
            throw new ArgumentException();
        }
        #endregion

        #endregion
    }
    #endregion
}
