using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using HomeInvestor.Actors;
using UMD;
using HomeInvestor.Dlg;


namespace HomeInvestor.ViewModels
{
	/// <summary>
	/// Справочник облигаций
	/// </summary>
	public class DictOblViewModel : INotifyPropertyChanged
	{
		public DictOblViewModel(IStorage storage)
		{
			_storage = storage;
			AddOblToDictCommand 	= new RelayCommand(AddOblToDict);
			AddCuponCommand			= new RelayCommand(AddCuponToObl);
			AddAmortCommand			= new RelayCommand(AddAmortToObl);
			RemoveCuponCommand		= new RelayCommand(RemoveCupon, CanRemoveCupon);
			RemoveAmortCommand		= new RelayCommand(RemoveAmort, CanRemoveAmort);
			RecalcDohodCommand 		= new RelayCommand(RecalcDohod);
			RemoveOblCommand 		= new RelayCommand(RemoveObl);

			_actor = new OblDictActor(this);
		}
		IStorage _storage;
		OblDictActor _actor;
		
		public RelayCommand AddCuponCommand{private set;get;}
		public RelayCommand AddAmortCommand{private set;get;}
		public RelayCommand AddOblToDictCommand{private set;get;}
		public RelayCommand RemoveCuponCommand{private set;get;}
		public RelayCommand RemoveAmortCommand{private set;get;}
		public RelayCommand RecalcDohodCommand{private set;get;}
		public RelayCommand RemoveOblCommand{private set;get;}
		
		private void AddOblToDict(object o)
		{
			var name = InputBox.GetString("Новая облигация", "Короткое имя:");
			if( name == null )
				return;
			var nv = new BondViewModel(_storage)
			{
				ShortName = name,
				PP = Obls.Count + 1
			};			
			Obls.Add(nv);
			//nv.Insert();
			_actor.Tell(new MsgAddObl(nv));
			//Thread.Sleep(100);
			//Obls.RePP();
		}

		public ObservableCollection<BondViewModel> Obls
		{
			get{
				if( _obls == null )
				{
					_obls = UMDQuery.Select<BondViewModel>().ToOC();
					_obls.RePP();
				}
				return _obls;
			}
		}
		private ObservableCollection<BondViewModel> _obls = null;

	    public event PropertyChangedEventHandler PropertyChanged;
	    protected virtual void OnPropertyChanged(string propertyName)
	    {
	    	if( PropertyChanged != null )
				PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
	    }

	    private void AddCuponToObl(object o)
	    {
	    	_storage.BeginTransaction();
	    	var obl = (BondViewModel) o;
	    	double delta = 0;
	    	decimal size = 0;
	    	if( obl.Cupons.Count >= 2 )
	    	{
	    		var d1 = obl.Cupons[obl.Cupons.Count - 1].DT;
	    		var d2 = obl.Cupons[obl.Cupons.Count - 2].DT;
	    		delta = (d1 - d2).TotalDays;
	    		size = obl.Cupons[obl.Cupons.Count - 1].Size;
	    	}

	    	var cup = new CuponViewModel(_storage)
	    	{
	    		OblId = obl.Id,
	    		Size = size
	    	};
	    	cup.Insert();
	    	if( delta > 0 )
	    		cup.DT = obl.Cupons[obl.Cupons.Count - 1].DT.AddDays(delta);
	    	else
	    		cup.DT = DateTime.Now;
	    	obl.Cupons.Add(cup);
	    	obl.Cupons.RePP();
	    	_storage.Commit();
	    }


    
	    private void AddAmortToObl(object o)
	    {
	    	_storage.BeginTransaction();
	    	var obl = (BondViewModel) o;

//	    	var n = new AmortModel()
//	    	{
//	    		OblId = obl.Id,
//	    		Size = 0
//	    	};
//	    	_storage.Insert(n);
	    	var amort = new AmortViewModel(_storage)
	    	{
	    		OblId = obl.Id,
	    		Size = 0
	    	};
	    	amort.Insert();
	    	amort.DT = DateTime.Now;
	    	obl.Amorts.Add(amort);
	    	obl.Amorts.RePP();
	    	//amort.PP = obl.Amorts.Count;
	    	//MessageBox.Show("Добавляем купон в " + obl.FullName);
	    	_storage.Commit();
	    }

	    void RemoveCupon(object o)
	    {
	    	var t = (Tuple<BondViewModel, CuponViewModel>)o;	    	
	    	t.Item1.RemoveCupon(t.Item2);
	    }

	    void RemoveAmort(object o)
	    {
	    	var t = (Tuple<BondViewModel, AmortViewModel>)o;	    	
	    	t.Item1.RemoveAmort(t.Item2);
	    }

	    bool CanRemoveCupon(object o)
	    {
	    	if( o == null )
	    		return false;
	    	var t = (Tuple<BondViewModel, CuponViewModel>)o;
	    	if( t.Item1 == null )
	    		return false;
	    	if( t.Item2 == null )
	    		return false;
	    	return true;
	    }

	    bool CanRemoveAmort(object o)
	    {
	    	if( o == null )
	    		return false;
	    	var t = (Tuple<BondViewModel, AmortViewModel>)o;
	    	if( t.Item1 == null )
	    		return false;
	    	if( t.Item2 == null )
	    		return false;
	    	return true;
	    }

	    void RecalcDohod(object o)
	    {			
	    	foreach(var obl in Obls)
	    	{
	    		obl.UpdateDohod();
	    	}
	    }

	    void RemoveObl(object o)
	    {
	    	if( o == null )
	    	{
	    		MessageBox.Show("Выберите облигацию для удаления");
	    		return;
	    	}
	    	var obl = o  as BondViewModel;
	    	//проверяем, чтобы облига нигде не участвовала
	    	foreach(var port in UMDQuery.Select<PortfolioViewModel>())
	    	{
	    		throw new Exception("надо восстановить");
	    		/*foreach(var pos in port.Positions)
	    		{	    			
	    			if( pos.Type == ActiveType.bond && pos.IdActive == obl.Id )
	    			{
	    				MessageBox.Show("Данная облигацию участвует в портфеле " + port.Name + "!\n" +
	    				                "Cначало выведите ее из портфеля");
	    				return;
	    			}
	    		}*/
	    	}

	    	if( MessageBox.Show("Удаляется облигация " + obl.FullName + "(" + obl.ShortName + ")\nУдаляем?", "Внимание!", MessageBoxButton.YesNo,
	    	                    MessageBoxImage.Warning) == MessageBoxResult.No )
	    		return;
	    	_storage.BeginTransaction();
	    	foreach(var c in obl.Cupons)
	    		c.Delete();
	    	foreach(var a in obl.Cupons)
	    		a.Delete();
	    	obl.Delete();
	    	Obls.Remove(obl);
	    	Obls.RePP();
	    	_storage.Commit();
	    }
	}
}