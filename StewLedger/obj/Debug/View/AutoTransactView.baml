   M S B A M L   `   `   `  ����L  HStewLedger, Version=1.0.6662.35663, Culture=neutral, PublicKeyToken=null1clr-namespace:StewLedger.ViewStewLedger.View  5   	   ;"clr-namespace:StewLedger.ViewModelStewLedger.ViewModel  5   	   &     StewLedger.View.AutoTransactView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:StewLedger.View     5   	   8	ViewModel"clr-namespace:StewLedger.ViewModel     5   	   -   5          �Name$
  This��5	   	    � Title$ AutoTransact��5
   	   $��353.985��6   $��441.131��6/    � DataContext 5      )   #StewLedger.ViewModel.AutoTransactVM  5   
   .��5      � .+�5   
   �� $��10,10,10,46q�6   .@�5      �� $��Auto Transaction��6   .��5      � ���	#FFE5E5E5.+�5      &   System.Windows.Controls.DataGrid  -   5          Name$
 grid��5      $��	5,0,10,69q�6-      AutoGenerateColumns .     
IsReadOnly .  ��6@   ��  �� Path$ Transaction� �� Mode$ TwoWay=�   Columns 5      4  .System.Windows.Controls.DataGridTemplateColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$
	 Name��65   
   
IsReadOnly
 .     CellTemplate 5   "   �� 5   &   .R�5   *   �� ��62   �� $
 Name�5   '   5        5       $	 Amount��65   
 .   5!   "   �� 5"   &   .R�5#   *   �� ��62   �� $ Amount�$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5$   '   5&        5(      $
	 Date��65   
 .   5)   "   �� 5*   &   .R�5+   *   (  "System.Windows.Controls.DatePicker     SelectedDate 65   �� $ AutoDate�$ TwoWay=�$ PropertyChanged=� �� StringFormat$ {0:M}��5-   '   5/        51      $
	 Auto��65   
 .   52   "   �� 53   &   .R�54   *   ��  ��Name$ cbAuto��63    d� 	IsChecked 6D   �� $
 Auto�$ TwoWay=�$ PropertyChanged=�55   '   57      59      �� 5;      $��Update��6   $��Right=�6.   $��	0,0,10,37q�6J   $��Bottom=�6]   $��75��6x   ��5<      �� *UpdateAutoTransactCmd+��6G   ��  �� ElementName$
 This���� 5=      -   5         ��Name$ cmbAccountsList��5=      $
��Left=�68   $��24��6S   $��	10,0,0,40q�6_   $��Bottom=�5>      $	��103��6:    �� Text$ Select Account��5@      ��5>   F   �� *AccountInfo+ �� SelectedItem 5?       �� *SelectedAccountItem+��5B      �� 5C      .R�5D   "   �� ��61   �� *Name+5E      5H      5J      5K      �� 5L      -   5        $��Auto Transfer��5L      .��5M      � ���	#FFE5E5E5.+�5N        -   5        $ gridTransfer��5N      $��
5,10,10,59q�65    .  ��6I   �� $ TransferList�$ PropertyChanged=� 5O        5P      $	 Comments��65   
 .  5R   "   �� 5S   &   .R�5T   *   �� ��62   �� *
Comments+$ PropertyChanged=�5U   '   5X        5Z      $	 Amount��65   
 .  5\   "   �� 5]   &   .R�5^   *   �� ��62   �� *Amount+$ PropertyChanged=�5_   '   5b        5d      $
	 Date��65   
 .  5e   "   �� 5f   &   .R�5g   *        Text 65   �� *
AutoDate+$ TwoWay=�$ PropertyChanged=�5h   '   5j        5l      $
	 Auto��65   
 .  5m   "   �� 5n   &   .R�5o   *   �� $ cbTransferAuto��63    6L   �� $
 Auto�$ TwoWay=�$ PropertyChanged=�5p   '   5r      5u      �� 5w      $��Update��6   $��75��6.   $��22��69   $��	0,0,10,23q�6E   $��Bottom=�6X   $��Right=�6s   ��5x      �� *UpdateAutoTransferCmd+��6G   �� $
 This��5z      5{      5|      �� 5}   
   -   5         ��Name$ btnExit��5}      $��Close��6"   $��Right=�62   $��	0,0,10,17q�6N   $��Bottom=�6a   $��75��6|   $��24��6�   5      5�      