   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  G CACNL_Planner, Version=1.4.2.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Animal_Xing_PlannerAnimal_Xing_Planner "   Animal_Xing_Planner.NoticeUC   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     6sys&clr-namespace:System;assembly=mscorlib     P LElysium, Version=2.0.976.4, Culture=neutral, PublicKeyToken=afa220db249e5b15/metro#http://schemas.codeplex.com/elysium  7params*http://schemas.codeplex.com/elysium/params  -local!clr-namespace:Animal_Xing_Planner  -     >�Name$  notice��.��� $	��300��$	��300������  j� TileMode$
 Tile=� �� ImageSource$/ )/ACNL_Planner;component/Resources/bg2.png��.+��� -    ��Name$ typeTabControl��$��Right=�$	��242��$��	0,10,10,0q�$	��Top=�$	��280��
���   �.@��� -    ��Name$ meetingTabItem��$��Meeting��$��/resources/#FinkHeavy)���Z�  � 	Resources  �� 
TargetType �� Property �� Value	 �� 
TargetType
 ��Name �� Triggers P� Property P� Value �� 
TargetName 
  Accent��%�   '��       �� " ��.���  � �� �� "	 ��.R��� $

 root��$��13.67(�$��31��
�������.���� $��Header����.  P�  �@��
IsSelected . . ��� $
 root��# �    � ��.��� .+��� -   $
 villagerNameLabel��$��Villager Name:��$
��Left=�#���   $��	17,34,0,0q�$	��Top=�$��Arial)�$
��Bold"��� -   $
 	timeLabel��$��Time:��$
��Left=�$��	27,75,0,0q�#���   $	��Top=�$��59��$��Arial)�$
��Bold"��� -   $
 separatorLabel��$��:��$
��Left=�$��
134,74,0,0q�#���   $	��Top=�$��19��$��12(�$
��Bold"�$��Arial)��� -    ��Name$ hourComboBox��$
��Left=�$��	81,75,0,0q�$	��Top=�$��48�� � ToolTip$
 Hour��$��Arial)�$��11(��� -   $ minuteComboBox��$
��Left=�$��
153,75,0,0q�$	��Top=�$��48��$ Minute��$��Arial)�$��11(�	� -	    	�Name$ homeRadioButton��$��
Your House��$
��Left=�$��
36,119,0,0q�$	��Top=�$��Arial)�$��12(�	� -
   $ awayRadioButton��$��Their House��$
��Left=�$��158,119,0,0q�$	��Top=�$��Arial)�$��12(��� -   $ villagerNameComboBox��$
��Left=�$��
118,32,0,0q�$	��Top=�$	��120��$��Arial)�$��11(��� -   $ deliveryTabItem��$��Delivery��$��3,0,-7,0q�$��/resources/#FinkHeavy)���Z�  ��%�   '��       �� " ��.���  � �� �� "	 ��.R��� $

 root��$��13.67(�$��31��
�������.���� $��Header����.  P�  �@��
IsSelected . . ��� $
 root��# �    � ��.��� .+��� -   $
 villagerNameLabel0��$��Villager Name:��$
��Left=�#���   $��	17,34,0,0q�$	��Top=�$��Arial)�$
��Bold"��� -   $ villagerNameComboBox1��$
��Left=�$��
118,32,0,0q�$	��Top=�$	��120��$��Arial)�$��11(��� -   $
 	itemLabel��$��Item:��$
��Left=�$��	27,75,0,0q�#���   $	��Top=�$��59��$��Arial)�$
��Bold"��� -    ��Name$ itemTextBox��$
��Left=�$��23��$��	86,73,0,0q� �� TextWrapping$
 Wrap=�$	��Top=�$	��120��$��Arial)��� -   $
 optionalLabel��$��Optional��$
��Left=�#���   $��
17,124,0,0q�$	��Top=�$��83��$��14(�$��Italic$�$��Arial)��� -   $
 
timeLabel2��$��Time:��$
��Left=�#���   $��
26,167,0,0q�$	��Top=�$��59��$��12(�$
��Bold"�$��Arial)��� -   $
 separatorLabel2��$��:��$
��Left=�#���   $��138,167,0,0q�$	��Top=�$��19��$��12(�$
��Bold"�$��Arial)��� -   $ hourComboBox2��$
��Left=�$��
85,167,0,0q�$	��Top=�$��48��$
 Hour��$��Arial)�$��11(��� -   $ minuteComboBox2��$
��Left=�$��157,167,0,0q�$	��Top=�$��48��$ Minute��$��Arial)�$��11(��� -   $ eventTabItem��$��Event��$��10(�$��8,0,-8,0q�$��/resources/#FinkHeavy)���Z�  ��%�   '��       �� " ��.���  � �� �� "	 ��.R��� $

 root��$��13.67(�$��31��
�������.���� $��Header����.  P�  �@��
IsSelected . . ��� $
 root��# �    � ��.��� .+��� -   $
 	dateLabel��$��Date:��$
��Left=�#���   $��	19,41,0,0q�$	��Top=�$��49��$��12(�$
��Bold"�$��Arial)�(  "System.Windows.Controls.DatePicker  -     Name$ 
datePicker��
 �� Now   SelectedDate# Z $
��Left=�$��	83,41,0,0q�$	��Top=�   FirstDayOfWeek$ Monday=�$	��141��   DisplayDate# Z $��Arial)� :  4System.Windows.Controls.Primitives.DatePickerTextBox��%6   '        �� "  .���  �@��
IsReadOnly . �� -   $
 
nameLabel1��$��Name:��$
��Left=�#���   $��	19,10,0,0q�$	��Top=�$��49��$��12(�$
��Bold"�$��Arial)��� -   $ nameTextBox��$
��Left=�$��23��$��	83,10,0,0q�$
 Wrap=�$	��Top=�$	��120�� J� 	AllowDrop .  $��Arial)��� -   $
 descriptionLabel��$��Description:��$
��Left=�#���   $��
10,153,0,0q�$	��Top=�$��78��$��12(�$
��Bold"�$��Arial)��� -   $
 optionalLabel2��$��Optional��$
��Left=�#���   $��
10,119,0,0q�$	��Top=�$��83��$��14(�$��Italic$�$��Arial)��� -   $ descriptionTextBox��$
��Left=�$��61��$��
93,132,0,0q�$
 Wrap=�$	��Top=�$	��161��$��Arial)��� -   $
 
timeLabel1��$��Time:��$
��Left=�#���   $��	19,78,0,0q�$	��Top=�$��59��$��12(�$
��Bold"�$��Arial)��� -   $
 separatorLabel1��$��:��$
��Left=�#���   $��
136,77,0,0q�$	��Top=�$��19��$��12(�$
��Bold"�$��Arial)��� -    $ hourComboBox1��$
��Left=�$��	83,78,0,0q�$	��Top=�$��48��$
 Hour��$��Arial)�$��11(��� -!   $ minuteComboBox1��$
��Left=�$��
155,78,0,0q�$	��Top=�$��48��$ Minute��$��Arial)�$��11(��� -"    ��Name$ cancelButton��$��Cancel��$
��Left=�$��28��$	��Top=�$	��107��$��
10,262,0,0q�$��Arial)�$
��Bold"� �� IsCancel . $��12(��� -#   $ 	addButton��$	��Add��$
��Left=�$��183,262,0,0q�$	��Top=�$	��107��$��28����. $��Arial)�$
��Bold"� �� 	IsDefault . $��12(�