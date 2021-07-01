using System;

namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// Description of DateTimeExts.
	/// </summary>
	public static class DateTimeExts
	{
		public static string ToStr(this DateTime dt)
	    {
	    	return dt.ToString("dd.MM.yyyy");
	    }

	    public static DateTime ToDt(this string str)
	    {
			return DateTime.ParseExact(str, "dd.MM.yyyy", null);
	    }
	}
}
