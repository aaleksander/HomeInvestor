/*
 * Создано в SharpDevelop.
 * Пользователь: AnufrievAA
 * Дата: 21.06.2019
 * Время: 16:29
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Threading;

namespace HomeInvestor
{
	/// <summary>
	/// Interaction logic for SplashScreen.xaml
	/// </summary>
	public partial class SplashScreen : Window
	{
		public SplashScreen()
		{
			InitializeComponent();
		}
		void Window_Loaded(object sender, RoutedEventArgs e)
		{
	        IAsyncResult result = null;
	
	        // This is an anonymous delegate that will be called when the initialization has COMPLETED
	        AsyncCallback initCompleted = delegate(IAsyncResult ar)
	        {
	            App.Current.ApplicationInitialize.EndInvoke(result);
	
	            // Ensure we call close on the UI Thread.
	            Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Invoker)delegate { Close(); });
	        };
	
	        // This starts the initialization process on the Application
	        result = App.Current.ApplicationInitialize.BeginInvoke(this, initCompleted, null);
		}

	    public void SetProgress(double progress)
	    {
	        // Ensure we update on the UI Thread.
	        Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Invoker)delegate {
	                               	//Debug.WriteLine(progress);
	                               	_progress.Value = progress; 
	                               });
	    }
	    
	    public void SetText(string txt)
	    {
	        // Ensure we update on the UI Thread.
	        Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Invoker)delegate {
	                               	_text.Text = txt;	                               	
	                               });
	    }	    
	}
}