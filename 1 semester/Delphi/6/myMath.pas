unit myMath;



interface
uses StdCtrls, SysUtils,Windows;

  type func = function(x:extended):extended;
  procedure makeTable(f : func; xn, xk : extended; m : integer; memo:TMemo);

implementation

  procedure makeTable(f : func; xn, xk : extended; m : integer; memo:TMemo);
  var
    x,h:extended;
    i:integer;
  begin
    try
    x := xn;
    h := (xk - xn) / m;
    for i:=0 to m do
    begin
      memo.lines.Add('x = ' + FloatToStrf(x,ffFixed, 6, 4) + ' y = ' + FloatToStrf(f(x),ffFixed, 6, 4));
      x := x + h;
    end;
    except
        on EDivideZero do
          showMessage("Diving by 0");
    end;

  end;

end.
 