   M S B A M L   `   `   `  ���� N  JHomeInvestor, Version=1.0.7381.17228, Culture=neutral, PublicKeyToken=nullA%clr-namespace:HomeInvestor.ViewModelsHomeInvestor.ViewModels  A%clr-namespace:HomeInvestor.ConvertersHomeInvestor.Converters  A%clr-namespace:HomeInvestor.ComponentsHomeInvestor.Components      HomeInvestor.Dlg.dlgObl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     4vm%clr-namespace:HomeInvestor.ViewModels     6conv%clr-namespace:HomeInvestor.Converters     6comp%clr-namespace:HomeInvestor.Components       � Title$  Облигации��$	��768��$
��1224�� J� SnapsToDevicePixels .  �� EdgeMode$ Aliased=� � WindowStartupLocation$ CenterScreen=�&   System.Windows.Media.TextOptions   TextFormattingMode$ Display=�$��16(� � 	Resources +   %HomeInvestor.ViewModels.BondViewModel   obl �� 
TargetType �� Property �� Value$   HomeInvestor.Converters.Str2DT 
 str2dt-   'HomeInvestor.Converters.OblCupConverter  
oblCupConv/   )HomeInvestor.Converters.OblAmortConverter  oblAmortConv��%}   &	        '��      &	 <     &	 A     &	 F       �� " ��.���  � ��$ 3q���  � ��$ 3q�      .��� 	 � ColumnDefinitions	 �� $
��1.5* �$	��150���� $
��Auto ��� $��1* �.+��� $��Облигации��$��0��.��]� ��. .+��� $	��Top=�
 �� Orientation$
 
Horizontal=�$��3q�.+��� $��Добавить������ *AddOblToDictCommand+�� $-��'Посчитать доходность������ *RecalcDohodCommand+�� $��Удалить�� � ToolTip$C =Удалить облигацию из справочника������ *RemoveOblCommand+����  �� ElementName$
 grid�� �� Path$ SelectedItem�&   System.Windows.Controls.DataGrid  -   $
��grid��   AutoGenerateColumns .     CanUserAddRows .     CanUserDeleteRows .  ���� *Obls+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ №п/п��   Width5	  /System.Windows.Controls.DataGridLengthConverter$ 50	    
IsReadOnly . 1
  +System.Windows.Controls.DataGridBoundColumn 
  Binding �� *PP+  $ Название��$ *	  �� *
FullName+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�  $ Короткое имя��$	 100	  �� *	ShortName+*   $HomeInvestor.Components.MyDGComboBox  $ Тип��$	 100	    Items$ Windows:OblTypes��4  .System.Windows.Controls.DataGridComboBoxColumn   DisplayMemberPath$
 Name��   SelectedValuePath$ Id��   SelectedValueBinding �� *TypeId+  $ Цена, %�� �� *Price+  $ Доходность, %�� �� *Dohod+ �� StringFormat$ {0:F2}���� $��1��$��5��$��Center=��� $6��0  Выберите облигацию слева��$��2��$��Center=�$��Center=�$��20(��� $��Данные��$��2������ " ��.���  � y�$ Visible=� �� Triggers ��  �� Binding �� $
 grid��$ SelectedItem� �� Value Z� .a���  � y�$ 	Collapsed=�.��]� ��. .+��� $
 Vertical=�$��5q�$	��Top=�.+��� $��20(�$
��Left=����� $
 grid��$ SelectedItem.FullName��� ]� ��.  $��3q�.+��� $��ISIN:��  �� TextAlignment$  Right=�$	��150��$
��Left=�$	��5,0q��� $	��200��$
��Left=����� $
 grid��$ SelectedItem.ISIN�$ TwoWay=�]� ��.  $��3q�.+��� $"��Дата погашения:��$  Right=�$	��150��$
��Left=�$	��5,0q�(  "System.Windows.Controls.DatePicker  $	��200��$
��Left=�!   SelectedDate! �� $
 grid��$ SelectedItem.PogashDT�]� ��.  $��3q�.+��� $��Номинал:��$  Right=�$	��150��$
��Left=�$	��5,0q��� $	��200��$
��Left=����� $
 grid��$ SelectedItem.Nominal��� .@��� $��Купоны��.��]� ��. .+��� $
 
Horizontal=�$	��Top=�.+��� $��Добавить������ *AddCuponCommand+���� $
 grid��$ SelectedItem��� $��Удалить������ *RemoveCuponCommand+��`� " `� 	Converter#" [ .1��� $ cupons��$ SelectedItem��� $
 grid��$ SelectedItem�  -   $��cupons�� .   .  ���� $
 grid��$ SelectedItem.Cupons�#   RowStyle# �� )  #System.Windows.Controls.DataGridRow"   ��  �� *First+ True.a���  � ��
 � ���   $ 	№ п/п��$ 50	  �� *PP+  $ Дата��$	 100	  �� *DT+$ �� 	Converter#$ [   $ 
Купон��$	 100	  �� *Size+�� $��Амортизация��.��]� ��. .+��� $
 
Horizontal=�$	��Top=�.+��� $��Добавить������ *AddAmortCommand+���� $
 grid��$ SelectedItem��� $��Удалить������ *RemoveAmortCommand+��`� #" [ .1��� $ amorts��$ SelectedItem��� $
 grid��$ SelectedItem�  -   $��amorts�� .   .  ���� $
 grid��$ SelectedItem.Amorts�# �� "   ��  �� *First+ True.a���  � ��
 � ���   $ 	№ п/п��$ 50	  �� *PP+  $ Дата��$	 100	  �� *DT+#$ [   $$ Амортизация, руб��$	 100	  �� *Size+�� $'��!Расчет доходности��.���� % �� 
IsReadOnly% . & �� AcceptsReturn& . "' �� VerticalScrollBarVisibility$
' Auto=�$( �� HorizontalScrollBarVisibility$
( Auto=����� $
 grid��$ SelectedItem.Raschet�