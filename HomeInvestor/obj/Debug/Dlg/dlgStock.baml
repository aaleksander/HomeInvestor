   M S B A M L   `   `   `  ����  HomeInvestor    HomeInvestor.Dlg.dlgStock   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        � Title$  
Акция��5      $	��300��6   $	��600��6    � WindowStartupLocation$ CenterScreen=�5      $��16(�5       J� SnapsToDevicePixels .  � 	Resources 5	       �� 
TargetType �� Property �� Value��%  '��       ']� �     �� 5
      " ��6
   .�5      ��  � ��$	 200��6   �� 5       �@��TextAlignment$ Right=�6%   �� 5       � ��$ 
0, 0, 5, 0q�6   5      �� 5      " ]�6
   .�5      ��  � ��$ 3q�6   5      5      .��5      ��  �� Orientation$ Vertical=�6   .+�5      ]� ��. .+�6$   �� $$��Полное название:��6.   $
��Left=�6F   �� 6^   ��6f   �� *
FullName+6�   ]� 5      ��. .+�6$   �� $��Тикер:��6.   �� 6>   ��6F   �� *Ticker+6a   ]� 5      ��. .+�6$   �� $��Валюта цены:��6.   �� 6D    �� DisplayMemberPath$
 Name��6p   	 �� SelectedValuePath$	 Id��6�   ��6M   �� *
Main.Currs+
 �� SelectedValue
 5      �� *CurrPriceId+6-   ]� 5      ��. .+�6$   �� $(��"Валюта дивидентов:��6.   �� 6J   $
 Name��6v   $	 Id��6�   ��6S   �� *
Main.Currs+
 5      �� *	CurrDivId+6+   ]� 5      ��. .+�6$   �� $��Тип акции:��6.   �� 6B   $
 Name��6s   $	 Id��6�   ��6K   �� *Main.StockTypes+
 5      �� *StockTypeId+6-   ]� 5      ��. .+�6$   �� $��Отрасль:��6.   �� 6@   $
 Name��6n   $	 Id��6�   ��6I   �� *Main.Otrasls+
 5      �� *
OtraslId+6*   �� 5      -   5        $��Ok��5      $	��100��6-   5       5!      