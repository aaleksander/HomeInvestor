   M S B A M L   `   `   `  ����N  JHomeInvestor, Version=1.0.7562.15659, Culture=neutral, PublicKeyToken=null7 clr-namespace:HomeInvestor.PagesHomeInvestor.Pages  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5      A%clr-namespace:HomeInvestor.ConvertersHomeInvestor.Converters  5           HomeInvestor.Pages.EtfPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      2local clr-namespace:HomeInvestor.Pages     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      6conv%clr-namespace:HomeInvestor.Converters     5      .��5      ]� ��. .+�5      ]� ��. $	��Top=�6#   $��5q�68   .+�5	      �� $��
Домой��6/   $
��Left=�6?   $��5q�6U   $��3q�6`   ��6   �� *NavHomeCommand+6`   �� 5
        �� Orientation$  
Horizontal=�6   $��Center=�6)   .+�5      �� $%��Справочник акций��6   $��24(�6(   5      5      �� 5      .@�5      �� $��
Акции��6   .��5      ]� ��. .+�5      �� $  
Horizontal=�6   $	��Top=�6+   .+�5      �� $��+��6   $��24��6=   $��5q�6H   ��6   �� *AddEtfCommand+6H   �� 5      $��-��6   $��24��5      $��5q�6   ��5      �� *RemoveEtfCommand+��6@   ��  �� ElementName$ 
gridStocks�� �� Path$ SelectedItem�5      �� 5      $
��edit��6   $��5q�5      $��3q�6   ��5      �� *EditEtfCommand+��6A   �� $ 
gridStocks��$ SelectedItem�5      5      &   System.Windows.Controls.DataGrid  5      -   5        $��
gridStocks��5   6      AutoGenerateColumns .     HeadersVisibility$ Column=�5   I      SelectionMode$ Single=�5         SelectionUnit$ FullRow=�6   ��5      �� *Etfs+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5   	   ,  &System.Windows.Controls.DataGridColumn   Header$ Название��6<   	   
IsReadOnly	 . 
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 200 6b   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6   �� *
FullName+6b     5   	   $ 
Тикер��6:   	 . $
 70 6^    6   �� *Ticker+6^     5   	   $ Цена��6D   $	
 150 6Y    6   �� *Price+ �� Mode$ TwoWay=�6Y     5   	   $ Тип��6?   	 . $	
 200 6a    6   �� *EtfType.Name+6a   5       5!      5"      �� 5#      $��Типы ETF��6   .��5$      ]� ��. .+�5%      �� $  
Horizontal=�6   $	��Top=�6+   .+�5&      �� $��+��6   $��24��6A   $��5q�6L   ��6   �� *AddEtfTypeCommand+6L   �� 5'      $��-��6   $��24��5(      $��5q�6   ��5'      �� *RemoveEtfTypeCommand+��6D   �� $ gridEtfTypes��$ SelectedItem�5(      5)        5*      -   5        $��gridEtfTypes��5+   6    .  $ Column=�5*   M   $ Single=�5+      $ FullRow=�6   ��5*      �� *
EtfTypes+ 5,        5-   	   $ Название��65   	 .  $	
 200 6Z    6   �� *Name+6Z   5/      50      51      52      54      55      