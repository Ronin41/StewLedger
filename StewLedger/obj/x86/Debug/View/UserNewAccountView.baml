   M S B A M L   `   `   `  ����L  HStewLedger, Version=1.0.6601.36209, Culture=neutral, PublicKeyToken=null1clr-namespace:StewLedger.ViewStewLedger.View  5   	   ;"clr-namespace:StewLedger.ViewModelStewLedger.ViewModel  5   	   (    "StewLedger.View.UserNewAccountView   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:StewLedger.View     5   	   1vm"clr-namespace:StewLedger.ViewModel     5   	     � Title$  New Account��5	   	   $	��300��6   $	��600��6*    � DataContext 5
      (   "StewLedger.ViewModel.UserAccountVM  5   
   .��5      �  � RowDefinitions 5   
   �� 5      $
��182* �6   �� 5      $	��87* �6    � ColumnDefinitions 5   
   �� 5      $
��103* �6   �� 5      $
��489* �6   .+�5   
   �� $
��Left=�6   $��21��6/   $��	32,46,0,0q�6;   $
��Wrap=�6N   $��
First Name��6b   $	��Top=�6t   $��77��6�   $��2��6�   �� 5   
   $
��Left=�6   $��21��6/   $��	32,80,0,0q�6;   $
��Wrap=�6N   $��	Last Name��6b   $	��Top=�6s   $��77��6�   $��2��6�   �� 5   
   $
��Left=�6   $��21��6/   $��
32,117,0,0q�6;   $
��Wrap=�6O   $��	User Name��6c   $	��Top=�6t   $��77��6�   $��2��6�   �� 5   
   $
��Left=�6   $��21��6/   $��
32,151,0,0q�6;   $
��Wrap=�6O   $��Password��6c   $	��Top=�6s   $��77��6�   $��2��6�   �� 5   
   -   5         ��Name$ tbFirstName��5      $��21��6(   $��
13,46,10,0q�64    �� TextWrapping$
 Wrap=�6H   $�� ��6\   $	��Top=�6d   $��1��6}   �� 5   
   -   5        $ 
tbLastName��5      $��21��6(   $��
13,79,10,0q�64   $
 Wrap=�6H   $�� ��6\   $	��Top=�6d   $��1��6}   �� 5   
   -   5        $ 
tbUserName��5      $��21��6(   $��13,117,10,0q�64   $
 Wrap=�6I   $�� ��6]   $	��Top=�6e   $��1��6~   G� 5   
   -   5         G�Name$ PasswordBox��5      $��21��6-   $��13,151,10,0q�69   $	��Top=�6N   $��1��6g   �� 5   
   -   5         ��Name$ btnSave��5      $��Create Account��6"   $
��Left=�6<   $��38��6W   $��	13,11,0,0q�6c   $	��Top=�6v   $��83��6�   $��1��6�   $��1��6�   $��11(�6�   �� 5   
   -   5        $ btnClear��5      $��Clear��6#   $
��Left=�63   $��38��6N   $��
117,11,0,0q�6Z   $	��Top=�6n   $��83��6�   $��1��6�   $��1��6�   6�   �� 5    
   -   5        $ btnExit��5       $
��Exit��6"   $
��Left=�61   $��38��6L   $��
226,11,0,0q�6X   $	��Top=�6l   $��83��6�   $��1��6�   $��1��6�   6�   5"      5#      