   M S B A M L   `   `   `  ����N  JHomeInvestor, Version=1.0.7562.15659, Culture=neutral, PublicKeyToken=nullA%clr-namespace:HomeInvestor.ConvertersHomeInvestor.Converters  5          HomeInvestor.Dlg.Cupons   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      6conv%clr-namespace:HomeInvestor.Converters     5        � WindowStartupLocation$  CenterScreen=�5       � Title$  ��5      $	��480��6   $	��640��6   $��16(�5       J� SnapsToDevicePixels .  � 	Resources 5      $   HomeInvestor.Converters.Str2DT 
  str2Dt)   #HomeInvestor.Converters.Decimal2Str  dec2Str��%:   &	        &	        5      6     5      6   5      .��5      ]� ��. .+�5      &   System.Windows.Controls.DataGrid     
IsReadOnly .    AutoGenerateColumns .     SelectionMode$ Single=�6S      SelectionUnit$ FullRow=�6j   ��6   ��    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Портфель��6   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6-   �� *Item1.Portfolio.Name+  5      $	 Облигация��6   
 6.   �� *Item2.Obl.FullName+  5      $	 Дата��6   
 6*   �� *
Item2.DT+ �� 	Converter# [    5      $	 
Купон��6   
 6*   �� *
Item2.Size+# [   5      $	 Кол-во��6   
 6+   �� *Item1.Volume+  5      $	 
Итого��6   
 6*   �� *Item3+# [ 5      5      5      