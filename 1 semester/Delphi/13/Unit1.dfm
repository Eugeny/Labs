object Form1: TForm1
  Left = 192
  Top = 124
  Width = 679
  Height = 450
  Caption = #1051#1072#1073'. '#1088#1072#1073' '#8470'13 '#1054#1079#1077#1088#1089#1082#1080#1081' '#1042'.'#1057
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
    Left = 440
    Top = 16
    Width = 205
    Height = 16
    Caption = #1057#1086#1089#1090#1086#1103#1085#1080#1077' '#1079#1072#1082#1072#1079#1086#1074' '#1085#1072' '#1089#1077#1075#1086#1076#1085#1103':'
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
  end
  object StringGrid1: TStringGrid
    Left = 8
    Top = 16
    Width = 417
    Height = 209
    ColCount = 4
    FixedCols = 0
    RowCount = 8
    TabOrder = 0
    ColWidths = (
      112
      119
      92
      82)
  end
  object Memo1: TMemo
    Left = 440
    Top = 40
    Width = 217
    Height = 177
    ReadOnly = True
    TabOrder = 1
  end
  object GroupBox1: TGroupBox
    Left = 16
    Top = 240
    Width = 377
    Height = 169
    Caption = #1044#1086#1073#1072#1074#1080#1090#1100' '#1085#1086#1074#1099#1081' '#1087#1088#1077#1076#1084#1077#1090
    Font.Charset = DEFAULT_CHARSET
    Font.Color = clWindowText
    Font.Height = -13
    Font.Name = 'MS Sans Serif'
    Font.Style = []
    ParentFont = False
    TabOrder = 2
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
    Left = 440
    Top = 312
    Width = 180
    Height = 25
    Caption = #1057#1086#1093#1088#1072#1085#1080#1090#1100' '#1074' '#1092#1072#1081#1083
    TabOrder = 3
    OnClick = Button2Click
  end
  object Button3: TButton
    Left = 440
    Top = 344
    Width = 180
    Height = 25
    Caption = #1047#1072#1075#1088#1091#1079#1080#1090#1100' '#1080#1079' '#1092#1072#1081#1083#1072
    TabOrder = 4
    OnClick = Button3Click
  end
  object Button4: TButton
    Left = 440
    Top = 376
    Width = 180
    Height = 25
    Caption = #1042#1099#1093#1086#1076
    TabOrder = 5
    OnClick = Button4Click
  end
  object Button5: TButton
    Left = 440
    Top = 280
    Width = 180
    Height = 25
    Caption = #1057#1086#1088#1090#1080#1088#1086#1074#1072#1090#1100' '#1087#1086' '#1076#1072#1090#1077' '#1087#1088#1080#1105#1084#1072
    TabOrder = 6
    OnClick = Button5Click
  end
  object Button6: TButton
    Left = 440
    Top = 248
    Width = 180
    Height = 25
    Caption = #1059#1076#1072#1083#1080#1090#1100' '#1074#1099#1076#1077#1083#1077#1085#1085#1099#1077' '#1087#1088#1077#1076#1084#1077#1090#1099
    TabOrder = 7
    OnClick = Button6Click
  end
  object OpenDialog1: TOpenDialog
    Filter = 'Database|*.rdb'
    Left = 520
    Top = 224
  end
  object SaveDialog1: TSaveDialog
    Filter = 'Database|*.rdb'
    Left = 464
    Top = 224
  end
end
