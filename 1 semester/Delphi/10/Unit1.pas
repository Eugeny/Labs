unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, Engine, StdCtrls, ExtCtrls;

type
  TForm1 = class(TForm)
    Image1: TImage;
    Button1: TButton;
    Button2: TButton;
    Button3: TButton;
    Button4: TButton;
    Button5: TButton;
    Button6: TButton;
    Button7: TButton;
    Timer1: TTimer;
    procedure Button7Click(Sender: TObject);
    procedure FormCreate(Sender: TObject);
    procedure Timer1Timer(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure Button2Click(Sender: TObject);
    procedure Button5Click(Sender: TObject);
    procedure Button4Click(Sender: TObject);
    procedure Button6Click(Sender: TObject);
    procedure Button3Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  scene : TScene;
  telega : TTelega;
implementation

{$R *.dfm}

procedure TForm1.Button7Click(Sender: TObject);
begin
  Application.Terminate;
end;

procedure TForm1.FormCreate(Sender: TObject);
begin
  scene := TScene.Create(image1.Canvas);
  telega := TTelega.CreateMeChild(scene);
  scene.y := image1.Height;
end;

procedure TForm1.Timer1Timer(Sender: TObject);
begin
  scene.Clear;
  scene.updatePosition;
  scene.DrawAll;
end;

procedure TForm1.Button1Click(Sender: TObject);
begin
  telega.addBox;
end;

procedure TForm1.Button2Click(Sender: TObject);
begin
  telega.deleteBox;
end;

procedure TForm1.Button5Click(Sender: TObject);
begin
  telega.vx := telega.vx + 4;
end;

procedure TForm1.Button4Click(Sender: TObject);
begin
  telega.vx := telega.vx - 4;
end;

procedure TForm1.Button6Click(Sender: TObject);
begin
  telega.vx := telega.vx + 7;
end;

procedure TForm1.Button3Click(Sender: TObject);
begin
 telega.vx := telega.vx - 7;
end;

end.
