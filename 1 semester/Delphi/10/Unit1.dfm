object Form1: TForm1
  Left = 192
  Top = 124
  Width = 681
  Height = 303
  AutoSize = True
  Caption = #1051#1072#1073'. '#1088#1072#1073' '#8470'10 '#1054#1079#1077#1088#1089#1082#1080#1081' '#1042'.'#1057
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
  object Image1: TImage
    Left = 0
    Top = 0
    Width = 665
    Height = 233
    Align = alTop
  end
  object Button1: TButton
    Left = 0
    Top = 240
    Width = 113
    Height = 25
    Caption = #1044#1086#1073#1072#1074#1080#1090#1100' '#1103#1097#1080#1082
    TabOrder = 0
    OnClick = Button1Click
  end
  object Button2: TButton
    Left = 120
    Top = 240
    Width = 113
    Height = 25
    Caption = #1059#1076#1072#1083#1080#1090#1100' '#1103#1097#1080#1082
    TabOrder = 1
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 288
    Top = 240
    Width = 49
    Height = 25
    Caption = '<<<'
    TabOrder = 2
    OnClick = Button3Click
  end
  object Button4: TButton
    Left = 344
    Top = 240
    Width = 41
    Height = 25
    Caption = '<'
    TabOrder = 3
    OnClick = Button4Click
  end
  object Button5: TButton
    Left = 392
    Top = 240
    Width = 49
    Height = 25
    Caption = '>'
    TabOrder = 4
    OnClick = Button5Click
  end
  object Button6: TButton
    Left = 448
    Top = 240
    Width = 41
    Height = 25
    Caption = '>>>'
    TabOrder = 5
    OnClick = Button6Click
  end
  object Button7: TButton
    Left = 560
    Top = 240
    Width = 75
    Height = 25
    Caption = #1042#1099#1093#1086#1076
    TabOrder = 6
    OnClick = Button7Click
  end
  object Timer1: TTimer
    Interval = 30
    OnTimer = Timer1Timer
    Left = 512
    Top = 240
  end
end
