   M S B A M L   `   `   `  ���� N  JHomeInvestor, Version=1.0.7381.17228, Culture=neutral, PublicKeyToken=null7 clr-namespace:HomeInvestor.PagesHomeInvestor.Pages  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf A%clr-namespace:HomeInvestor.ComponentsHomeInvestor.Components  "    HomeInvestor.Pages.StockPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     2local clr-namespace:HomeInvestor.Pages     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     6comp%clr-namespace:HomeInvestor.Components     .��]� ��. .+�]� ��. $	��Top=�$��5q�.+��� $��
Домой��$
��Left=�$��5q�$��3q����� *NavHomeCommand+��   �� Orientation$  
Horizontal=�$��Center=�.+��� $%��Справочник акций��$��24(��� .@��� $��
Акции��.��]� ��. .+��� $  
Horizontal=�$	��Top=�.+��� $��+��$��24��$��5q����� *AddStockCommand+�� $��-��$��24��$��5q����� *RemoveStockCommand+����  �� ElementName$ 
gridStocks�� �� Path$ SelectedItem��� $
��edit��$��5q�$��3q����� *EditStockCommand+���� $ 
gridStocks��$ SelectedItem��  � ColumnDefinitions �� �� .+�&   System.Windows.Controls.DataGrid  -   $��
gridStocks��   AutoGenerateColumns .     HeadersVisibility$ Column=�   SelectionMode$ Single=�   SelectionUnit$ FullRow=�$��0������ *Stocks+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 Название��
   
IsReadOnly
 .    Width5  /System.Windows.Controls.DataGridLengthConverter$	 200 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
FullName+  $	 
Тикер��
 . $ 70  �� *Ticker+  $	 Цена��$	 150  �� *Price+ �� Mode$ TwoWay=�  $	 Тип��
 . $	 200  �� *StockType.Name+  $	 Тип��
 . $	 200  �� *Otrasl.Name+4  .System.Windows.Controls.DataGridTemplateColumn     CellTemplate �� .R��� $��Center=�$	��5,0q���  �� Command �� *NotesCommand+.�.��� $��заметки...���� $��Дивиденты��$��2���� $��Типы акций��.��]� ��. .+��� $  
Horizontal=�$	��Top=�.+��� $��+��$��24��$��5q����� *AddStockTypeCommand+�� $��-��$��24��$��5q����� *RemoveStockTypeCommand+���� $ gridStockTypes��$ SelectedItem�  -   $��gridStockTypes�� .  $ Column=�$ Single=�$ FullRow=����� *
StockTypes+   $	 Название��
 .  $	 200  �� *Name+�� $��Отрасли��.��]� ��. .+��� $  
Horizontal=�$	��Top=�.+��� $��+��$��24��$��5q����� *AddOtraslCommand+�� $��-��$��24��$��5q����� *RemoveOtraslCommand+���� $ gridOtrasls��$ SelectedItem�  -   $��gridOtrasls�� .  $ Column=�$ Single=�$ FullRow=����� *	Otrasls+   $	 Название��
 .  $	 200  �� *Name+