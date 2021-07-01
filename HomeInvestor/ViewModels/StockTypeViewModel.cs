/*
 * Создано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 12.11.2019
 * Время: 13:46
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using UMD;

namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// Description of StockTypeViewModel.
	/// </summary>
	[Alias("stock_type")]
	[Properties()]
	public class StockTypeViewModel: UMDViewModelBase
	{
		public StockTypeViewModel(IStorage st): base(st)
		{
		}
		
		public StockTypeViewModel(IStorage st, UMDObjectModel obj): base(st, obj)
		{
		}
		
		public override string ToString()
		{
			return "А, " + Name;
		}

	}
	
	
}
