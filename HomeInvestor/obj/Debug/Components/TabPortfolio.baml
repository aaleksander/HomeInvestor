   M S B A M L   `   `   `  ����N  JHomeInvestor, Version=1.0.7562.15659, Culture=neutral, PublicKeyToken=nullA%clr-namespace:HomeInvestor.ConvertersHomeInvestor.Converters  5      A%clr-namespace:HomeInvestor.ComponentsHomeInvestor.Components  5      *    $HomeInvestor.Components.TabPortfolio   5   
   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      6conv%clr-namespace:HomeInvestor.Converters     5      6comp%clr-namespace:HomeInvestor.Components     5      ��5      �� *Color+��5      �� *Name+  � 	Resources  5	         RedBrush �� Color  SolidBorderBrush  
GreenBrush  DisabledBackgroundBrush  DisabledBorderBrush  DisabledForegroundBrush)   #HomeInvestor.Converters.Decimal2Str  dec2str0   *HomeInvestor.Converters.PercentToGridWidth  prc2GridLength/   )HomeInvestor.Converters.True2RedConverter  true2Red*   $HomeInvestor.Converters.NCDConverter 	 ncdConverter)   #HomeInvestor.Converters.Plan2String 

 pl2str(   "HomeInvestor.Converters.Plan2Color 
 pl2col �� 
TargetType �� Property �� Value��%�  &	        &	      &	 ;     &	 Z     &	 x     &	 �     &	 �     &	 �     &	 �     &		 �     &	
      &	     '�� &    �� 5
   
   $	 Red��6+   �� 5   
   $
 #888��63   �� 5   
   $ Green��6-   �� 5   
   $
 #EEE��6:   �� 5   
   $
 #AAA��66   �� 5   
   $
 #888��6:     5      6     5      6     5      6     5      6     5      6     5      6   �� 5      " ��6
   .�5      ��  � ��$ 3q�6   5      5      ��5      �� 5       �� 
TargetType" ��6%   .R�5      � .+�5       �� $��Border��5!   !   $��0,0,0,-1q�5"   !   #��[ 5$   !   $��1,1,1,1q�5%      ��5#   !   �� *Color+._�5'   "   ��  ��Name$ ContentSite��63   $��Center=�5(   %   $��Center=�5)   %   $��Header��5*   %   $��	12,2,12,0q�5+   %   ��. 5,   %   5-      5.       �� Triggers 5/      P� 50       P� Property �@��
IsSelected	 P� Value	 . . �51   "   ��  �@K�ZIndex$	 100��6A   �� 52   "   
 �� 
TargetName$
 Border��6)    6S   �� *Color+ � ���� 53   "   $
 Border��6)    � ��$ 1,1,1,0q�6X   54      P� 55       � {�	 .  . �56   "   �� $
 Border��6)   # [ 6S    � ���� 57   "   $
 Border��6)   # [ 6T    � ���� 58   "   # [ 6?    � ��59      5;      .��5>      ]� ��. .+�5?      ]� $	��Top=�6   $��0,-3,0,0q�6@   ��.  ��6#   �� *Color+.+�5@      �� $��20(�6+   $��10,3,0,0q�6L   $
��Left=�6^   ��6   �� *Name+
 � Tag 69   �� *Id+6^   �� 5A      $��(��6   $��10,0,0,0q�6   $��Center=�6*   $
��Left=�6E   �� 5B      $��Center=�6/   $��
0, 0, 0, 0q�6]   $
��Left=�6q   ��6   �� *Description+ 6J   �� *Id+6q   �� 5C      $��)��6   $��Center=�6   $��
0, 0, 0, 0q�63   $
��Left=�6G   �� 5D      $!��Добавить актив��6   $
��Left=�6D   $	��200��6Z   $��10,3,3,3q�6f   ��6%   �� *
AddCommand+6f   �� 5E      $��Настройка��6   $
��Left=�6D   $	��100��6Z   ��6    �� *SettingsCommand+6Z   �� 5F      $%��Удалить портфель��6   $��Right=�6I   $	��300��6`   ��6'   �� *RemoveCommand+6`   5G      �� 5H      $��Bottom=�6    �� Orientation$ 
Horizontal=�6'   .+�5I      �� $%��Общая стоимость: ��6   �� 5J      ��6   �� *	TotalCost+ �� 	Converter# [ 5K      �� 5L      $,��&Облигации в портфеле��6   .��5M      &   System.Windows.Controls.DataGrid  -   5        $
��grid��5M   4      AutoGenerateColumns .  ��6   �� *	Actives+   RowStyle 5N   	   �� 5O      )  #System.Windows.Controls.DataGridRow"  6   .�5P      ��  �@ 
IsSelected 5Q      �� *
IsSelected+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5S         Columns 5U      0	  *System.Windows.Controls.DataGridTextColumn	  5V      ,
  &System.Windows.Controls.DataGridColumn 
  Header$ 	№ п/п��6    
  
IsReadOnly .  
  Width5  /System.Windows.Controls.DataGridLengthConverter$ 30 6R   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6)   �� *PP+6R   	  5W      $ Тип��6    . $	 250 6R    6'   �� *Type+6R   	  5X      $ Название��6    . $	 250 6W    6,   �� *Name+6W   	  5Y      $ Кол-во��6    .  $ 60 6X    6*   �� *Volume+6X   	  5Z      $# Общая стоимость��6    .  63   �� *	TotalCost+6Q   4  .System.Windows.Controls.DataGridTemplateColumn  5[   
   $	 100 6!   $ % в портфеле��6-    
  SortMemberPath$ Percent��6C      CellTemplate 5\      �� 5]      .R�5^      � .+�5_      �  � ColumnDefinitions 5`      �� 5a      ��6$   �� *	Percent+# [  �� ConverterParameter$ 1���� 5b      ��6$   �� *	Percent+# [ $ 2��.+�5d      �� 
����ح�$��0��6*   5e      �� 5f      $��Center=�6J   $��Center=�6g   ��6   �� *	Percent+ �� StringFormat$ {0:F2}��6g   5g      5h   
   5j      5l      5m      5n      5o      