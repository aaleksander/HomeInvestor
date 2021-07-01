using System;
using System.ComponentModel;

namespace UMD
{
	/// <summary>
	/// базовый класс всех ViewModelов
	/// </summary>
	public class ViewModelBase: INotifyPropertyChanged
	{
		public ViewModelBase()
		{
		}

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

	    protected static string Dt2Str(DateTime val)
	    {
	    	return val.ToString("dd.MM.yyyy");
	    }

	    protected static DateTime Str2Dt(string str)
	    {
			return DateTime.ParseExact(str, "dd.MM.yyyy", null);
	    }
	}
}
