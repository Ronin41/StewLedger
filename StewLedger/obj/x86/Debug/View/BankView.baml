   M S B A M L   `   `   `  ����L  HStewLedger, Version=1.0.6601.36209, Culture=neutral, PublicKeyToken=null1clr-namespace:StewLedger.ViewStewLedger.View  5      ;"clr-namespace:StewLedger.ViewModelStewLedger.ViewModel  5          StewLedger.View.BankView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      /localclr-namespace:StewLedger.View     5      1vm"clr-namespace:StewLedger.ViewModel     5      X TXceed.Wpf.Toolkit, Version=3.2.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5        � Title$  BankView��5
      $��514.912��6   $	��600��6)   .��5      �  � RowDefinitions 5   
   �� 5      $
��323* �6   �� 5      $	��74* �6    � ColumnDefinitions 5   
   �� 5      $��3* �6   �� 5      .+�5   
   �� $��	0,31,0,70q�6    �� Orientation$ Vertical=�6(   $��2��6?    � DataContext 5      !   StewLedger.ViewModel.BankVM  5      .+�5      �� $
��Wrap=�6   $
��Name��6,   $��	10,5,10,0q�68   �� 5      -   5         ��Name$ tbName��5      $��23��6&    �� TextWrapping$
 Wrap=�62   $�� ��6F   $��
10,10,10,0q�6N   �� 5      $
��Wrap=�6   $��Phone Number��6,   $��	10,5,10,0q�6@   %  Xceed.Wpf.Toolkit.MaskedTextBox  5      -   5          Name$ tbPhoneNumber��5   !   $��23��68   $��
10,10,10,0q�6D      Mask$ (000)000-0000��6X   6o   �� 5      $
��Wrap=�6   $��Routing Number��6,   $��	10,5,10,0q�6B   �� 5      -   5        $ tbRoutingNumber��5      $��23��6/   $
 Wrap=�6;   $�� ��6O   $��
10,10,10,0q�6W   �� 5      $
��Wrap=�6   $
��City��6,   $��	10,5,10,0q�68   �� 5       -   5        $ tbCity��5       $��23��6&   $
 Wrap=�62   $��
10,10,10,0q�6G   �� 5!      $
��Wrap=�6   $��State��6,   $��22��69   $��	10,5,10,0q�6E   �� 5#      -   5        	 ��Name$	 	cmbStates��5#      $��
10,10,10,0q�6*   ��5$      $   StewLedger.ViewModel.StateList  5%      5'      �� 5(      $
��Wrap=�6   $��Zipcode��6,   $��	10,5,10,0q�6;   �� 5)      -   5        $ tbZip��5)      $��23��6%   $
 Wrap=�61   $�� ��6E   $��
10,10,10,0q�6M   
 �� 	MaxLength$
 5��6a   5*      �� 5+   
   -   5         ��Name$ btnAdd��5+      $��Add Bank��6!   $
��Left=�64   $��	39,22,0,0q�6O   $��1��6b   $	��Top=�6o   $��75��6�   ��6�   ��  �� Path$ AddNewBankCmd� �� RelativeSource ��  �� Mode$ FindAncestor=� �� AncestorType" ��� 5-   
   -   5        $ btnExit��5-      $
��Exit��6"   $
��Left=�61   $��
160,22,0,0q�6L   $��1��6`   $	��Top=�6m   $��75��6�   6�   5.      5/      