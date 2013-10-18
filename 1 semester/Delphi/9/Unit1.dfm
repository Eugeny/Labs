object Form1: TForm1
  Left = 231
  Top = 139
  Width = 411
  Height = 426
  Caption = #1051#1072#1073'. '#1088#1072#1073' '#8470'9 '#1054#1079#1077#1088#1089#1082#1080#1081' '#1042'.'#1057
  Color = clBtnFace
  Font.Charset = DEFAULT_CHARSET
  Font.Color = clWindowText
  Font.Height = -11
  Font.Name = 'MS Sans Serif'
  Font.Style = []
  OldCreateOrder = False
  PixelsPerInch = 96
  TextHeight = 13
  object PageControl1: TPageControl
    Left = 0
    Top = 0
    Width = 395
    Height = 388
    ActivePage = TabSheet2
    Align = alClient
    TabOrder = 0
    object TabSheet1: TTabSheet
      Caption = #1056#1080#1089#1091#1085#1086#1082
      object Label9: TLabel
        Left = 224
        Top = 336
        Width = 26
        Height = 13
        Caption = 'BK = '
      end
      object Label8: TLabel
        Left = 120
        Top = 336
        Width = 27
        Height = 13
        Caption = 'CH = '
      end
      object Label7: TLabel
        Left = 8
        Top = 336
        Width = 27
        Height = 13
        Caption = 'AO = '
      end
      object Label6: TLabel
        Left = 232
        Top = 272
        Width = 22
        Height = 24
        Caption = 'Y3'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -19
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
      end
      object Label5: TLabel
        Left = 120
        Top = 272
        Width = 22
        Height = 24
        Caption = 'Y2'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -19
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
      end
      object Label4: TLabel
        Left = 8
        Top = 272
        Width = 22
        Height = 24
        Caption = 'Y1'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -19
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
      end
      object Label3: TLabel
        Left = 232
        Top = 248
        Width = 24
        Height = 24
        Caption = 'X3'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -19
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
      end
      object Label2: TLabel
        Left = 120
        Top = 248
        Width = 24
        Height = 24
        Caption = 'X2'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -19
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
      end
      object Label12: TLabel
        Left = 256
        Top = 336
        Width = 3
        Height = 13
      end
      object Label11: TLabel
        Left = 152
        Top = 336
        Width = 3
        Height = 13
      end
      object Label10: TLabel
        Left = 40
        Top = 336
        Width = 3
        Height = 13
      end
      object Label1: TLabel
        Left = 8
        Top = 248
        Width = 24
        Height = 24
        Caption = 'X1'
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -19
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
      end
      object Image1: TImage
        Left = -5
        Top = 0
        Width = 377
        Height = 241
      end
      object Edit6: TEdit
        Left = 264
        Top = 272
        Width = 49
        Height = 21
        TabOrder = 0
        Text = '1'
      end
      object Edit5: TEdit
        Left = 152
        Top = 272
        Width = 49
        Height = 21
        TabOrder = 1
        Text = '5'
      end
      object Edit4: TEdit
        Left = 40
        Top = 272
        Width = 49
        Height = 21
        TabOrder = 2
        Text = '1'
      end
      object Edit3: TEdit
        Left = 264
        Top = 248
        Width = 49
        Height = 21
        TabOrder = 3
        Text = '5'
      end
      object Edit2: TEdit
        Left = 152
        Top = 248
        Width = 49
        Height = 21
        TabOrder = 4
        Text = '3'
      end
      object Edit1: TEdit
        Left = 40
        Top = 248
        Width = 49
        Height = 21
        TabOrder = 5
        Text = '1'
      end
      object Button1: TButton
        Left = 96
        Top = 304
        Width = 153
        Height = 25
        Caption = #1053#1072#1088#1080#1089#1086#1074#1072#1090#1100
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -16
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 6
        OnClick = Button1Click
      end
    end
    object TabSheet2: TTabSheet
      Caption = #1043#1088#1072#1092#1080#1082
      ImageIndex = 1
      object Label13: TLabel
        Left = 8
        Top = 280
        Width = 84
        Height = 16
        Caption = #1053#1072#1095#1072#1083#1100#1085#1086#1077' '#1061
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
      end
      object Label14: TLabel
        Left = 8
        Top = 304
        Width = 75
        Height = 16
        Caption = #1050#1086#1085#1077#1095#1085#1086#1077' '#1061
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
      end
      object Label15: TLabel
        Left = 8
        Top = 328
        Width = 25
        Height = 16
        Caption = #1064#1072#1075
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
      end
      object Chart1: TChart
        Left = 0
        Top = 8
        Width = 377
        Height = 257
        BackWall.Brush.Color = clWhite
        BackWall.Brush.Style = bsClear
        Title.Text.Strings = (
          'y = xarctgx - ln(sqrt(1 + x * x))')
        Legend.Visible = False
        View3D = False
        TabOrder = 0
        object Series1: TLineSeries
          Marks.ArrowLength = 8
          Marks.Visible = False
          SeriesColor = clRed
          Dark3D = False
          Pointer.Draw3D = False
          Pointer.InflateMargins = True
          Pointer.Style = psRectangle
          Pointer.Visible = False
          XValues.DateTime = False
          XValues.Name = 'X'
          XValues.Multiplier = 1.000000000000000000
          XValues.Order = loAscending
          YValues.DateTime = False
          YValues.Name = 'Y'
          YValues.Multiplier = 1.000000000000000000
          YValues.Order = loNone
        end
      end
      object Edit7: TEdit
        Left = 104
        Top = 280
        Width = 41
        Height = 21
        TabOrder = 1
        Text = '2'
      end
      object Edit8: TEdit
        Left = 104
        Top = 304
        Width = 41
        Height = 21
        TabOrder = 2
        Text = '10'
      end
      object Edit9: TEdit
        Left = 104
        Top = 328
        Width = 41
        Height = 21
        TabOrder = 3
        Text = '1'
      end
      object BitBtn1: TBitBtn
        Left = 192
        Top = 296
        Width = 177
        Height = 33
        Caption = #1055#1086#1089#1090#1088#1086#1080#1090#1100' '#1075#1088#1072#1092#1080#1082
        Font.Charset = DEFAULT_CHARSET
        Font.Color = clWindowText
        Font.Height = -13
        Font.Name = 'MS Sans Serif'
        Font.Style = []
        ParentFont = False
        TabOrder = 4
        OnClick = BitBtn1Click
        Kind = bkOK
      end
    end
  end
end
