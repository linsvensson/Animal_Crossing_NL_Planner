   M S B A M L   `   `   `  ���� G  CACNL_Planner, Version=1.4.2.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:Animal_Xing_PlannerAnimal_Xing_Planner  $    Animal_Xing_Planner.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     P LElysium, Version=2.0.976.4, Culture=neutral, PublicKeyToken=afa220db249e5b15/metro#http://schemas.codeplex.com/elysium  9metroc,http://schemas.codeplex.com/elysium/controls  7params*http://schemas.codeplex.com/elysium/params  -local!clr-namespace:Animal_Xing_Planner  -     Elysium.Controls.Window   Name$
  Main�� � Title$! Animal Crossing: NL Planner��$��489.988��$��405.975�� � 
ResizeMode$ CanMinimize=�$��Center=�$��Center=�$��Center=�$��Center=� � SizeToContent$ WidthAndHeight=�   IsMainWindow .  � WindowStyle$ SingleBorderWindow=�   HasDropShadow .   � Icon$ Resources/acicon.ico�� � WindowStartupLocation$ CenterScreen=�	 � 	Resources	 
 �� 
TargetType �� Triggers P� Property P� Value �� Property �� Value   CustomHeaderStyle �� 
TargetType ��Name �� Padding=  7System.Windows.Controls.Primitives.DataGridColumnHeader)  #System.Windows.Controls.DataGridRow ��Name J� SnapsToDevicePixels �� Color?  9System.Windows.Controls.Primitives.SelectiveScrollingGrid � ColumnDefinitions � RowDefinitions?  9System.Windows.Controls.Primitives.DataGridCellsPresenter   
ItemsPanelA  ;System.Windows.Controls.Primitives.DataGridDetailsPresenter   DetailsVisibility$   SelectiveScrollingOrientation;  5System.Windows.Controls.SelectiveScrollingOrientation   Vertical �� ConverterParameter&   System.Windows.Controls.DataGrid#   RowDetailsScrollingConverter �� 	Converter �� RelativeSource  �� AncestorType:	  4System.Windows.Controls.Primitives.DataGridRowHeader7
  1System.Windows.Controls.DataGridHeadersVisibility
! 
  Row!"   HeadersVisibilityConverter  GroupHeaderStyle# 9�Name$ 9� 
IsExpanded*  $System.Windows.Controls.DataGridCell'  !Animal_Xing_Planner.TextConverter  TextConverter��%   '��       &	  D     '  �    '  	    &	 �    '  U    &	 �    �� "
 �� P�  � ��$ 	PART_Icon��. ���  � ��$ 32���� "
 ��.���  � ��
 �������  � �� �	#FF07034B��  � ��
 ���� ��  �@�OverridesDefaultStyle . ��  � �� �� " ��.R��� $��0,0,1,0q�
�������
�������._��� $ ContentHeader��#��z��$ 5,5,5,0q�$
��Left=�$
��Bold"� P�  �@J�IsMouseOver . . ���  � ��
 ���� ��  � �� �	#FFE6D27A�� "
  .���  � ��
 ���� ��  � ��
 �   ���  � ��$
 Bold"��� "
  .���  � ��
 �������  �@=�ErrorTemplate Z� ��  � �� �� "  .R��� $ 
DGR_Border��#��z��#��z�� . ���� $ Transparent��._�   �� $
��Auto ��� $��* � �� $��* ��� $
��Auto �.+�  $��1��#��z # z   $��1��$��1��#y�z  �� *AreRowDetailsFrozen+# Z # Z  �� "   	  $��2��$ Vertical=�y��� *HeadersVisibility+# Z! # Z"  �� "   �� "
 ��.���  � �� �� " ��.R�9� $	# exp��$ . 
������ 
���   ����� $��Italic$����� *Name+.���� �� "
  .�-    P�  �@ 
IsSelected . . ���  � ��
 ��d���  � ��
 �������  � ��
 ����   .��� $��	10,0,0,-1q�$	��470��$	��378��$
��Left=����� % �� ImageSource$% Resources/bg.png��& j� TileMode$
& Tile=�.+��� -   ' ��Name$' userNameLabel��$��10,10,119,340q�$��16(�$��Arial)� 
 Accent#���  $
��Bold"���Z� �� $��22,39,26,10q�.@��� $
��Home��$��/resources/#FinkHeavy)�$��	-5,-1,5,1q���Z� .��� .+��� #���  $��4q�$
��Left=�$	��276��$��25,1,0,0q�$	��Top=�$	��372��
��������� #���  $	��1.5q�$
��Left=�$	��264��$��31,7,0,0q�$	��Top=�$	��360���� ���	#FFFFD30C$	��1.5q�$
��Left=�$��2��$��	45,40,0,0q�$	��Top=�$	��333���� -   ( ��Name$( noticeImage��$��Resources/board.png��$��196,12,128,242q�$
��Fill=�$
��Auto=�$
��Auto=��� -   $' headerLabel��$��Notices��$
��Left=�$��	69,15,0,0q�$	��Top=�$��94��$��14(�$��Arial)�$
��Bold"����	#FF383838$��30���� -   ) ��Name$) noticeListView��$��Disabled=�* $� IsVirtualizing* . + $� VirtualizationMode$+ 	Recycling=�$��Arial)�$��11(�$��36,50,38,50q���. |�.  
���   ���Z� ���� "
 ��.���  � �� �� " ��.R��� , �� CornerRadius$, 0�� . $��0,-1,0,1q�
������ #��z��._��� $��InnerBorder��$, 0��$��0q�._�� .+��� $��UpperHighlight��$y�	Collapsed=�
������ �� $��0��#��z��# z - �� Triggers- P�  �@J�IsMouseOver . . ���  � ��
 ������� # �   � ����  � ��
 �   ��� . �� 
TargetName$. UpperHighlight�� � y�$ Visible=�P�  �@��
IsSelected . . ��� # �   � ����  � ��
 �������  � ��
 ���� �� $. UpperHighlight�� � y�$ Visible=� ^� / ^� 
Conditions/ �� 0 �� Binding0 �� 1 �� Path$
1 Type�2 �� Value	2 Event0���  �@�ToolTip �� $
1 Name�^� / �� 0 �� $
1 Type�2 Delivery0���  �@�ToolTip �� $
1 Item�3 � ContextMenu3 �� -   $
��menu��4 �� 	StaysOpen4 . .@�m� -   $��
deleteItem��$��Remove��5 �� View5 �� 6 �� AllowsColumnReorder6 .  !7 �� ColumnHeaderContainerStyle#7 �   .P��� 8 �� Width$8 75��9 �� DisplayMemberBinding9 �� *Type+.���� -	   
: � Tag$
: Type��$
��TYPE��$��76��
�������$��12(�
���   �$��-4,0,0,0q��� $	8 100��9 �� *Name+.���� -
   $
: Name��$
��NAME��$��12(�
���   �; �� CellTemplate; �� .R��� < �� TextAlignment$
< Left=�$��-4,0,0,0q����	#FFB23333= � ToolTip= �� $
1 Name��� $8 90��9 �� *Info+.���� -   $
: Info��$
��INFO��$��88��$��12(�
���   ��� $8 80��9 �� *
StopTime+.���� -   $: StopTime��$
��TIME��$��82��$��12(�
���   ��� -   > ��Name$> 	addButton��$�� ��$
��Left=�$��350,240,0,0q�$	��Top=�$��34��$��31��$��15����Z� ���� " ��.R��� $ bdr_main��$��0,0,4,4q�$��0q�
���   ����� $% Resources/addBtn.png��? j� Stretch$? UniformToFill=�._��� $��Center=�$��Center=�$��8,6,8,6q�$��Content��- P� 
border�� � ��$	 3.0q�P�  �@J�IsMouseOver . . ��� $. bdr_main�� � �� �� $% Resources/addBtn.png��@ �� Opacity$	@ 0.7Q�$? UniformToFill=�P�  � �� . . ��� $. bdr_main�� � �� �� $% Resources/addBtn.png��$	@ 0.4Q�$? UniformToFill=��� -   $> removeButton��$�� ��$
��Left=�$��
36,240,0,0q�$	��Top=�$��30��A J� RenderTransformOrigin$A 0.5,0.5&�$��34��$��15����Z� ��Z� z�V� .~��� B �� ScaleX$B -1Q��� �� T� ���� " ��.R��� $ bdr_main��$��0,0,4,4q�$��0q�
���   ����� $% Resources/removeBtn.png��$? UniformToFill=�._��� $��Center=�$��Center=�$��8,6,8,6q�$��Content��- P� 
border�� � ��$	 3.0q�P�  �@J�IsMouseOver . . ��� $. bdr_main�� � �� �� $% Resources/removeBtn.png��$	@ 0.7Q�$? UniformToFill=�P�  � �� . . ��� $. bdr_main�� � �� �� $% Resources/removeBtn.png��$	@ 0.4Q�$? UniformToFill=��� -   C ��Name$C profileTabItem��$��Profile��$��/resources/#FinkHeavy)�$��Right=�$��87��.��� .+��� -   $( tpcImage��$
��Left=�$	��255��$	��Top=�$	��346��$��Resources/tpc.png��$
��Fill=�$��	23,14,0,0q��� -   $' 
mayorLabel��$�� ��$
��Left=�$��157,120,0,0q�$	��Top=�$	��160��$��18(�$��Arial)����	#FFF1920A$��29���� -   $' messageLabel��$-��'No profile set, create one in settings!��$
��Left=�$��	66,24,0,0q�$	��Top=�$	��279��$��30��$��Arial)����	#FF616161$
��Bold"��� -   $' 	townLabel��$�� ��$��157,86,34,0q�$	��Top=�$��18(�$��29��$��Arial)����	#FFF3A739�� -   $' fcLabel��$�� ��$
��Left=�$��191,158,0,0q�$	��Top=�$	��154��$��18(�$��Arial)����	#FFBE60CD�� -   $' fcHeaderLabel��$��FC��$
��Left=�$��152,158,0,0q�$	��Top=�$��34��$��18(�$��Arial)����	#FFBE60CD�� -   $' dcLabel��$�� ��$
��Left=�$��191,194,0,0q�$	��Top=�$	��154��$��18(�$��Arial)����	#FFBE60CD�� -   $' dcHeaderLabel��$��DC��$
��Left=�$��152,194,0,0q�$	��Top=�$��34��$��18(�$��Arial)����	#FFBE60CD�� -   $( profileImage��$
��Left=�$	��112��$��	46,77,0,0q�$	��Top=�$��74��D J� 	AllowDropD . $
��Fill=��� -   $C checklistTabItem��$��	Checklist��$��/resources/#FinkHeavy)�$��Right=�$��98��$��-6,0,6,0q�.��� ���� $% Resources/bugfish.png��$	@ 0.6Q�$? UniformToFill=�$
& Tile=�.+��� -   $> reloadButton��$��Reload��$
��Left=�$��	382,0,0,0q�$	��Top=�$��30��$��15��$��Arial)�$
��Bold"�$��12(�$= Reload checklist��$A 0.5,0.5&�$��30��  -   $��checklistDataGrid��E   CanUserAddRowsE .  $��	9,16,7,10q�
���   �F   CanUserResizeRowsF .  G   CanUserReorderColumnsG .  $��Arial)�$A 0.5,0.5&�H   GridLinesVisibility$
H None=�$	��500��$	��500��!I   EnableColumnVirtualizationI . J   EnableRowVirtualizationJ . K   AutoGenerateColumnsK .  L   CanUserSortColumnsL .  $	��259��$	��406��M   CanUserResizeColumnsM .  ��Z� N �� 
GroupStyleN (  "System.Windows.Controls.GroupStyle  O   ContainerStyle#O [ P   PanelP �� .R�>  8System.Windows.Controls.Primitives.DataGridRowsPresenter  Q   ColumnsQ 4  .System.Windows.Controls.DataGridCheckBoxColumn  ,  &System.Windows.Controls.DataGridColumnR   Header$R  ��S   
IsReadOnlyS .  1  +System.Windows.Controls.DataGridBoundColumnT   BindingT �� $1 Checked�0  *System.Windows.Controls.DataGridTextColumn  $
R NAME��S . T �� $
1 Name�  $R MONTH��S . T �� $1 Month�  $R LOCATION��S . T �� $1 Location�5  /System.Windows.Controls.DataGridHyperlinkColumn  $
R INFO��S . T �� $
1 Info�U   ContentBindingU �� *Text+# [ V   ElementStyleV �� "
 ��.�-   z�V� .~��� �� W �� AngleY$W 0.148Q��� T� X T� Y$X 0.497Q��� $��342,8,14,307q�$��Resources/time.png��Y J� Opacity$
Y 0.75Q��� -   $' timeTextLabel��$��11:44��$��0,33,30,310q�$��20(�$��Arial)�$��Right=�$��85�����	#FF008BDA$
��Bold"�$��Center=��� -   $' dateTextLabel��$��Right=�$��	0,11,69,0q�$	��Top=�$��54��$��14(�$��Arial)�
���   �$A 0.5,0.5&�$��25��$��78 / 77��$
��Bold"�z�V� .~��� �� �� Z �� Angle$Z 2.824Q�T� �� -   $' dayTextLabel��$��Right=�$��0,8,34,0q�$	��Top=�$��32��$��14(�$��Arial)�
���   �$A 0.5,0.5&�$��25��$��Su��$
��Bold"�[   TitleBar[ �  �� $��65 �$��65���� $��65 �$��65���� $��30 �$��30��$��RightToLeft=��� $��30 �$��30��$��RightToLeft=�.+��� -    $��1��$��settings��#\   DefaultCaptionButtonStyleKey#��� \ �� -!   $��2��$��?��#��� \ 