   M S B A M L   `   `   `  ����N  JHomeInvestor, Version=1.0.7562.15659, Culture=neutral, PublicKeyToken=null7 clr-namespace:HomeInvestor.PagesHomeInvestor.Pages  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5      A%clr-namespace:HomeInvestor.ComponentsHomeInvestor.Components  5      "    HomeInvestor.Pages.StockPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      2local clr-namespace:HomeInvestor.Pages     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      6comp%clr-namespace:HomeInvestor.Components     5      .��5      ]� ��. .+�5	      ]� ��. $	��Top=�6#   $��5q�68   .+�5
      �� $��
Домой��6/   $
��Left=�6?   $��5q�6U   $��3q�6`   ��6   �� *NavHomeCommand+6`   �� 5        �� Orientation$  
Horizontal=�6   $��Center=�6)   .+�5      �� $%��Справочник акций��6   $��24(�6(   5      5      �� 5      .@�5      �� $��
Акции��6   .��5      ]� ��. .+�5      �� $  
Horizontal=�6   $	��Top=�6+   .+�5      �� $��+��6   $��24��6?   $��5q�6J   ��6   �� *AddStockCommand+6J   �� 5      $��-��6   $��24��5      $��5q�6   ��5      �� *RemoveStockCommand+��6B   ��  �� ElementName$ 
gridStocks�� �� Path$ SelectedItem�5      �� 5      $
��edit��6   $��5q�5      $��3q�6   ��5      �� *EditStockCommand+��6C   �� $ 
gridStocks��$ SelectedItem�5      5      � 5       � ColumnDefinitions 5      �� 5   	   �� 5   	   .+�5      &   System.Windows.Controls.DataGrid  -   5        $��
gridStocks��5    7      AutoGenerateColumns .     HeadersVisibility$ Column=�5   L      SelectionMode$ Single=�5          SelectionUnit$ FullRow=�6   $��0��6I   ��5      �� *Stocks+   Columns 5!   	   0  *System.Windows.Controls.DataGridTextColumn  5"   
   ,  &System.Windows.Controls.DataGridColumn	   Header$	 Название��6=   
   
IsReadOnly
 .    Width5  /System.Windows.Controls.DataGridLengthConverter$	 200 6c   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6   �� *
FullName+6c     5#   
   $	 
Тикер��6;   
 . $ 70 6_    6   �� *Ticker+6_     5$   
   $	 Цена��6E   $	 150 6Z    6   �� *Price+ �� Mode$ TwoWay=�6Z     5%   
   $	 Тип��6B   
 . $	 200 6d    6   �� *StockType.Name+6d     5&   
   $	 Тип��6?   
 . $	 200 6a    6   �� *Otrasl.Name+6a   4  .System.Windows.Controls.DataGridTemplateColumn  5'   
      CellTemplate 5(      �� 5)      .R�5*      �� $��Center=�6   $	��5,0q�67   �� 5+   )    �� Command 63   �� *NotesCommand+.�6T   .�5,   -   �� $��заметки...��67   5-   *   5.      5/      51      53   	   �� 55      $��Дивиденты��6   $��2��6$   57   	   58      59      5:      �� 5;      $��Типы акций��6   .��5<      ]� ��. .+�5=      �� $  
Horizontal=�6   $	��Top=�6+   .+�5>      �� $��+��6   $��24��6C   $��5q�6N   ��6   �� *AddStockTypeCommand+6N   �� 5?      $��-��6   $��24��5@      $��5q�6   ��5?      �� *RemoveStockTypeCommand+��6F   �� $ gridStockTypes��$ SelectedItem�5@      5A        5B      -   5        $��gridStockTypes��5C   6    .  $ Column=�5B   O   $ Single=�5C      $ FullRow=�6   ��5B      �� *
StockTypes+ 5D        5E   	   $	 Название��65   
 .  $	 200 6Z    6   �� *Name+6Z   5G      5H      5I      �� 5K      $��Отрасли��6   .��5L      ]� ��. .+�5M      �� $  
Horizontal=�6   $	��Top=�6+   .+�5N      �� $��+��6   $��24��6@   $��5q�6K   ��6   �� *AddOtraslCommand+6K   �� 5O      $��-��6   $��24��5P      $��5q�6   ��5O      �� *RemoveOtraslCommand+��6C   �� $ gridOtrasls��$ SelectedItem�5P      5Q        5R      -   5        $��gridOtrasls��5S   6    .  $ Column=�5R   L   $ Single=�5S      $ FullRow=�6   ��5R      �� *	Otrasls+ 5T        5U   	   $	 Название��65   
 .  $	 200 6Z    6   �� *Name+6Z   5W      5X      5Y      5Z      5[      5\      