unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, Unit2, StdCtrls, Grids;

type
  TForm1 = class(TForm)
    StringGrid1: TStringGrid;
    Label1: TLabel;
    Edit1: TEdit;
    Edit2: TEdit;
    Label2: TLabel;
    Edit3: TEdit;
    Button1: TButton;
    Button2: TButton;
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  pz : Tpz;
implementation

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
begin
  pz := Tpz.create;
  Edit2.text:=pz.OBP(Edit1.Text);
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
  pz := Tpz.create;
  edit2.Text := '';
  Edit2.text:=pz.OBP(Edit1.Text);
  with pz,StringGrid1 do begin
    mszn[Cells[0,1][1]]:=StrToFloat(Cells[1,1]);
    mszn[Cells[0,2][1]]:=StrToFloat(Cells[1,2]);
    mszn[Cells[0,3][1]]:=StrToFloat(Cells[1,3]);
    mszn[Cells[0,4][1]]:=StrToFloat(Cells[1,4]);
    mszn[Cells[0,5][1]]:=StrToFloat(Cells[1,5]);
    mszn[Cells[0,6][1]]:=StrToFloat(Cells[1,6]);
    mszn[Cells[0,7][1]]:=StrToFloat(Cells[1,7]);
  end;
  Edit3.text:=FloatToStrF(pz.AV(Edit2.Text),ffFixed, 2,2);
end;


procedure TForm1.FormCreate(Sender: TObject);
begin
  pz:=Tpz.create;
  StringGrid1.Cells[0,0] := 'Name';
  StringGrid1.Cells[1,0] := 'Znach';
  StringGrid1.Cells[0,1] := 'k';
  StringGrid1.Cells[1,1] := '0.1';
  StringGrid1.Cells[0,2] := 'm';
  StringGrid1.Cells[1,2] := '5.2';
  StringGrid1.Cells[0,3] := 'n';
  StringGrid1.Cells[1,3] := '2.4';
  StringGrid1.Cells[0,4] := 'j';
  StringGrid1.Cells[1,4] := '6';
  StringGrid1.Cells[0,5] := 'i';
  StringGrid1.Cells[1,5] := '8.4';
  StringGrid1.Cells[0,6] := 'v';
  StringGrid1.Cells[1,6] := '5.3';
  StringGrid1.Cells[0,7] := 'u';
  StringGrid1.Cells[1,7] := '5.4';
end;

end.
