unit Unit1;

interface

uses
  Windows, Messages, SysUtils, Variants, Classes, Graphics, Controls, Forms,
  Dialogs, StdCtrls, Unit2;

type
  TForm1 = class(TForm)
    GroupBox1: TGroupBox;
    Label1: TLabel;
    Edit1: TEdit;
    Button1: TButton;
    GroupBox2: TGroupBox;
    Label2: TLabel;
    Edit2: TEdit;
    Button2: TButton;
    Label3: TLabel;
    Memo1: TMemo;
    Edit3: TEdit;
    Label4: TLabel;
    Label5: TLabel;
    procedure FormCreate(Sender: TObject);
    procedure Button1Click(Sender: TObject);
    procedure FormClose(Sender: TObject; var Action: TCloseAction);
    procedure Button2Click(Sender: TObject);
  private
    { Private declarations }
  public
    { Public declarations }
  end;

var
  Form1: TForm1;
  hash : THashTable;

implementation

{$R *.dfm}

procedure TForm1.FormCreate(Sender: TObject);
begin
  hash := THashTable.create(10);
end;

procedure TForm1.Button1Click(Sender: TObject);
var item : TItem;
begin
  item.key := strToInt(edit3.Text);
  item.str := edit1.Text;
  hash.add(item);
  hash.print(memo1);
  edit1.Clear;
  edit3.Clear;
end;

procedure TForm1.FormClose(Sender: TObject; var Action: TCloseAction);
begin
  hash.free;
end;

procedure TForm1.Button2Click(Sender: TObject);
var item : TItem;
begin
  if(hash.find(strToInt(edit2.Text), item)) then
    label5.Caption := item.str
  else
    label5.Caption := 'Не найдено';
  edit2.Clear;
end;

end.
