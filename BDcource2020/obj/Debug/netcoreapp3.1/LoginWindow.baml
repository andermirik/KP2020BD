   M S B A M L   `   `   `  ����G  CBDcource2020, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:BDcource2020BDcource2020  5   	       BDcource2020.LoginWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:BDcource2020     5   	     � WindowStyle$
  None=�5   	   $��Century Gothic)�5	   	    � 
ResizeMode$ NoResize=�5
   	    � AllowsTransparency$
 True��5   	   $��18(�5   	    � WindowStartupLocation$ CenterScreen=�5   	   $	��512��5   	   $	��367��6   .��5      ]� $
��True��6   ���	#FFEDEFF9.+�5   
   � -   5        $��40��5      $	��Top=�6   ���	#FF001F89.+�5      �� -   5        $��btn_exit��5      $
��Left=�6%   $��	331,0,0,0q�6@   $��36��6S      MyButtonStyle#���   6^   ���	#00000000.��5      �� $��exit.png��6   5      �� 5      -   5        $��btn_minimize��5      $
��Left=�6)   $��	295,0,0,0q�6D   $��36��6W   #���   6b   ���	#00000000.��5      �� $��minimize.png��6    J� OpacityMask
 �   �6.   5      �� 5      $
��Left=�6   $��10,0,0,0q�63   $ ��Авторизуйтесь��6E   $
��Wrap=�6Z   $��Center=�6n   
�������6�   5      � 5   
   ��5      �� 5       �� ImageSource$ loginfon.jpg��6    j� TileMode$
 None=�68    j� Stretch$
 Fill=�6H    �� RelativeTransform 5      V� 5      .~�5      �� 	 �� CenterY$		 0.5Q�6-   
 �� CenterX$	
 0.5Q�6;   �� 5       �� CenterY$	 0.5Q�6,    �� CenterX$	 0.5Q�6:   �� 5        �� Angle$ 0Q�6.    �� CenterY$	 0.5Q�68    �� CenterX$	 0.5Q�6F   T� 5!      5"      5$      .+�5'      �� $��45,0,45,158q�6   ���	#CC2F3859 �� CornerRadius$ 12,12,12,12��6A   $��6q�6\    �� Padding$ 4q�6p   $	��156��6|   $��Bottom=�6�   5(      �� 5+      -   5         ��Name$ btn_authorization��5+      $"��Авторизоваться��60   $��Center=�6I   #���   6f   $��0,0,0,35q�6�   $	��245��6�   $��40��6�   $��Bottom=�6�   ���	#E5001F89
�������6�   �� 5,      -   5         ��Name$ 	txb_login��5,      $��Center=�6)   $��	0,0,0,244q�6F   $��admin��6Y    �� TextWrapping$ NoWrap=�6f   $��Center=�6|   $	��242��6�   $��30��6�   $��Bottom=�6�   G� 5-      -   5         G�Name$ txb_password��5-      $��Center=�60   $��	0,0,0,178q�6M    G� Password$ admin��6`   $��Center=�6q   $	��242��6�   $��30��6�   $��Bottom=�6�   �� 5.      $
��Left=�6   $��
62,0,0,279q�63   $%��Имя пользователя��6G   $
��Wrap=�6_   
�������$��22��6�   $��Bottom=�6�   �� 5/      $
��Left=�6   $��
62,0,0,213q�63   $��Пароль��6G   $
��Wrap=�6U   
�������$��22��6|   $��Bottom=�6�   50      52      56      