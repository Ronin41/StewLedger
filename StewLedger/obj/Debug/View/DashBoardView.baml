   M S B A M L   `   `   `  ����c  _System.Windows.Interactivity, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35qPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.InteractivitySystem.Windows.Interactivity  5      L HStewLedger, Version=1.0.6662.35663, Culture=neutral, PublicKeyToken=null;"clr-namespace:StewLedger.ViewModelStewLedger.ViewModel 5      | xSystem.Windows.Controls.DataVisualization.Toolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35�{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting 5	      #   StewLedger.View.DashBoardView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ^iPclr-namespace:System.Windows.Interactivity;assembly=System.Windows.Interactivity     5      1vm"clr-namespace:StewLedger.ViewModel     5      X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      �wpftk{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit  5	      -   5          �Name$
  This��5       � Title$ 
StewLedger��5      $
��1000��6    � DataContext 5      &   StewLedger.ViewModel.DashBoardVM  5   
   .��5      � -   5         �Name$
 grid��5      $��0,0,0,2q�6   ��5   
   �� 5       �� EndPoint$ 0.5,1&�6"    � MappingMode$ RelativeToBoundingBox=�63    �� 
StartPoint$ 0.5,0&�6W   .��5      �  � Color$ 	#FF153A0F��6    � Offset$ 0.653Q�61   � 5      $ 	#FF3D1141��6   $ 0.231Q�61   5      	 � ColumnDefinitions	 5!   
   �� 5"      $
��661* �6   �� 5#      $
��206* �6   .+�5%   
   o� $��25��6   $��0,0,0,0q�6   $	��Top=�6,   $��2��6D   .@�5&      m� $
��File��6   .@�5'      m� -   5        
 m�Name$
 menuExitProgram��5'      $
��Exit��64   6B   5(      m� 5*      $��Account��6   .@�5+      m� -   5        $
 menuDeleteAccount��5+      $��Delete User Account��66    m� Command 6S   �� *DeleteUserAcount+ m� CommandParameter 5,      ��  �� ElementName$
 This��5.      m� 50      $��Ledger��6   .@�51      m� -   5        $
 menuBank��51      $
��Bank��6-   6<   m� 52      -   5        $
 menuAutoTransaction��52      $��Auto Transactions��68    6S   �� *OpenNewUpdateTransferWndCmd+ 53      �� $
 This��54      55      �� 58   
   $��	0,25,0,10q�6   $��2��6)   .@�5:      �� $��Dasboard��6   .��5;      � ���	#FFE5E5E5.+�5<      ]� .+�5=      ��  �� Orientation$ Vertical=�6%   .+�5?      >  8System.Windows.Controls.DataVisualization.Charting.Chart  -   5          Name$ PieChart��5?   *   $	��400��6<   $	��250��6H      Title$ Expense Breakdown��6U   $��	0,100,0,0q�6o      Series 5A   &   B  <System.Windows.Controls.DataVisualization.Charting.PieSeries  ?  9System.Windows.Controls.DataVisualization.Charting.Series   Title$ 
Test Chart��66   H  BSystem.Windows.Controls.DataVisualization.Charting.DataPointSeries   IndependentValueBinding 6I   ��  �� Path$	 Key�   DependentValueBinding 5B   *   �� $ Value�5D   '   5F      5G      5H      5I      5K      �� 5M      $��Accounts��6   $
��Auto=�6)   $
��Auto=�6[   .��5N      � ���	#FFC9CBDA$
��Auto=�6.    � RowDefinitions 5O      �� 5P      $
��235* �6(   �� 5Q      $	��23* �6(   	 5S      �� 5T      $
��105* �6+   �� 5U      $
��157* �6+   .+�5W      �� $��Accounts��6   $��	0,0,177,0q�61   $��2��6D   .��5X      �  5Y      �� 5Z   "   $	��33* �60   �� 5[   "   $��5* �60   .+�5]      �� -   5         ��Name$ btnCreateAccount��5]   %   $��Create��6?   $
��Left=�6P   $��28��5^   %   $��	10,10,0,0q�61   $��1��6D   $	��Top=�6Q   $��91��6i   ��5_   %   �� *OpenNewAccountWndCmd+��6N   �� $
 This��&   System.Windows.Controls.DataGrid  5c      -	   5          Name$ gridAccounts��5c   '      AutoGenerateColumns .     
IsReadOnly . ���	#FFD4C8C8$��14(�5d   P   ��5c   =   �� *
Accounts+   Columns 5g   "   4  .System.Windows.Controls.DataGridTemplateColumn  5h   &   ,  &System.Windows.Controls.DataGridColumn   Header$ ID��6=      
IsReadOnly .    CellTemplate 5i   *   �� 5j   .   .R�5k   2   ��  �� 
IsReadOnly . ��6:   �� *Id+6N   5l   /   5n   '     5p   &   $
 Name��6=    .  5q   *   �� 5r   .   .R�5s   2   ��  . ��6:   �� *AccountName+6W   5t   /   5v   '     5x   &   $
 Type��6=    .  5y   *   �� 5z   .   .R�5{   2   ��  . ��6:   �� *AccountType+6W   5|   /   5~   '     5�   &   $ Balance��6=    .  5�   *   �� 5�   .   .R�5�   2   ��  . ��6:   �� *	Balance+6S   5�   /   5�   '     5�   &   $ Account Number��6=    .   5�   *   �� 5�   .   .R�5�   2   ��  .  ��6:   �� *AccountNumber+6Y   5�   /   5�   '     5�   &   $ Delete��6=    .      CanUserReorder  .  !   CanUserResize! .   5�   *   �� 5�   .   .R�5�   2   �� $ 	btnDelete��69   $��X��6L   ��6X   �� *DeleteAccount+��5�   9   �� $
 This��" �� Mode$" OneWay=� 5�   6     5�   :   5�   3   5�   /   5�   '   5�      5�      5�      �� 5�      $��1��6   $��Ledger��6/   $��2��6?   $��Visible=�6P   $��
-172,0,0,0q�6�   .��5�      � 	 5�      �� 5�   "   $
��746* �63   �� 5�   "   $
��263* �63    5�      �� 5�   "   $
��121* �60   �� 5�   "   $	��26* �60   .+�5�        -
   5        $ 
gridLedger��5�   '   $��0,0,0,3q�6S   $��0��6d   $��2��6q    .  .  ���	#FF49839E$��14(�6�   ��6;   ��  5�   "     5�   &   $ Transact Number��6=    .  5�   *   �� 5�   .   .R�5�   2   ��  . ��6:   �� *
LedgerId+6T   5�   /   5�   '   0	  *System.Windows.Controls.DataGridTextColumn	  5�   &   $
 Date��69   1
  +System.Windows.Controls.DataGridBoundColumn# 
  Binding# 6G   �� *Date+$ �� StringFormat$$ {0:dd/MM/yyyy}��  5�   &   $
 Name��6=    .  5�   *   �� 5�   .   .R�5�   2   ��  . ��6:   �� *Name+6P   5�   /   5�   '     5�   &   $ Amount��6=    .  5�   *   �� 5�   .   .R�5�   2   ��  . ��6:   �� *Amount+��6R   �� *Color+6o   5�   /   5�   '     5�   &   $ Balance��6=    .  5�   *   �� 5�   .   .R�5�   2   �� % ��Name$% tbCurrentBalance��6:    . ��6T   �� *	Balance+6m   5�   /   5�   '     5�   &   $ 	TransType��6=    .  5�   *   �� 5�   .   .R�5�   2   ��  . ��6:   �� *	TransType+6U   5�   /   5�   '     5�   &   $ Comments��6=    . &   Width5  /System.Windows.Controls.DataGridLengthConverter$	& 300 6b    5�   *   �� 5�   .   .R�5�   2   ��  . ��6:   �� *
Comments+6T   5�   /   5�   '     5�   &   $
 Edit��6=    5�   *   �� 5�   .   .R�5�   2   �� $
��Edit��69   5�   /   5�   '     5�   &   $ Delete��6=    5�   *   �� 5�   .   .R�5�   2   �� $ btnDeleteTransact��69   $��X��6T   ��5�   9   �� *DeleteTransactionCmd+��5�   9   �� $
 This�� 5�   6     5�   :   5�   3   5�   /   5�   '   5�      �� 5�      $��1��6)   $��2��66   $��0,2,10,0q�6J   .@�5�   "   �� $��Transactions��6*   .��5�   &   �� $��1��61   $��2��6>   .+�5   *   �� $ 
Horizontal=�65   $��1��6N   $��25��6[   .+�5  .   �� $
��Name��68   �� 5  .   -   5        $% tbLedgerName��5  6   $	��100��6L   $��20��6X   $��10,0,0,0q�6d   �� 5  .   $��amount��68   $��10,0,0,0q�6F   �� 5  .   -   5        $% tbLedgerAmount��5  6   $	��100��6N   $��10,0,0,0q�6Z   �� 5  .   $��comments��68   $��10,0,0,0q�6H   �� 5  .   -   5        $% tbLedgerComments��5  6   $	��240��6P   $��10,0,0,0q�6\   5  +   �� 5	  *   $ 
Horizontal=�65   $��1��6N   $��30��6[   $��	0,10,0,30q�6g   .+�5
  .   (  "System.Windows.Controls.DatePicker  -   5        '  Name$' 
ledgerDate��5
  9   $	��100��6M   $��0q�6Y   (   SelectedDateFormat$( Short=�6d   �� 5  .   $ Vertical=�69   $��10,0,10,-18q�6P   .+�5  2   �� $��Category��6<   �� 5  2   -   5        ) ��Name$) listCat��5  ;   $	��130��6L   $��30��6X   ��5  6   #  StewLedger.ViewModel.CatTypes  5  :   5  3   5  /   �� 5  .   $ Vertical=�69   $��10,0,10,-18q�6P   .+�5  2   �� $��Transaction Type��6<   $
��10,0q�6T   �� 5  2   -   5        $) listTransact��5  ;   $	��130��6Q   $��30��6]   ��5  6   '  !StewLedger.ViewModel.TransactType  5  :   5  3   5  /   �� 5!  .   $��	0,0,0,-57q�67   $	��110��6J   .��5"  2   �� $��0,0,0,0q�6=   .+�5#  6   �� -   5        * ��Name$* cbAutoTransaction��5#  ?   $
��Auto��6Z   $
��10,0q�6i   	� 5$  6   $��Monthly��6B   $
��10,0q�6T   	� 5%  6   $��	Bi Weekly��6B   $
��10,0q�6V   	� 5&  6   $��Weekly��6B   $��10,0,0,0q�6S   5'  3   5(  /   �� 5)  .   -   5        $ btnAddTransact��5)  5   $	��Add��6M   $	��100��6[   $��3��6j   ��5*  5   �� *CreateTransactionCmd+��6^   �� $
 This��5+  +   5-  '   5/  #   �� 51  "   $��	Transfers��6*   .��52  &   �� $
��Auto=�63   $��Hidden=�6X   .��53  *   �� $ 
Horizontal=�65   $��50��6N   .+�54  .   �� $��5,0,0,0q�69   .+�55  2   �� $��Source Account��6<   �� 56  2   -   5        $) listSourceAccnt��56  ;   $	��200��6v   ��6T   �� *
Accounts+��57  6   �� 58  :   .R�59  >   �� ��6H   �� *AccountName+5:  ;   5<  3   5=  /   �� 5>  .   $��5,0,0,0q�69   .+�5?  2   �� $��Destination Account��6<   �� 5@  2   -   5        $) listDestinationAccnt��5@  ;   $	��200��6Y   ��6f   �� *
Accounts+��5A  6   �� 5B  :   .R�5C  >   �� ��6H   �� *AccountName+5D  ;   5F  3   5H  /   �� 5I  .   $��5,0,0,0q�69   .+�5J  2   �� $��Amount��6=   �� 5K  2   -   5        $% tbTransferAmnt��5K  :   $	��200��6R   5M  /   �� 5N  .   $��5,0,0,0q�69   .+�5O  2   �� $��Comments��6<   �� 5P  2   -   5        $% tbTransferComments��5P  :   $	��200��6V   5R  /   �� 5S  .   -   5        $* cbAutoTransfer��5S  7   $
��Auto��6O   $	��100��6^   $	��100��6k   $��10,16,0, 10q�6w   �� 5T  .   -   5        $ btnTransfer��5T  5   $��Transfer��6J   $��75��6]   $��	-30,0,0,0q�6h   ��5U  1   �� *CreateTransferCmd+��6W   �� $
 This��5V  +   5W  '   5X  #   5Z     5[     5\     5]     5^     5`     5b     5c     