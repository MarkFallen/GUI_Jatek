   M S B A M L   `   `   `  ����D  @LifeAtNik, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:LifeAtNik	LifeAtNik  5   	   9!clr-namespace:LifeAtNik.RenderersLifeAtNik.Renderers  5   	   3clr-namespace:LifeAtNik.LogicsLifeAtNik.Logics  5   	       LifeAtNik.MainWindow   5   	   \ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:LifeAtNik     5   	   6renderer!clr-namespace:LifeAtNik.Renderers     5   	   0logicclr-namespace:LifeAtNik.Logics     5   	   -   5          � Title$  
MainWindow��5
   	   $	��900��6   $
��1600��6)    � WindowState$ 	Maximized=�6�    � DataContext 5          LifeAtNik.Logics.GameLogic  5   
   .��5      � -   5         �Name$
 grid��5       � ColumnDefinitions 5   
   �� 5      $��1* �6   �� 5      $��2* �6   �� 5      $��2* �6   �� 5      $��2* �6    � RowDefinitions 5   
   �� 5      $��1* �6   �� 5      $	��21* �6   .+�5   
   �� $��0��6_   $��0��6o   
������� �� ContentStringFormat$$ Kérdések megválaszolva: {0}��6�   ��6   �� *
Answered+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�6�   � 5   
   -   5        	 �Name$	 pb_tudas��5      $��1��6(   $��0��68   
 � Minimum$
 0Q�6E    � Maximum$	 100Q�6Q   
�������$��3��6�    � Value 6_   �� *Tudas+$ TwoWay=�$ PropertyChanged=�6�   !   LifeAtNik.Renderers.Display  5   
   -   5          Name$ display��5      $��4��6,   $��0,0,0,0q�6@   $��1��6Q   5       5!      