   M S B A M L   `   `   `  ����  HomeInvestor    HomeInvestor.Dlg.dlgBond   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        � Title$  Облигация��5      $	��300��6   $	��600��6!    � WindowStartupLocation$ CenterScreen=�5      $��16(�5       J� SnapsToDevicePixels .  � 	Resources 5       �� 
TargetType �� Property �� Value��%  '��       ']� �     �� 5	      " ��6
   .�5
      ��  � ��$	 200��6   �� 5       �@��TextAlignment$ Right=�6%   �� 5       � ��$ 
0, 0, 5, 0q�6   5      �� 5      " ]�6
   .�5      ��  � ��$ 3q�6   5      5      .��5      ��  �� Orientation$ Vertical=�6   .+�5      ]� ��. .+�6$   �� $��Название:��6.   $
��Left=�6?   �� 6W   ��6_   �� *
FullName+6|   ]� 5      ��. .+�6$   �� $��ISIN:��6.   �� 6=   ��6E   �� *ISIN+6^   ]� 5      ��. .+�6$   �� $$��Валюта номинала:��6.   �� 6H    �� DisplayMemberPath$
 Name��6t   	 �� SelectedValuePath$	 Id��6�   ��6Q   �� *
Main.Currs+
 �� SelectedValue
 5      �� *CurrNominalId+6/   ]� 5      ��. .+�6$   �� $��Номинал:��6.   �� 6@   ��6H   �� *	Nominal+6d   ]� 5      ��. .+�6$   �� $"��Валюта купонов:��6.   �� 6G   $
 Name��6s   $	 Id��6�   ��6P   �� *
Main.Currs+
 5      �� *CurrCuponId+6-   ]� 5      ��. .+�6$   �� $ ��Тип облигации:��6.   �� 6F   $
 Name��6v   $	 Id��6�   ��6O   �� *Main.BondTypes+
 5      �� *
BondTypeId+6,   �� 5      -   5        $��Ok��5      $	��100��6-   5      5       