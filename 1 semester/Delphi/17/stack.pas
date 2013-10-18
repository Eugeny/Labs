unit Stack;

interface
uses
  StdCtrls,SysUtils;
type

TInf = char;

PElem = ^TElem;
TElem = record
  value : TInf;
  next : PElem;
end;


TStack = class(TObject)
  top, temp : PElem;
  procedure Adds(item : TInf);
  procedure Reads(var item : TInf);
  procedure print(var memo : TMemo);
  procedure addAfter(elem : PElem; item : TInf);
  procedure addBefore(elem : PElem; item : TInf);
  procedure readAfter(elem : PElem; var item : TInf);
  procedure poisk(key : char; var item : TInf);
  procedure sort;
  constructor Create;
end;

implementation

constructor TStack.Create;
begin
  inherited Create;
  top := nil;
end;

procedure TStack.Adds(item : TInf);
var elem : PElem;
begin
  new(elem);
  elem^.value := item;
  elem^.next := nil;
  if(top <> nil) then
    elem^.next := top;
  top := elem;
end;

procedure TStack.Reads(var item : Tinf);
begin
  if(top = nil) then exit;
  item := top^.value;
  temp := top;
  top := top^.next;
  dispose(temp);
end;

procedure TStack.print(var memo : TMemo);
var str : string;
begin
  temp := top;
  str := 'Stack : [';
  while(temp <> nil) do
  begin
    if(temp = top) then
      str := str + temp^.value
    else str := str + ', ' + temp^.value;
    temp := temp^.next;
  end;
  str := str + ']';
  memo.Lines.add(str);
end;

procedure TStack.addAfter(elem : PElem; item : TInf);
begin
  if(elem = nil) then exit;
  new(temp);
  temp^.value := item;
  temp^.next := elem^.next;
  elem^.next := temp;
end;

procedure TStack.addBefore(elem : PElem; item : TInf);
begin
  addAfter(elem, elem^.value);
  elem^.value := item;
end;

procedure TStack.readAfter(elem : PElem; var item : TInf);
begin
  temp := elem^.next;
  item := temp^.value;
  elem^.next := temp^.next;
  dispose(temp);
end;

procedure TStack.sort;
var i,len : integer;
  procedure swap(a,b : PElem);
  var t : TInf;
  begin
    t := a^.value;
    a^.value := b^.value;
    b^.value := t;
  end;
begin
  len := 0;
  temp := top;
  while(temp <> nil) do
  begin
    inc(len);
    temp := temp^.next;
  end;

  for i := 1 to len do
  begin
    temp := top;
    while(temp^.next <> nil) do
    begin
      if(temp^.value > temp^.next^.value) then
        swap(temp, temp^.next);
      temp := temp.next;
    end;
  end;

end;

procedure TStack.poisk(key : char; var item : TInf);
begin
  temp := top;
  item := char(0);
  while(temp <> nil) do
  begin
    if(temp^.value = key) then
    begin
      item := temp^.value;
      break;
    end;

    temp := temp^.next;
  end;
end;

end.
 