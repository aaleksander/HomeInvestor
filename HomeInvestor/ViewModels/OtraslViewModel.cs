/*
 * Создано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 11.12.2019
 * Время: 17:43
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using UMD;

namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// отрасль у акции (облигации?)
	/// </summary>
	[AliasAttribute("otrasl")]
	[Properties()]
	public class OtraslViewModel: UMDViewModelBase
	{
		public OtraslViewModel(IStorage st): base(st)
		{
		}
		
		public OtraslViewModel(IStorage st, UMDObjectModel obj): base(st, obj)
		{
		}
	}
}
