   M S B A M L   `   `   `  ����N  JHomeInvestor, Version=1.0.7562.15659, Culture=neutral, PublicKeyToken=nullA%clr-namespace:HomeInvestor.ViewModelsHomeInvestor.ViewModels  5      A%clr-namespace:HomeInvestor.ConvertersHomeInvestor.Converters  5      A%clr-namespace:HomeInvestor.ComponentsHomeInvestor.Components  5          HomeInvestor.Dlg.dlgObl   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      4vm%clr-namespace:HomeInvestor.ViewModels     5      6conv%clr-namespace:HomeInvestor.Converters     5      6comp%clr-namespace:HomeInvestor.Components     5        � Title$  Облигации��5      $	��768��6   $
��1224��6!    J� SnapsToDevicePixels .  �� EdgeMode$ Aliased=�5	       � WindowStartupLocation$ CenterScreen=�5
      &   System.Windows.Media.TextOptions   TextFormattingMode$ Display=�5      $��16(�5       � 	Resources 5      +   %HomeInvestor.ViewModels.BondViewModel   obl �� 
TargetType �� Property �� Value$   HomeInvestor.Converters.Str2DT 
 str2dt-   'HomeInvestor.Converters.OblCupConverter  
oblCupConv/   )HomeInvestor.Converters.OblAmortConverter  oblAmortConv��%�   &	        '��      &	 }     &	 �     &	 �       5      6   �� 5      " ��6	   .�5      ��  � ��$ 3q�6   �� 5       � ��$ 3q�6   5        5      6     5      6     5      6   5      .��5      � 	 � ColumnDefinitions	 5      �� 5      $
��1.5* �6   $	��150��6#   �� 5      $
��Auto �6   �� 5      $��1* �6   .+�5      �� $��Облигации��6   $��0��6    .��5      ]� ��. .+�5       �� $	��Top=�6   
 �� Orientation$
 
Horizontal=�6&   $��3q�6?   .+�5!      �� $��Добавить��6   ��6!   �� *AddOblToDictCommand+�� 5"      $-��'Посчитать доходность��6   ��6-   �� *RecalcDohodCommand+�� 5#      $��Удалить��6    � ToolTip$C =Удалить облигацию из справочника��6    ��6K   �� *RemoveOblCommand+��5$      ��  �� ElementName$
 grid�� �� Path$ SelectedItem�5%      &   System.Windows.Controls.DataGrid  5&      -   5        $
��grid��5&   H      AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  ��6   �� *Obls+   Columns 5'      0  *System.Windows.Controls.DataGridTextColumn  5(      ,  &System.Windows.Controls.DataGridColumn   Header$ №п/п��6      Width5	  /System.Windows.Controls.DataGridLengthConverter$ 50	 6)      
IsReadOnly . 1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding 64   �� *PP+6K     5)      $ Название��6   $ *	 6-    67   �� *
FullName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�  5*      $ Короткое имя��6   $	 100	 61    6=   �� *	ShortName+*   $HomeInvestor.Components.MyDGComboBox  51      $ Тип��6   $	 100	 6'      Items$ Windows:OblTypes��52      4  .System.Windows.Controls.DataGridComboBoxColumn   DisplayMemberPath$
 Name��53         SelectedValuePath$ Id��54         SelectedValueBinding 55      �� *TypeId+  56      $ Цена, %��6    6,   �� *Price+  57      $ Доходность, %��6    62   �� *Dohod+ �� StringFormat$ {0:F2}��59      5:      5;      �� 5=      $��1��6   $��5��6!   $��Center=�6+   �� 5>      $6��0  Выберите облигацию слева��6   $��2��60   $��Center=�6@   $��Center=�6[   $��20(�6x   �� 5?      $��Данные��6   $��2��6   ��5@      �� 5A      " ��6   .�5B      ��  � y�$ Visible=�60    �� Triggers 5C      �� 5D       �� Binding 6#   �� $
 grid��$ SelectedItem� �� Value 6[   Z� .a�5E      ��  � y�$ 	Collapsed=�68   5F      5H      .��5J      ]� ��. .+�5K      �� $
 Vertical=�6   $��5q�6(   $	��Top=�63   .+�5L      �� $��20(�6O   $
��Left=�6]   ��6   �� $
 grid��$ SelectedItem.FullName�6]   �� 5M      ]� 5N      ��.  $��3q�6'   .+�5O      �� $��ISIN:��6     �� TextAlignment$  Right=�6   $	��150��65   $
��Left=�6A   $	��5,0q�6W   �� 5P      $	��200��6W   $
��Left=�6c   ��6   �� $
 grid��$ SelectedItem.ISIN�$ TwoWay=�6c   5Q      ]� 5R      ��.  $��3q�6'   .+�5S      �� $"��Дата погашения:��6   $  Right=�6)   $	��150��6?   $
��Left=�6K   $	��5,0q�6a   (  "System.Windows.Controls.DatePicker  5T      $	��200��6   $
��Left=�6   !   SelectedDate! 65   �� $
 grid��$ SelectedItem.PogashDT�5U      ]� 5V      ��.  $��3q�6'   .+�5W      �� $��Номинал:��6   $  Right=�6"   $	��150��68   $
��Left=�6D   $	��5,0q�6Z   �� 5X      $	��200��6   $
��Left=�6   ��62   �� $
 grid��$ SelectedItem.Nominal�5Y      5a      �� 5c      .@�5d      �� $��Купоны��6   .��5e      ]� ��. .+�5f   	   �� $
 
Horizontal=�6   $	��Top=�6-   .+�5g   
   �� $��Добавить��6   ��6$   �� *AddCuponCommand+��6H   �� $
 grid��$ SelectedItem��� 5h   
   $��Удалить��6   ��6#   �� *RemoveCuponCommand+��5i      `� 5j      " `� 	Converter#" [ 6   .1�5k      �� $ cupons��6   $ SelectedItem�6*   �� 5l      $
 grid��6   $ SelectedItem�6(   5m      5o      5p   
     5q   	   -   5        $��cupons��5r   ,    .   .  ��5q      �� $
 grid��$ SelectedItem.Cupons�#   RowStyle# 5s   
   �� 5t      )  #System.Windows.Controls.DataGridRow"  6    5u      �� 5v       6%   �� *First+ True6?   .a�5w      ��  � ��
 � ���6I   5x      5z       5|   
     5}      $ 	№ п/п��6   $ 50	 6D    6-   �� *PP+6D     5~      $ Дата��6   $	 100	 6f    6,   �� *DT+$ �� 	Converter#$ [ 6f     5      $ 
Купон��6   $	 100	 6F    6-   �� *Size+6F   5�   
   5�   	   5�      �� 5�      $��Амортизация��6   .��5�      ]� ��. .+�5�   	   �� $
 
Horizontal=�6   $	��Top=�6-   .+�5�   
   �� $��Добавить��6   ��6$   �� *AddAmortCommand+��6H   �� $
 grid��$ SelectedItem��� 5�   
   $��Удалить��6   ��6#   �� *RemoveAmortCommand+��5�      `� 5�      #" [ 6   .1�5�      �� $ amorts��6   $ SelectedItem�6*   �� 5�      $
 grid��6   $ SelectedItem�6(   5�      5�      5�   
     5�   	   -   5        $��amorts��5�   ,    .   .  ��5�      �� $
 grid��$ SelectedItem.Amorts�# 5�   
   �� 5�      "  6    5�      �� 5�       6%   �� *First+ True6?   .a�5�      ��  � ��
 � ���6I   5�      5�       5�   
     5�      $ 	№ п/п��6   $ 50	 6D    6-   �� *PP+6D     5�      $ Дата��6   $	 100	 6f    6,   �� *DT+#$ [ 6f     5�      $$ Амортизация, руб��6   $	 100	 6Q    68   �� *Size+6Q   5�   
   5�   	   5�      �� 5�      $'��!Расчет доходности��6   .��5�      �� % �� 
IsReadOnly% . & �� AcceptsReturn& . "' �� VerticalScrollBarVisibility$
' Auto=�5�      $( �� HorizontalScrollBarVisibility$
( Auto=�63   ��5�   "   �� $
 grid��$ SelectedItem.Raschet�5�   3   5�      5�      5�      5�      5�      5�      