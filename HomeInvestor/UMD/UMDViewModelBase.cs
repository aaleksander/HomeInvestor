using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Media;
using Microsoft.Win32;

namespace UMD
{
    public static class ObservableCollectionExts
    {
        public static ObservableCollection<T> RePP<T>(this ObservableCollection<T> aList ) where T:UMDViewModelBase
        {
            if (aList.Count == 0)
                return aList;
            
            //проверим, нужна ли перенумерация
            if( !aList.IsNeedPP() )
            	return aList;
            
            UMDManager.Storage.BeginTransaction();
            int pp = 1;
            foreach (var i in aList)
            {
                i.PP = pp;
                pp++;
            }
            UMDManager.Storage.Commit();
            return aList;
        }

        //нуждается ли список в перенумерации
        public static bool IsNeedPP<T>(this ObservableCollection<T> aList ) where T:UMDViewModelBase
        {
            T prev = null;
            foreach(var i in aList)
            {
            	if( prev == null )
            	{
            		prev = i;
            		if( prev.PP != 1 )
            			return true;
            		continue;
            	}
            	if( prev.PP + 1 != i.PP )
            		return true;
            	prev = i;
            }

        	return false;
        }
    }

	public class AliasAttribute: Attribute
	{
		private string _alias;
		public AliasAttribute(string a)
		{
			_alias = a;
		}

		public string Alias{
			get{
				return _alias;
			}
		}
	}

	public class DependencyAttribute: Attribute
	{
		Type _type;
		public DependencyAttribute(Type a)
		{
			_type = a;
		}

		public Type Type{
			get{
				return _type;
			}
		}
	}

	public class PropertiesAttribute: Attribute
	{
		public string[] List;
		public PropertiesAttribute(params string[] aList)
		{
			List = aList;
		}
	}

	#region атрибуты для полей (используется в WizardDataGrid)
	/// <summary>
	/// поле только для чтения
	/// </summary>
	public class FieldReadOnlyAttribute:Attribute{}

	/// <summary>
	/// описывает основные свойсва поля: порядковый номер и заголовок
	/// </summary>
	public class FieldAttribute: Attribute
	{
		public FieldAttribute(int aOrd, string aHeader)
		{
			Header = aHeader;
			Ord = aOrd;
		}
		public string Header{private set; get;}
		public int Ord{private set;get;}
	}

    public class FieldSelectorAttribuye : Attribute
    {
        
    }

	public class SuppressFieldsAttribute: Attribute
	{
		public SuppressFieldsAttribute(params string[] aList)
		{
			Fields = aList;
		}
		
		public string[] Fields{private set;get;}
	}

    public class CancelReadOnlyAttribute : Attribute
    {
        public CancelReadOnlyAttribute(params string[] aList)
        {
            Fields = aList;
        }
        public string[] Fields { private set; get; }
    }

    /// <summary>
    /// поле будет отображено, как выпадающий список
    /// </summary>
    public class FieldComboBoxAttribute : Attribute
    {
        public FieldComboBoxAttribute(string aItems)
        {
            Items = aItems;
        }

        public string Items { private set; get; }
    }

    //}

        /// <summary>
        /// поле отображается как выпадающий список. Еще содержимое зависит от значения поля aFieldName
        /// </summary>
        public class FieldSelectorComboBox: Attribute
	{
		/// <summary>
		/// 
		/// </summary>
		/// <param name="aFieldName">поле, от значения которого зависит состав выпадающего списока</param>
		/// <param name="aItemSources">список источников для списка, индекс с единицы</param>
		public FieldSelectorComboBox(string aFieldName, params string[] aItemSources)
		{
			
		}
	}

    //public class FieldComboBoxSmartAttribute : Attribute
    //{
    //    public FieldComboBoxSmartAttribute(string aItemsSource, string aDisplayPath)
    //    {//связь всегда по Id
    //        ItemsSource = aItemsSource; //откуда брать список
    //        DisplayPath = aDisplayPath; //что показывать
    //    }

    //    public string ItemsSource { private set; get; }
    //    public string DisplayPath { private set; get; }
    //}

	/// <summary>
	/// список селекторов для каждого значения поля, начиная с нуля
	/// </summary>
	public class FieldSelectorsAttribute: Attribute
	{
		public FieldSelectorsAttribute(string aFieldName, params object[] selectors)
		{
			Field = aFieldName;
			Templates = new Dictionary<int, string>();
			int i = 0;
			foreach(var s in selectors)
			{
				if( s.ToString().Trim() != "" )
					Templates[i] = s.ToString();
				i++;
			}
		}

		public string Field{private set; get;}
		public Dictionary<int, string> Templates{private set;get;}
	}

	#endregion

	/// <summary>
	/// базовый класс-представление для универсальной модели базы данных
	/// </summary>
	public class UMDViewModelBase : INotifyPropertyChanged
	{
	    public event PropertyChangedEventHandler PropertyChanged;
	    protected virtual void OnPropertyChanged(string propertyName)
	    {
	    	if( PropertyChanged != null )
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
	    }

	    protected virtual void OnPropertyChanged()
	    {
	    	if( PropertyChanged != null )
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(null));
	    }

		public bool IsSelected{
			set{
				_isSelected = value;
				OnPropertyChanged("IsSelected");
			}
			get{
				return _isSelected;
			}
		}
		private bool _isSelected;

        private UMDObjectModel _data = null;

        public bool RealTimeUpdate { set; get; }

        public UMDObjectModel Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                OnPropertyChanged();
            }
        }

        protected IStorage _storage = null;

		/// <summary>
		/// при очистке кэша, такие объекты не удаляются
		/// </summary>
		public bool NoClear{set;get;}

		public UMDViewModelBase(IStorage aSt, UMDObjectModel aObj)//: base(aSt, aObj)
		{
            _storage = aSt;
            _data = aObj;

			if( aObj != null )
			{
				if( InitProperties2(aObj.Id) == false )
					LoadProperties(aObj.Id);
			}
            RealTimeUpdate = true;
        }

        public UMDViewModelBase(IStorage aStorage)
		{
            _storage = aStorage;

            _data = new UMDObjectModel();

			//var t = this.GetType();
			_data.Id = -1;

			InitProperties2();

			NoClear = false;
            RealTimeUpdate = true;
        }

		protected void InitProperties(params UMDPropertyModel[] aProps)
		{
			_properties = new List<UMDPropertyViewModel>();
			int i = 0;
			foreach(var p in aProps)
			{
				_properties.Add(new UMDPropertyViewModel(this._storage, p)
				                {
				                	RealTimeUpdate = this.RealTimeUpdate,
				                	PPInObject = i
				                }
				               );
				i++;
			}
		}

		private bool InitProperties2(int aObjectId = -1)
		{
			Type t = this.GetType();
			var props = (PropertiesAttribute)Attribute.GetCustomAttribute(t, typeof(PropertiesAttribute));

			if( props == null ) //свойство не задано
				return false;

			if( props.List.Count() % 2 != 0 )
				throw new Exception("Аттрибут Properties - это список пар \"тип-Name\", конечное количество параметров атрибута должно быть четным");

			_properties = new List<UMDPropertyViewModel>();
			//проходим по свойствам (парами)
			UMDPropertyViewModel prop;
			for(int i=0; i<props.List.Count(); i += 2 )
			{
				if( aObjectId != -1 )
				{//загружаем свойства конкретного объекта
					prop = UMDManager.Instance.GetPropertyForObject(aObjectId, i >> 1);
					if( prop == null ) //свойства нет, а должно быть
					{//заводим новое свойство
						var newProp = new UMDPropertyModel()
						{
							ObjectId = Id,
							PropertyName = props.List[i + 1],
							Type = props.List[i],
							StrValue = "", //все остальные и так нулевыми будут
							PPInObject = i >> 1
						};
						if( _storage != null )
							_storage.Insert(newProp); //вставляем в БД
						prop = new UMDPropertyViewModel(_storage, newProp);
						UMDManager.Instance.AppendCache(prop);
					}
					_properties.Add(prop);
				}
				else
				{//создаем новое свойство
					var newProp = new UMDPropertyModel()
					{
						ObjectId = Id,
						PropertyName = props.List[i + 1],
						Type = props.List[i],
						StrValue = "", //все остальные и так нулевыми будут
						PPInObject = i >> 1
					};
					_properties.Add(new UMDPropertyViewModel(_storage, newProp)
					                {
					                	RealTimeUpdate = this.RealTimeUpdate,
					                }
					               );
				}
			}
			return true;
		}

		public static string GetAlias<T>()
		{
			Type t = typeof(T);
			var aliasAttr = (AliasAttribute)Attribute.GetCustomAttribute(t, typeof(AliasAttribute));
			if( aliasAttr == null )
				throw new Exception("У объекта " + t.ToString() + " не определен атрибут Name");
			return aliasAttr.Alias;
		}

//		public static ObservableCollection<T> LoadAll<T>(string aWhere, IStorage aSt,
//		                                              Func<UMDObjectModel, T> aCreator)
//		{
//			string  alias = GetAlias<T>();
//
//			var res = new ObservableCollection<T>();
//
//			var q = aSt.Select<UMDObjectModel>("Name='" + alias + "'", "PP");
//			foreach(var s in q)
//			{
//				res.Add(aCreator(s));
//			}
//			return res;
//		}

		/// <summary>
		/// загрузить из базы свойства для объекта
		/// </summary>
		/// <param name="aId"></param>
		protected void LoadProperties(int aId)
		{
			int i=0;
			UMDPropertyViewModel prop = null;
			_properties = new List<UMDPropertyViewModel>();
			do
			{
				prop = UMDManager.Instance.GetPropertyForObject(aId, i); //new UMDPropertyViewModel(this.Storage, aId, i); //читаем по этому объекту свойство с определенным номером
				if( prop == null ) //свойства кончились, заканчиваем
					break;
				_properties.Add(prop);
				i++;
			}while( prop != null ); //пока свойства не кончатся
		}

		private List<UMDPropertyViewModel> _properties; 

		/// <summary>
		/// возвращает истину, если есть объкт с таким идешником
		/// </summary>
		/// <param name="aId"></param>
		/// <returns></returns>
		public static bool CheckId<T>(int aId)
		{
			return UMDManager.Instance.CheckObjectById<T>(aId);
		}

		public static T First<T>()
		{
			return UMDManager.Instance.GetFirstObject<T>();
		}

		#region установить свойство
		protected void SetProp(int aIndex, SolidColorBrush br)
		{
			var r = br.Color.R;
			var g = br.Color.G;
			var b = br.Color.B;
			var val = string.Format("{0}.{1}.{2}",r, g, b);
			_properties[aIndex].StrValue = val;
		}

		protected void SetProp(int aIndex, int aValue)
		{
			_properties[aIndex].IntValue = aValue;
		}

		protected void SetProp(int aIndex, DateTime aValue)
		{
			_properties[aIndex].StrValue = aValue.ToString("dd.MM.yyyy");
		}

		protected void SetProp(int aIndex, string aValue)
		{
			_properties[aIndex].StrValue = aValue;
		}

		protected void SetProp(int aIndex, double aValue)
		{
			_properties[aIndex].RealValue = aValue;
		}

		protected void SetProp(int aIndex, decimal aValue)
		{
			_properties[aIndex].RealValue = (double)aValue;
		}

		protected void SetProp(int aIndex, bool aValue)
		{
			_properties[aIndex].IntValue = aValue? 1: 0;
		}
		#endregion

		#region взять свойство
		protected int GetInt(int aIndex)
		{
            try
            {
                return _properties[aIndex].IntValue;
            }
			catch
            {
                return 0;
            }
		}

		protected string GetStr(int aIndex)
		{
			return _properties[aIndex].StrValue;
		}

		protected SolidColorBrush GetColor(int aIndex)
		{
			var str = _properties[aIndex].StrValue;
			
			if( str == null )
				return Brushes.White;
			if( str == "" )
				return Brushes.White;
			var pp = str.Split('.');
			var r = byte.Parse(pp[0]);
			var g = byte.Parse(pp[1]);
			var b = byte.Parse(pp[2]);
			return new SolidColorBrush(Color.FromRgb(r, g, b));
		}

		protected DateTime GetDT(int aIndex)
		{
			return DateTime.ParseExact(_properties[aIndex].StrValue, "dd.MM.yyyy", null);
		}
		
		protected double GetReal(int aIndex)
		{
			return _properties[aIndex].RealValue;
		}

		protected decimal GetDecimal(int aIndex)
		{
			return (decimal)_properties[aIndex].RealValue;
		}

		protected bool GetBool(int aIndex)
		{
			return _properties[aIndex].IntValue != 0;
		}
		#endregion

		/// <summary>
		/// удаляем сущность из БД и кэша
		/// </summary>
		public virtual void Delete()
		{
		    if (_storage != null) // если это не заглушка
		    {
                //удаляем свойства
                foreach (var p in _properties)
                {
                    _storage.Delete(p.Data);
                    UMDManager.Instance.Remove(p);
                }
                //удаляем саму сущность (объект)
                _storage.Delete(_data);
		    }

		    UMDManager.Instance.Remove(this);
        }

        public int Insert()
		{//загоняем объект в БД
        	Debug.WriteLine("Insert");

			var aliasAttr = (AliasAttribute)Attribute.GetCustomAttribute(this.GetType(), typeof(AliasAttribute));
			if( aliasAttr == null )
				throw new Exception("У объекта '" + this.GetType().ToString() + "' не определен атрибут Alias");

			_data.Alias = aliasAttr.Alias;
			//Сохраняем в БД сам объект
			_storage.Insert(_data);
			//обновляем кэш
			UMDManager.Instance.AppendCache(this);
			//сохраняем свойства
			foreach(var p in _properties)
			{
				p.Data.ObjectId = _data.Id;
				_storage.Insert(p.Data);
				p.RealTimeUpdate = true;
				UMDManager.Instance.AppendCache(p); //Кидаем свойтсво в кэш
			}
			RealTimeUpdate = true;

			return _data.Id;
		}

		public int Id
		{
			set{
				_data.Id = value;
                OnPropertyChanged();
				UpdateIfNeed();
			}
			get{
				return _data.Id;
			}
		}

        /// <summary>
        /// обновить, если нужно
        /// </summary>
        protected virtual void UpdateIfNeed()
        {
            if (RealTimeUpdate && _storage != null )
            {
                _storage.Update(_data);
                //				using(var db = new DB() )
                //				{
                //					db.Update(_data);
                //				}
            }
        }

        [Field(1, "№п/п")]
        [FieldReadOnly]
        public int PP{
			set{
				_data.PP = value;
                OnPropertyChanged();
				UpdateIfNeed();
			}
			get{
				return _data.PP;
			}
		}

        //[Field(2, "№ п/п")]
        //public int AlterPP
        //{
        //    set
        //    {
        //        _data.AlterPP = value;
        //        RaisePropertyChanged();
        //        UpdateIfNeed();
        //    }
        //    get
        //    {
        //        return _data.AlterPP;
        //    }
        //}

        [Field(3, "Название")]
		public string Name{
			set{
				_data.Name = value;
                OnPropertyChanged();
                UpdateIfNeed();
			}
			get{
				return _data.Name;
			}
		}

		public string Alias{
			set{
				_data.Alias =value;
                OnPropertyChanged();
                UpdateIfNeed();
			}
			get{
				return _data.Alias;
			}
		}

		public int ParentId{
			set{
				_data.ParentId = value;
                OnPropertyChanged();
                UpdateIfNeed();
			}
			get{
				return _data.ParentId;
			}
		}

        //public int AlterParentId
        //{
        //    set
        //    {
        //        _data.AlterParentId = value;
        //        RaisePropertyChanged();
        //        UpdateIfNeed();
        //    }
        //    get
        //    {
        //        return _data.AlterParentId;
        //    }
        //}

        /// <summary>
        /// создает объект-заглушку, который не попадет в БД
        /// </summary>
        /// <param name="aName"></param>
        /// <returns></returns>
        public static T Stub<T>(string aName) where T: UMDViewModelBase
		{
			T res = (T)Activator.CreateInstance(typeof(T), null, new UMDObjectModel());
			res.RealTimeUpdate = false;
			res.Name = aName;
			return res;
		}
		
		public void BeginTransaction()
		{
			_storage.BeginTransaction();
		}
		
		public void Commit()
		{
			_storage.Commit();
		}
		
		public void Rollback()
		{
			_storage.Rollback();
		}

		protected List<string> _supressFields = new List<string>();
		protected void SuppressField(string aFieldName)
		{
			_supressFields.Add(aFieldName);
		}

		public List<string> SuppressFeild{
			get{
				return _supressFields;
			}
		}

        public delegate void SendEventHandler(object sender, object e);
        public event SendEventHandler OnEvent;

        public void SendEvent(object aMsg)
        {
        	if( OnEvent != null )
            	OnEvent.Invoke(this, aMsg);
        }

	    //public virtual string GetPath(Func<UMDViewModelBase, UMDViewModelBase> getParent,
        //    Func<UMDViewModelBase, string> aGetPath)
        //{
        //       var sb = new StringBuilder();
        //    var par = getParent(this);
        //    while (par != null )
        //    {
        //        if (sb.Length > 0)
        //            sb.Insert(0, " / ");
        //        sb.Insert(0, aGetPath(par));
        //           if( par.ParentId != 0 )
        //            par = getParent(par);
        //           else
        //               break;
        //    }
        //    return sb.ToString();
        //}
    }
}
