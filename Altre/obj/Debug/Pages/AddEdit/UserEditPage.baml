   M S B A M L   `   `   `  ����@  <Altre, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Altre.Pages.AddEditAltre.Pages.AddEdit  5      &     Altre.Pages.AddEdit.UserEditPage   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:Altre.Pages.AddEdit     5        N� Title$  UserEditPage��5	      .��5      �  � ColumnDefinitions 5   
   �� 5      $	��200 �6   �� 5       � RowDefinitions 5   
   �� 5      $��* �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��* �6   �� 5      $��40 �6   .+�5   
   �� $��
Логин��6   $��1��5         TextBlockStyle#��[  5      �� 5   
   -   5         ��Name$ LoginBx��5!      $��1��5      $��1��5      $��5q�5      $	��400��5       $
��Left=�5"      ��5#      �� *Users.username+�� 5%   
   $��Пароль��6   $��2��5&      #��[  5'      �� 5)   
   -   5        $ PassBx��5,      $��1��5)      $��2��5*      $��5q�5+      $	��400��5-      $
��Left=�5.      ��5/      �� *Users.password+�� 51   
   $��3��6   $��Работник��52      #��[  53      �� 55   
   -   5         ��Name$ 	EmplsCmbx��56      $��1��55      $��3��57      $��3q�58      $	��400��59      $
��Left=�5:       �� DisplayMemberPath$ 	full_name��5<       �� SelectedItem 5;      �� *
Employee+5<      �� 5>   
   -   5         ��Name$ SaveBtn��5>      $��5��5?      $��Сохранить��5@      $	��180��5A      $��30��5B        ButtonStyleDefault#��[ 5C      5D      5E      5F      