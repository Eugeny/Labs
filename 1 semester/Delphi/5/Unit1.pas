unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, Grids, Buttons;

type
  TForm1 = class(TForm)
    StringGrid1: TStringGrid;
    Label1: TLabel;
    StringGrid2: TStringGrid;
    Label2: TLabel;
    BitBtn1: TBitBtn;
    BitBtn2: TBitBtn;
    procedure FormCreate(Sender: TObject);
    procedure BitBtn2Click(Sender: TObject);
    procedure BitBtn1Click(Sender: TObject);
  private
    procedure DeleteNegative;
  public
    { Public declarations }
  end;

const N = 7;

type
TMas = array[1..1] of integer;
PMas = ^TMas;
var
  ms:PMas;
  msSize:integer;
  Form1: TForm1;


implementation

{$R *.dfm}

procedure TForm1.FormCreate(Sender: TObject);
var i:integer;
begin
  randomize;
  getMem(ms, N * sizeof(integer));
  for i:=1 to N do
  begin
    stringgrid1.Cells[i - 1, 0] := intToStr(random(1000) - 500);
    ms[i] := strToInt(stringGrid1.Cells[i - 1, 0]);
  end;
  msSize := N;
end;



procedure TForm1.BitBtn2Click(Sender: TObject);
begin
 FreeMem(ms, N * sizeof(integer));
 form1.close;
end;

procedure TForm1.DeleteNegative;
var i,j,z : integer;
begin
  i := 1;
  while(i <= msSize) do
  begin
      if(ms[i] >= 0) then
      begin
        inc(i);
        continue;
      end;
      for j := i + 1 to msSize do
        ms[j - 1] := ms[j];
      msSize := msSize - 1;

  end;
end;

procedure TForm1.BitBtn1Click(Sender: TObject);
var i:integer;
begin
  DeleteNegative;
  for i:=1 to msSize do
    stringGrid2.Cells[i - 1, 0] := intToStr(ms[i]);
end;

end.
