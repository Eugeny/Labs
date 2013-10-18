unit Unit2;

interface
  uses StdCtrls,SysUtils;
type
  TItem = record
    str : string;
    key : integer;
  end;

  PNode = ^TNode;
  TNode = record
    value : TItem;
    next : PNode;
  end;

  TMs = array[1..1] of PNode;
  PMs = ^Tms;

  THashTable = class(TObject)
    m : integer;
    hash : PMs;
    constructor create(_m : integer);
    procedure add(item : TItem);
    function find(key : integer; var item : TItem) : boolean;
    procedure print(memo : TMemo);
    destructor free;
  end;

implementation

  constructor THashTable.create(_m : integer);
  var i : integer;
  begin
    inherited create;
    m := _m;
    GetMem(hash, sizeof(PNode) * m);
    for i := 0 to m - 1 do
      hash[i] := nil;
  end;

  procedure THashTable.add(item : TItem);
  var hIndex : integer;
  temp : PNode;
  begin
    hIndex := item.key mod m;
    new(temp);
    temp^.value := item;
    temp^.next := hash[hIndex];
    hash[hIndex] := temp;
  end;

  function THashTable.find(key : integer; var item : TItem) : boolean;
  var hIndex : integer;
  temp : PNode;
  begin
    result := false;
    hIndex := key mod m;
    temp := hash[hIndex];
    while(temp <> nil) do
    begin
      if(temp^.value.key = key) then
      begin
        item := temp^.value;
        result := true;
        break;
      end;
      temp := temp^.next;
    end;
  end;

  procedure THashTable.print(memo : TMemo);
  var i : integer;
  txt : string;
  first : boolean;
  temp : PNode;
  begin
    memo.Clear;
    for i := 0 to m - 1 do
    begin
      txt := intToStr(i + 1) + ' - ';
      first := false;
      temp := hash[i];
      while(temp <> nil) do
      begin
        if(not first) then
        begin
          first := true;
          txt := txt + temp^.value.str + '(' + intToStr(temp^.value.key) + ')';
        end else
          txt := txt + ', ' + temp^.value.str + '(' + intToStr(temp^.value.key) + ')';
        temp := temp^.next;
      end;
      memo.Lines.Add(txt);
    end;
  end;

  destructor THashTable.free;
  var i : integer;
  temp, cur : PNode;
  begin
    for i := 0 to m - 1 do
    begin
      cur := hash[i];
      while(cur <> nil) do
      begin
        temp := cur;
        cur := cur^.next;
        dispose(temp);
      end;
    end;
    FreeMem(hash, sizeof(PNode) * m);
  end;

end.
