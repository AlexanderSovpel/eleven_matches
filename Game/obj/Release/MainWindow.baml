   M S B A M L   `   `   `  ���� ?  ;Game, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:GameProjectGameProject      GameProject.MainWindow   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     +localclr-namespace:GameProject     -     �Name$
  Main�� � Title$ The Eleven Game�� � WindowStartupLocation$ CenterScreen=� � 	Resources    	aboutPage �� Color �� Opacity  headingPause  	gameRules 
 button  endGamePage��%j  &	        7  7  7  &	 �    7  7  � $��	aboutPage��$��3��$��2������ $ Black��$	 0.7Q�.+��� $��Center=�$��Center=�.+��� 8��  %#одиннадцать спичек�� 8�� ��Двое игроков по очереди берут одну, две или три спички. Проигрывает тот, кто забирает последнюю!�� -   $	��150��$	��100��8�� .��Играть� $��endGamePage��$��3��$��2������ $ Black��$	 0.7Q�.+��� $��Center=�$��Center=�.+��� $��winner��8��  �� -   $	��150��$	��100��8�� .��Заново.��� -   $��mainGrid��  	mainStyle#��[ .+�� -   $��	container�� � ColumnDefinitions �� $	��200 ��� �� $	��200 � � RowDefinitions �� $
��Auto ��� .+��� -   $��
player1Log��$��0��$��1��.+���  �� Orientation$ 
Horizontal=�.+��� -   $��player1�� 
 texBox#��[ $��
20,20,0,20q�$
��Left=�$	��130���� -   $��player1Save��  mark#��[ $��Center=�$��	15,0,20,0q�$y�Hidden=�� $��20, 20q� �� $
��Auto ���  �� �� .+��� $��0��$��0��$��0, 10q�побед:�� -	   $��
player1Win��$��0��$��1��$��0, 10q�	 �� TextAlignment$	 Right=��� $��1��$��0��$��0, 10q�поражений:�� -
   $��player1Loose��$��1��$��1��$��0, 10q�$	 Right=��� -   $��
player2Log��$��2��$��1��.+��� $ 
Horizontal=�.+��� -   $��player2Save��#��[ $��Center=�$��	20,0,15,0q�$y�Hidden=��� -   $��player2��#��[ $��
0,20,20,20q�$��Right=�$	��130��� $��20, 20q� �� $
��Auto ���  �� �� .+��� $��0��$��0��$��0, 10q�побед:�� -   $��
player2Win��$��0��$��1��$��0, 10q�$	 Right=��� $��1��$��0��$��0, 10q�поражений:�� -   $��player2Loose��$��1��$��1��$��0, 10q�$	 Right=��� -   $��	gameField��$��1��$��1��
�������.+��� -   $��	gameState��$	��100��$	��200��$	 Center=�
 �� Padding$

 0,20q��� -   $��matchesTaken��$	��100��$	��200��$��0,80,0,0q�����  �� ElementName$ matchesList�� �� Path$ SelectedItems.Count��� -   $��matches��$ 
Horizontal=�$��Center=��� -   $��makeTurn��$��Сделать ход��#��[ $	��100��$	��200��$��Center=�$��	0,100,0,0q�o� $��0��$��3��#��[ $��0,0,0,1q�$��30��$��Center=����� # � ������ # � ��.@�m� $��Игра��$��10,0,0,0q�.@�m� $��
Новая�� m� Command$ ApplicationCommands.New��m� $��Открыть��$ ApplicationCommands.Open��m� $��Сохранить��$ ApplicationCommands.Save���� m� $��Закончить��$ ApplicationCommands.Close��m� -   $��Настройки��$��30��$��	10,0,10,0q�m� $��Об игре��$ ApplicationCommands.Help��$��30��