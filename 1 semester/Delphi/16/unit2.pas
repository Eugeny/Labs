unit Unit2;

interface

uses
  StdCtrls, SysUtils;

type

PElem = ^TElem;
TElem = record
      value : integer;
      next, prev : PElem;
end;

TInf = integer;

TList = class(TObject)
     pb, pe, pt : PElem;
     constructor create;
     procedure clear;
     procedure add_back(value : TInf);
     procedure add(index : integer; value:TInf);
     procedure printToListBox(var listBox : TListBox);
     procedure delete(index, count : integer);
     function getElem(index : integer) : PElem;
end;

implementation

constructor TList.create;
begin
     inherited create;
     pb := nil;
     pe := nil;
end;

procedure TList.clear;
begin
  pb := nil;
  pe := nil;
end;

procedure TList.add_back(value : integer);
begin
     new(pt);
     pt^.next := nil;
     pt^.prev := nil;;
     pt^.value := value;
     if(pb = nil) then
     begin
          pb := pt;
          pe := pt;
     end else
     begin
          pe^.next := pt;
          pt^.prev := pe;
          pe := pt;
     end;
end;

procedure TList.printToListBox(var listBox : TListBox);
begin
     listBox.Items.Clear;
     pt := pb;
     while(pt <> nil) do
     begin
          listBox.items.add(intToStr(pt^.value));
          pt := pt^.next;
     end;
end;

procedure TList.Delete(index, count : integer);
var
   beg,t : PElem;
begin
   pt := getElem(index);
   beg := pt^.prev;
   while((pt <> nil) and (count > 0)) do
   begin
        dec(count);
        t := pt;
        pt := pt^.next;
        dispose(t);
   end;
   if(beg = nil) then
   begin
        pb := pt;
   end else
   begin
        beg^.next := pt;
        if(pt <> nil) then
          pt^.prev := beg;
   end;
   if(pt <> nil) then
        pt^.prev := beg
   else
       pe := pb;
end;

function TList.GetElem(index : integer):PElem;
begin
     pt := pb;
     while((pt <> nil) and (index > 0)) do
     begin
          pt := pt^.next;
          dec(index);
     end;
     result := pt;
end;

procedure TList.add(index : integer; value : integer);
var temp : PElem;
begin
  if(index = -1) then
  begin
    new(temp);
    temp^.next := pb;
    pb^.prev := temp;
    pb := temp;
  end else
  begin
    pt := getElem(index);
    if(pt = nil) then
      add_back(value)
    else begin
      new(temp);
      temp^.value := value;
      temp^.next := pt^.next;
      temp^.prev := pt;
      pt^.next := temp;
    end;
  end;
end;


end.

