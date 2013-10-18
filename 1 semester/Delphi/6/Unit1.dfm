object Form1: TForm1
  Left = 192
  Top = 124
  Width = 299
  Height = 484
  Caption = #1051#1072#1073'. '#1088#1072#1073' '#8470'6 '#1054#1079#1077#1088#1089#1082#1080#1081' '#1042'.'#1057
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object Label1: TLabel
    Left = 8
    Top = 8
    Width = 20
    Height = 20
    Caption = 'Xn'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -17
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label2: TLabel
    Left = 8
    Top = 32
    Width = 19
    Height = 20
    Caption = 'Xk'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -17
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label3: TLabel
    Left = 8
    Top = 56
    Width = 13
    Height = 20
    Caption = 'm'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -17
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Label4: TLabel
    Left = 8
    Top = 80
    Width = 9
    Height = 20
    Caption = 'e'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -17
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object Edit1: TEdit
    Left = 40
    Top = 8
    Width = 65
    Height = 21
    TabOrder = 0
    Text = '0.1'
  end
  object Edit2: TEdit
    Left = 40
    Top = 32
    Width = 65
    Height = 21
    TabOrder = 1
    Text = '0.8'
  end
  object Edit3: TEdit
    Left = 40
    Top = 56
    Width = 65
    Height = 21
    TabOrder = 2
    Text = '6'
  end
  object Edit4: TEdit
    Left = 40
    Top = 80
    Width = 65
    Height = 21
    TabOrder = 3
    Text = '0.001'
  end
  object RadioGroup1: TRadioGroup
    Left = 120
    Top = 8
    Width = 153
    Height = 89
    Caption = #1060#1091#1085#1082#1094#1080#1103
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -16
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ItemIndex = 0
    Items.Strings = (
      'f(x)'
      's(x)')
    ParentFont = False
    TabOrder = 4
  end
  object Memo1: TMemo
    Left = 8
    Top = 104
    Width = 265
    Height = 297
    TabOrder = 5
  end
  object BitBtn1: TBitBtn
    Left = 8
    Top = 416
    Width = 105
    Height = 25
    Caption = #1042#1099#1087#1086#1083#1085#1080#1090#1100
    TabOrder = 6
    OnClick = BitBtn1Click
    Kind = bkOK
  end
  object BitBtn2: TBitBtn
    Left = 160
    Top = 416
    Width = 105
    Height = 25
    TabOrder = 7
    Kind = bkClose
  end
end
