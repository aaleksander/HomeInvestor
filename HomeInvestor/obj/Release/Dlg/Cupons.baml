   M S B A M L   `   `   `  ���� N  JHomeInvestor, Version=1.0.7381.17228, Culture=neutral, PublicKeyToken=nullA%clr-namespace:HomeInvestor.ConvertersHomeInvestor.Converters      HomeInvestor.Dlg.Cupons   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6conv%clr-namespace:HomeInvestor.Converters       � WindowStartupLocation$  CenterScreen=� � Title$  ��$	��480��$	��640��$��16(� J� SnapsToDevicePixels .  � 	Resources $   HomeInvestor.Converters.Str2DT 
  str2Dt)   #HomeInvestor.Converters.Decimal2Str  dec2Str��%   &	        &	          .��]� ��. .+�&   System.Windows.Controls.DataGrid     
IsReadOnly .    AutoGenerateColumns .     SelectionMode$ Single=�   SelectionUnit$ FullRow=�����    Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 Портфель��1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *Item1.Portfolio.Name+  $	 Облигация��
 �� *Item2.Obl.FullName+  $	 Дата��
 �� *
Item2.DT+ �� 	Converter# [    $	 
Купон��
 �� *
Item2.Size+# [   $	 Кол-во��
 �� *Item1.Volume+  $	 
Итого��
 �� *Item3+# [ 