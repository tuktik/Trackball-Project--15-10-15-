   M S B A M L   `   `   `  ����  Trackball Project$    Trackball_Project.DriverWindow   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   T PMahApps.Metro, Version=1.1.2.0, Culture=neutral, PublicKeyToken=f4fb5a3c4d1e5b4f;Controls,http://metro.mahapps.com/winfx/xaml/controls  5   	     � Title$#  트랙볼 디바이스 설정��5   	   (  "MahApps.Metro.Controls.MetroWindow   TitlebarHeight$ 40��6      TitleForeground
 �����$	��400��5   	   $	��600��6   
������� � 
ResizeMode$ CanMinimize=�5   	    � WindowStartupLocation$ CenterScreen=�6"    J� OpacityMask
 �   � � Icon$ /Image/icon/Icon.png��6[      EnableDWMDropShadow . .��5	      � .+�5
   
   �� -   5        $��Driver_Icon��5
      $)��#/Image/Device Driver Background.png��6#   $��30��6P   $��30,10,0,330q�6[   $
��Left=�6p   �� 5   
   -   5        $��DeviceDriver_Page_Title��5      $	��250��63   $��30��6?   $��20(�6K   $��80, 0, 0, 320q�6Y   $
��Left=�6p   " 디바이스 드라이버 설정6�   �� 5   
   -   5        $��
DeviceList��5      $	��240��6$   $	��520��61   ���#5872A5$��2q�6S   $��
0, 0, 0, 0q�6g   6{   �� 5   
   -   5        $ ��DeviceDriverSetting_Button��5      $	��400��6T   $��30��6`   $��0, 300, 0, 0q�6l   ���#5872A5.��6�   해당 디바이스 선택6�   5      5      