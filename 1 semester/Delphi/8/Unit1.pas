unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, Buttons;


     type TItem = record
      date : string[30];
      townCode : string[30];
      townName : string[30];
      time : integer;
      cost : integer;
      numberPhoneTo, numberPhoneFrom : integer;
  end;

type
  TForm1 = class(TForm)
    Label1: TLabel;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    Label6: TLabel;
    Edit1: TEdit;
    Edit2: TEdit;
    Edit3: TEdit;
    Edit4: TEdit;
    Edit5: TEdit;
    Edit6: TEdit;
    BitBtn1: TBitBtn;
    Memo1: TMemo;
    Button1: TButton;
    Button2: TButton;
    Button3: TButton;
    Button4: TButton;
    Label7: TLabel;
    Edit7: TEdit;
    OpenDialog1: TOpenDialog;
    SaveDialog1: TSaveDialog;
    Button5: TButton;
    procedure Button4Click(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure BitBtn1Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
  private
    procedure writeDataToMemo;
    procedure addItemToMemo(index : integer;item : TItem);
  public
    { Public declarations }
  end;


var
  Form1: TForm1;
  fz : file of TItem;
  data : array[1..100] of TItem;
  itemsCount : integer;
  fileName:string;

implementation

{$R *.dfm}

procedure TForm1.Button4Click(Sender: TObject);
begin
  Application.Terminate;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
  Memo1.Clear;
  fileName := '';
end;

procedure TForm1.addItemToMemo(index : integer; item : TItem);
begin
  memo1.Lines.add(intToStr(index) + ' ' + item.date + ' ' + intToStr(item.numberPhoneFrom) +
  ' ---> ' + intToStr(item.numberPhoneTo) + '(' + item.townName + ' - ' + item.townCode + ') - ' +
  intToStr(item.time) + ' seconds. ' + intToStr(item.cost) + ' cents for 1 second');
end;

procedure TForm1.writeDataToMemo;
var i,j : integer;
item : TItem;
townNames : array[1..100] of string;
townSums, townTimes : array[1..100] of integer;
townsCount : integer;
begin
  townsCount := 0;
  memo1.clear;
  for i := 1 to itemsCount do
  begin
    for j := 1 to townsCount + 1 do
      if(townNames[j] = data[i].townName) then
        break;
    if(j > townsCount) then
    begin
      inc(townsCount);
      townNames[townsCount] := data[i].townName;
      townSums[townsCount] := data[i].cost * data[i].time;
      townTimes[townsCount] := data[i].time;
    end else
    begin
      inc(townSums[j], data[i].cost * data[i].time);
      inc(townTimes[j], data[i].time);
    end;
    AddItemToMemo(i, data[i]);
  end;
  memo1.Lines.Add('==================================================================');
  for i := 1 to townsCount do
    memo1.Lines.Add(intToStr(i) + ' - ' + townNames[i] + ' : summa = ' +
    intToStr(townSums[i]) + ', all time = ' + intToStr(townTimes[i]));
end;


procedure TForm1.Button2Click(Sender: TObject);
begin
  itemsCount := 0;
  OpenDialog1.Title := 'Загрузить базу из файла';
  if(OpenDialog1.Execute) then
  begin
    filename := openDialog1.FileName;
    assignFile(fz, filename);
    reset(fz);
    while(not eof(fz)) do
    begin
      inc(itemsCount);
      Read(fz, data[itemsCount]);
    end;
    writeDataToMemo;
    closefile(fz);
  end;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  itemsCount := 0;
  fileName := '';
end;

procedure TForm1.BitBtn1Click(Sender: TObject);
begin
  inc(itemsCount);
  with data[itemsCount] do
  begin
      date := edit1.Text;
      townCode := edit2.Text;
      townName := edit3.Text;
      time := strToInt(edit4.Text);
      cost := strToInt(edit5.Text);
      numberPhoneFrom := strToInt(edit6.Text);
      numberPhoneTo := strToInt(edit7.Text);
  end;
  writeDataToMemo;
  edit1.text := '';
  edit2.text := '';
  edit3.text := '';
  edit4.text := '';
  edit5.text := '';
  edit6.text := '';
  edit7.text := '';
end;

procedure TForm1.Button3Click(Sender: TObject);
var i : integer;
begin
  if(fileName = '') then
    Button5Click(sender)
  else begin
    AssignFile(fz, fileName);
    rewrite(fz);
    for i:=1 to itemsCount do
      write(fz, data[i]);
    closeFile(fz);
  end;
end;

procedure TForm1.Button5Click(Sender: TObject);
begin
  SaveDialog1.Title := 'Сохранить базу в файл';
  if(saveDialog1.Execute) then
  begin
    filename := saveDialog1.FileName;
    Button3Click(sender);
  end;
end;

end.
