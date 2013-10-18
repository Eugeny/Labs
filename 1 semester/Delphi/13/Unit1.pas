unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ComCtrls, Grids;

type TItem = record
  tip : string[30];
  model : string[30];
  date : TDate;
  ready : boolean;
end;

type
  TForm1 = class(TForm)
    StringGrid1: TStringGrid;
    Memo1: TMemo;
    Label1: TLabel;
    GroupBox1: TGroupBox;
    DateTimePicker1: TDateTimePicker;
    Label2: TLabel;
    ComboBox1: TComboBox;
    Label3: TLabel;
    Label4: TLabel;
    Edit1: TEdit;
    CheckBox1: TCheckBox;
    Button1: TButton;
    Button2: TButton;
    Button3: TButton;
    Button4: TButton;
    OpenDialog1: TOpenDialog;
    SaveDialog1: TSaveDialog;
    Button5: TButton;
    Button6: TButton;
    procedure FormCreate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button6Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
  private
    procedure updateGrid;
    procedure updateInformation;
    procedure deleteItem(index : integer);
    procedure quickSort(l, r : integer);
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  items : array[1..100] of TItem;
  itemsCount : integer;
  fi : file of TItem;
implementation

{$R *.dfm}

procedure TForm1.updateInformation;
var tips : array[1..100] of string;
tipsCount : integer;
i,j : integer;
found : boolean;
text : string;
begin
  memo1.Lines.Clear;
  tipsCount := 0;
  for i := 1 to itemsCount do
  begin
    found := false;
    for j := 1 to tipsCount do
      if(tips[j] = items[i].tip) then
      begin
        found := true;
        break;
      end;
    if(not found) then
    begin
      inc(tipsCount);
      tips[tipsCount] := items[i].tip;
    end;
  end;

  for i := 1 to tipsCount do
  begin
    memo1.Lines.add(tips[i] + ':');
    for j := 1 to itemsCount do
      if(items[j].tip = tips[i]) then
      begin
        text := DateToStr(items[j].date) + ' - ' + items[j].model;
        if(items[j].ready) then
          text := text + '(Готов)';
        memo1.lines.add(text);
      end;
  end;

end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  itemsCount := 0;
  StringGrid1.Cells[0,0] := 'Предмет';
  StringGrid1.Cells[1,0] := 'Модель';
  StringGrid1.Cells[2,0] := 'Дата приёма';
  StringGrid1.Cells[3,0] := 'Состояние';
  updateGrid;
end;

procedure TForm1.updateGrid;
var i : integer;
begin
  stringGrid1.RowCount := itemsCount + 1;
  for i := 1 to itemsCount do
  begin
    stringGrid1.Cells[0,i] := items[i].tip;
    stringGrid1.Cells[1,i] := items[i].model;
    stringGrid1.Cells[2,i] := DateToStr(items[i].date);
    if(items[i].ready) then
      stringGrid1.Cells[3,i] := 'Готов'
    else stringGrid1.Cells[3,i] := 'Не готов';
  end;
  updateInformation;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
  inc(itemsCount);
  items[itemsCount].tip := comboBox1.Text;
  items[itemsCount].model := edit1.Text;
  items[itemsCount].date := DateTimePicker1.Date;
  items[itemsCount].ready := CheckBox1.Checked;
  updateGrid;
end;

procedure TForm1.Button2Click(Sender: TObject);
var fname : string;
i : integer;
begin
  if(not saveDialog1.Execute) then exit;
  fname := SaveDialog1.FileName;
  AssignFile(fi, fname);
  rewrite(fi);
  for i := 1 to itemsCount do
    write(fi, items[i]);
  closeFile(fi);
end;

procedure TForm1.Button4Click(Sender: TObject);
begin
   application.Terminate;
end;

procedure TForm1.Button3Click(Sender: TObject);
var fname :string;
begin
  if(not openDialog1.Execute) then exit;
  fname := openDialog1.FileName;
  assignFile(fi, fname);
  reset(fi);
  while(not eof(fi)) do
  begin
    inc(itemsCount);
    read(fi, items[itemsCount]);
  end;
  closeFile(fi);
  updateGrid;
end;

procedure TForm1.Button6Click(Sender: TObject);
var l,r,i : integer;
begin
  l := stringGrid1.Selection.Top;
  r := stringGrid1.Selection.Bottom;
  for i := l to r do
    deleteItem(i);
  updateGrid;
end;

procedure TForm1.deleteItem(index : integer);
var i : integer;
begin
  for i := index to itemsCount - 1 do
    items[i] := items[i + 1];
  dec(itemsCount);
end;

procedure TForm1.Button5Click(Sender: TObject);
begin
  quickSort(1, itemsCount);
  updateGrid;
end;

procedure TForm1.quickSort(l, r : integer);
var
   Lo, Hi: Integer;
   t : TItem;
   pivot : TDate;
begin
   Lo := l;
   Hi := r;
   Pivot := items[(Lo + Hi) div 2].date;
   repeat
     while items[Lo].date < Pivot do Inc(Lo) ;
     while items[Hi].date > Pivot do Dec(Hi) ;
     if Lo <= Hi then
     begin
       T := items[Lo];
       items[Lo] := items[Hi];
       items[Hi] := T;
       Inc(Lo) ;
       Dec(Hi) ;
     end;
   until Lo > Hi;
   if Hi > l then quickSort(l, Hi) ;
   if Lo < r then quickSort(Lo, r) ;
end;


end.
