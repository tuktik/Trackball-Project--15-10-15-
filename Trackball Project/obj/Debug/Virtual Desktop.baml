   M S B A M L   `   `   `  ����  Trackball Project%    virtual_desktop.Virtual_Desktop   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   -   5          �Name$  virtual_window��5   	    � Title$ Virtual_Desktop��5   	   $	��350��6!   $	��525��6.    � 
ResizeMode$ CanMinimize=�6:   
������� � WindowStyle$
 None=�5   3    � AllowsTransparency .  � 	Resources 5         move_preview_Storyboard �� TargetProperty �� 
TargetName9  3System.Windows.Media.Animation.EasingDoubleKeyFrame O� KeyTime	 O� Value��%�   &	        �� 5   
   .��5      U� $U O(UIElement.RenderTransform).(TransformGroup.Children)[3].(TranslateTransform.X)�6,   $ preview_grid��6�   .\�5        $ 0:0:1��6'   $
	 -558Q�67   5      5      5      ��5      �� 5   
   
 �� Color$
 	#FFAAAAAA��6    �� Opacity$	 0.9Q�6,   .��5      � -   5         �Name$ virtual_grid��5       � RowDefinitions 5   
   �� 5      $��4* �6   �� 5      .+�5   
   �� -   5         ��Name$ preview_canvas��5      }�. .+�5      � -   5        $ preview_grid��5       J� RenderTransformOrigin$ 0.5,0.5&�6)   z�5      V� 5       .~�5!      �� �� 5"      �� 5#      T� 5$      5%       � ColumnDefinitions 5'      �� 5(      �� 5)      �� 5*      �� 5+      .+�5-      �� -   5         ��Name$ preview_image1��5-      $
��Fill=�60   �� 5.      -   5        $ preview_image2��5.      $��1��60   $
��Fill=�6@   �� 5/      -   5        $ preview_image3��5/      $��2��60   $
��Fill=�6@   �� 50      -   5        $ preview_image4��50      $��3��60   $
��Fill=�6@   51      52      � 53   
   $��1��6   $��
60,0,60,20q�6    54      �� 55      �� 56      �� 57      �� 58      .+�5:      �� -	   5        $ Image1��5:      $��20q�6$   |�. 6I   �� 5;      -
   5        $ Image2��5;      $��20q�6$   $��1��60   |�. 6Z   �� 5<      -   5        $ Image3��5<      $��20q�6$   $��2��60   |�. 6Z   �� 5=      -   5        $ Image4��5=      $��20q�6$   $��3��60   |�. 6Z   5>      5?      5@      