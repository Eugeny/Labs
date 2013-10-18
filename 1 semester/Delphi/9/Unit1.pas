unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls, ComCtrls, TeEngine, Series, Buttons,
  TeeProcs, Chart, Math;

type
  TForm1 = class(TForm)
    PageControl1: TPageControl;
    TabSheet1: TTabSheet;
    Label9: TLabel;
    Label8: TLabel;
    Label7: TLabel;
    Label6: TLabel;
    Label5: TLabel;
    Label4: TLabel;
    Label3: TLabel;
    Label2: TLabel;
    Label12: TLabel;
    Label11: TLabel;
    Label10: TLabel;
    Label1: TLabel;
    Image1: TImage;
    Edit6: TEdit;
    Edit5: TEdit;
    Edit4: TEdit;
    Edit3: TEdit;
    Edit2: TEdit;
    Edit1: TEdit;
    Button1: TButton;
    TabSheet2: TTabSheet;
    Chart1: TChart;
    Label13: TLabel;
    Label14: TLabel;
    Label15: TLabel;
    Edit7: TEdit;
    Edit8: TEdit;
    Edit9: TEdit;
    BitBtn1: TBitBtn;
    Series1: TLineSeries;
    procedure Button1Click(Sender: TObject);
    procedure BitBtn1Click(Sender: TObject);
  private
    function MaxValue(a,b,c:integer):integer;
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

function TForm1.MaxValue(a,b,c:integer):integer;
var r : integer;
begin
    r := a;
    if(b > r) then
      r := b;
    if(c > r) then
      r := c;
    Result := r;
end;

function Len(x1,x2,y1,y2:integer) : extended;
begin
    result := sqrt(sqr(x1-x2) + sqr(y1-y2));
end;

function Square(a,b,c:extended) : extended;
var p : extended;
begin
  p := (a + b + c) / 2;
  result := sqrt(p*(p-a)*(p-b)*(p-c));
end;

procedure TForm1.Button1Click(Sender: TObject);
var dx,dy,ab,ac,bc,_ab,_ac,_bc,_bk,_ch,_ao,_s,s,bk,ch,ao,_ak, _bo, _ah : extended;
ax,bx,cx,ay,by,cy,a,b,_x1,_x2,_x3,_y1,_y2,_y3 : integer;
begin

  image1.Canvas.Rectangle(0,0, Image1.Width, Image1.Height);

  _x1 := strToInt(edit1.Text);
  _x2 := strToInt(edit2.Text);
  _x3 := strToInt(edit3.Text);
  _y1 := strToInt(edit4.Text);
  _y2 := strToInt(edit5.Text);
  _y3 := strToInt(edit6.Text);


  _ab := len(_x1,_x2,_y1,_y2);
  _bc := len(_x2,_x3,_y2,_y3);
  _ac := len(_x1,_x3,_y1,_y3);
  _s := square(_ab,_bc,_ac);
  _ao := 2 * _s / _bc;
  _bk := 2 * _s / _ac;
  _ch := 2 * _s / _ab;

  Label10.Caption := floatToStrF(_ao,ffFixed,2,2);
  Label11.Caption := floatToStrF(_ch,ffFixed,2,2);
  Label12.Caption := floatToStrF(_bk,ffFixed,2,2);



  dx := (image1.Width - 20) / MaxValue(_x1,_x2,_x3);
  dy := (image1.Height - 20) / MaxValue(_y1,_y2,_y3);

  ax := Round(_x1 * dx);
  bx := Round(_x2 * dx);
  cx := Round(_x3 * dx);
  ay := Round(_y1 * dy);
  by := Round(_y2 * dy);
  cy := Round(_y3 * dy);

  image1.Canvas.MoveTo(ax, ay);
  image1.Canvas.LineTo(bx, by);
  image1.Canvas.LineTo(cx, cy);
  image1.Canvas.LineTo(ax, ay);

  image1.Canvas.TextOut(ax - 5, ay - 5, 'A');
  image1.Canvas.TextOut(bx - 5, by + 5, 'B');
  image1.Canvas.TextOut(cx - 5, cy - 5, 'C');
end;

function f(x : extended) : extended;
begin
  result := x * arctan(x) - ln(sqrt(1 + x * x));
end;

procedure TForm1.BitBtn1Click(Sender: TObject);
var xn,xk,xd,x : extended;
begin
  xn := strToFloat(edit7.Text);
  xk := strToFloat(edit8.Text);
  xd := strToFloat(edit9.Text);

  Chart1.BottomAxis.Automatic := false;
  Chart1.BottomAxis.Maximum := xk;
  Chart1.BottomAxis.Minimum := xn;
  Chart1.LeftAxis.Automatic := false;
  Chart1.LeftAxis.Maximum := Max(f(xn), f(xk));
  Chart1.LeftAxis.Minimum := Min(f(xn), f(xk));

  Chart1.SeriesList[0].Clear;
  x := xn;
  while(x <= xk) do
  begin
    Chart1.SeriesList[0].AddXY(x, f(x));
    x := x + xd;
  end;

end;

end.
