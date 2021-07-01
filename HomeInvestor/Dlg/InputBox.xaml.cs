/*
 * Created by SharpDevelop.
 * User: AnufrievAA
 * Date: 22.03.2019
 * Time: 15:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace HomeInvestor.Dlg
{
	/// <summary>
	/// Interaction logic for InputBox.xaml
	/// </summary>
	public partial class InputBox : Window
	{
		private enum Modes {str, i, dec};
		
		public InputBox()
		{
			InitializeComponent();
		}
		
		private Modes _mode = Modes.str;
		
		public static string GetString(string caption, string propmpt, string value = "")
		{
			var dlg = new InputBox();
			dlg._mode = Modes.str;
			dlg.Title = caption;
			dlg.prompt.Text = propmpt;
			dlg.val.Text = value;
			var res = dlg.ShowDialog();
			if( res.HasValue && res.Value )
			{
				return dlg.val.Text;
			}
			return null;
		}
		
		public static int? GetInt(string caption, string propmpt, int value = 0)
		{
			var dlg = new InputBox();
			dlg._mode = Modes.i;
			dlg.Title = caption;
			dlg.prompt.Text = propmpt;
			dlg.val.Text = value.ToString();
			var res = dlg.ShowDialog();
			if( res.HasValue && res.Value )
			{
				return int.Parse(dlg.val.Text);
			}
			return null;
		}
		
		public static Decimal? GetDecimal(string caption, string propmpt, decimal value = 0)
		{
			var dlg = new InputBox();
			dlg._mode = Modes.dec;
			dlg.Title = caption;
			dlg.prompt.Text = propmpt;
			dlg.val.Text = value.ToString();
			var res = dlg.ShowDialog();
			if( res.HasValue && res.Value )
			{
				return Decimal.Parse(dlg.val.Text);
			}
			return null;
		}
		
		public static Double? GetDouble(string caption, string propmpt, double value = 0)
		{
			var dlg = new InputBox();
			dlg._mode = Modes.dec;
			dlg.Title = caption;
			dlg.prompt.Text = propmpt;
			dlg.val.Text = value.ToString();
			var res = dlg.ShowDialog();
			if( res.HasValue && res.Value )
			{
				return Double.Parse(dlg.val.Text);
			}
			return null;
		}

		void Button_Click(object sender, RoutedEventArgs e)
		{
			switch(_mode)
			{
				case Modes.str: 
					DialogResult = true; 
					return;
				case Modes.i: 
					int res;
					if( int.TryParse(val.Text, out res) )
					{
						DialogResult = true;
						return;
					}
					else
					{
						MessageBox.Show("Недопустимые символы, должны быть только цифры");
						return;
					}
				case Modes.dec:
					decimal resD;
					if( decimal.TryParse(val.Text, out resD) )
					{
						DialogResult = true;
						return;
					}
					else
					{
						MessageBox.Show("Недопустимые символы, должно быть дробное число");
						return;
					}					
			}
			
			DialogResult = true;
		}
		void Window_Loaded(object sender, RoutedEventArgs e)
		{
			val.Focus();
		}
	}
}