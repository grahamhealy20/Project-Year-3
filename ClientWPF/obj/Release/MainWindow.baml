   M S B A M L   `   `   `  ���� I  EMahApps.Metro, Version=1.1.3.28, Culture=neutral, PublicKeyToken=nullV;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.MetroMahApps.Metro.Controls   	ClientWPF   ClientWPF.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    NControls;clr-namespace:MahApps.Metro.Controls;assembly=MahApps.Metro    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  BabyGuard Baby Monitor��$	��588��$	��724�� � WindowStartupLocation$ CenterScreen=�$	��723��$	��588��(   "MahApps.Metro.Controls.MetroWindow   RightWindowCommands +   %MahApps.Metro.Controls.WindowCommands  .@��� $��	Go Online���� .����  �� Orientation$ 
Horizontal=�.+��� $��4 0 0 0q�$��Center=�$��Register��.���  � RowDefinitions �� $��50 ��� $��* ��� $��50 ��� $��50 �.+��� -   $��	LogoImage��$
��Left=�$-��'Images/Logo/BabyGuardsidewardssmall.png��$��10, 0, 0, 0q��� -   $��UserName��$��0��$��Right=�$��Center=�$��15(�$��10, 0, 10, 0q�(� $��1�� (� Stretch$ Uniform=�$
��Left=�$��10, 10, 0, 10q�.���� -   $��Image��$	��640��$	��480��� $��1��$��Right=�$	��175�� �� $��50 ��� $��* �.+��� $��0��$��30(�.��Alerts�� -   $��	AlertList��$��1��$��Right=�$��0,0,10,0q� � 	Resources  �� 
TargetType �� Property	 �� Value��%6   '��       �� " ��.���  � ��$	 0, 10, 0, 0q����� .R�� $
��Left=�
 � ColumnDefinitions
 �� $��50 ��� $��60 ��� $��50 �.+��� $��0��$��20(�����  �� Path$ Temperature��� $��1��$��Center=����� $
 Type��� $��2��$��Center=����� $
 Time��� -   $��PercentLabel��$��2��$
��Left=�$��Center=�$��25(�$��10, 0, 0, 0q��� -   $��	TempLabel��$��2��$��Right=�$��Center=�$��25(�$��0, 0, 10, 0q�Temperature:�� -   $��Activate��$��3��$
��Left=�$	��100��$��30��$��10, 0, 0, 0q�
����ح�
�������.��Start�� -   $��SignOut��$��3��$��Right=�$	��100��$��30��$��0, 0, 10, 0q�.��
Sign Out�� -	   $��	InfoLabel��$��3��$
��Left=�$��	121,0,0,0q�$��Center=�