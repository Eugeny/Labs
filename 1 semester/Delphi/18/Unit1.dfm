object Form1: TForm1
  Left = 192
  Top = 124
  Width = 746
  Height = 383
  Caption = #1051#1072#1073'. '#1088#1072#1073#1086#1090#1072' '#8470'18 '#1054#1079#1077#1088#1089#1082#1080#1081' '#1042'.'#1057
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  OnCreate = FormCreate
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 8
    Top = 8
    Width = 24
    Height = 20
    Caption = 'N ='
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label2: TLabel
    Left = 224
    Top = 16
    Width = 57
    Height = 20
    Caption = #1044#1077#1088#1077#1074#1086
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object TreeView1: TTreeView
    Left = 224
    Top = 40
    Width = 257
    Height = 257
    Indent = 19
    TabOrder = 0
  end
  object Edit1: TEdit
    Left = 40
    Top = 8
    Width = 65
    Height = 21
    TabOrder = 1
  end
  object Button1: TButton
    Left = 112
    Top = 8
    Width = 97
    Height = 25
    Caption = #1048#1079#1084#1077#1085#1080#1090#1100
    TabOrder = 2
    OnClick = Button1Click
  end
  object StringGrid1: TStringGrid
    Left = 8
    Top = 40
    Width = 209
    Height = 257
    ColCount = 2
    FixedCols = 0
    RowCount = 10
    Options = [goFixedVertLine, goFixedHorzLine, goVertLine, goHorzLine, goRangeSelect, goEditing]
    TabOrder = 3
    ColWidths = (
      103
      97)
  end
  object Button2: TButton
    Left = 8
    Top = 312
    Width = 209
    Height = 25
    Caption = #1042#1074#1077#1089#1090#1080' '#1076#1077#1088#1077#1074#1086
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
    TabOrder = 4
    OnClick = Button2Click
  end
  object Memo1: TMemo
    Left = 496
    Top = 40
    Width = 217
    Height = 265
    TabOrder = 5
  end
  object Button3: TButton
    Left = 512
    Top = 312
    Width = 169
    Height = 25
    Caption = #1054#1073#1093#1086#1076' '#1076#1077#1088#1077#1074#1072
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
    TabOrder = 6
    OnClick = Button3Click
  end
  object Button4: TButton
    Left = 272
    Top = 312
    Width = 137
    Height = 25
    Caption = #1059#1076#1072#1083#1080#1090#1100' '#1076#1077#1088#1077#1074#1086
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
    TabOrder = 7
  end
end
