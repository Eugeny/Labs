unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, unit2, StdCtrls;

type
  TForm1 = class(TForm)
    ListBox1: TListBox;
    Label1: TLabel;
    Button1: TButton;
    Button2: TButton;
    Button3: TButton;
    procedure Button1Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  list : TList;
  selectedItems : array[1..100] of integer;
  selectedIndexes : array[1..100] of integer;
  selectedCount : integer;
implementation

{$R *.dfm}

procedure TForm1.Button1Click(Sender: TObject);
var i,l : integer;
begin
  randomize;
  l := random(5) + 10;
  list.Clear;
  for i := 1 to l do
    list.add_back(random(100));
  list.printToListBox(listBox1);
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  list := TList.create;
end;

procedure TForm1.Button2Click(Sender: TObject);
var i : integer;
begin
  selectedCount := 0;
  for i := 0 to ListBox1.Count - 1 do
    if(listBox1.Selected[i]) then
    begin
      inc(selectedCount);
      selectedIndexes[selectedCount] := i;
      SelectedItems[selectedCount] := StrToInt(ListBox1.Items[i]);
    end;
  for i := 1 to selectedCount do
  begin
    list.delete(selectedIndexes[i] - i - 1, 1);
  end;
  list.printToListBox(listBox1);
end;

procedure TForm1.Button3Click(Sender: TObject);
var i, selStart : integer;
begin
  for selStart := 0 to ListBox1.Count - 1 do
    if(ListBox1.selected[selStart]) then
      break;
  if(selStart >= listBox1.Count) then exit;
  for i := 1 to selectedCount do
    list.add(selStart + i - 2, selectedItems[i]);
  list.printToListBox(listBox1);
end;

end.
