unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, Grids, StdCtrls;

type
  TForm1 = class(TForm)
    Label1: TLabel;
    Edit1: TEdit;
    Label2: TLabel;
    Edit2: TEdit;
    Button1: TButton;
    StringGrid1: TStringGrid;
    Button2: TButton;
    procedure FormCreate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
  private
    procedure updateTableSize();
  public
    { Public declarations }
  end;

const NMAX = 100;
type
  Matrix = Array[1..NMAX,1..NMAX] of string;
var
  Form1: TForm1;
  n,m : integer;

implementation

{$R *.dfm}

procedure TForm1.FormCreate(Sender: TObject);
begin
  Randomize;
  n := 3;
  m := 3;
  stringGrid1.Cells[0,0] := 'Массив А:';
  updateTableSize();
end;

procedure TForm1.updateTableSize();
var i,j : integer;
begin
try
  edit1.text := intToStr(n);
  edit2.text := intToStr(m);
  stringGrid1.RowCount := n + 1;
  stringGrid1.ColCount := m + 1;
  for i := 1 to n do
    stringGrid1.cells[0,i] := 'j = ' + intToStr(i);
  for i := 1 to m do
    stringGrid1.Cells[i,0] := 'i = ' + intToStr(i);
  for i := 1 to n do
    for j := 1 to m do
      stringGrid1.Cells[j,i] := intToStr(random(100));
except
  on ERangeError do
  begin
    showMessage('Ошибка! Выход за пределы диапазона');
    exit;
  end;
  on EConvertError do
  begin
    showMessage('Ошибка! Невозможное преобразование');
    exit;
  end;
  else begin
    showMessage('Возникла неожиданная исключительная ситуация');
    exit;
  end;
end;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
  n := strToInt(edit1.Text);
  m := strToInt(edit2.Text);
  updateTableSize();
end;

procedure TForm1.Button2Click(Sender: TObject);
var mat:Matrix;
i,j : integer;
begin
  for i:=1 to n do
    for j:=1 to m do
      mat[j][i] := stringGrid1.Cells[i,j];
  for i:=1 to n do
    for j:=1 to m do
    begin
      if(i mod 2 = 1) then
        stringGrid1.Cells[j, i + 1] := mat[i,j]
      else
        stringGrid1.Cells[j, i - 1] := mat[i,j];
    end;
end;

end.
