using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace HomeInvestor.Components
{
	/// <summary>
	/// Interaction logic for MyDGComboBox.xaml
	/// </summary>
	public partial class MyDGComboBox : DataGridComboBoxColumn
	{
		public MyDGComboBox()
		{
			InitializeComponent();
		}

		public static DependencyProperty ItemsProperty;

		static MyDGComboBox()
		{
			ItemsProperty = DependencyProperty.Register("Items", typeof(String), typeof(MyDGComboBox), 
			                                            new FrameworkPropertyMetadata("", new PropertyChangedCallback(OnItemsChanged)));
		}

		public string Items{
			set{
				SetValue(ItemsProperty, value);
			}
			get{
				return (string)GetValue(ItemsProperty);
			}
		}

		private static void OnItemsChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
		{
			MyDGComboBox s = (MyDGComboBox) sender;

			string items = (string)e.NewValue;
			string typ = "";
			string link = "";
			int a = items.IndexOf(':');
			if( a != -1)
			{
				typ = items.Substring(0, a);
				link = items.Substring(a + 1);
			}
			else
			{
				link = items;
			}

			Binding b = new Binding(link);
			if( typ == "Window" ) 
				b.RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(Window), 1);

			if( typ == "Page" )
				b.RelativeSource = new RelativeSource(RelativeSourceMode.FindAncestor, typeof(Page), 1);

			Style st = new Style(typeof(ComboBox));
			//сеттер
			var sett = new Setter(ComboBox.ItemsSourceProperty, b);
			st.Setters.Add(sett);

			s.ElementStyle = st;
			s.EditingElementStyle = st;
		}
	}
}