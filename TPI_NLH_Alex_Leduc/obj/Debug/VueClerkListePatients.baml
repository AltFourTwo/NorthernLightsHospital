   M S B A M L   `   `   `  ����M  ITPI_NLH_Alex_Leduc, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:TPI_NLH_Alex_LeducTPI_NLH_Alex_Leduc  5      .    (TPI_NLH_Alex_Leduc.VueClerkListePatients   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      2local clr-namespace:TPI_NLH_Alex_Leduc     5        N� Title$  Patients��5	      $	��720��6   $	��400��6$   .��5      � 
������� � ColumnDefinitions 5   
   �� 5      $��50 �6   �� 5      $��4* �6   �� 5      $��1* �6   �� 5      $��1* �6   �� 5      $��4* �6   �� 5      $��4* �6   �� 5      $��50 �6    � RowDefinitions 5   
   �� 5      $��50 �6   �� 5      $��1* �6   �� 5      $��3* �6   �� 5      $��3* �6   �� 5      $��3* �6   �� 5      $��3* �6   �� 5      $��3* �6   �� 5      $��1* �6   �� 5      $��30 �6   .+�5!   
   �� -   5        $��	lblHeader��5!      $��Patients��6!   $��18(�5"      $��SemiBold"�6   $��1��5#      $��5��6    $��Center=�5$      $��Center=�6-   �� 5'   
   $��Recherche :��6   $��1��6&   $��2��66   $
��Left=�5(      $	��Top=�6+   �� 5)   
   -   5        $��	txtSearch��5)      $��
5,25,10,10q�6#   $��1��5*      $��2��6"   $��3��6/   6C   &   System.Windows.Controls.DataGrid  5-   
   -   5        $��
dgPatients��5-      $ �� IsSynchronizedWithCurrentItem$
 trueY�6%      CanUserAddRows .     AutoGenerateColumns .     
IsReadOnly . $��1��5/      $��3��6#   $��5��60   $��4��6D   ��5.      ��  � ContextMenu 50      �� 51      .@�52      m� -   5        $��	menuItem1��52      $��Non implémenté��60   $y�Hidden=�6H   m� 53      -   5        $��	menuItem2��53      $��Non implémenté��60   $y�Hidden=�6H   54         Columns 56      0  *System.Windows.Controls.DataGridTextColumn  57      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Prenom��6%   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 65   ��  �� Path$ Prenom�  58      $		 Nom��6%   
 62   �� $	 Nom�  59      $	 Parent��6%   
 65   �� $ Parent�  5:      $	 Date de Naissance��6%   
 6@   �� $ 	BirthDate�  5;      $	 	Assurance��6%   
 68   �� $ 	Assurance�5=      �� 5@   
   -   5        $��	btnRetour��5@      $	��◄��6"   $��20(�6.   $	��5,5q�6<   6I   �� 5A   
   -   5        $��btnNouveauPatient��5A      $��Nouveau Patient��6*   $��5q�6D   $��5��5B      $��2��6!   $��Center=�5C      $��Center=�6.   6I   5D      5E      