program Project1;

{$APPTYPE CONSOLE}

uses
  SysUtils,
  Count;

var
  n,m,resRec, resIter : integer;
  c : TCount;
begin
  c := TCount.Create;
  write('M = ');
  readln(m);
  write('N = ');
  readln(n);
  resRec := c.countRec(m,n);
  resIter := c.countIter(m,n);
  writeln('Recursion Result = ', resRec);
  writeln('Iteration Result = ', resIter);
  readln;
  c.Free;
end.
