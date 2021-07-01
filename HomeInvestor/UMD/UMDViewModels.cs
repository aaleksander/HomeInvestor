using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace UMD
{
	/// <summary>
	/// представление для модели объекта
	/// </summary>
	public class UMDObjectViewModel: ViewModelBase //<UMDObjectModel>
	{
		//кэши
		private static Dictionary<int, UMDObjectModel> _cacheById = new Dictionary<int, UMDObjectModel>();

        private UMDObjectModel _data = null;
        IStorage _storage = null;

        private bool _realtimeUpdate = false;

        public UMDObjectViewModel()
		{
			_data = new UMDObjectModel();			
			Id = -1;
            _realtimeUpdate = true;

        }

		public UMDObjectViewModel(UMDObjectModel aM)
		{
			_data = aM;
            _realtimeUpdate = true;
        }

        /// <summary>
        /// обновить, если нужно
        /// </summary>
        protected virtual void UpdateIfNeed()
        {
            if (_realtimeUpdate)
            {
                _storage.Update(_data);
            }
        }

        /// <summary>
        /// Идешник
        /// </summary>
        public int Id{
			set{
				if( _data.Id != value )
				{
					_data.Id = value;
                    OnPropertyChanged();
                    UpdateIfNeed();
				}
			}
			get{
				return _data.Id;
			}
		}

		/// <summary>
		/// Идешник родителя, если есть
		/// </summary>
		public int ParentId{
			set{
				if( _data.ParentId != value )
				{
					_data.ParentId = value;
                    OnPropertyChanged();
                    UpdateIfNeed();
				}
			}
			get{
				return _data.ParentId;
			}
		}

		public string Alias{
			set{
				if( _data.Alias != value )
				{
					_data.Alias = value;
                    OnPropertyChanged();
                    UpdateIfNeed();
				}
			}
			get{
				return _data.Alias;
			}
		}
		
		public int PP{
			set{
				if( _data.PP != value )
				{
					_data.PP = value;
                    OnPropertyChanged();
                    UpdateIfNeed();
				}
			}
			get{
				return _data.PP;
			}
		}

		public string Name{
			set{
				if( _data.Name != value )
				{
					_data.Name = value;
                    OnPropertyChanged();
                    UpdateIfNeed();
				}
			}
		}
	}

	/// <summary>
	/// представление для модели свойства
	/// </summary>
	public class UMDPropertyViewModel: ViewModelBase//<UMDPropertyModel>
	{
		//кэш просто по идешнику
		private static Dictionary<int, UMDPropertyModel> _cacheById=new Dictionary<int, UMDPropertyModel>(); 
		//кэш по идешнику объекта и номеру свойства
		private static Dictionary<Tuple<int, int>, UMDPropertyModel> _cacheByObjIdAndPP = 
			new Dictionary<Tuple<int, int>, UMDPropertyModel>();
		
		public UMDPropertyViewModel(IStorage aSt)//: base(aSt)
		{
            _storage = aSt;
//			RealTimeUpdate = true;
			Id = -1;
		}

        private IStorage _storage = null;
        private UMDPropertyModel _data = null;
        public UMDPropertyModel Data
        {
            set
            {
                _data = value;
                OnPropertyChanged();
            }
            get
            {
                return _data;
            }
        }

        public bool RealTimeUpdate { set; get; }
		public UMDPropertyViewModel(IStorage aSt, UMDPropertyModel aM)//:base(aSt, aM)
		{
            _storage = aSt;
            _data = aM;
               
			RealTimeUpdate = true;
		}

		/// <summary>
		/// загружаем свойство по идешнику объекта и номеру
		/// </summary>
		/// <param name="aObjectId"></param>
		/// <param name="aPPinObject"></param>
		/// <returns></returns>
		public static UMDPropertyModel Load2(IStorage aSt, int aObjectId, int aPPinObject, bool aThrow = false)
		{
			var key = Tuple.Create(aObjectId, aPPinObject);
			if( _cacheByObjIdAndPP.ContainsKey(key) )
			{
				return _cacheByObjIdAndPP[key];
			}
			else
			{//в кэше нету, читаем
				var q = aSt.Select<UMDPropertyModel>("ObjectId=" + aObjectId.ToString()
 			                                       + " and PPInObject=" + aPPinObject.ToString(), "");
				if( q.Count() == 0 )
				{
					if( aThrow == false )
					{
						return null;
					}
					else
					{
						throw new Exception("Не могу загрузить свойство '" + aPPinObject.ToString() + 
					    	                " для объекта '" + aObjectId.ToString() + "'");
					}
				}
				else
				{
					var prop = q.First();
					_cacheByObjIdAndPP[key] = prop;
					return prop;
				}
			}
		}

        /// <summary>
        /// обновить, если нужно
        /// </summary>
        protected virtual void UpdateIfNeed()
        {
            if (RealTimeUpdate)
            {
                lock (UMDManager.Instance.Loading)
                {
                    _storage.Update(_data);
                }
            }
        }

        /// <summary>
        /// Идешник
        /// </summary>
        public int Id{
			set{
				if( _data.Id != value )
				{
					_data.Id = value;
                    OnPropertyChanged();
                    UpdateIfNeed();
				}
			}
			get{
				return _data.Id;
			}
		}

		public int ObjectId{
			set{
				if( _data.ObjectId != value )
				{
					_data.ObjectId = value;
                    OnPropertyChanged();
                    UpdateIfNeed();
				}
			}
			get{
				return _data.ObjectId;
			}
		}

		public string PropertyName{
			set{
				if( _data.PropertyName != value )
				{
					_data.PropertyName = value;
                    OnPropertyChanged();
                    UpdateIfNeed();
				}
			}
			get{
				return _data.PropertyName;
			}
		}

		public string Type{
			set{
				if( _data.Type != value )
				{
					_data.Type = value;
                    OnPropertyChanged();
                    UpdateIfNeed();
				}
			}
			get{
				return _data.Type;
			}
		}

		public int IntValue{
			set{
				if( _data.IntValue != value )
				{
					_data.IntValue = value;
                    OnPropertyChanged();
                    UpdateIfNeed();
				}
			}
			get{
				return _data.IntValue;
			}
		}

		public string StrValue{
			set{
				if( _data.StrValue != value )
				{
					_data.StrValue = value;
                    OnPropertyChanged();
                    UpdateIfNeed();
				}
			}
			get{
				return _data.StrValue;
			}
		}

		public double RealValue{
			set{
				if( _data.RealValue != value )
				{
					_data.RealValue = value;
                    OnPropertyChanged();
                    UpdateIfNeed();
				}
			}
			get{
				return _data.RealValue;
			}
		}
		
		/// <summary>
		/// порядковый номер свойства в объекте
		/// </summary>
		public int PPInObject{
			set{
				if( _data.PPInObject != value )
				{
					_data.PPInObject = value;
                    OnPropertyChanged();
                    UpdateIfNeed();
				}
			}
			get{
				return _data.PPInObject;
			}
		}
		
	}
}
