   M S B A M L   `   `   `  ���� N  JHomeInvestor, Version=1.0.7381.17228, Culture=neutral, PublicKeyToken=nullA%clr-namespace:HomeInvestor.ConvertersHomeInvestor.Converters  A%clr-namespace:HomeInvestor.ComponentsHomeInvestor.Components  *    $HomeInvestor.Components.TabPortfolio   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     6conv%clr-namespace:HomeInvestor.Converters     6comp%clr-namespace:HomeInvestor.Components     ���� *Color+���� *Name+  � 	Resources     RedBrush �� Color  SolidBorderBrush  
GreenBrush  DisabledBackgroundBrush  DisabledBorderBrush  DisabledForegroundBrush)   #HomeInvestor.Converters.Decimal2Str  dec2str0   *HomeInvestor.Converters.PercentToGridWidth  prc2GridLength/   )HomeInvestor.Converters.True2RedConverter  true2Red*   $HomeInvestor.Converters.NCDConverter 	 ncdConverter)   #HomeInvestor.Converters.Plan2String 

 pl2str(   "HomeInvestor.Converters.Plan2Color 
 pl2col �� 
TargetType �� Property �� Value��%"  &	        &	      &	      &	 0     &	 @     &	 P     &	 `     &	 e     &	 j     &		 o     &	
 t     &	 y     '�� ~     �� $	 Red���� $
 #888���� $ Green���� $
 #EEE���� $
 #AAA���� $
 #888��            �� " ��.���  � ��$ 3q�����  �� 
TargetType" ��.R�� .+��� $��Border��$��0,0,0,-1q�#��[ $��1,1,1,1q����� *Color+._���  ��Name$ ContentSite��$��Center=�$��Center=�$��Header��$��	12,2,12,0q���.  �� Triggers P�  P� Property �@��
IsSelected	 P� Value	 . . ���  �@K�ZIndex$	 100���� 
 �� 
TargetName$
 Border�� �� *Color+ � ���� $
 Border�� � ��$ 1,1,1,0q�P�  � {�	 .  . ��� $
 Border��# [  � ���� $
 Border��# [  � ���� # [  � ��.��]� ��. .+�]� $	��Top=�$��0,-3,0,0q���.  ���� *Color+.+��� $��20(�$��10,3,0,0q�$
��Left=����� *Name+
 � Tag �� *Id+�� $��(��$��10,0,0,0q�$��Center=�$
��Left=��� $��Center=�$��
0, 0, 0, 0q�$
��Left=����� *Description+ �� *Id+�� $��)��$��Center=�$��
0, 0, 0, 0q�$
��Left=��� $!��Добавить актив��$
��Left=�$	��200��$��10,3,3,3q����� *
AddCommand+�� $��Настройка��$
��Left=�$	��100������ *SettingsCommand+�� $%��Удалить портфель��$��Right=�$	��300������ *RemoveCommand+�� $��Bottom=� �� Orientation$ 
Horizontal=�.+��� $%��Общая стоимость: ���� ���� *	TotalCost+ �� 	Converter# [ �� $,��&Облигации в портфеле��.��&   System.Windows.Controls.DataGrid  -   $
��grid��   AutoGenerateColumns .  ���� *	Actives+   RowStyle �� )  #System.Windows.Controls.DataGridRow"  .���  �@ 
IsSelected �� *
IsSelected+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�   Columns 0	  *System.Windows.Controls.DataGridTextColumn	  ,
  &System.Windows.Controls.DataGridColumn 
  Header$ 	№ п/п�� 
  
IsReadOnly .  
  Width5  /System.Windows.Controls.DataGridLengthConverter$ 30 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *PP+	  $ Тип�� . $	 250  �� *Type+	  $ Название�� . $	 250  �� *Name+	  $ Кол-во�� .  $ 60  �� *Volume+	  $# Общая стоимость�� .  �� *	TotalCost+4  .System.Windows.Controls.DataGridTemplateColumn  $	 100 $ % в портфеле�� 
  SortMemberPath$ Percent��   CellTemplate �� .R�� .+��  � ColumnDefinitions �� ���� *	Percent+# [  �� ConverterParameter$ 1���� ���� *	Percent+# [ $ 2��.+��� 
����ح�$��0���� $��Center=�$��Center=����� *	Percent+ �� StringFormat$ {0:F2}��