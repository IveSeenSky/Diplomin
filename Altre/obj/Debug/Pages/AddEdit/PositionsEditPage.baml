   M S B A M L   `   `   `  ����@  <Altre, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Altre.Pages.AddEditAltre.Pages.AddEdit  5      +    %Altre.Pages.AddEdit.PositionsEditPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:Altre.Pages.AddEdit     5        N� Title$  PositionsEditPage��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5       � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��* �6   �� 5      $��40 �6   .+�5   
   �� $��Название��6   $��2��5         TextBlockStyle#��[  5      �� 5   
   -   5         ��Name$ LoginBx��5#      $��1��5      $��2��5       $��5q�5!      $	��400��5"      $
��Left=�5$      ��5%      �� *position_name+�� 5'   
   $��Описание��6   $��3��5(      #��[  5)      �� 5+   
   -   5        $ PassBx��5.      $��1��5+      $��3��5,      $��5q�5-      $	��400��5/      $
��Left=�50      ��51      �� *job_description+�� 53   
   $��1��6   $��
Отдел��54      #��[  55      �� 57   
   -   5         ��Name$ DepBx��58      $��1��57      $��1��59      $��3q�5:      $	��400��5;      $
��Left=�5<       �� DisplayMemberPath$ department_name��5>       �� SelectedItem 5=      �� *Departments+5>      �� 5@   
   -   5         ��Name$ SaveBtn��5@      $	��100��5A      $��Сохранить��5B      $	��180��5C      $��30��5D        ButtonStyleDefault#��[ 5E      5F      �� 5H   
   $��Мин З/П��6   $��4��5I      #��[  5J      �� 5L   
   $��1��6   $��4��5M      $��5q�5N      $	��400��5O      $
��Left=�5P      ��5Q      �� *salary_range_min+�� 5S   
   $��Макс З/П��6   $��5��5T      #��[  5U      �� 5W   
   $��1��6   $��5��5X      $��5q�5Y      $	��400��5Z      $
��Left=�5[      ��5\      �� *salary_range_max+5]      5^      