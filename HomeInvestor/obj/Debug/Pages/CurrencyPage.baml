   M S B A M L   `   `   `  ����N  JHomeInvestor, Version=1.0.7562.15659, Culture=neutral, PublicKeyToken=null7 clr-namespace:HomeInvestor.PagesHomeInvestor.Pages  5      U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf 5      %    HomeInvestor.Pages.CurrencyPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      2local clr-namespace:HomeInvestor.Pages     5      Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     5      .��5      ]� ��. .+�5      ]� ��. $	��Top=�6#   $��5q�68   .+�5	      �� $��
Домой��6/   $
��Left=�6?   $��5q�6U   $��3q�6`   ��6   �� *NavHomeCommand+6`   �� 5
        �� Orientation$  
Horizontal=�6   $��Center=�6)   .+�5      �� $��Валюты��6   $��24(�6   5      5      � 5       � ColumnDefinitions 5      �� 5      $	��200 �6   �� 5      $��* �6   .+�5      �� $��Валюты��6   $��0��6   .��5      &   System.Windows.Controls.DataGrid     AutoGenerateColumns .     HeadersVisibility$
 None=�6I      
IsReadOnly .    SelectionMode$ Single=�5         SelectionUnit$ FullRow=�6   ��5      �� *Currs+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 64   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 6   �� *Name+64   5      5      �� 5      $��1��6   .@�5      �� $��График��6   .��5      #  LiveCharts.Wpf.CartesianChart  -   5        
  Name$
 chart��5      &   LiveCharts.Wpf.Charts.Base.Chart   AxisX 5         LiveCharts.Wpf.DateAxis  	  LiveCharts.Wpf.Axis 	  
Foreground
 ��=H�
  LiveCharts.Wpf.WindowAxis 
  HeaderForeground
 �   � 	  
ShowLabels .    InitialDateTime$ 
01.01.2019��5&         Period 5#      �� *Period+ �� Mode$ TwoWay=� 	  	Separator 5'        LiveCharts.Wpf.Separator  5(         StrokeThickness$ 1Q�6+      Stroke
 �   �6?   5*      5-      5.      �� 5/      $��Таблицей��6   51      52      53      54      55      