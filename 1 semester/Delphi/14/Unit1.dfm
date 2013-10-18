object Form1: TForm1
  Left = 192
  Top = 124
  Width = 574
  Height = 497
  Caption = #1051#1072#1073'. '#1088#1072#1073' '#8470'14 '#1054#1079#1077#1088#1089#1082#1080#1081' '#1042'.'#1057
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
  object StringGrid1: TStringGrid
    Left = 16
    Top = 24
    Width = 521
    Height = 265
    ColCount = 4
    FixedCols = 0
    RowCount = 10
    TabOrder = 0
    ColWidths = (
      150
      133
      112
      110)
  end
  object GroupBox1: TGroupBox
    Left = 16
    Top = 288
    Width = 353
    Height = 169
    Caption = #1044#1086#1073#1072#1074#1080#1090#1100' '#1085#1086#1074#1099#1081' '#1087#1088#1077#1076#1084#1077#1090
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
    TabOrder = 1
    object Label2: TLabel
      Left = 7
      Top = 97
      Width = 146
      Height = 16
      Caption = #1044#1072#1090#1072' '#1087#1088#1080#1105#1084#1072' '#1074' '#1088#1077#1084#1086#1085#1090
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -13
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
    end
    object Label3: TLabel
      Left = 8
      Top = 33
      Width = 70
      Height = 20
      Caption = #1055#1088#1077#1076#1084#1077#1090
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -16
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
    end
    object Label4: TLabel
      Left = 8
      Top = 65
      Width = 61
      Height = 20
      Caption = #1052#1086#1076#1077#1083#1100
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -16
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
    end
    object DateTimePicker1: TDateTimePicker
      Left = 159
      Top = 92
      Width = 186
      Height = 21
      Date = 40433.524398807870000000
      Time = 40433.524398807870000000
      TabOrder = 0
    end
    object ComboBox1: TComboBox
      Left = 96
      Top = 33
      Width = 145
      Height = 24
      ItemHeight = 16
      ItemIndex = 0
      TabOrder = 1
      Text = #1058#1077#1083#1077#1074#1080#1079#1086#1088
      Items.Strings = (
        #1058#1077#1083#1077#1074#1080#1079#1086#1088
        #1050#1086#1084#1087#1100#1102#1090#1077#1088
        #1056#1072#1076#1080#1086#1087#1088#1080#1105#1084#1085#1080#1082
        #1052#1086#1085#1080#1090#1086#1088)
    end
    object Edit1: TEdit
      Left = 96
      Top = 68
      Width = 121
      Height = 24
      TabOrder = 2
    end
    object CheckBox1: TCheckBox
      Left = 8
      Top = 120
      Width = 97
      Height = 17
      Caption = #1043#1086#1090#1086#1074
      Font.Charset = DEFAULT_CHARSET
      Font.Color = clWindowText
      Font.Height = -16
      Font.Name = 'MS Sans Serif'
      Font.Style = []
      ParentFont = False
      TabOrder = 3
    end
    object Button1: TButton
      Left = 112
      Top = 136
      Width = 137
      Height = 25
      Caption = #1044#1086#1073#1072#1074#1080#1090#1100
      TabOrder = 4
      OnClick = Button1Click
    end
  end
  object Button2: TButton
    Left = 376
    Top = 328
    Width = 161
    Height = 25
    Caption = #1057#1086#1093#1088#1072#1085#1080#1090#1100' '#1074' '#1092#1072#1081#1083
    TabOrder = 2
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 376
    Top = 360
    Width = 161
    Height = 25
    Caption = #1047#1072#1075#1088#1091#1079#1080#1090#1100' '#1080#1079' '#1092#1072#1081#1083#1072
    TabOrder = 3
    OnClick = Button3Click
  end
  object Button4: TButton
    Left = 376
    Top = 392
    Width = 161
    Height = 25
    Caption = #1042#1099#1093#1086#1076
    TabOrder = 4
    OnClick = Button4Click
  end
  object Button5: TButton
    Left = 376
    Top = 296
    Width = 161
    Height = 25
    Caption = #1057#1086#1088#1090#1080#1088#1086#1074#1072#1090#1100' '#1087#1086' '#1076#1072#1090#1077' '#1087#1088#1080#1105#1084#1072
    TabOrder = 5
    OnClick = Button5Click
  end
  object OpenDialog1: TOpenDialog
    Filter = 'Database|*.rdb'
    Left = 200
    Top = 184
  end
  object SaveDialog1: TSaveDialog
    Filter = 'Database|*.rdb'
    Left = 264
    Top = 184
  end
end
