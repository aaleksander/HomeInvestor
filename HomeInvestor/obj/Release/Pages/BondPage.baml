   M S B A M L   `   `   `  ���� N  JHomeInvestor, Version=1.0.7381.17228, Culture=neutral, PublicKeyToken=null7 clr-namespace:HomeInvestor.PagesHomeInvestor.Pages  A%clr-namespace:HomeInvestor.ConvertersHomeInvestor.Converters  !    HomeInvestor.Pages.BondPage   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     2local clr-namespace:HomeInvestor.Pages     6conv%clr-namespace:HomeInvestor.Converters       � 	Resources   �� 
TargetType �� Property �� Value$   HomeInvestor.Converters.Str2DT 
  str2dt-   'HomeInvestor.Converters.OblCupConverter  
oblCupConv/   )HomeInvestor.Converters.OblAmortConverter  oblAmortConv��%n   '��       &	  7     &	 <     &	 A     �� " ��.���  � ��$ 3q���  � ��$ 3q�      .��]� ��. .+�]� ��. $	��Top=�$��5q�.+��� $��
Домой��$
��Left=�$��5q�$��3q����� *NavHomeCommand+��  �� Orientation$ 
Horizontal=�$��Center=�.+��� $-��'Справочник облигаций��$��24(��� .@��� $��Облигации��.��]� ��. .+��� $ 
Horizontal=�$	��Top=�.+��� $��+��$��24��$��5q����� *AddBondCommand+�� $��-��$��24��$��5q����� *RemoveBondCommand+����  �� ElementName$
 grid�� �� Path$ SelectedItem��� $
��edit��$��5q�$��3q����� *EditBondCommand+���� $
 grid��$ SelectedItem��  � ColumnDefinitions �� �� .+�&   System.Windows.Controls.DataGrid  -   $
��grid��   AutoGenerateColumns .  	   HeadersVisibility$	 Column=�
   SelectionMode$
 Single=�   SelectionUnit$ FullRow=����� *Bonds+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Название��   
IsReadOnly .    Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
FullName+  $
 ISIN�� . $	 150  �� *ISIN+  $ Цена��$	 150  �� *Price+ �� Mode$ TwoWay=�  $ Тип�� . $	 100  �� *BondType.Name+  $ Номинал�� . $	 100  �� *
NominalNow+  $ К погашению�� . $	 100  �� *DohToPogash+ �� 	Converter )	   #HomeInvestor.Converters.Decimal2Str	  4
  .System.Windows.Controls.DataGridTemplateColumn
   
  CellTemplate �� .R��� $ 
Horizontal=�.+��� $��Center=�$	��5,0q���  �� Command �� *NotesCommand+.�.��� $��заметки...���� $��Настройки��$��1������ " ��.���  � y�$ Visible=� �� Triggers ��  �� Binding �� $
 grid��$ SelectedItem� �� Value Z� .a���  � y�$ 	Collapsed=�.��]� ��. .+��� $ Vertical=�$��5q�$	��Top=�.+��� $��20(�$
��Left=����� $
 grid��$ SelectedItem.FullName��� ]� ��.  $��3q�.+��� $��ISIN:�� �� TextAlignment$ Right=�$	��180��$
��Left=�$	��5,0q��� $	��200��$
��Left=����� $
 grid��$ SelectedItem.ISIN�$ TwoWay=�]� ��.  $��3q�.+��� $$��Дата размещения:��$ Right=�$	��180��$
��Left=�$	��5,0q�(  "System.Windows.Controls.DatePicker  $	��200��$
��Left=�   SelectedDate �� $
 grid��$ SelectedItem.VypuskDT�]� ��.  $��3q�.+��� $"��Дата погашения:��$ Right=�$	��180��$
��Left=�$	��5,0q�  $	��200��$
��Left=� �� $
 grid��$ SelectedItem.PogashDT�]� ��.  $��3q�.+��� $��Номинал:��$ Right=�$	��180��$
��Left=�$	��5,0q��� $	��200��$
��Left=����� $
 grid��$ SelectedItem.Nominal� �� UpdateSourceTrigger$ PropertyChanged=�]� ��.  $��3q�.+��� $��Возраст:��$ Right=�$	��180��$
��Left=�$	��5,0q��� $	��200��$
��Left=����� $
 grid��$ SelectedItem.Age�$ PropertyChanged=�$ OneWay=�]� ��.  $��3q�.+��� $��До погашения:��$ Right=�$	��180��$
��Left=�$	��5,0q��� $	��200��$
��Left=����� $
 grid��$! SelectedItem.DaysLeftPogash�$ PropertyChanged=�$ OneWay=��� .@��� $��Купоны��.��]� ��. .+��� $ 
Horizontal=�$	��Top=�.+��� $��Добавить������ *AddCuponCommand+���� $
 grid��$ SelectedItem��� $��Удалить������ *RemoveCuponCommand+��`�  `� 	Converter# [ .1��� $ cupons��$ SelectedItem��� $
 grid��$ SelectedItem�  -   $��cupons�� .     CanUserDeleteRows .  ���� $
 grid��$ SelectedItem.Cupons�   RowStyle �� )  #System.Windows.Controls.DataGridRow"   ��  �� *First+ True.a���  � ��
 � ���   $ 	№ п/п��$ 50  �� *PP+  $ Дата��$	 150  �� *DT+# [    $ 
Купон��$	 100  �� *Size+�� $��Амортизация��.��]� ��. .+��� $ 
Horizontal=�$	��Top=�.+��� $��Добавить������ *AddAmortCommand+���� $
 grid��$ SelectedItem��� $��Удалить������ *RemoveAmortCommand+��`� # [ .1��� $ amorts��$ SelectedItem��� $
 grid��$ SelectedItem�  -   $��amorts�� .   .  ���� $
 grid��$ SelectedItem.Amorts� �� "   ��  �� *First+ True.a���  � ��
 � ���   $ 	№ п/п��$ 50  �� *PP+  $ Дата��$	 150  �� *DT+# [    $$ Амортизация, руб��$	 100  �� *Size+�� $'��!Расчет доходности���� $!��Типы облигаций��.��]� ��. .+��� $ 
Horizontal=�$	��Top=�.+��� $��+��$��24��$��5q����� *AddBondTypeCommand+�� $��-��$��24��$��5q����� *RemoveBondTypeCommand+���� $ gridBondTypes��$ SelectedItem�  -   $��gridBondTypes�� .  $	 Column=�$
 Single=�$ FullRow=����� *	BondTypes+   $ Название�� .  $	 200  �� *Name+