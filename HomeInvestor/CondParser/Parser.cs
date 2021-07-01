using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace HomeInvestor.CondParser
{
	public static class Parser
	{
		/// <summary>
		/// выбрать из текста все условия
		/// </summary>
		/// <param name="str"></param>
		/// <returns></returns>
		public static List<string> GetConds(string str) //TODO нужно возвращать вместе с номером строки, чтобы в дальнейшем выводить ошибку
		{
			return str.Split('\n')
				.Select(x => x.Trim())
				.Where(s => s.StartsWith(@"""") && s.EndsWith(@""""))
				.Select(x => x.Trim('"')).ToList();
		}

		public static Condition Parse(string src)
		{
			WhatDos wd	= WhatDos.none;
			Whats   w 	= Whats.none;
			Conds	c	= Conds.none;
			object  v   = null;
			
			var pp = src.Split(' ');
			if( pp.Length != 5 )
				return null;
			wd = ParseWD(pp[0]);
			w = ParseW(pp[2]);
			c = ParseC(pp[3]);
			v = ParseV(pp[4]);

			return new Condition(wd, w, c, v);
		}

		public static string LastError{private set;get;}

		private static WhatDos ParseWD(string str)
		{
			var sels = new List<string>(){"продавать", "продать", "sell"};
			var buys = new List<string>(){"покупать", "купить", "buy"};

			if( sels.Any(x => x == str.ToLower()) )
				return WhatDos.sell;

			if( buys.Any(x => x == str.ToLower()) )
				return WhatDos.buy;

			return WhatDos.none;
		}

		private static Whats ParseW(string str)
		{
			var dict = new Dictionary<string, Whats>(){
				{"цена", Whats.price},
				{"price", Whats.price},
				{"%", Whats.percent},
				{"%%", Whats.gl_percent},
				{"дата", Whats.date},
				{"возраст", Whats.age},
			};

			if( dict.ContainsKey(str.ToLower()) )
			{
				return dict[str.ToLower()];
			}

			return Whats.none;
		}

		private static Conds ParseC(string str)
		{
			var dict = new Dictionary<string, Conds>(){
				{"больше", Conds.great},
				{">", Conds.great},
				{"выше", Conds.great},
				{"меньше", Conds.less},
				{"<", Conds.less},
				{"ниже", Conds.less}
			};

			if( dict.ContainsKey(str.ToLower()) )
			{
				return dict[str.ToLower()];
			}

			return Conds.none;
		}

		static object ParseV(string str)
		{			
			decimal f = 0;
			if( decimal.TryParse(str, out f) )
				return f;

			DateTime dt;

			if( DateTime.TryParseExact(str, "dd.mm.yyyy", null, DateTimeStyles.None, out dt) )
				return dt;

			return null;
		}
	}
}
