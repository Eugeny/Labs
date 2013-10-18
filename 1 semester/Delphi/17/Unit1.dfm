object Form1: TForm1
  Left = 192
  Top = 124
  Width = 382
  Height = 279
  Caption = #1051#1072#1073'. '#1088#1072#1073#1086#1090#1072' '#8470'17 '#1054#1079#1077#1088#1089#1082#1080#1081' '#1042'.'#1057
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
    Left = 16
    Top = 8
    Width = 101
    Height = 20
    Caption = #1055#1077#1088#1077#1084#1077#1085#1085#1099#1077':'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label2: TLabel
    Left = 160
    Top = 104
    Width = 73
    Height = 16
    Caption = #1056#1077#1079#1091#1083#1100#1090#1072#1090':'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object StringGrid1: TStringGrid
    Left = 16
    Top = 32
    Width = 137
    Height = 209
    ColCount = 2
    FixedCols = 0
    RowCount = 8
    TabOrder = 0
    ColWidths = (
      62
      64)
    RowHeights = (
      24
      24
      24
      24
      24
      24
      24
      24)
  end
  object Edit1: TEdit
    Left = 160
    Top = 32
    Width = 185
    Height = 21
    TabOrder = 1
    Text = 'k*(m+n*j)^i-v/u'
  end
  object Edit2: TEdit
    Left = 160
    Top = 72
    Width = 185
    Height = 21
    TabOrder = 2
  end
  object Edit3: TEdit
    Left = 240
    Top = 104
    Width = 105
    Height = 21
    TabOrder = 3
  end
  object Button1: TButton
    Left = 160
    Top = 136
    Width = 185
    Height = 25
    Caption = #1055#1077#1088#1077#1074#1077#1089#1090#1080' '#1074' '#1087#1086#1089#1090#1092#1080#1082#1089#1085#1091#1102' '#1092#1086#1088#1084#1091
    TabOrder = 4
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 160
    Top = 168
    Width = 185
    Height = 25
    Caption = #1056#1072#1089#1089#1095#1080#1090#1072#1090#1100
    TabOrder = 5
    OnClick = Button2Click
  end
end
