using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using hsb.Extensions;
using hsb.WPF.Utilities;
using hsb.WPF.EventArguments;

namespace hsb.WPF
{
    #region 【Abstract Class : PropertyItems】
    /// <summary>
    /// プロパティセット
    /// </summary>
    public abstract class PropertyItems : IEnumerable<PropertyItemBase>, IDataErrorInfo, INotifyPropertyChanged
    {
        #region ■ Properties

        #region - Properties : プロパティリスト
        /// <summary>
        /// プロパティリスト
        /// </summary>
        protected List<PropertyItemBase> Properties { get; set; }
        #endregion

        #region - Item : インデクサ(1)
        /// <summary>
        /// インデクサ(1)
        /// </summary>
        /// <param name="propertyName">string : プロパティ名</param>
        /// <returns>PropertyItemBase</returns>
        public PropertyItemBase this[string propertyName]
        {
            get
            {
                var property = Properties.Find(v => v.Name == propertyName);
                if (property == null)
                    throw new ArgumentOutOfRangeException();
                return property;
            }
        }
        #endregion

        #region - Item : インデクサ(2)
        /// <summary>
        /// プロパティアイテム(2)
        /// </summary>        
        /// <param name="idx">int : インデックス</param>
        /// <returns>PropertyItemBase</returns>
        public PropertyItemBase this[int idx]
        {
            get
            {
                if (idx >= 0 && idx < Properties.Count)
                    return Properties[idx];
                else
                    throw new ArgumentOutOfRangeException();
            }
        }
        #endregion

        #region - IsChanged : 何れかのプロパティが変更されたか？
        /// <summary>
        /// 何れかのプロパティが変更されたか？
        /// </summary>
        public virtual bool IsChanged
        {
            get { return Properties.Where(v => (IsChangedFilter?.Invoke(v) ?? true) == true && v.IsChanged).Any(); }
        }
        #endregion

        #region - HasError : エラーが存在する？
        /// <summary>
        /// エラーが存在する？
        /// </summary>
        public virtual bool HasError
        {
            get
            {
                return Properties.Where(v => (HasErrorFilter?.Invoke(v) ?? true) == true && v.HasError).Any();
            }
        }
        #endregion

        #region - Errors : エラーが発生したプロパティアイテムの列挙子を返す
        /// <summary>
        /// エラーが発生したプロパティアイテムの列挙子を返す
        /// </summary>
        /// <returns>IEnumerable of PropertyItemBase</returns>
        public IEnumerable<PropertyItemBase> Errors()
        {
            return Properties.Where(v => v.HasError);
        }
        #endregion

        #region - IDataErrorInfo.Error : IdataErrorInfo.Error の実装
        /// <summary>
        /// IdataErrorInfo.Error の実装
        /// </summary>
        string IDataErrorInfo.Error
        {
            get { return HasError ? "Has Error" : null; }
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
            get { return Properties.Find(v => v.Name == columnName)?.ErrorMessage; }
        }
        #endregion

        #region - PropertyNames : プロパティ名の列挙子を返す。
        /// <summary>
        /// プロパティ名の列挙子を返す。
        /// </summary>
        public IEnumerable<string> PropertyNames
        {
            get { return Properties.Select(property => property.Name); }
        }
        #endregion

        #region - Values : 値の列挙子を返す。
        /// <summary>
        /// 値の列挙子を返す。
        /// </summary>
        public IEnumerable<object> Values
        {
            get { return Properties.Select(property => property.GetValue()); }
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

        #region - PropertyItemInvalidated : プロパティアイテムでバリデーションエラーが発生した
        /// <summary>
        /// プロパティアイテムでバリデーションエラーが発生した
        /// </summary>
        public event EventHandler<PropertyItemInvalidatedEventArgs> PropertyItemInvalidated;
        #endregion

        #region - ErrorRaised : プロパティアイテムでエラーが発生した
        /// <summary>
        /// プロパティアイテムでエラーが発生した
        /// </summary>
        public event EventHandler<PropertyItemEventArgs> ErrorRaised;
        #endregion

        #region - ErrorCleard : プロパティアイテムのエラーが解消した
        /// <summary>
        /// プロパティアイテムのエラーが解消した
        /// </summary>
        public event EventHandler<PropertyItemEventArgs> ErrorCleard;
        #endregion

        #region - ValidationCheckFilter : バリデーションチェック実行時用のフィルター
        /// <summary>
        /// バリデーションチェック実行時用のフィルター
        /// </summary>
        public Func<PropertyItemBase, bool> ValidationCheckFilter;
        #endregion

        #region - IsChangedFilter : 項目の変更通知をモデルに伝播するさいのフィルター
        /// <summary>
        /// 項目の変更通知をモデルに伝播するさいのフィルター
        /// </summary>
        public Func<PropertyItemBase, bool> IsChangedFilter;
        #endregion

        #region - HasErrorFilter : 項目のエラー通知をモデルに伝播するさいのフィルター
        /// <summary>
        /// 項目のエラー通知をモデルに伝播するさいのフィルター
        /// </summary>
        public Func<PropertyItemBase, bool> HasErrorFilter;
        #endregion

        #endregion

        #region ■ Constructor
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public PropertyItems()
        {
            Properties = new List<PropertyItemBase>();
        }
        #endregion

        #region ■ Protected Methods

        #region - AddProperty : プロパティアイテムを追加する
        /// <summary>
        /// プロパティアイテムを追加する
        /// </summary>
        /// <param name="property">PropertyItemBase</param>
        /// <returns>PropertyItemBase</returns>
        protected PropertyItemBase AddProperty(PropertyItemBase property)
        {
            // PropertyChanged を波及させる
            property.PropertyChanged += OwnedPropertyChanged;
            // バリデーションエラーをトラップする
            property.Invalidated += OwnedPropertyInvalidated;
            Properties.Add(property);
            return property;
        }
        #endregion

        #endregion

        #region ■ Methods

        #region - GetEnumerator : IEnumerable.GetEnumerator() の実装
        /// <summary>
        /// 反復処理用の列挙子を返す (Generic)
        /// 　IEnumerable.GetEnumerator() の実装
        /// </summary>
        /// <returns>IEnumerator of PropertyItemBase</returns>
        IEnumerator<PropertyItemBase> IEnumerable<PropertyItemBase>.GetEnumerator()
        {
            return Properties.GetEnumerator();
        }
        #endregion

        #region - GetEnumerator : IEnumerable.GetEnumerator() の実装
        /// <summary>
        /// 反復処理用の列挙子を返す
        /// 　IEnumerable.GetEnumerator() の実装
        /// </summary>
        /// <returns>IEnumerator of PropertyItemBase</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return Properties.GetEnumerator();
        }
        #endregion

        #region - RaisePropertyChanged : INotifyPropertyChanged.PropertyChangedイベントを発生させる。 (1)
        /// <summary>
        /// INotifyPropertyChanged.PropertyChangedイベントを発生させる。 (1)
        /// </summary>
        /// <param name="propertyName">string : プロパティ名</param>
        public void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region - RaisePropertyChanged : INotifyPropertyChanged.PropertyChangedイベントを発生させる。 (2)
        /// <summary>
        /// INotifyPropertyChanged.PropertyChangedイベントを発生させる。 
        /// </summary>
        /// <param name="propertyName">IEnumerable of string : プロパティ名</param>
        public void RaisePropertyChanged(IEnumerable<string> propertyNames)
        {
            if (PropertyChanged != null)
            {
                foreach (string propertyName in propertyNames)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region - RaisePropertyChanged : INotifyPropertyChanged.PropertyChangedイベントを発生させる。 (3)
        /// <summary>
        /// INotifyPropertyChanged.PropertyChangedイベントを発生させる。 (3)
        /// </summary>
        /// <param name="propertyName">array of string : プロパティ名</param>
        public void RaisePropertyChanged(params string[] propertyNames)
        {
            RaisePropertyChanged(propertyNames);
        }
        #endregion

        #region - ValidationCheck : バリデーションチェックの実行
        /// <summary>
        /// バリデーションチェックの実行
        /// </summary>
        /// <returns>bool</returns>
        public bool ValidationCheck()
        {
            var result = true;
            var filter = ValidationCheckFilter ?? delegate(PropertyItemBase v) { return true; };
            foreach (var property in Properties.Where(filter))
            {
                var ret = property.ValidationCheck();
                if (ret.Failed && result)
                    result = false;
            }
            return result;
        }
        #endregion

        #region - Clean : 状態（変更済・エラー）をクリアーする
        /// <summary>
        /// 状態（変更済・エラー）をクリアーする
        /// </summary>
        public void Clean()
        {
            foreach (var property in Properties)
                property.Clean();
        }
        #endregion

        #region - SetValues : 値を一括設定する
        /// <summary>
        /// 値を一括設定する
        /// </summary>
        /// <param name="properties">IEnumerable of PropertyItemBase : 設定する値のセット</param>
        /// <param name="propertyFilter">Func : フィルター</param>
        public void SetValues(IEnumerable<PropertyItemBase> properties, Func<PropertyItemBase, bool> propertyFilter = null)
        {
            var filter = propertyFilter ?? delegate(PropertyItemBase v) { return true; };
            foreach (var property in properties.Where(filter))
            {
                var f = Properties.Find(v => v.Name == property.Name);
                if (f != null)
                    f.SetValue(property.GetValue());
            }
        }
        #endregion

        #region - Reset : プロパティアイテムの値を初期値に戻す
        /// <summary>
        /// プロパティアイテムの値を初期値に戻す
        /// </summary>
        /// <param name="propertyFilter"></param>
        public void Reset(Func<PropertyItemBase, bool> propertyFilter = null)
        {
            var filter = propertyFilter ?? delegate(PropertyItemBase v) { return true; };
            foreach (var property in Properties.Where(filter))
                property.Reset();
        }
        #endregion

        #endregion

        #region ■ Event Handlers 

        #region - OwnedPropertyChanged : プロパティアイテムの値が変更された
        /// <summary>
        /// プロパティアイテムの値が変更された
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">PropertyChangedEventArgs</param>
        protected virtual void OwnedPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var property = sender as PropertyItemBase;
            if (property != null)
            {
                if (e.PropertyName == "Value")
                    RaisePropertyChanged(property.Name);

                // モデルに変更を通知する
                if (e.PropertyName == "IsChanged" && IsChangedFilter?.Invoke(property) == true)
                    RaisePropertyChanged(e.PropertyName);

                // モデルにエラーを通知する
                if (e.PropertyName == "HasError" && HasErrorFilter?.Invoke(property) == true)
                {
                    RaisePropertyChanged(e.PropertyName);
                    if (property.HasError)
                        ErrorRaised?.Invoke(this, new PropertyItemEventArgs(property));
                    else
                        ErrorCleard?.Invoke(this, new PropertyItemEventArgs(property));
                }
            }
        }
        #endregion

        #region - OwnedPropertyInvalidated : プロパティアイテムでバリデーションエラーが発生した
        /// <summary>
        /// プロパティアイテムでバリデーションエラーが発生した
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">ValidationCheckedEventArgs</param>
        protected virtual void OwnedPropertyInvalidated(object sender, ValidationCheckedEventArgs e)
        {
            var property = sender as PropertyItemBase;
            if (property != null)
            {
                PropertyItemInvalidated?.Invoke(this, new PropertyItemInvalidatedEventArgs(property, e.ValidationResult));
            }
        }
        #endregion

        #endregion
    }
    #endregion

}
