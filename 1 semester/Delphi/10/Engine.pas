unit Engine;

interface

uses Graphics, StdCtrls, Math, Types;

type TBase = class(TObject)
public
  canvas : TCanvas;
  vx : extended;
  x,y : extended;
  offsetX, offsetY : extended;
  width : extended;
  childrenCount : longint;
  children : array[1..15] of TBase;

  constructor Create(cnv : TCanvas);
  constructor CreateMeChild(parent:TBase);

  procedure Init;virtual;
  procedure drawMe(bx,by : longint);virtual;abstract;
  procedure draw(bx, by : longInt);
  procedure updatePosition;
  procedure addObject(o : TBase);
  procedure deleteObject(i : longInt);
end;

type TTelega = class(TBase)
public
  procedure init;override;
  procedure drawMe(bx, by : longint);override;
  procedure addBox;
  procedure deleteBox;
end;

type TBox = class(TBase)
public
  procedure drawMe(bx, by : longint);override;
end;


type TScene = class(TBase)
public
  procedure drawAll;
  procedure drawMe(bx, by : longint);override;
  procedure clear;
end;


implementation

constructor TBase.Create;
begin
  canvas := cnv;
  offsetX := 0;
  offsetY := 0;
  childrenCount := 0;
  init;
end;

constructor TBase.CreateMeChild;
begin
  canvas := parent.canvas;
  parent.addObject(self);
  offsetX := 0;
  offsetY := 0;
  childrenCount := 0;
  init;
end;


procedure TBase.Init;
begin
  width := 0;
end;

procedure TBase.addObject;
begin
  inc(childrenCount);
  children[childrenCount] := o;
end;

procedure TBase.deleteObject;
var j : integer;
begin
  if(childrenCount > 0) then
  begin
    for j := i to childrenCount - 1 do
      children[j] := children[j + 1];
    dec(childrenCount);
  end;
end;

procedure TBase.draw(bx, by : integer);
var i :integer;
begin
  drawMe(bx + round(x), by + round(y));
  for i := 1 to childrenCount do
    children[i].draw(bx + round(x + offsetX), by + round(y + offsetY));
end;

procedure TBase.updatePosition;
var i : integer;
begin
  if(abs(vx) < 0.1) then vx := 0;
  x := x + vx;
  if(x < 0) then
  begin
    x := 0;
    vx := -vx;
  end;
  if(x + width > canvas.ClipRect.Right) then
  begin
    x := canvas.ClipRect.Right - width;
    vx := -vx;
  end;
  vx := vx * 0.95;
  for i := 1 to childrenCount do
    children[i].updatePosition;
end;


procedure TScene.drawAll;
begin
  Canvas.Brush.Color := clGray;
  draw(0,0);
end;

procedure TScene.drawMe(bx, by : longInt);
begin

end;

procedure TScene.clear;
begin
  canvas.Brush.Color := clWhite;
  canvas.Rectangle(canvas.ClipRect);
end;


procedure TTelega.init;
begin
  offsetX := 10;
  offsetY := -23;
  width := 115;
end;

procedure TTelega.drawMe(bx, by : longInt);
var a,b,c:TPoint;
begin
  canvas.Pen.Width := 1;

  canvas.Ellipse(bx + 5, by, bx + 25, by - 20);
  canvas.Ellipse(bx + 90, by, bx + 110, by - 20);
  canvas.Rectangle(bx, by - 20, bx + 115, by - 23);
  canvas.Rectangle(bx, by - 23, bx + 10, by - 35);
  a.X := bx + 115;a.Y := by - 23;
  b.x := bx + 90;b.Y := by - 35;
  c.X := bx + 90;c.y := by - 23;
  canvas.Polygon([a,b,c]);
end;

procedure TTelega.addBox;
var nx, ny : longInt;
newBox : TBox;
begin
  if(childrenCount >= 10) then exit;
  newBox := TBox.CreateMeChild(self);
  if(childrenCount < 5) then
  begin
    newBox.x := (childrenCount - 1) * 20;
    newBox.y := 0;
  end else if((childrenCount >= 5) and (childrenCount <= 7)) then
  begin
    newBox.x := 10 + (childrenCount - 5) * 20;
    newBox.y := -20;
  end else if((childrenCount >= 8) and (childrenCount <= 9)) then
  begin
    newBox.x := 20 + (childrenCount - 8) * 20;
    newBox.y := -40;
  end else if(childrenCount = 10) then
  begin
    newBox.x := 30;
    newBox.y := -60;
  end;
end;

procedure TTelega.deleteBox;
var i : integer;
begin
   deleteObject(childrenCount);
end;


procedure TBox.drawMe(bx, by : longInt);
begin
  canvas.Rectangle(bx, by, bx + 20, by - 20);
end;

end.


