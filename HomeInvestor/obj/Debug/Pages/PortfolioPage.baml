   M S B A M L   `   `   `  ����N  JHomeInvestor, Version=1.0.7562.15659, Culture=neutral, PublicKeyToken=null7 clr-namespace:HomeInvestor.PagesHomeInvestor.Pages  5      A%clr-namespace:HomeInvestor.ConvertersHomeInvestor.Converters  5      &     HomeInvestor.Pages.PortfolioPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      2local clr-namespace:HomeInvestor.Pages     5      6conv%clr-namespace:HomeInvestor.Converters     5        � 	Resources  5         ForegroundStyle �� 
TargetType �� Property �� Value �� RelativeSource �� AncestorType)  #System.Windows.Controls.DataGridRow �� Path �� 	Converter*   $HomeInvestor.Converters.CheckHistory��%�   &	        �� 5      " ��6"   .�5	      ��  � �� 5
      ��  �� "  $ Item.CheckHistory�   5      5      .��5      ]� ��. .+�5      ]� ��. $	��Top=�6#   $��5q�68   ��6C   �� *Color+.+�5      �� $��
Домой��64   $
��Left=�6D   $��5q�6Z   $��3q�6e   ��6   �� *Main.NavHomeCommand+6e   �� 5      $��Новый актив��6+   $
��Left=�6A   $��5q�6W   $��3q�6b   ��6   �� *
AddCommand+6b   �� 5      $��Удалить��6   $��Right=�6@   $��5q�6W   $��3q�6b   ��6   �� *RemoveCommand+6b   �� 5      $��Настройки��6   $��Right=�6D   $��5q�6[   $��3q�6f   ��6    �� *SettingsCommand+6f   �� 5       �� Orientation$ 
Horizontal=�6   $��Center=�6)   .+�5      �� $��24(�6&   ��6   �� *Name+6&   �� 5      $��(��6   $��24(�6   $��10,0,0,0q�6(   �� 5      $��Stretch=�6-   	 �� TextAlignment$	 Center=�6K   $��24(�6b   ��6   �� *Description+6b   �� 5      $��)��6   $��24(�6   5      5      �� 5      $ 
Horizontal=�6   $��Bottom=�6(   .+�5      �� $$��Общая стоимость:��6   �� 5      $��10,0,0,0q�6H   $
��Bold"�6Z   ��6   �� *	TotalCost+ )   #HomeInvestor.Converters.Decimal2Str  6Z   5      &   System.Windows.Controls.DataGrid  5       -   5        $
��grid��5    K   
   AutoGenerateColumns
 .  ��6   �� *	Positions+   Columns 5!      0  *System.Windows.Controls.DataGridTextColumn  5"      ,  &System.Windows.Controls.DataGridColumn   Header$ Тип��6      
IsReadOnly . 1  +System.Windows.Controls.DataGridBoundColumn   Binding 6(   �� *
TypeName+6E     5#      $ Название��6    .  6+   �� *
FullName+6I     5$      $ Кол-во��6    .   6*   �� *Volume+      	CellStyle 5&   
   �� 5'      *  $System.Windows.Controls.DataGridCell"  6   .�5(      ��  � �� 6/   �� *CheckHistoryBrush+5)      5+        5:      $ Цена��6    .  6(   �� *Price+6C     5;      $ Стоимость��6    .  6,   �� *	TotalCost+   6h     5<      $ Цена покупки��6    .   6/   �� *
AvgPrice+6L     5=      $ Фин.рез.��6    .  6+   �� *FinRez+   6d   4	  .System.Windows.Controls.DataGridTemplateColumn	  5>   	      Width5
  /System.Windows.Controls.DataGridLengthConverter$	 200
 6    $ % в портфеле��6,      SortMemberPath$ Percent��6B    	  CellTemplate 5?      �� 5@      .R�5A      � .+�5B      �  � ColumnDefinitions 5C      �� 5D      ��6    �� *	Percent+ 0   *HomeInvestor.Converters.PercentToGridWidth   �� ConverterParameter$ 1���� 5E      ��6    �� *	Percent+   $ 2��.+�5G      �� 
����ح�$��0��6)   5H      �� 5I   
   $��Center=�6F   $��Center=�6c   ��6   �� *	Percent+ �� StringFormat$ {0:F2}��6c   5J   
   5K   	   5M        5O      $ Моя дох��6    .  6*   �� *CurrentDohod+   6i     5P      $ Дох.к погаш.��6    .  6/   �� *Dohod+   6g   	  5R   	   $ *
 6     5S      �� 5T      .R�5U      ]� ��.  .+�5V      �� $��Center=�6   $	��5,0q�64   $��Right=�6A   �� 5W   &    �� Command 60   �� *RemoveCommand+.�6R   .�5X   *   �� $��Удалить...��64   5Y   '   5Z      �� 5[      $��Center=�6   $	��5,0q�64   $
��Left=�6A   �� 5\   &    60   �� *HistoryCommand+.�6S   .�5]   *   �� $��История...��64   5^   '   5_      5`   
   5a   	   5c      5h      5i      5j      