   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5       ACNL_Planner"   Animal_Xing_Planner.NoticeUC   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      6sys&clr-namespace:System;assembly=mscorlib     5      -   5          >�Name$  NoticeUc��5      .��5
      � $	��300��6   $	��300��6   ��5   
   �� 5       j� TileMode$
 Tile=�6    �� ImageSource$2 ,/ACNL_Planner;component/Resources/bg/bg2.png��6)   .+�5   
   �� -   5         ��Name$ TypeTabControl��5      $��Right=�6-   $	��242��6I   $��	0,10,10,0q�6V   $	��Top=�6i   $	��280��6�   
���   �.@�5      �� -   5         ��Name$ MeetingTabItem��5      $��Meeting��6.   $!��/resources/fonts/#FinkHeavy)�6U   ��6?   Z�  � 	Resources 5       �� 
TargetType �� Property �� Value	 �� 
TargetType
 �� Orientation �� Color �� Opacity ��Name �� Triggers P� Property P� Value �� 
TargetName 
  Accent��%m  '��       �� 5      " ��6   .�5      ��  � �� 5      �� 5   "   "	 ��62   .R�5   &   �� $
 
Horizontal=�61   ��5   *   �� 5   .   $ 	#FFF15941��6>   $	 0.1Q�6P   .+�5   *   �� $9��3/ACNL_Planner;component/Resources/notice/coffee.png��60   $��20��6m   $��20��6x   $��Center=�6�   $��Center=�6�   �� 5   *   $
 root��60   $��13.67(�6>   $��31��6O   
�������.��5   .   �� $��Header��6?   ��. 6V   5   +   5   '    5   &   P� 5    *    �@��
IsSelected . . �5!   .   �� $
 root��65   # �   6]    � ��5"   +   5$   #   5&      5'      5(      .��5)      � $��	-8,0,-7,0q�6   ��5*      �� 5+      $ 	#FFF15941��6*   $	 0.1Q�6<   .+�5-      �� -   5        $ MVillagerNameLabel��5-      $��Villager Name:��68   $
��Left=�6Q   #���   6l   $��	17,34,0,0q�6�   $	��Top=�6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   �� 5.      -   5         ��Name$ MVillagerNameComboBox��5.      $
��Left=�6>   $��
109,33,0,0q�6Y   $	��Top=�6m   $	��120��6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   $��11(�6�   ���	#FF383838$��0��6�   �� 5/      -   5        $ 
MTimeLabel��5/      $��Time:��60   $
��Left=�6@   $��	27,75,0,0q�6[   #���   6n   $	��Top=�6�   $��59��6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   �� 50      -   5        $ MHourComboBox��50      $
��Left=�66   $��	79,73,0,0q�6Q   $	��Top=�6d   $��48��6|    � ToolTip$
 Hour��6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   $��11(�6�   ���	#FF383838$��1��6�   �� 51      -   5        $ MSeparatorLabel��51      $��:��65   $
��Left=�6A   $��
134,74,0,0q�6\   #���   6p   $	��Top=�6�   $��19��6�   $��12(�6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   �� 52      -	   5        $ MMinuteComboBox��52      $
��Left=�68   $��
153,73,0,0q�6S   $	��Top=�6g   $��48��6   $ Minute��6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   $��11(�6�   ���	#FF383838$��2��6�   	� 53      -
   5         	�Name$ HomeRadioButton��53   "   $��
Your House��6;   $
��Left=�6P   $��
38,121,0,0q�6k   $	��Top=�6   #���   6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   $��12(�6�   $��3��6   	� 54      -   5        $ AwayRadioButton��54   "   $��Their House��6;   $
��Left=�6Q   $��150,121,0,0q�6l   $	��Top=�6�   #���   6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   $��12(�6�   $��4��6  55      56      �� 5:      -   5        $ DeliveryTabItem��5:      $��Delivery��6/   $��3,0,-7,0q�6A   $!��/resources/fonts/#FinkHeavy)�6i   ��6S   Z�  5;      ��%n  '��       �� 5<      " ��6   .�5=      ��  � �� 5>      �� 5?   "   "	 ��62   .R�5@   &   �� $
 
Horizontal=�61   ��5A   *   �� 5B   .   $ 	#FF55A0DE��6>   $	 0.1Q�6P   .+�5D   *   �� $:��4/ACNL_Planner;component/Resources/notice/present.png��60   $��20��6n   $��20��6y   $��Center=�6�   $��Center=�6�   �� 5E   *   $
 root��60   $��13.67(�6>   $��31��6O   
�������.��5F   .   �� $��Header��6?   ��. 6V   5G   +   5H   '    5I   &   P� 5J   *    �@��
IsSelected . . �5K   .   �� $
 root��65   # �   6]    � ��5L   +   5N   #   5P      5Q      5R      .��5S      � $��	-8,0,-7,0q�6   ��5T      �� 5U      $ 	#FF55A0DE��6*   $	 0.1Q�6<   .+�5W      �� -   5        $ DVillagerNameLabel��5W      $��Villager Name:��68   $
��Left=�6Q   #���   6l   $��	17,34,0,0q�6�   $	��Top=�6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   �� 5X      -   5        $ DVillagerNameComboBox��5X      $
��Left=�6>   $��
109,33,0,0q�6Y   $	��Top=�6m   $	��120��6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   $��11(�6�   ���	#FF383838$��5��6�   �� 5Y      -   5        $ 	ItemLabel��5Y      $��Item:��6/   $
��Left=�6?   $��	57,75,0,0q�6Z   #���   6m   $	��Top=�6�   $��40��6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   �� 5Z      -   5         ��Name$ ItemTextBox��5Z      $
��Left=�63    �� 	MaxLength$ 13��6N   $��23��6]   $��
109,75,0,0q�6i    �� TextWrapping$
 Wrap=�6}   $	��Top=�6�   $	��120��6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   ���	#FF383838$��6��6  �� 5[      -   5         ��Name$ 	DGroupBox��5[      $��Optional��62   $��17,121,18,10q�6D   $��Italic$�6Z   $D��>/ACNL_Planner;component/Resources/fonts/#SecondaRoundW00-Heavy)�6m   $��10(�6�   ���	#FF3838386�   �� 5\      -   5        $ 
DTimeLabel��5\      $��Time:��60   $
��Left=�6@   #���   6[   $��
42,158,0,0q�6�   $	��Top=�6�   $��44��6�   $��12(�6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   �� 5]      -   5        $ DHourComboBox��5]      $
��Left=�66   $��
94,156,0,0q�6Q   $	��Top=�6e   $��48��6}   $
 Hour��6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   $��11(�6�   ���	#FF383838$��7��6�   �� 5^      -   5        $ DSeparatorLabel��5^      $��:��65   $
��Left=�6A   #���   6\   $��147,158,0,0q�6�   $	��Top=�6�   $��19��6�   $��12(�6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   �� 5_      -   5        $ DMinuteComboBox��5_      $
��Left=�68   $��166,156,0,0q�6S   $	��Top=�6h   $��48��6�   $ Minute��6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   $��11(�6�   ���	#FF383838$��8��6�   5`      5a      �� 5e      -   5        $ EventTabItem��5e      $��Event��6,   $��10(�6;   $��	8,0,-31,0q�6I   $!��/resources/fonts/#FinkHeavy)�6r   $
��Left=�6�   $��67��6�   ��6\   Z�  5f      ��%n  '��       �� 5g      " ��6   .�5h      ��  � �� 5i      �� 5j   "   "	 ��62   .R�5k   &   �� $
 
Horizontal=�61   ��5l   *   �� 5m   .   $ 	#FF09BD09��6>   $	 0.1Q�6P   .+�5o   *   �� $:��4/ACNL_Planner;component/Resources/notice/cracker.png��60   $��20��6n   $��20��6y   $��Center=�6�   $��Center=�6�   �� 5p   *   $
 root��60   $��13.67(�6>   $��31��6O   
�������.��5q   .   �� $��Header��6?   ��. 6V   5r   +   5s   '    5t   &   P� 5u   *    �@��
IsSelected . . �5v   .   �� $
 root��65   # �   6]    � ��5w   +   5y   #   5{      5|      5}      .��5~      � $��	-8,0,-7,0q�6   ��5      �� 5�      $ 	#FF09BD09��6*   $	 0.1Q�6<   .+�5�      �� -   5        $ 	DateLabel��5�      $��Date:��6/   $
��Left=�6?   #���   6Z   $��	37,43,0,0q�6�   $	��Top=�6�   $��41��6�   $��12(�6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   (  "System.Windows.Controls.DatePicker  5�      -   5          Name$ 
DatePicker��5�   L   
 �� Now   SelectedDate# Z 6!   $
��Left=�6`   $��	83,44,0,0q�6{   $	��Top=�6�      FirstDayOfWeek$ Monday=�6�   $	��141��6�      DisplayDate# Z 6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   ���	#FF383838$��10��6@   5�      :  4System.Windows.Controls.Primitives.DatePickerTextBox��%[   '        �� 5�      "  6$   .�5�   "   ��  �@��
IsReadOnly . 6?   5�      5�      5�      �� 5�      -   5        $ 
ENameLabel��5�      $��Name:��60   $
��Left=�6@   #���   6[   $��	29,10,0,0q�6�   $	��Top=�6�   $��49��6�   $��12(�6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   �� 5�      -   5        $ NameTextBox��5�      $
��Left=�63   $ 13��6N   $��23��6]   $��	83,10,0,0q�6i   $
 Wrap=�6|   $	��Top=�6�   $	��141��6�    J� 	AllowDrop .  $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   ���	#FF383838$��9��6  �� 5�      -   5        $ DescriptionLabel��5�      $��Description:��66   $
��Left=�6N   #���   6i   $��
15,152,0,0q�6�   $	��Top=�6�   $��78��6�   $��12(�6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   �� 5�      -   5        $ 	EGroupBox��5�      $��Optional��62   $��10,121,10,10q�6D   $��Italic$�6Z   $D��>/ACNL_Planner;component/Resources/fonts/#SecondaRoundW00-Heavy)�6m   ���	#FF3838386�   �� 5�      -   5        $ DescriptionTextBox��5�      $
��Left=�6:   $ 30��6U   $��53��6d   $��
98,137,0,0q�6p   $
 Wrap=�6�   $	��Top=�6�   $	��153��6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   ���	#FF383838$��13��6  �� 5�      -   5        $ 
ETimeLabel��5�      $��Time:��60   $
��Left=�6@   #���   6[   $��	36,77,0,0q�6�   $	��Top=�6�   $��46��6�   $��12(�6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   �� 5�      -   5        $ ESeparatorLabel��5�      $��:��65   $
��Left=�6A   #���   6\   $��
136,77,0,0q�6�   $	��Top=�6�   $��19��6�   $��12(�6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   �� 5�      -    5        $ EHourComboBox��5�      $
��Left=�66   $��	83,77,0,0q�6Q   $	��Top=�6d   $��48��6|   $
 Hour��6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   ���	#FF383838$��11(�6�   $��11��6�   �� 5�      -!   5        $ EMinuteComboBox��5�      $
��Left=�68   $��
155,77,0,0q�6S   $	��Top=�6g   $��48��6   $ Minute��6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   ���	#FF383838$��11(�6�   $��12��6�   5�      5�      5�      �� 5�   
   -"   5         ��Name$ CancelButton��5�      $��Cancel��6'   $
��Left=�68   $��28��6S   $	��Top=�6_   $	��107��6w   $��
10,262,0,0q�6�   $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   $
��Bold"�6�     �� IsCancel  . $��12(�6	  $��14��6  �� 5�   
   -#   5        $ 	AddButton��5�      $	��Add��6$   $
��Left=�62   $��183,262,0,0q�6M   $	��Top=�6b   $	��107��6z   $��28��6�   ��. $-��'/resources/fonts/#SecondaRoundW00-Heavy)�6�   $
��Bold"�6�   ! �� 	IsDefault! . $��12(�6  $��15��6!  5�      5�      