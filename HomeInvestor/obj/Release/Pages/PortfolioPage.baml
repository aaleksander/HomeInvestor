   M S B A M L   `   `   `  ���� N  JHomeInvestor, Version=1.0.7381.17228, Culture=neutral, PublicKeyToken=null7 clr-namespace:HomeInvestor.PagesHomeInvestor.Pages  A%clr-namespace:HomeInvestor.ConvertersHomeInvestor.Converters  &     HomeInvestor.Pages.PortfolioPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     2local clr-namespace:HomeInvestor.Pages     6conv%clr-namespace:HomeInvestor.Converters       � 	Resources     ForegroundStyle �� 
TargetType �� Property �� Value �� RelativeSource �� AncestorType)  #System.Windows.Controls.DataGridRow �� Path �� 	Converter*   $HomeInvestor.Converters.CheckHistory��%\   &	        �� " ��.���  � �� ��  �� "  $ Item.CheckHistory�   .��]� ��. .+�]� ��. $	��Top=�$��5q����� *Color+.+��� $��
Домой��$
��Left=�$��5q�$��3q����� *Main.NavHomeCommand+�� $��Новый актив��$
��Left=�$��5q�$��3q����� *
AddCommand+�� $��Удалить��$��Right=�$��5q�$��3q����� *RemoveCommand+�� $��Настройки��$��Right=�$��5q�$��3q����� *SettingsCommand+��  �� Orientation$ 
Horizontal=�$��Center=�.+��� $��24(����� *Name+�� $��(��$��24(�$��10,0,0,0q��� $��Stretch=�	 �� TextAlignment$	 Center=�$��24(����� *Description+�� $��)��$��24(��� $ 
Horizontal=�$��Bottom=�.+��� $$��Общая стоимость:���� $��10,0,0,0q�$
��Bold"����� *	TotalCost+ )   #HomeInvestor.Converters.Decimal2Str  &   System.Windows.Controls.DataGrid  -   $
��grid��
   AutoGenerateColumns
 .  ���� *	Positions+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Тип��   
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
TypeName+  $ Название�� .  �� *
FullName+  $ Кол-во�� .   �� *Volume+      	CellStyle �� *  $System.Windows.Controls.DataGridCell"  .���  � �� �� *CheckHistoryBrush+  $ Цена�� .  �� *Price+  $ Стоимость�� .  �� *	TotalCost+     $ Цена покупки�� .   �� *
AvgPrice+  $ Фин.рез.�� .  �� *FinRez+   4	  .System.Windows.Controls.DataGridTemplateColumn	     Width5
  /System.Windows.Controls.DataGridLengthConverter$	 200
 $ % в портфеле��   SortMemberPath$ Percent�� 	  CellTemplate �� .R�� .+��  � ColumnDefinitions �� ���� *	Percent+ 0   *HomeInvestor.Converters.PercentToGridWidth   �� ConverterParameter$ 1���� ���� *	Percent+   $ 2��.+��� 
����ح�$��0���� $��Center=�$��Center=����� *	Percent+ �� StringFormat$ {0:F2}��  $ Моя дох�� .  �� *CurrentDohod+     $ Дох.к погаш.�� .  �� *Dohod+   	  $ *
  �� .R�]� ��.  .+��� $��Center=�$	��5,0q�$��Right=���  �� Command �� *RemoveCommand+.�.��� $��Удалить...���� $��Center=�$	��5,0q�$
��Left=���  �� *HistoryCommand+.�.��� $��История...��