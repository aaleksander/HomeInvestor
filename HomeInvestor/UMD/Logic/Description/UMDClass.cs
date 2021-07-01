using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace UMD.Logic.Description
{
	/// <summary>
	/// объект универсальной структуры данных
	/// </summary>
	public class UMDClass:IOutputDescription
	{
		/// <summary>
		/// общий словарик, для контроля уникальность классов
		/// </summary>
		private static List<string> _aliases = new List<string>();

	    public bool IsDefault
	    {
	        set { }
	        get { return _outputs.Any(o => o.IsDefault); }
	    }
		/// <summary>
		/// очистить словарь алиасов
		/// </summary>
		public static void ClearAliases()
		{
			_aliases.Clear();
		}

        //public string Name{set;get;}    //имя класса
        /// <summary>
        /// описание класса
        /// </summary>
        public string Description{set;get;}

		//private void CheckAlias(string aAlias)
		//{
		//	if( _aliases.ContainsKey(aAlias) )
		//		throw new Exception("Класс с таким Name уже существует");
		//}

		public UMDClass(string aName, string aDescription)
		{
			//CheckAlias(aName);
			//Name = aName;
			Description = aDescription;
		    Outputs.Add(new UMDSlotDescription("default", "выход по умолчанию", this)
		    {
		        IsDefault = true
		    });
			
            //добавляем в словарик
			_aliases.Add(aName);
		}

		public UMDClass Class{get{return null;}}
		
//		public UMDClass(string aName)
//		{
//			CheckAlias(aName);
//			Name = aName;
//			Description = aName;
//			Outputs["default"] = new UMDOutputSlotDescr("default", "out", this);
//		}

		#region Работа с выходами	
		/// <summary>
		/// добавить вход
		/// </summary>
		/// <param name="aAlias">псевдоним входа</param>
		/// <param name="aDescription">описание входа</param>
		/// <param name="aCanEmpty">может ли быть пустым</param>
		/// <param name="aTypes">типы, которые могут цеплять сюда свой выход</param>
		/// <returns></returns>
		public UMDSlotDescription AddOutput(string aAlias, string aDescription)
		{
			//проверяем уникальность
            if( IsHasOutputByName(aAlias) )
                    throw new Exception("Выход с таким Name уже есть");

            var res = new UMDSlotDescription(aAlias, aDescription, this);

            if ( Outputs.Count == 1 && IsHasOutputByName(aAlias))
			{//удалим выход по умолчанию
				Outputs.Remove(GetOutputSlotByName(aAlias));
			}

            //удалим вход по умолчанию
		    var def = Outputs.FirstOrDefault(x => x.IsDefault);
		    if (def != null)
		        Outputs.Remove(def);

			Outputs.Add(res);
			return res;
		}

	    private bool IsHasOutputByName(string aName)
	    {
            foreach (var i in Outputs)
            {
                if (i.Name == aName)
                    return true;
            }
	        return false;
	    }

	    private UMDSlotDescription GetOutputSlotByName(string aName)
	    {
	        return Outputs.FirstOrDefault(i => i.Name == aName);
	    }

	    //public UMDConnectorBase AddOutput(string aAlias)
		//{
		//	return AddOutput(aAlias, aAlias);
		//}

		public List<UMDSlotDescription> Outputs{
			private set{
				_outputs = value;
			}
			get{
				/// <summary>
				/// подтянуть выходы из списков
				/// </summary>
				return _outputs;
			}
		}
		private List<UMDSlotDescription> _outputs = new List<UMDSlotDescription>();
        #endregion

        #region Работа с входами
        /// <summary>
        /// добавить вход
        /// </summary>
        /// <param name="aAlias">псевдоним входа</param>
        /// <param name="aDescription">описание входа</param>
        /// <param name="aCanEmpty">может ли быть пустым</param>
        /// <param name="aTypes">типы, которые могут цеплять сюда свой выход</param>
        /// <returns></returns>
        public UMDSlotDescription AddInput(string aAlias, string aDescription, bool aCanEmpty, params IOutputDescription[] aTypes)
        {
            //проверяем уникальность
            if (IsHasInputByName(aAlias))
                throw new Exception("Вход с таким Name уже существует");

            var res = new UMDSlotDescription(aAlias, aDescription, this);
            int ind = 0;
            foreach (var a in aTypes) //смотрим, что мы там может получить на вход
            {
                res.AddPermission(a);
                ind++;
            }

            Inputs.Add(res);
            return res;
        }

        private bool IsHasInputByName(string aName)
        {
            foreach (var i in Inputs)
            {
                if (i.Name == aName)
                    return true;
            }
            return false;
        }

        private UMDSlotDescription GetInputSlotByName(string aName)
        {
            return Inputs.FirstOrDefault(i => i.Name == aName);
        }

        public List<UMDSlotDescription> Inputs
        {
            private set
            {
                _inputs = value;
            }
            get
            {
                return _inputs;
            }
        }
        private List<UMDSlotDescription> _inputs = new List<UMDSlotDescription>();
        #endregion

        #region Работа со списками
        //public UMDListDescr AddList(string aAlias, UMDClass aClass, string aDescriptioin, bool aIsOutputs)
        //{
        //	var res = new UMDListDescr(aAlias, aClass, aDescriptioin, aIsOutputs);
        //	Lists.Add(res);
        //	return res;
        //}

        //public Collection<UMDListDescr> Lists{
        //	get{
        //		return _lists;
        //	}
        //}
        //private Collection<UMDListDescr> _lists = new Collection<UMDListDescr>();
        #endregion

        #region генератор кода
        //public CodeGenerator CG{
        //	get{
        //		if( _cg == null )
        //		{
        //			_cg = new CodeGenerator();
        //		}
        //		return _cg;
        //	}				
        //}
        //private CodeGenerator _cg = null;
        #endregion

        /// <summary>
        /// возвращает истину, если сигнал с output можно подать на input
        /// </summary>
        /// <param name="output"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static bool CanConnect(IOutputDescription output, UMDSlotDescription input)
        {
        	var outClass = output.Class;
        	var inClass = input.Class;

        	foreach(var o in inClass.Inputs)
        	{
        		if( o.CanConnect(output) )
        			return true;
        	}

        	return false;
        }
    }
}
