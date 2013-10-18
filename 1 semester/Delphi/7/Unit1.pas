unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls;

type
  TForm1 = class(TForm)
    Label1: TLabel;
    Edit1: TEdit;
    Label2: TLabel;
    Label3: TLabel;
    Label4: TLabel;
    Label5: TLabel;
    procedure Edit1KeyPress(Sender: TObject; var Key: Char);
  private
    function getMinSizeWord(var pos : integer) : string;
    function getNoRepeatCharsCount(s : string) : integer;
  public
    { Public declarations }
  end;

var
  Form1: TForm1;

implementation

{$R *.dfm}

function TForm1.getMinSizeWord(var pos:integer) : string;
var s,text : string;
i,j,min,b,count : integer;
begin
  text := edit1.text;
  i := 1;
  j := 0;
  count := 0;
  b := 0;
  min := 11111;
  while(i <= length(text) + 1) do
  begin
    if((i = length(text) + 1) or ((text[i] = ' ') and (j > 0))) then
    begin
      inc(count);
      if(j < min) then
      begin
         s := copy(text, b, j);
         pos := count;
         min := j;
      end;
      j := 0;
      b := i + 1;
    end else if(text[i] <> ' ') then
    begin
      inc(j);
    end;
    inc(i);
  end;
  result := s;
end;

function TForm1.getNoRepeatCharsCount(s : string) : integer;
var mn:set of char;
i,j,res : integer;
begin
  res := 0;
  mn := [];
  for i:=1 to length(s) do
  begin
     if(not(s[i] in mn)) then
      Include(mn, s[i]);
  end;
  for i:= 1 to 255 do
    if(char(i) in mn) then
      inc(res);
  result := res;
end;

procedure TForm1.Edit1KeyPress(Sender: TObject; var Key: Char);
var s : string;
index : integer;
begin
if(Key = #13) then
begin
  s := getMinSizeWord(index);
  label4.Caption := intToStr(index);
  label5.Caption := intToStr(getNoRepeatCharsCount(s));
end;
end;

end.
