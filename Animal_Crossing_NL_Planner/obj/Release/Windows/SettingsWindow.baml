   M S B A M L   `   `   `  ���� G  CACNL_Planner, Version=1.4.2.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Animal_Xing_PlannerAnimal_Xing_Planner  (    "Animal_Xing_Planner.SettingsWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     P LElysium, Version=2.0.976.4, Culture=neutral, PublicKeyToken=afa220db249e5b15/metro#http://schemas.codeplex.com/elysium  7params*http://schemas.codeplex.com/elysium/params  -local!clr-namespace:Animal_Xing_Planner  -     � Title$  Settings��$	��280��$	��340�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterOwner=� � ShowInTaskbar .    Elysium.Controls.Window   IsMainWindow .  � 	Resources    CustomHeaderStyle �� 
TargetType �� Property �� Value	 �� 
TargetType
 ��Name �� Padding �� Triggers P� Property P� Value  ListBoxItemStyle  ItemsPanelTemplate1 K� IsItemsHost��%�  &	        &	 w    &	 �    �� " ��.���  � ��
 �������  � �� �	#FF07034B��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � �� �� "	 ��.R��� $��0,0,1,0q�
�������
�������._��� $
 ContentHeader��#��z��$ 5,5,5,0q�$
��Left=�#��z��$
��Bold"� P�  �@J�IsMouseOver . . ���  � ��
 ���� ��  � �� �	#FFE6D27A�� " ��.���  � ��$ Stretch=���  � ��$ Stretch=��� .R�$�  . $��Stretch=�.��� $
��Left=�$	��330������  j� TileMode$
 Tile=� �� ImageSource$/ )/ACNL_Planner;component/Resources/bg2.png�� j� Stretch$ UniformToFill=�.+��� -    ��Name$ settingsTabControl��$	��184��$��
10,10,10,0q�$	��Top=� �� TabStripPlacement$	 Top=�
���   �.@��� -    ��Name$ generalTabItem��$��General��$��0,0,-3,0q�$��/resources/#FinkHeavy)���Z� ��Z�   ��Name �� Triggers �� 
TargetName 
 Accent��%�   '��       �� " ��.���  � �� �� "	 ��.R��� $
 root��$��16.67(�$��31��
�������.���� $��Header����.  P�  �@��
IsSelected . . ��� $
 root��# �   � ��.��� .+��� -    ��Name$ updateButton��$��Check for Update��$
��Left=�$��
90,100,0,0q�$	��Top=�$	��125��$��30��$��Arial)�$
��Bold"�$��12(��� -    ��Name$ soundSlider��$
��Left=�$	��Top=�$��46��$��33�� � SmallChange$ 1Q� � Maximum$ 1Q�$��	72,38,0,0q� J� RenderTransformOrigin$ 
0.919,0.97&��� -   $ 
soundLabel��$��Sound:��$
��Left=�$	��Top=�$��52��$��12(�$
��Bold"�#���  $��Arial)�$��	15,38,0,0q�$��33���� -    ��Name$ trayCheckBox��$��Minimize to tray��$
��Left=�$��
169,38,0,0q�$	��Top=�$��26��$	��119��$��Arial)�$
��Bold"�$��12(��� -   $ profileTabItem��$��Profiles��$��0,0,-3,0q�$��/resources/#FinkHeavy)���Z� ��Z�  ��%�   '��       �� " ��.���  � �� �� "	 ��.R��� $
 root��$��16.67(�$��31��
�������.���� $��Header����.  P�  �@��
IsSelected . . ��� $
 root��# �   � ��.��� .+��� -	    ��Name$ profileListView��$
��Auto=�  $� VirtualizationMode$  	Recycling=�$
��Left=�$	��100��$��	10,10,0,0q�$	��Top=�$	��172��
���   �$��11(�$��Arial)�$��Stretch=����� *
Profiles+���� " ��.���  � �� �� "	 ��.R��� ! �� CornerRadius$! 0��" J� SnapsToDevicePixels" . $��0,-1,0,1q�
������ #��z��._��� $��InnerBorder��$! 0��$��0q�._�� .+��� $��UpperHighlight��$y�	Collapsed=�
������ �� $��0��#��z��#" z"  P�  �@J�IsMouseOver . . ���  � ��
 ������� # �   � ����  � ��
 �   ��� $ UpperHighlight�� � y�$ Visible=�P�  �@��
IsSelected . . ��� # �   � ����  � ��
 �������  � ��
 ���� �� $ UpperHighlight�� � y�$ Visible=���  � ��$
 Left=� ^� # ^� 
Conditions# �� $ �� Binding$ �� % �� Path$
% Type�& �� Value& Delivery�� $ �� ' �� ElementName$
' this��$% (IsMouseOver)�& True0���  �@�ToolTip �� $
% Item�( � ContextMenu( �� -
   $
��menu��) �� 	StaysOpen) . .@�m� -   $��
deleteItem��$��Remove��* �� View* �� + �� AllowsColumnReorder+ .  !, �� ColumnHeaderContainerStyle#, �   .P��� - �� Width$- 80��. �� DisplayMemberBinding. �� *Mayor+.���� $��MAYOR��$��80��
�������$��12(�
���   �$��-6,0,0,0q�/ �� CellTemplate/ �� .R��� 0 �� TextAlignment$
0 Left=�$��-6,0,0,0q��� $- 94��. �� *Town+.���� $
��TOWN��$��94��
�������$��12(�
���   �$��-4,0,0,0q�/ �� .R��� $
��Bajs��$
0 Left=�$��
-400,0,0,0q��� -   $ 	newButton��$��New Profile��$
��Left=�$��
10,115,0,0q�$	��Top=�$	��172��$��27��$��Arial)�$
��Bold"�$��12(��� -   $ 
editButton��$��Edit Profile��$
��Left=�$��
187,42,0,0q�$	��Top=�$��99��$��26��$��Arial)�$
��Bold"�$��12(��� -   $ 	useButton��$��Use Profile��$
��Left=�$��
187,10,0,0q�$	��Top=�$��99��$��27��$��Arial)�$
��Bold"�$��12(��� -   $ deleteButton��$��Delete Profile��$
��Left=�$��
187,83,0,0q�$	��Top=�$��99��$��27��$��Arial)�$
��Bold"�$��12(��� -   $ themeTabItem��$��Theme��$��12(�$��
10,0,-10,0q�$��/resources/#FinkHeavy)���Z�  ��%�   '��       �� " ��.���  � �� �� "	 ��.R��� $
 root��$��16.67(�$��31��
�������.���� $��Header����.  P�  �@��
IsSelected . . ��� $
 root��# �   � ��.��� .+��� -   $ accentLabel��$��Accent:��$
��Left=�$��5,39,0,0q�$	��Top=�$��90��#���  $��12(�$
��Bold"�$��Arial)��� -   $ greenButton��$�� ��$
��Left=�$��5,68,0,0q�$	��Top=�$��42��$��34�����	#FF008000��Z�  1 ��Name��%:  '��       �� " ��.���  � ��$	 5.0q���  � �� �� "	 ��.R��� $1 Border��$��3q����	#FF008000._��� $��2q�$��Center=�$��Center=���.  P�  �@J�IsKeyboardFocused . . ��� $ Border�� � �� �	#FF4F4F4FP�  �@��IsDefaulted . . ��� $ Border�� � �� �#202020P�  �@J�IsMouseOver . . ��� $ Border�� � �� �	#FF4F4F4FP�  � �� . . ��� $ Border�� � �� �	#FF4F4F4FP�  � {� .  . ��� $ Border�� � �� �	#FF4F4F4F��  � �� �#888888�� -   $ 
blueButton��$�� ��$
��Left=�$��	68,68,0,0q�$	��Top=�$��42��$��34�����	#FF017BCD��Z�  ��%:  '��       �� " ��.���  � ��$	 3.0q���  � �� �� "	 ��.R��� $1 Border��$��2q����	#FF017BCD._��� $��2q�$��Center=�$��Center=���.  P�  �@J�IsKeyboardFocused . . ��� $ Border�� � �� �	#FF4F4F4FP�  �@��IsDefaulted . . ��� $ Border�� � �� �#202020P�  �@J�IsMouseOver . . ��� $ Border�� � �� �	#FF4F4F4FP�  � �� . . ��� $ Border�� � �� �	#FF4F4F4FP�  � {� .  . ��� $ Border�� � �� �	#FF4F4F4F��  � �� �#888888�� -   $ purpleButton��$�� ��$
��Left=�$��
129,68,0,0q�$	��Top=�$��42��$��34�����	#FF800080��Z�  ��%:  '��       �� " ��.���  � ��$	 3.0q���  � �� �� "	 ��.R��� $1 Border��$��2q����	#FF800080._��� $��2q�$��Center=�$��Center=���.  P�  �@J�IsKeyboardFocused . . ��� $ Border�� � �� �	#FF4F4F4FP�  �@��IsDefaulted . . ��� $ Border�� � �� �#202020P�  �@J�IsMouseOver . . ��� $ Border�� � �� �	#FF4F4F4FP�  � �� . . ��� $ Border�� � �� �	#FF4F4F4FP�  � {� .  . ��� $ Border�� � �� �	#FF4F4F4F��  � �� �#888888�� -   $ orangeButton��$�� ��$
��Left=�$��
190,68,0,0q�$	��Top=�$��42��$��34�����	#FFFFA500��Z�  ��%:  '��       �� " ��.���  � ��$	 3.0q���  � �� �� "	 ��.R��� $1 Border��$��2q����	#FFFFA500._��� $��2q�$��Center=�$��Center=���.  P�  �@J�IsKeyboardFocused . . ��� $ Border�� � �� �	#FF4F4F4FP�  �@��IsDefaulted . . ��� $ Border�� � �� �#202020P�  �@J�IsMouseOver . . ��� $ Border�� � �� �	#FF4F4F4FP�  � �� . . ��� $ Border�� � �� �	#FF4F4F4FP�  � {� .  . ��� $ Border�� � �� �	#FF4F4F4F��  � �� �#888888�� -   $ 	redButton��$�� ��$
��Left=�$��
250,68,0,0q�$	��Top=�$��42��$��34�����	#FFFF0000��Z�  ��%:  '��       �� " ��.���  � ��$	 3.0q���  � �� �� "	 ��.R��� $1 Border��$��2q����	#FFFF0000._��� $��2q�$��Center=�$��Center=���.  P�  �@J�IsKeyboardFocused . . ��� $ Border�� � �� �	#FF4F4F4FP�  �@��IsDefaulted . . ��� $ Border�� � �� �#202020P�  �@J�IsMouseOver . . ��� $ Border�� � �� �	#FF4F4F4FP�  � �� . . ��� $ Border�� � �� �	#FF4F4F4FP�  � {� .  . ��� $ Border�� � �� �	#FF4F4F4F��  � �� �#888888�� -   $ 
backButton��$
��Back��$
��Left=�$��
13,206,0,0q�$	��Top=�$	��118��$��32��$��Arial)�$
��Bold"�$��12(�