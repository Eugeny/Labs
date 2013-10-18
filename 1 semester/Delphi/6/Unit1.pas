unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, Buttons, ExtCtrls, myMath, Math;

type
  TForm1 = class(TForm)
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Edit1: TEdit;
    Edit2: TEdit;
    Edit3: TEdit;
    Edit4: TEdit;
    RadioGroup1: TRadioGroup;
    Memo1: TMemo;
    BitBtn1: TBitBtn;
    BitBtn2: TBitBtn;
    procedure BitBtn1Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  e:extended;
  Form1: TForm1;

implementation

{$R *.dfm}

function fx(x:extended) : extended;
begin
  fx := x * arctan(x) - ln(sqrt(1 + x * x));
end;

function sx(x:extended):extended;
var s,a: extended; n,k: integer;
begin
try
  a := sqr(x)/2; n := 1;
  s := a;
  while abs(a) > e do
    begin
      n := n + 1;
      a := -a * sqr(x) * (2*n - 3) / 2 / n;
      s := s + a;
    end;
  sx := s;
  except
     on EInvalidOp do
      k := MessageDlg('Неправильная операция с плавающей точкой. Продолжить вычисления?', mtError, [mbOk, mbCancel], 0);
      on EOverflow do
      k := MessageDlg('Перевыполнение в процессе выполнения операции. Продолжить вычисления?', mtError, [mbOk, mbCancel], 0);
     else
      k := MessageDlg('Неизвестная исключительная ситуация. Продолжить вычисления?', mtError, [mbOk, mbCancel], 0);
  end;

  if(k = mrNo) then
    halt(1)
  else if(k = mrYes) then
    result := 1;
end;



procedure TForm1.BitBtn1Click(Sender: TObject);
var xn,xk: extended;
m:integer;
begin
  memo1.clear;
  xn := StrToFloat(edit1.Text);
  xk := StrToFloat(edit2.Text);
  m := StrToInt(edit3.Text);
  e := StrToFloat(edit4.text);
  memo1.lines.add('xn = ' + floatToStrf(xn,ffFixed, 6, 4));
  memo1.lines.add('xk = ' + floatToStrf(xk,ffFixed, 6, 4));
  memo1.lines.add('m = ' + floatToStr(m));
  memo1.lines.add('e = ' + floatToStrf(e,ffFixed, 8, 5));
  if(RadioGroup1.ItemIndex = 0) then
  begin
    memo1.Lines.add('Рассчет f(x):');
    makeTable(fx,xn,xk,m,memo1);
  end else
  begin
     memo1.Lines.add('Рассчет s(x):');
     makeTable(sx,xn,xk,m,memo1);
  end;
end;

end.
