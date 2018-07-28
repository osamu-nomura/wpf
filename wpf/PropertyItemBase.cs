using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using hsb;
using hsb.WPF.Utilities;
using hsb.WPF.EventArguments;

namespace hsb.WPF
{
    #region 【Abstract Class : PropertyItemBase】
    /// <summary>
    /// プロパティ項目抽象クラス
    /// </summary>
    public abstract class PropertyItemBase : IDataErrorInfo, INotifyPropertyChanged
    {
        #region ■ Private Members 

        #region - _Name : プロパティ名
        /// <summary>
        /// プロパティ名
        /// </summary>
        private string _Name = null;
        #endregion

        #region - _AcceptInvalidValue : 不正な値を受け付ける？
        /// <summary>
        /// 不正な値を受け付ける？
        /// </summary>
        private bool _AcceptInvalidValue = true;
        #endregion

        #region - _IsChanged : フィールドが変更された
        /// <summary>
        /// フィールドが変更された
        /// </summary>
        private bool _IsChanged = false;
        #endregion

        #region - _ErrorMessage : エラーメッセージ
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        private string _ErrorMessage = null;
        #endregion

        #region - _IsReadOnly : 読み取り専用
        /// <summary>
        /// 読み取り専用
        /// </summary>
        private bool _IsReadOnly = false;
        #endregion

        #region - _Tag : タグ
        /// <summary>
        /// タグ
        /// </summary>
        private object _Tag = null;
        #endregion

        #region - : _Caption : キャプション
        /// <summary>
        /// キャプション
        /// </summary>
        private string _Caption = null;
        #endregion

        #region - _Description : 説明
        /// <summary>
        /// 説明
        /// </summary>
        private string _Description = null;
        #endregion

        #endregion

        #region ■ Properties

        #region - Name : プロパティ名
        /// <summary>
        /// プロパティ名
        /// </summary>
        public string Name
        {
            get { return _Name; }
            protected set
            {
                if (_Name != value)
                {
                    _Name = value;
                    RaisePropertyChanged(nameof(Name));
                }
            }
        }
        #endregion

        #region - AcceptInvalidValue : 不正な値を受け付ける？
        /// <summary>
        /// 不正な値を受け付ける？
        ///    Trueの場合、ValidationCheckでエラーになった値もValueへの代入を認める
        /// </summary>
        public bool AcceptInvalidValue
        {
            get { return _AcceptInvalidValue; }
            set
            {
                if (_AcceptInvalidValue != value)
                {
                    _AcceptInvalidValue = value;
                    RaisePropertyChanged(nameof(AcceptInvalidValue));
                }
            }
        }
        #endregion

        #region - IsChanged : フィールドが変更された
        /// <summary>
        /// フィールドが変更された
        /// </summary>
        public bool IsChanged
        {
            get { return _IsChanged; }
            protected set
            {
                if (_IsChanged != value)
                {
                    _IsChanged = value;
                    RaisePropertyChanged(nameof(IsChanged));
                }
            }
        }
        #endregion

        #region - ErrorMessage : エラーメッセージ
        /// <summary>
        /// エラーメッセージ
        /// </summary>
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
            private set
            {
                if (_ErrorMessage != value)
                {
                    _ErrorMessage = value;
                    RaisePropertyChanged(nameof(ErrorMessage));
                    RaisePropertyChanged(nameof(HasError));
                }
            }
        }
        #endregion

        #region - HasError : エラーが存在する？
        /// <summary>
        /// エラーが存在する？
        /// </summary>
        public bool HasError { get { return !string.IsNullOrEmpty(ErrorMessage); } }
        #endregion

        #region - IsReadOnly : 読み取り専用
        /// <summary>
        /// 読み取り専用
        /// </summary>
        public bool IsReadOnly
        {
            get { return _IsReadOnly; }
            set
            {
                if (_IsReadOnly != value)
                {
                    _IsReadOnly = value;
                    RaisePropertyChanged(nameof(IsReadOnly));
                }
            }
        }
        #endregion

        #region - Tag : タグ
        /// <summary>
        /// タグ
        /// </summary>
        public object Tag
        {
            get { return _Tag; }
            set
            {
                if (_Tag != value)
                {
                    _Tag = value;
                    RaisePropertyChanged(nameof(Tag));
                }
            }
        }
        #endregion

        public string Caption
        {
            get { return _Caption;  }
            set
            {
                if (_Caption != value)
                {
                    _Caption = value;
                    RaisePropertyChanged(nameof(Caption));
                }
            }
        }

        #region - Description : 説明
        /// <summary>
        /// 説明
        /// </summary>
        public string Description
        {
            get { return _Description; }
            set
            {
                if (_Description != value)
                {
                    _Description = value;
                    RaisePropertyChanged(nameof(Description));
                }
            }
        }
        #endregion

        #region - IDataErrorInfo.Error : IDataErrorInfo.Error の実装
        /// <summary>
        /// IDataErrorInfo.Error の実装
        /// </summary>
        string IDataErrorInfo.Error
        {
            get { return ErrorMessage; }
        }
        #endregion

        #region - IDataErrorInfo.Item : IDataErrorInfo.Item の実装
        /// <summary>
        /// IDataErrorInfo.Item の実装
        /// </summary>
        /// <param name="columnName">string : フィールド名</param>
        /// <returns></returns>
        string IDataErrorInfo.this[string columnName]
        {
            get { return (columnName == "Value") ? ErrorMessage : null; }
        }
        #endregion

        #endregion

        #region ■ Events / Delegate

        #region - PropertyChanged : INotifyPropertyChanged.PropertyChanged の実装 
        /// <summary>
        /// INotifyPropertyChanged.PropertyChanged の実装 
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region - ValidationChecked : バリデーションチェック後イベント 
        /// <summary>
        /// バリデーションチェック後イベント 
        /// </summary>
        public event EventHandler<ValidationCheckedEventArgs> ValidationChecked;
        #endregion

        #region - Invalidated : インバリッドイベント
        /// <summary>
        /// インバリッドイベント
        /// </summary>
        public event EventHandler<ValidationCheckedEventArgs> Invalidated;
        #endregion

        #region - ValueChanging : 値変更前イベント
        /// <summary>
        /// 値変更前イベント
        /// </summary>
        public event EventHandler<ValueChangingEventArgs> ValueChanging;
        #endregion

        #region - ValueChanged : 値変更後イベント
        /// <summary>
        /// 値変更後イベント
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> ValueChanged;
        #endregion

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PropertyItemBase()
        {
            // Got nothing to do...
        }
        #endregion

        #region ■ Protected Methods 

        #region - RaisePropertyChanged : INotifyPropertyChanged.PropertyChangedイベントを発生させる。
        /// <summary>
        /// INotifyPropertyChanged.PropertyChangedイベントを発生させる。 
        /// </summary>
        /// <param name="propertyName">string : プロパティ名</param>
        protected void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region - InvokeValidationChecked : バリデーションチェック後イベントを発行する
        /// <summary>
        /// バリデーションチェック後イベントを発行する
        /// </summary>
        /// <param name="result">Resut : バリデーションチェック結果</param>
        /// <param name="value">object : 値</param>
        protected virtual void InvokeValidationChecked(Result result, object value)
        {
            ValidationChecked?.Invoke(this, new ValidationCheckedEventArgs(result, value));
        }
        #endregion

        #region - InvokeInvalidated : インバリッドイベントを発行する
        /// <summary>
        /// インバリッドイベントを発行する
        /// </summary>
        /// <param name="result">Resut : バリデーションチェック結果</param>
        /// <param name="value">object : 値</param>
        protected virtual void InvokeInvalidated(Result result, object value)
        {
            Invalidated?.Invoke(this, new ValidationCheckedEventArgs(result, value));
        }
        #endregion

        #region - InvokeValueChanging : 値変更前イベントを発生させる。
        /// <summary>
        /// 値変更前イベントを発生させる。
        /// </summary>
        /// <param name="e">ValueChangingEventArgs</param>
        protected virtual void InvokeValueChanging(ValueChangingEventArgs e)
        {
            ValueChanging?.Invoke(this, e);
        }
        #endregion

        #region - InvokeValueChanged : 値変更後イベントを発生させる。
        /// <summary>
        /// 値変更後イベントを発生させる。
        /// </summary>
        /// <param name="e">ValueChangedEventArgs</param>
        protected virtual void InvokeValueChanged(ValueChangedEventArgs e)
        {
            if (!IsChanged)
                IsChanged = true;
            ValueChanged?.Invoke(this, e);
        }
        #endregion

        #region - SetError : エラーメッセージのセット(1)
        /// <summary>
        /// エラーメッセージのセット
        /// </summary>
        /// <param name="s"></param>
        protected virtual void SetError(string s)
        {
            ErrorMessage = s;
        }
        #endregion

        #region - SetError : 戻り値よりエラーメッセージのセット(2)
        /// <summary>
        /// 戻り値よりエラーメッセージのセット
        /// </summary>
        /// <param name="result">Result</param>
        protected void SetError(Result result)
        {
            SetError((result.Failed) ? result.ErrorMessage : null);
        }
        #endregion

        #endregion

        #region ■ Methods

        #region - GetValue : 値を取得する（抽象）
        /// <summary>
        /// 値を取得する（抽象）
        /// </summary>
        /// <returns>object</returns>
        public abstract object GetValue();
        #endregion

        #region - SetValue : 値を設定する（抽象）
        /// <summary>
        /// 値を設定する（抽象）
        /// </summary>
        /// <param name="o"></param>
        public abstract void SetValue(object o);
        #endregion

        #region - ValidationCheck : バリデーションチェックの実行（抽象）
        /// <summary>
        /// バリデーションチェックの実行（抽象）
        /// </summary>
        /// <returns>Result</returns>
        public abstract Result ValidationCheck();
        #endregion

        #region - Clean : 状態（変更済・エラー）をクリアーする
        /// <summary>
        /// 状態（変更済・エラー）をクリアーする
        /// </summary>
        public void Clean()
        {
            IsChanged = false;
            SetError((string)null);
        }
        #endregion

        #region - Reset : 値をリセットする（値を初期値に戻す）
        /// <summary>
        /// 値をリセットする（値を初期値に戻す）
        /// </summary>
        public virtual void Reset()
        {
            Clean();
        }
        #endregion

        #region - ToString : 文字列化する
        /// <summary>
        /// 文字列化する
        /// </summary>
        /// <returns>string</returns>
        public override string ToString()
        {
            return GetValue().ToString();
        }
        #endregion

        #endregion
    }
    #endregion


}
