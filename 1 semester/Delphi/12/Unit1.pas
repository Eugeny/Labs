unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, ExtCtrls, Grids, Buttons;

type TItem = record
  w : integer;
  c : integer;
end;

type SByte = set of byte;

type
  TForm1 = class(TForm)
    StringGrid1: TStringGrid;
    Label1: TLabel;
    RadioGroup1: TRadioGroup;
    Label2: TLabel;
    Edit1: TEdit;
    Label3: TLabel;
    Label4: TLabel;
    Edit2: TEdit;
    Edit3: TEdit;
    Button1: TButton;
    Button2: TButton;
    Memo1: TMemo;
    Label5: TLabel;
    Button3: TButton;
    BitBtn1: TBitBtn;
    Label6: TLabel;
    Label7: TLabel;
    Label8: TLabel;
    Label9: TLabel;
    Label10: TLabel;
    Label11: TLabel;
    procedure FormCreate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure BitBtn1Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
  private
    procedure UpdateGrid;
    procedure VG(wMax : integer);
    procedure deleteItem(index : integer);
    procedure LOG(str : string);
    procedure PP(wMax : integer);
    procedure MS(wMax : integer);
    procedure MV(wMax : integer);
    procedure SS(wMax : integer);
    procedure SP(wMax : integer);
    function setToStr(st : SByte; count : integer) : string;
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  items : array[1..100] of TItem;
  itemsCount : integer;
  maxCost : integer;
  resultItems : array[1..100] of integer;
  resultItemsCount : integer;

implementation

{$R *.dfm}

procedure TForm1.FormCreate(Sender: TObject);
var i : integer;
begin
  StringGrid1.Cells[1,0] := 'Вес';
  StringGrid1.Cells[2,0] := 'Цена';
  itemsCount := 5;
  items[1].w := 16; items[1].c := 18;
  items[2].w := 11; items[2].c := 20;
  items[3].w := 12; items[3].c := 17;
  items[4].w := 13; items[4].c := 19;
  items[5].w := 14; items[5].c := 13;
  updateGrid;
end;

procedure TForm1.UpdateGrid;
var i, j: integer;
begin
   StringGrid1.RowCount := itemsCount + 1;
   for i := 1 to StringGrid1.RowCount do
   begin
    stringGrid1.Cells[0,i] := intToStr(i);
    stringGrid1.Cells[1,i] := intToStr(items[i].w);
    stringGrid1.Cells[2,i] := intToStr(items[i].c);
   end;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
  if((edit2.Text = '') or (edit3.Text = '')) then exit;
  inc(itemsCount);
  items[itemsCount].w := strToInt(edit2.Text);
  items[itemsCount].c := strToInt(edit3.Text);
  updateGrid;
  edit2.Text := '';
  edit3.Text := '';
end;

procedure TForm1.Button2Click(Sender: TObject);
var f, t, i : integer;
begin
  f := stringGrid1.Selection.Top;
  t := stringGrid1.Selection.Bottom;
  for i := f to t do
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

procedure TForm1.VG(wMax : integer);
var _ost, i : integer;
  st,resSt : set of byte;
  procedure recVG(index, curw, ost : integer);
  var _curw, _ost : integer;
  flag : boolean;
  begin
    flag := false;
      LOG('');
    LOG('Текущий вариант : ' + setToStr(st, itemsCount) + ' (index = ' + intToStr(index) +')');
    if(index = itemsCount + 1) then
    begin
      LOG('Найдено решение! Стоимость : ' + intToStr(ost));
      if(ost > maxCost) then
      begin
        maxCost := ost;
        resSt := st;
      end;
        LOG('');
      exit;
    end;
    _curw := curw + items[index].w;
    if(_curw <= wMax) then
    begin
      flag := true;
      LOG('Взять ' + intToStr(index) +'ый предмет');
      include(st, index);
      recVG(index + 1, _curW, ost);
      Exclude(st, index);
    end;
    _ost := ost - items[index].c;
    if(_ost > maxCost) then
    begin
      if(flag) then      LOG('Текущий вариант : ' + setToStr(st, itemsCount) + ' (index = ' + intToStr(index) +')');      flag := true;
      LOG('Не брать ' + intToStr(index) +'ый предмет');
      recVG(index + 1, curw, _ost);
    end;
    if(flag = false) then
    begin
      LOG('Вариант не подходит');
      LOG('');
    end;
  end;
begin
  _ost := 0;
  for i := 1 to itemsCount do
  begin
    inc(_ost, items[i].c);
    exclude(st, i);
  end;
  recVG(1, 0, _ost);
  for i := 1 to itemsCount do
    if(i in resSt) then
    begin
      inc(resultItemsCount);
      resultItems[resultItemsCount] := i;
    end;
end;

procedure TForm1.BitBtn1Click(Sender: TObject);
var t,wMax,i : integer;
begin
  t := GetTickCount;
  memo1.Clear;
  label8.Caption := '';
  label9.Caption := '';
  resultItemsCount := 0;
  maxCost := 0;
  wMax := StrToInt(Edit1.Text);
  case RadioGroup1.ItemIndex of
    0 : VG(wMax);
    1 : PP(wMax);
    2 : MS(wMax);
    3 : MV(wMax);
    4 : SS(wMax);
    5 : SP(wMax);
  end;
  Label8.Caption := intToStr(maxCost);
  for i := 1 to resultItemsCount do
  begin
    label9.Caption := label9.Caption + intToStr(resultItems[i]);
    if(i < resultItemsCount) then
      label9.Caption := label9.Caption + ', ';
  end;
  label11.Caption := intToStr(GetTickCount - t);
end;

procedure TForm1.LOG(str : string);
begin
  memo1.Lines.add(str);
end;

function TForm1.setToStr(st : SByte; count : integer) : string;
var i : integer;
first : boolean;
begin
  first := false;
  result := '[';
  for i := 1 to count do
    if(i in st) then
    begin
      if(first = false) then
      begin
        first := true;
        result := result + intToStr(i);
      end else
        result := result + ', ' + intToStr(i);
    end;
  result := result + ']';
end;


procedure TForm1.PP(wMax : integer);
var  i : integer;
  st,resSt : set of byte;
  procedure recPP(index : integer);
  var w, c,i  : integer;
  flag : boolean;
  begin
    LOG('');
    LOG('Текущий вариант : ' + setToStr(st, itemsCount));
    if(index = itemsCount + 1) then
    begin
      for i := 1 to itemsCount do
        if(i in st) then
        begin
          inc(w, items[i].w);
          inc(c, items[i].c);
        end;
      LOG('Найдено решение! Стоимость : ' + intToStr(c) + ' Вес : ' + intToStr(w));
      if((w <= wMax) and (c > maxCost)) then
      begin
        resSt := st;
        maxCost := c;
      end;
      exit;
    end;
    Include(st, index);
    LOG('Беру ' + intToStr(index) + 'ый предмет');
    recPP(index + 1);
    Exclude(st, index);
    LOG('');
    LOG('Текущий вариант : ' + setToStr(st, itemsCount));
    LOG('Не беру ' + intToStr(index) + 'ый предмет');
    recPP(index + 1);
  end;
begin
  for i := 1 to itemsCount do
    exclude(st, i);
  recPP(1);
  for i := 1 to itemsCount do
    if(i in resSt) then
    begin
      inc(resultItemsCount);
      resultItems[resultItemsCount] := i;
    end;
end;

procedure TForm1.MS(wMax : integer);
var i,w,j : integer;
st : set of byte;
  function getMax : integer;
  var i,ma : integer;
  begin
    result := 0;
    ma := 0;
    for i := 1 to itemsCount do
      if((not( i in st)) and (items[i].c > ma)) then
      begin
         ma := items[i].c;
         result := i;
      end;
  end;
begin
  for i := 1 to itemsCount do
    exclude(st, i);
  w := 0;
  for i := 1 to itemsCount do
  begin
    j := getMax;
    LOG('Максимальная стоимость у ' + intToStr(j) + 'ого предмета - ' + intToStr(items[j].c));
    st := st + [j];
    if(items[j].w + w <= wMax) then
    begin
      LOG('Беру');
      inc(w, items[j].w);
      inc(maxCost, items[j].c);
      inc(resultItemsCount);
      resultItems[resultItemsCount] := j;
    end else
      LOG('Не беру - предмет не влазит');
    LOG('');
  end;
end;

procedure TForm1.MV(wMax : integer);
var i,w,j : integer;
st : set of byte;
  function getMin : integer;
  var i,mi : integer;
  begin
    result := 0;
    mi := 99999999;
    for i := 1 to itemsCount do
      if((not( i in st)) and (items[i].w < mi)) then
      begin
         mi := items[i].w;
         result := i;
      end;
  end;
begin
  for i := 1 to itemsCount do
    exclude(st, i);
  w := 0;
  for i := 1 to itemsCount do
  begin
    j := getMin;
    LOG('Минимальный вес у ' + intToStr(j) + 'ого предмета - ' + intToStr(items[j].w));
    st := st + [j];
    if(items[j].w + w <= wMax) then
    begin
      LOG('Беру');
      inc(w, items[j].w);
      inc(maxCost, items[j].c);
      inc(resultItemsCount);
      resultItems[resultItemsCount] := j;
    end else
      LOG('Не беру - предмет не влазит');
    LOG('');
  end;
end;

procedure TForm1.SS(wMax : integer);
var i,w,j : integer;
st : set of byte;
  function getMaxSS : integer;
  var i : integer;
  ma : extended;
  begin
    result := 0;
    ma := 0;
    for i := 1 to itemsCount do
      if((not( i in st)) and (items[i].c / items[i].w > ma)) then
      begin
         ma := items[i].c / items[i].w;
         result := i;
      end;
  end;
begin
  for i := 1 to itemsCount do
    exclude(st, i);
  w := 0;
  for i := 1 to itemsCount do
  begin
    j := getMaxSS;
    LOG('Максимальное cоотношение цена/вес у ' + intToStr(j) + 'ого предмета - ' + floatToStrF(items[j].c / items[j].w, ffFixed, 6, 3));
    st := st + [j];
    if(items[j].w + w <= wMax) then
    begin
      LOG('Беру');
      inc(w, items[j].w);
      inc(maxCost, items[j].c);
      inc(resultItemsCount);
      resultItems[resultItemsCount] := j;
    end else
      LOG('Не беру - предмет не влазит');
    LOG('');
  end;
end;

procedure TForm1.SP(wMax : integer);
var resSt,st : set of byte;
i,cur : integer;
  function get : integer;
  var i, j, z, ind,w : integer;
  begin
    result := 0;
    w := 0;
    randomize;
    for i := 1 to itemsCount do
      exclude(st, i);
    for i := 1 to itemsCount do
    begin
      j := random(itemsCount - i + 1) + 1;
      ind := 0;
      for z := 1 to itemsCount do
      begin
        if(not(j in st)) then
          inc(ind);
        if(ind = j) then
          break;
      end;
      if(items[z].w + w < wMax) then
      begin
        inc(w,items[z].w);
        inc(result, items[z].c);
        include(st, j);
      end;
    end;
  end;
begin
  for i := 1 to 100 do
  begin
    cur := get;
    LOG('Найден вариант : ' + setToStr(st, itemsCount) + ' - ' + intToStr(cur));
    if(cur > maxCost) then
    begin
      maxCost := cur;
      resSt := st;
    end;
  end;
  for i := 1 to itemsCount do
    if(i in resSt) then
    begin
      inc(resultItemsCount);
      resultItems[resultItemsCount] := i;
    end;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
  Application.Terminate;
end;

end.
