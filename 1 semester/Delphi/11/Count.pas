unit Count;

interface

type TCount = class(TObject)
private
  mat : array[0..100, 0..100] of integer;
public
  function countRec(m, n : integer) : integer;
  function countIter(m, n : integer) : integer;
  constructor Create;
  procedure clear;
  function rec(m, n : integer) : integer;
end;

implementation

constructor TCount.Create;
begin
  clear;
end;

procedure TCount.clear;
var i, j : integer;
begin
  for i := 0 to 100 do
    for j := 0 to 100 do
      mat[i][j] := -1;
end;

function TCount.rec(m, n : integer) : integer;
begin
  if(mat[m][n] <> -1) then
  begin
    result := mat[m][n];
    exit;
  end;
  if(m = 0) then
    result := n + 1
  else if(n = 0) then
    result := rec(m - 1, 1)
  else
    result := rec(m - 1, countRec(m, n - 1));
  mat[m][n] := result;
end;

function TCount.countRec(m, n : integer) : integer;
begin
  clear;
  result := rec(m, n);
end;

function TCount.countIter(m, n : integer) : integer;
var i, j : integer;
begin
  clear;
  for j := 0 to m do
    for i := 0 to n do
    begin
      if(j = 0) then
        mat[j][i] := i + 1
      else if(i = 0) then
        mat[j][i] := mat[j - 1][1]
      else
        mat[j][i] := mat[j - 1][mat[j][i - 1]];
    end;
  result := mat[m][n];
end;

end.
 