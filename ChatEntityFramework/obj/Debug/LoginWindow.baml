   M S B A M L   `   `   `  ����N  JChatEntityFramework, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:ChatEntityFrameworkChatEntityFramework  5   	   %    ChatEntityFramework.LoginWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   3local!clr-namespace:ChatEntityFramework     5   	     � Title$  LoginWindow��5   	   $	��450��6   $	��800��6*   .��5	      � ��5
   
   �� 5       �� ImageSource$ /mainMenu_Wallpaper.jpg��6   .+�5   
   �� -   5         ��Name$ 	LoginForm��5      $	��400��6(   $��Center=�64   $	��300��6Q   $��
0,124,0,10q�6^   .+�5      �� -   5         ��Name$ username��5   )   
�������$��35q�6;    �� TextWrapping$
 Wrap=�6G   $��25��6[   $	��140��6g      UsernameBox#��[  6s   6�   G� 5      -   5         G�Name$ password��5   -   
�������$��28��6?   $	��140��6K    G� 	MaxLength$ 25��6W    G� PasswordChar$ *��6f   
������ 
�������$��Center=�6�    � 	Resources 5      	 �� 
TargetType
 �� Property �� Value��%^   '��       �� 5      "	 ��6   .�5      �� 
 �@��CornerRadius$ 5��69   5      5      5      �� 5      
������ $��10q�5       �� CornerRadius$ 5��5      5      �� 5      -   5        ���#292B2F
�������$
��Show��5   =   $��52��6L    J� RenderTransformOrigin$ 
0.49,1.418&�6W    5      ��%^   '��       �� 5      "	 ��6   .�5      �� 
 �@��CornerRadius$ 5��69   5      5      5       �� 5!      -   5         ��Name$ SubmitCredential��5!   =   ���#292B2F
�������$��40q�6W   $��20��6c   $��60��6o   $��Login��6�    5"      ��%^   '��       �� 5#      "	 ��6   .�5$      �� 
 �@��CornerRadius$ 5��69   5%      5&      5'      5(      �� 5)   
   $��Log into your account��6   $��Center=�60   $��0,37,0,0q�6M   $	��Top=�6_   $��Center=�6w   $��18(�6�   
�������$
��Bold"�6�   �� 5*   
   $��Password��6   $
��Left=�6#   $��257,217,0,0q�6>   $	��Top=�6S   $��Center=�6k   $��12(�6�   
�������$
��Bold"�6�   5+      5,      