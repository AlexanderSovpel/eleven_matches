   M S B A M L   `   `   `  ���� ?  ;Game, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GameGame      Game.SettingsWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     $localclr-namespace:Game       � Title$  Settings��$	��300��$	��330�� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=�.���    	mainStyle#��[   � ColumnDefinitions �� ��  � RowDefinitions �� $��55��$
��Auto ��� $��55��$
��Auto ��� $��55��$
��Auto ��� $��55��$
��Auto ��� $��55��$
��Auto �.+���   heading#��[ $��0��$��2��$��настройки���� $��1��$��0��$��20,0,0,0q�$��Всего спичек:��$��Center=��� -   $��maxMatchesCountTextBox��$��1��$��1�� 
 texBox#��[ �� $��2��$��0��$��20,0,0,0q�$��Можно взять:��$��Center=��� -   $��maxSelectTextBox��$��2��$��1��#��[ �� -   $��error��$��3��$��2��  	errorText#��[ �� -   $��4��$��0��$��отменить��$��	20,0,10,0q� 
 button#��[ �� -   $��
saveButton��$��4��$��1��$��сохранить��$��	10,0,20,0q�#��[ 