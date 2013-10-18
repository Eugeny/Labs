unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, Grids, StdCtrls, ComCtrls;

type

  TItem = record
    tip, model : string[30];
    date : TDate;
    ready : boolean;
  end;

  TMs = array[1..1] of TItem;
  PMs = ^TMs;

  TForm1 = class(TForm)
    StringGrid1: TStringGrid;
    GroupBox1: TGroupBox;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    DateTimePicker1: TDateTimePicker;
    ComboBox1: TComboBox;
    Edit1: TEdit;
    CheckBox1: TCheckBox;
    Button1: TButton;
    Button2: TButton;
    Button3: TButton;
    Button4: TButton;
    Button5: TButton;
    OpenDialog1: TOpenDialog;
    SaveDialog1: TSaveDialog;
    procedure FormCreate(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
  private
  public
    { Public declarations }
  end;

  PElem = ^TElem;
  TElem = record
    value : TItem;
    next : PElem;
  end;

  TList = class(TObject)
    pb,pe,pc : PElem;
    mt, n : integer;
    constructor Create;
    procedure AddBack(item : TItem);
    procedure DeleteBegin(var item : TItem);
  end;

  TMyList = class(TList)
    constructor Create;
    procedure writeToStringGrid(var sg : TStringGrid);
    procedure saveToFile(fname : string);
    procedure loadFromFile(fname : string);
    procedure sort;
  end;


var
  Form1: TForm1;
  list : TMyList;

implementation

constructor TList.Create;
begin
  Inherited Create;
  mt := sizeof(TElem);
  n := 0;
  pb := nil;
  pe := nil;
end;

procedure TList.AddBack(item : TItem);
var i : integer;
begin
  new(pc);
  pc^.value := item;
  pc^.next := nil;
  if(pe = nil) then
  begin
    pb := pc;
    pe := pc;
  end else
  begin
    pe^.next := pc;
    pe := pe^.next;
  end;
end;

procedure TList.DeleteBegin(var item:TItem);
var i : integer;
begin
  if(pb = nil) then exit;
  item := pb.value;
  pb := pb.next;
  freeMem(pb, sizeof(PElem));
end;

constructor TMyList.Create;
begin
  inherited Create;
end;

procedure TMyList.writeToStringGrid(var sg : TStringGrid);
var i : integer;
begin
  i := 0;
  pc := pb;
  while(pc <> nil) do
  begin
    inc(i);
    sg.Cells[0, i] := pc^.value.tip;
    sg.Cells[1, i] := pc^.value.model;
    sg.Cells[2, i] := DateToStr(pc^.value.date);
    if(pc^.value.ready) then
      sg.Cells[3, i] := 'Готов'
    else sg.Cells[3, i] := 'Не готов';
    pc := pc^.next;
  end;
end;

procedure TMyList.saveToFile(fname : string);
var fi : file of TItem;
begin
  assignFile(fi, fname);
  rewrite(fi);
  pc := pb;
  while(pc <> nil) do
  begin
    write(fi, pc^.value);
    pc := pc^.next;
  end;
  closeFile(fi);
end;

procedure TMyList.loadFromFile(fname : string);
var fi : file of TItem;
it : TItem;
begin
  pb := nil;
  pe := nil;
  assignFile(fi, fname);
  reset(fi);
  pc := pb;
  while(not eof(fi)) do
  begin
    read(fi, it);
    addBack(it);
  end;
  closeFile(fi);
end;

procedure TMyList.sort;
  procedure swap(a,b : PElem);
  var t : TItem;
  begin
    t := a^.value;
    a^.value := b^.value;
    b^.value := t;
  end;
  var pt : PElem;
  len,i : integer;
begin
  pc := pb;
  len := 0;
  while(pc <> nil) do
  begin
    inc(len);
    pc := pc^.next;
  end;
  for i := 1 to len do
  begin
    pc := pb;
    while(pc <> pe) do
    begin
      if(pc^.value.date > pc^.next^.value.date) then
        swap(pc, pc^.next);
      pc := pc^.next;
    end;
  end;

end;

{$R *.dfm}

procedure TForm1.FormCreate(Sender: TObject);
begin
  StringGrid1.Cells[0, 0] := 'Предмет';
  StringGrid1.Cells[1, 0] := 'Модель';
  StringGrid1.Cells[2, 0] := 'Дата приёма';
  StringGrid1.Cells[3, 0] := 'Состояние';
  list := TMyList.Create;
end;

procedure TForm1.Button4Click(Sender: TObject);
begin
  Application.Terminate;
end;

procedure TForm1.Button1Click(Sender: TObject);
var it: TItem;
begin
  it.tip := ComboBox1.Text;
  it.model := Edit1.Text;
  it.date := DateTimePicker1.Date;
  it.ready := CheckBox1.Checked;
  list.AddBack(it);
  list.writeToStringGrid(StringGrid1);
end;


procedure TForm1.Button2Click(Sender: TObject);
begin
  if(not saveDialog1.Execute) then exit;
  list.saveToFile(saveDialog1.FileName);
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
  if(not opendialog1.Execute) then exit;
  list.loadFromFile(openDialog1.FileName);
  list.writeToStringGrid(stringGrid1);
end;

procedure TForm1.Button5Click(Sender: TObject);
begin
  list.sort;
  list.writeToStringGrid(stringGrid1);
end;

end.
