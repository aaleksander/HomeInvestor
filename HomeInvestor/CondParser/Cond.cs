using System;
using System.Text;
using HomeInvestor.ViewModels;
using UMD;

namespace HomeInvestor.CondParser
{
	/// <summary>
	/// что далеть
	/// </summary>
	public enum WhatDos {none, buy, sell}

	/// <summary>
	/// на что смотреть
	/// </summary>
	public enum Whats{none,
		price, 		//цена
		percent, 	//процент в портфеле 
		gl_percent, //глобальный портфель
		date,		//дата
		age			//возраст облигации
	}

	/// <summary>
	/// условие
	/// </summary>
	public enum Conds{none,
		less,	//меньше
		great	//больше
	}
			
	
	/// <summary>
	/// Description of Cond.
	/// </summary>
	public class Condition
	{
		public Condition(WhatDos wd, Whats w, Conds cond, object value)
		{
			WhatDo = wd;
			What = w;
			Cond = cond;
			Value = value;
		}

		/// <summary>
		/// что делать
		/// </summary>
		public WhatDos WhatDo{set;get;}
		
		/// <summary>
		/// на что смотреть
		/// </summary>
		public Whats What{set;get;}
		
		/// <summary>
		/// условие
		/// </summary>
		public Conds Cond{set;get;}
		
		/// <summary>
		/// значение
		/// </summary>
		public object Value{set;get;}
		
		public override string ToString()
		{
			var sb = new StringBuilder();
			sb.Append(What + " ");
			switch(Cond)
			{
				case Conds.great: sb.Append("> "); break;
				case Conds.less: sb.Append("< "); break;
			}
			
			if( Value is DateTime )
				sb.Append( ((DateTime)Value).ToString("dd.mm.yyyy") );
			else
				sb.Append(Value);
			sb.Append(" => " + WhatDo);
			return sb.ToString();
		}
	}
}
