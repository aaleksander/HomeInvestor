using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using HomeInvestor.CondParser;

namespace HomeInvestor.Dlg
{
	/// <summary>
	/// Interaction logic for dlgNotes.xaml
	/// </summary>
	public partial class dlgNotes : Window, INotifyPropertyChanged
	{
		public dlgNotes()
		{
			InitializeComponent();
			DataContext = this;
		}

		void Button_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}

		public ObservableCollection<HomeInvestor.CondParser.Condition> Conds{
			get{
				return _conds;
			}
			private set{
				_conds = value;
				OnPropertyChanged("Conds");
			}
		}
		ObservableCollection<HomeInvestor.CondParser.Condition> _conds = new ObservableCollection<HomeInvestor.CondParser.Condition>();
		
		
		
	    public event PropertyChangedEventHandler PropertyChanged;
	    protected virtual void OnPropertyChanged(string propertyName)
	    {
	    	if( PropertyChanged != null )
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
	    }
		void Text_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
		{
			Debug.WriteLine("key up");
			var cc = Parser.GetConds(text.Text);
			foreach(var c in cc)
			{
				var cond = Parser.Parse(c);
				Debug.WriteLine(cond);
			}
		}
	}
}