   M S B A M L   `   `   `  ���� N  JHomeInvestor, Version=1.0.7381.17228, Culture=neutral, PublicKeyToken=null7 clr-namespace:HomeInvestor.PagesHomeInvestor.Pages  U QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf %    HomeInvestor.Pages.CurrencyPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     2local clr-namespace:HomeInvestor.Pages     Dlvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf     .��]� ��. .+�]� ��. $	��Top=�$��5q�.+��� $��
Домой��$
��Left=�$��5q�$��3q����� *NavHomeCommand+��   �� Orientation$  
Horizontal=�$��Center=�.+��� $��Валюты��$��24(��  � ColumnDefinitions �� $	��200 ��� $��* �.+��� $��Валюты��$��0��.��&   System.Windows.Controls.DataGrid     AutoGenerateColumns .     HeadersVisibility$
 None=�   
IsReadOnly .    SelectionMode$ Single=�   SelectionUnit$ FullRow=����� *Currs+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 �� *Name+�� $��1��.@��� $��График��.��#  LiveCharts.Wpf.CartesianChart  -   
  Name$
 chart��&   LiveCharts.Wpf.Charts.Base.Chart   AxisX   LiveCharts.Wpf.DateAxis  	  LiveCharts.Wpf.Axis 	  
Foreground
 ��=H�
  LiveCharts.Wpf.WindowAxis 
  HeaderForeground
 �   � 	  
ShowLabels .    InitialDateTime$ 
01.01.2019��   Period �� *Period+ �� Mode$ TwoWay=� 	  	Separator   LiveCharts.Wpf.Separator     StrokeThickness$ 1Q�   Stroke
 �   ��� $��Таблицей��