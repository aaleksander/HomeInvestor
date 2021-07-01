using System;
using System.Diagnostics;
using System.Windows.Media;
using HomeInvestor.CondParser;
using UMD;

namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// Description of ConditionViewModel.
	/// </summary>
	public class ConditionViewModel: ViewModelBase
	{
		public ConditionViewModel(Condition cond, StockViewModel stock, MainViewModel main)
		{
			Init(cond, stock, main);
		}

		public ConditionViewModel(Condition cond, BondViewModel bond, MainViewModel main)
		{
			Init(cond, bond, main);
		}

		public ConditionViewModel(Condition cond, EtfViewModel etf, MainViewModel main)
		{
			Init(cond, etf, main);
		}

		MainViewModel _main;

		void Init(Condition cond, UMDViewModelBase obj, MainViewModel main)
		{
			_obj = obj;
			_cond = cond;
			_main = main;
		}
		
		UMDViewModelBase _obj;
		Condition _cond;
		
		public string Name
		{
			get{
				if( _obj is StockViewModel )
					return (_obj as StockViewModel).FullName;
				
				if( _obj is BondViewModel )
					return (_obj as BondViewModel).FullName;

				if( _obj is EtfViewModel )
					return (_obj as EtfViewModel).FullName;				
				return "???";
			}
		}

		public string Cond{
			get{
				return _cond.ToString();
			}
		}

		public Brush Color{
			get{
				if( Checked ) //условие сработало
				{
					switch(_cond.WhatDo)
					{
						case WhatDos.buy: return Brushes.LightGreen;
						case WhatDos.sell: return Brushes.LightPink;
					}
				}

				return Brushes.LightGray;
			}
		}

		bool Checked{
			get{
				switch(_cond.What)
				{
					case Whats.price: 		return CheckPrice();
					case Whats.percent: 	return CheckPercent();
					case Whats.gl_percent: 	return Check_Gl_Percent();
					case Whats.date:		return CheckDate();
					default: Debug.WriteLine("неизвестное условие: " + _cond.What.ToString()); break;
				}

				return false;
			}
		}

		/// <summary>
		/// проверка даты
		/// </summary>
		/// <returns></returns>
		bool CheckDate()
		{
			var dt = (DateTime)_cond.Value;
			switch(_cond.Cond)
			{
				case Conds.great: return DateTime.Now > dt;
				case Conds.less:  return DateTime.Now < dt;
			}
			return false;
		}

		/// <summary>
		/// проверяет процент в портфеле
		/// </summary>
		/// <returns></returns>
		bool CheckPercent()
		{			
			//ищем по всем портфелям
			foreach(var port in _main.Portfolios)
			{
				foreach(var pos in port.Positions)
				{
					if( pos.Active.GetHashCode() == _obj.GetHashCode() )
					{//нашли
						switch(_cond.Cond)
						{
								case Conds.great: if( pos.Percent > (decimal)_cond.Value) return true; break;
								case Conds.less:  if( pos.Percent < (decimal)_cond.Value) return true; break;
						}
					}
				}
			}
			return false;
		}

//TODO сделать историю дивидентов по акциям
//TODO потом целиком перейти на историю (объем позиции, средняя и т.д.)
//TODO на основании истории подсчитать доход по каждой позиции (только дивидены и купоны)
//TODO на основании истории построить график доходов

		/// <summary>
		/// проверяет глобальный процент инструмента
		/// </summary>
		/// <returns></returns>
		bool Check_Gl_Percent()
		{			
			//ищем по всем портфелям
			foreach(var pos in _main.Positions)
			{
				if( pos.Active.GetHashCode() == _obj.GetHashCode() )
				{//нашли
					switch(_cond.Cond)
					{
						case Conds.great: if( pos.Percent > (decimal)_cond.Value) return true; break;
						case Conds.less:  if( pos.Percent < (decimal)_cond.Value) return true; break;
					}
				}
			}

			return false;
		}
		
		/// <summary>
		/// проверяет цену
		/// </summary>
		/// <returns></returns>
		bool CheckPrice()
		{
			switch(_cond.Cond)
			{
				case Conds.great: return Price > (decimal)_cond.Value;
				case Conds.less: return Price < (decimal)_cond.Value;
			}
			
			return false;
		}

		decimal Price
		{
			get{
				var stockViewModel = _obj as StockViewModel;
				if( stockViewModel != null )
					return stockViewModel.Price;
				
				var bondViewModel = _obj as BondViewModel;
				if( bondViewModel != null )
					return bondViewModel.Price;
				
				var etfViewModel = _obj as EtfViewModel;
				if( etfViewModel != null )
					return etfViewModel.Price;
				
				return 0;
			}
		}
	}
}
