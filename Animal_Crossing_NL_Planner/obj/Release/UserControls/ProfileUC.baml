   M S B A M L   `   `   `  ���� G  CACNL_Planner, Version=1.4.2.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Animal_Xing_PlannerAnimal_Xing_Planner  #    Animal_Xing_Planner.ProfileUC   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     P LElysium, Version=2.0.976.4, Culture=neutral, PublicKeyToken=afa220db249e5b15/metro#http://schemas.codeplex.com/elysium  7params*http://schemas.codeplex.com/elysium/params  -local!clr-namespace:Animal_Xing_Planner  -     >�Name$  profile�� � 	Resources    TextBoxBorder �� 
StartPoint �� EndPoint � MappingMode � Color � Offset!   Animal_Xing_Planner.Profile  ods  CustomHeaderStyle �� 
TargetType �� Property	 �� Value
 �� 
TargetType ��Name �� Padding �� TextAlignment �� Triggers P� Property P� Value  validationTemplate  textBoxInError ]� 
Conditions �� Property �� Value �� Self �� RelativeSource �� Path�� %�  &	        &	 �     &	 �     &	     &	     7   �� $	 0,0&�$
 0,20&�$ Absolute=���� $ 	#FFD40707��$
 0.05Q�� $ 	#FFF13C3C��$
 0.07Q�� $ 	#FF890505��$ 1Q�  �� " ��.���  � ��
	 �������  � ��	 �	#FF07034B��  � ��
	 ���� ��  �@�OverridesDefaultStyle	 . ��  � ��	 �� "
 ��.R��� $��0,0,1,0q�
�������
�������._��� $ ContentHeader��#��z��$ 5,5,5,0q�#��z��$
 Left=�$
��Bold"�$��11(� P�  �@J�IsMouseOver . . ���  � ��
	 ���� ��  � ��	 �	#FFE6D27A�� �� " �� ]�  ��  �@=�HasError . ��  �@J�	IsFocused . /���  �@�ToolTip	 �� # Z $) #(Validation.Errors)[0].ErrorContent���  � ��	 �	#FFFFC3C3]�  ��  �@=�HasError . ��  �@J�	IsFocused .  /���  �@�ToolTip	 �� # Z $) #(Validation.Errors)[0].ErrorContent���  � ��8	   ��  � ��$		 1.5q�.��� $	��400��$	��400��$	��392������  j� TileMode$
 Tile=� �� ImageSource$/ )/ACNL_Planner;component/Resources/bg2.png��.+��� -    ��Name$ townTextBox��$��Arial)� �� TextWrapping$ WrapWithOverflow =� 
 Accent#���  " �� VerticalScrollBarVisibility$
 Auto=� J� RenderTransformOrigin$ 0.5,0.5&�$��105,41,167,333q� =� ErrorTemplate# [ #��[ $��Center=�$��1��.������ $
 Town� �� Source# [  �� UpdateSourceTrigger$ Explicit=�  �� ValidationRules  -   'Animal_Xing_Planner.EmptyValidationRule  .���� -   $ nameTextBox��$��Arial)�$
 Auto=�#���  $! �� HorizontalScrollBarVisibility$
! Auto=�$ WrapWithOverflow =�$ 0.5,0.5&�$��105,10,167,364q�# [ #��[ $��Center=�$��0��.������ $ Mayor�# [ $ Explicit=�    .���� -   $ 	dcTextBox��$��Arial)�$ WrapWithOverflow =�#���  $
 Auto=�$ 0.5,0.5&�$��105,103,167,271q�# [ #��[ $��Center=�$��3��.������ $ DC�# [ $ PropertyChanged=�  *   $Animal_Xing_Planner.FCValidationRule  .���� -   $ 	fcTextBox��$��Arial)�$ WrapWithOverflow =�#���  $
 Auto=�$ 0.5,0.5&�$��105,72,167,302q�# [ #��[ $��Center=�$��2��.������ $ FC�# [ $ Explicit=�    .���� -   " ��Name$" 
saveButton��$
��Save��$
��Left=�$��28��$	��Top=�$��90��$��285,340,0,0q�$��Arial)�$
��Bold"�$��10������ " ��.���  � {�	 .   ^� # ^� 
Conditions# �� $ �� Binding$ �� % �� ElementName$% nameTextBox��$ (Validation.HasError)�	 False�� $ �� $% townTextBox��$ (Validation.HasError)�	 False�� $ �� $% 	fcTextBox��$ (Validation.HasError)�	 False.0���  � {�	 . �� -   $��Cancel��$
��Left=�$��28��$	��Top=�$��90��$��156,340,0,0q�$��Arial)�$
��Bold"�& �� IsCancel& . $��9��$��12(��� -   ' ��Name$' 	nameLabel��$��Mayor Name:��#���  $
��Left=�$��	15,10,0,0q�$	��Top=�$��90��$
��Bold"��� -	   $' 	townLabel��$��
Town Name:��#���  $
��Left=�$��	15,41,0,0q�$	��Top=�$��90��$
��Bold"��� -
   $' fcLabel��$��Friend Code:��#���  $
��Left=�$��	15,72,0,0q�$	��Top=�$��90��$
��Bold"��� -   $' dcLabel��$��Dream Code:��#���  $
��Left=�$��
15,103,0,0q�$	��Top=�$��90��$
��Bold"��� -   $' messageLabel��$��Tagline:��#���  $
��Left=�$��
15,135,0,0q�$	��Top=�$��90��( � ToolTip$(( ">Optional< Will appear on your TPC��$
��Bold"��� -   $ messageTextBox��$
��Left=�$��26��#���  $
 Auto=�$��105,135,0,0q�$ WrapWithOverflow =�$	��Top=�$	��142��$(( ">Optional< Will appear on your TPC��$��Arial)�$��Center=�$! Visible=�$��4���� -   ) ��Name$) villagerListView��$��Disabled=�$
��Left=�* $� VirtualizationMode$* 	Recycling=�$	��134��$��156,189,0,0q�$	��Top=�$	��219��
���   �$��11(�$��Arial)�$,( &A town always has at least 5 villagers������ " ��.���  � ��	 �� "
 ��.R��� + �� CornerRadius$+ 0��, J� SnapsToDevicePixels, . $��0,-1,0,1q�
������ #��z��._��� $��InnerBorder��$+ 0��$��0q�._�� .+��� $��UpperHighlight��$y�	Collapsed=�
������ �� $��0��#��z��#, z, - �� Triggers- P�  �@J�IsMouseOver . . ���  � ��
	 ������� #	 �   � ����  � ��
	 �   ��� . �� 
TargetName$. UpperHighlight�� � y�$	 Visible=�P�  �@��
IsSelected . . ��� #	 �   � ����  � ��
	 �������  � ��
	 ���� �� $. UpperHighlight�� � y�$	 Visible=���  � ��$
	 Left=� ^� # �� $ �� $
 Type� Delivery�� $ �� $
% this��$ (IsMouseOver)� True0���  �@�ToolTip	 �� $
 Item�/ � ContextMenu/ �� -   $
��menu��0 �� 	StaysOpen0 . .@�m� -   $��
deleteItem��$��Remove��1 �� View1 �� 2 �� AllowsColumnReorder2 .  !3 �� ColumnHeaderContainerStyle#3 �  .P��� 4 �� Width$4 75��5 �� DisplayMemberBinding5 �� *Name+.���� $
��NAME��$��75��
�������$��12(�
���   �$��-6,0,0,0q��� $4 58��5 �� *Personality+.���� $
��TYPE��$��58��
�������$��12(�
���   �$��-4,0,0,0q��� $4 86��5 �� *	Species+.���� $��SPECIES��$��87��
�������$��12(�
���   �$��-2,0,0,0q��� -   6 ��Name$6 typeComboBox��7 �� Text$
7 Type��8 $� IsVirtualizing8 . $* 	Recycling=�$
��Left=�$��
15,199,0,0q�$	��Top=�$	��109��$��Arial)�$��5��$��11(���.  9 �� 
IsEditable9 . �� -   $6 nameComboBox��$
7 Name��8 . $* 	Recycling=�$
��Left=�$��
15,261,0,0q�$	��Top=�$	��109��$��Arial)�$��7��$��11(�9 . �� -   $" 	addButton��$	��Add��$
��Left=�$��36��$	��Top=�$	��109��$��
15,298,0,0q�$��Arial)�$
��Bold"�$��8��$��12(��� -   $6 speciesComboBox��$7 Species��8 . $* 	Recycling=�$
��Left=�$��
15,230,0,0q�$	��Top=�$	��109��$��Arial)�$��6��$��11(�9 . �� #���  $��1q�$
��Left=�$	��130��$��
268,21,0,0q�$	��Top=�$��90��._��� $��	-1,20,1,9q�#���  .���� $��58��$��64��$ Center=�#���  $��10,0,0,0q�Drag�� image�� here�� -   : ��Name$: 
profileImg��$
��Left=�$	��130��$��
268,21,0,0q�$	��Top=�$��90��$
��Fill=�