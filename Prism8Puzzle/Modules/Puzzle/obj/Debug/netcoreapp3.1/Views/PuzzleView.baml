   M S B A M L   `   `   `  ����A  =Puzzle, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Puzzle.ViewsPuzzle.Views  5      C ?Controls, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5(clr-namespace:Controls;assembly=ControlsControls 5
      E AConverters, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;,clr-namespace:Converters;assembly=Converters
Converters 5          Puzzle.Views.PuzzleView   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ,localclr-namespace:Puzzle.Views     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      S OPrism.Wpf, Version=8.0.0.1909, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59$prismhttp://prismlibrary.com/  5	      3ctrl(clr-namespace:Controls;assembly=Controls  5
      =
converters,clr-namespace:Converters;assembly=Converters  5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True5       � 	Resources 5      $  Converters.VisibilityConverter   VisibilityConverter"  Converters.HintTextConverter  HintTextConverter �� 
TargetType  hintBtn �� Property �� Value �� 
TargetType ��Name �� Triggers P� Property	 P� Value
 �� 
TargetName �� RelativeSource �� Mode �� AncestorType �� 	Converter  forwardBackwardBtn  puzzleTemplate  Controls.MyBusyIndicator  Name   BusyContent   IsBusy   AdditionalContent � RowDefinitions � ColumnDefinitions � ToolTip ��Name  Controls.PuzzleControl   ItemsSource   CustomCommand �� Path   CustomCommandParameter J� Opacity  settingsTemplate � 	Resources" ,� VerticalScrollBarVisibility �� TextAlignment �� Orientation��%�  &	        &	      &	 &     7  &	 9    &	 E    7  7  7  7   &	       5   
   6)     5   
   6'   �� 5   
   " ��6   .�5      ��  � ��$ 3q�6'   �� 5       � ��$ 60��6'   �� 5       � ��$ 60��6&   �� 5       � ��$ Center=�64   �� 5       � ��$ Center=�62   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $
 brdr��6!   
���   �$��1q�6C   
��� ���._�5      �� $��hintTb��6(   $��?��66   $��Center=�6?   $��Center=�6\   $��55(�6w   5       5      P� 5       �@J�IsMouseOver$
	 True��6=   . �5   "   �� $
 hintTb��6C    6W   �� *DataContext.HintTileProp+ �� $ FindAncestor=� UserControl8    � ��5"      P� 5#       � {�$	 False��6;   . �5$   "   �� $

 brdr��6)    � ��
 �����6Q   5%      5'      5)      5*      �� 5+   
   " ��6   .�5,      ��  � ��$ 3q�6'   �� 5-       � ��$ 60��6'   �� 5.       � ��$ 60��6&   �� 5/       � ��$ Center=�64   �� 50       � ��$ Center=�62   �� 51       � �� 52      �� 53      " ��6&   .R�54      �� $
 brdr��6!   
���   �$��1q�6C   
�������._�55      �� #��z��6/   56       57      P� 58       � {�$	 False��6;   . �59   "   �� $

 brdr��6)    � ��
 �����6Q   5:      5<      5>      5?      �� 5A   
   .R�5B      � 
��� � �.+�5C        $
 Busy��6'    5D   '   �� *DataContext.BusyContent+ �� $ FindAncestor=� UserControl 5E   '   �� *DataContext.IsBusy+ �� $ FindAncestor=� UserControl 5F      � 5G       5H      �� 5I   "   $
��Auto �60   �� 5J   "    5L      �� 5M   "   �� 5N   "   �� 5O   "   .+�5R      �� $��0��6%   8��  $ Go Back��6�   ��6^   �� *DataContext.GoBackCommand+ �� $ FindAncestor=� UserControl��5S   "   �� 5T   &   $,��&/Library;component/Images/lessThan.png��6,   5V      �� 5W      $ hintBtn��6%   8�� $��1��6W   $
 Hint��6c  ��6g   �� *DataContext.HintMoveCommand+ �� $ FindAncestor=� UserControl��6�   �� *DataContext.HintTileProp+ �� $ FindAncestor=� UserControl6c  �� 5X      8�� $��2��6Q   $��0��6a   $ 
Go Forward��6�   ��6n   �� *DataContext.GoForwardCommand+ �� $ FindAncestor=� UserControl��5Y   "   �� 5Z   &   $,��&/Library;component/Images/moreThan.png��6,   5\      (� 5^      $��0q�6&   $��1��61   $��0��6>   $��3��6N   .��5_   "   � .+�5`   &     $��1��5a   )   $��2��69    5b   )   �� *!DataContext.MainBoard.TilesList+ �� $ FindAncestor=� UserControl 5c   )   �� $ DataContext.MoveCommand� �� $ FindAncestor=� UserControl 5d   )   �� *DataContext+ �� *Self+5f   #   5g      �� 5h      
���   �$	 0.1Q�6<   $��0��6J   $��2��6W   $��0��5i      $��3��6-   y�5j      �� *DataContext.IsViewBusy+8   �� $ FindAncestor=� UserControl5k      5m      5n      5o      �� 5q   
    5r      ���� 5s      'J�       " J�6   .�5t      ��  � ��$ 15(�61   6?   �� 5u       � ��$ Italic$�62   6D   �� 5v       � ��
 � � �6D   �� 5w       �@��Padding$ 5q�60   6<   �� 5x       �@��Margin$ 
5,10,15,10q�6/   6D   �� 5y       �@��TextAlignment$ Center=�66   6G   5z      5{      .R�5|      � 
��� ��� 5}      �� 5~      �� 5      .+�5�      ,� $ Disabled=�6+   .��5�      /� .T�5�      J� .*�6$   '%"The woods are lovely, dark and deep,5�      J� 5�      .*�6$   But I have promises to keep,5�      J� 5�      .*�6$   !And miles to go before I sleep,5�      J� 5�      .*�6$   " And miles to go before I sleep."5�      J� 5�      $��Normal$�6$   $ Right=�67   .*�6M   Robert Frost5�      5�      5�      �� 5�      $��1��6   $ 
Horizontal=�6*   $��Center=�6C   $��Center=�6`   .+�5�      �� $��Center=�6    $��Center=�6=   $��15(�6X   $��Icons Provided By :  ��6f   6�   �� 5�      $��Center=�6   $��Center=�6:   $��https://icons8.com/��5�   $   ��5�      �� *!DataContext.IconsWebSiteCommand+ �� $ FindAncestor=� UserControl.��5�      �� $��15��6    $��15��6,   $*��$/Library;component/Images/icons8.png��67   $ https://icons8.com/��6e   5�      5�      5�      5�      5�      .��5�      � .+�5�   
   �� ��5�      �� 5�      " ��6     �� Triggers  5�      �� 5�      ! �� Binding! 6&   �� *
IsSettings+" �� Value	" False6E   .a�5�      ��  � ��# [ 6@   5�      �� 5�      ! 6&   �� *
IsSettings+" True6E   .a�5�      ��  � ��# [ 6@   5�      5�      5�      5�      5�      