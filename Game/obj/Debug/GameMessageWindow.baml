   M S B A M L   `   `   `  ����?  ;Game, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:GameGame  5   	       Game.GameMessageWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   $localclr-namespace:Game     5   	     � Title$  GameMessageWindow��5   	   $��187.5��6#   $	��400��62    � 
ResizeMode$ NoResize=�6>    � WindowStartupLocation$ CenterScreen=�6T   .��5	      �    	mainStyle#���   6   .+�5
   
   �� -   5         ��Name$ messageText��5
      $��Center=�5      $
��Wrap=�5      $��	TextBlock��5      $��Center=�5      $��	0,-50,0,0q�6/   �� 5   
   $��Center=�6   $��Bottom=�5      $��0,0,0,20q�5       �� Orientation$ 
Horizontal=�5      .+�5      �� -   5         ��Name$ OkButton��5      $��OK��5      $��75��5      $
��10,0q�6    
 button#���  6*   $��5q�6K    �� 	IsDefault . 6n   �� 5      -   5        $ 	YesButton��5      $
��Да��5      $��75��5      $
��10,0q�6   #���  6*   $��5q�6K    . 6o   �� 5      -   5        $ NoButton��5      $��Нет��5      $��75��5      $
��10,0q�6   #���  6*   $��5q�6K    �� IsCancel . 6n   �� 5      -   5        $ CancelButton��5      $��Отмена��5      $��75��5      $
��10,0q�6   #���  6*   $��5q�6K    . 6r   5       5#      5$      