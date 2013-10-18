object Form1: TForm1
  Left = 192
  Top = 124
  Width = 509
  Height = 228
  Caption = #1051#1072#1073'. '#1088#1072#1073' '#8470'5 '#1054#1079#1077#1088#1089#1082#1080#1081' '#1042'.'#1057' '
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
    Left = 24
    Top = 8
    Width = 166
    Height = 24
    Caption = #1048#1089#1093#1086#1076#1085#1099#1081' '#1084#1072#1089#1089#1080#1074':'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -19
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label2: TLabel
    Left = 24
    Top = 80
    Width = 98
    Height = 24
    Caption = #1056#1077#1079#1091#1083#1100#1090#1072#1090':'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -19
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object StringGrid1: TStringGrid
    Left = 24
    Top = 40
    Width = 465
    Height = 33
    ColCount = 7
    FixedCols = 0
    RowCount = 1
    FixedRows = 0
    TabOrder = 0
  end
  object StringGrid2: TStringGrid
    Left = 24
    Top = 112
    Width = 465
    Height = 33
    ColCount = 7
    FixedCols = 0
    RowCount = 1
    FixedRows = 0
    TabOrder = 1
  end
  object BitBtn1: TBitBtn
    Left = 24
    Top = 160
    Width = 217
    Height = 25
    Caption = #1059#1076#1072#1083#1080#1090#1100' '#1086#1090#1088#1080#1094#1072#1090#1077#1083#1100#1085#1099#1077' '#1095#1080#1089#1083#1072
    TabOrder = 2
    OnClick = BitBtn1Click
    Kind = bkOK
  end
  object BitBtn2: TBitBtn
    Left = 248
    Top = 160
    Width = 97
    Height = 25
    Caption = #1042#1099#1093#1086#1076
    TabOrder = 3
    OnClick = BitBtn2Click
    Kind = bkClose
  end
end
