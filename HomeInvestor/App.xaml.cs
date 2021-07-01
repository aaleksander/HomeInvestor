using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using HomeInvestor.Actors;

namespace HomeInvestor
{
	internal delegate void Invoker();
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public App()
		{
			AppDomain.CurrentDomain.UnhandledException += delegate( object sender, UnhandledExceptionEventArgs args )
			{
				MessageBox.Show("Глобальная ошибка: " + args.ExceptionObject);
				Environment.Exit(0);
			};

	    	var loader = new DBLoader();

	    	loader.OnLoaded += () => {lock(locker){ _complete = true;}};

	        ApplicationInitialize = _applicationInitialize;
		}

		bool _complete = false;
		object locker = new object();

	    public static new App Current
	    {
	        get { return Application.Current as App; }
	    }
	    internal delegate void ApplicationInitializeDelegate(SplashScreen splashWindow);
	    internal ApplicationInitializeDelegate ApplicationInitialize;
	    private void _applicationInitialize(SplashScreen splashWindow)
	    {
	    	//актор, который будет следить за прогрессом загрузки
	    	var progress = new DBProgress();
			progress.OnProgress += curr => {
	    							    		splashWindow.SetProgress(curr);
	    	};

	    	//даем команду загружать базу данных
	    	//ActorSystem.Instance["loader"].Tell(new MsgLoadDB());
	    	ActorSystem.Instance.Tell<DBLoader>(new MsgLoadDB());

	    	//ждем пока загрузится
	    	while(true)
	    	{
	    		Thread.Sleep(100);
	    		lock(locker)
	    		{
	    			if( _complete )
	    				break;
	    		}
	    	}

	    	splashWindow.SetText("запуск приложения...");
	    	Thread.Sleep(50);
	    	ActorSystem.Instance.RemoveActor(progress); //больше не нужен
	    	//запускаем основное окно
			Dispatcher.BeginInvoke(DispatcherPriority.Normal, (Invoker)delegate {
					MainWindow = new Window1();
					MainWindow.Show();
			});
	    }
	}
}