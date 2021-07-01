/*
 * Создано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 12.11.2019
 * Время: 14:49
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using UMD;

namespace HomeInvestor.ViewModels
{
	[Alias("bond_type")]
	[Properties()]
	public class BondTypeViewModel: UMDViewModelBase
	{
		public BondTypeViewModel(IStorage st): base(st)
		{
		}
		
		public BondTypeViewModel(IStorage st, UMDObjectModel obj): base(st, obj)
		{
		}
		
		
		public override string ToString()
		{
			return "О, " + Name;
		}
	}
}
