   M S B A M L   `   `   `  ����I  EMahApps.Metro, Version=1.1.3.28, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls  5   	    	ClientWPF   ClientWPF.MainWindow   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   NControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  BabyGuard Baby Monitor��5   	   $	��588��6(   $	��724��65    � WindowStartupLocation$ CenterScreen=�5   	   $	��723��6.   $	��588��6=   (   "MahApps.Metro.Controls.MetroWindow   RightWindowCommands 5      +   %MahApps.Metro.Controls.WindowCommands  5	   
   .@�5
      �� $��	Go Online��6   �� 5      .��5      ��  �� Orientation$ 
Horizontal=�6   .+�5      �� $��4 0 0 0q�6    $��Center=�5      $��Register��5      5      5      5      .��5      �  � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��* �6   �� 5      $��50 �6   �� 5      $��50 �6   .+�5   
   �� -   5        $��	LogoImage��5      $
��Left=�6!   $-��'Images/Logo/BabyGuardsidewardssmall.png��6<   $��10, 0, 0, 0q�6m   6�   �� 5   
   -   5        $��UserName��5      $��0��6$   $��Right=�61   $��Center=�6M   $��15(�6h   $��10, 0, 10, 0q�6v   6�   (� 5   
   $��1��6    (� Stretch$ Uniform=�6   $
��Left=�61   $��10, 10, 0, 10q�6L   .��5      �� -   5        $��Image��5      $	��640��6!   $	��480��6-   5       � 5!   
   $��1��6   $��Right=�6   $	��170��68    5"      �� 5#      $��50 �6    �� 5$      $��* �6    .+�5&      �� $��0��6   $��30(�6!   .��6/   Alerts67   �� 5'      -   5        $��	AlertList��5'      $��1��6'   $��Right=�64   $��0,0,10,0q�6P    � 	Resources 5(       �� 
TargetType �� Property	 �� Value��%[   '��       �� 5)      " ��6   .�5*      ��  � ��$	 0, 10, 0, 0q�64   5+      5,      ��5-      �� 5.      .R�5/      � $
��Left=�6   
 � ColumnDefinitions
 50      �� 51   "   $��50 �63   6@   �� 52   "   $��60 �63   6@   �� 53   "   $��50 �63   6@   .+�55      �� $��0��6(   $��20(�68   ��6F   ��  �� Path$ Temperature��� 56      $��1��6(   $��Center=�68   ��6S   �� $
 Type��� 57      $��2��6(   $��Center=�68   ��6S   �� $
 Time�58      59      5;      5<      �� 5?   
   -   5        $��PercentLabel��5?      $��2��6(   $
��Left=�65   $��Center=�6P   $��25(�6k   $��10, 0, 0, 0q�6y   6�   �� 5@   
   -   5        $��	TempLabel��5@      $��2��6%   $��Right=�62   $��Center=�6N   $��25(�6i   $��0, 0, 10, 0q�6w   Temperature:6�   �� 5A   
   -   5        $��Activate��5A      $��3��6!   $
��Left=�6.   $	��100��6I   $��30��6U   $��10, 0, 0, 0q�6a   
����ح�
�������.��6�   Start6�   �� 5B   
   -   5        $��SignOut��5B      $��3��6    $��Right=�6-   $	��100��6I   $��30��6U   $��0, 0, 10, 0q�6a   .��6�   
Sign Out6�   5C      5E      