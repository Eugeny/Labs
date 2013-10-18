unit unit2;

interface
uses   StdCtrls,ComCtrls,SysUtils;

type

    TItem = record
        name : string[80];
        passport : integer;
    end;

    PNode = ^TNode;
    TNode = record 
        value : TItem;
        left, right : PNode;
    end;
    
    TTree = class(TObject)
        root : PNode;
        constructor Create;
        procedure Add(value : TItem);
        procedure PrintToTreeView(treeView : TTreeView);
        procedure Walk(memo : TMemo);
        procedure clear; 
    end;

implementation

constructor TTree.Create;
begin
    inherited Create;
    clear;
end;

procedure TTree.clear;
begin
    root := nil;
end;

procedure TTree.Add(value : TItem);
var 
    temp : PNode;   
    procedure add_node(cur, item : PNode); 
    begin
        if(item^.value.passport > cur^.value.passport) then
        begin
            if(cur^.right = nil) then
                cur^.right := item
            else
                add_node(cur^.right, item)
        end else 
        begin
            if(cur^.left = nil) then
                cur^.left := item
            else
                add_node(cur^.left, item)
        end; 
    end; 
begin
    new(temp);
    temp^.value := value;
    if(root = nil) then 
        root := temp
    else
        add_node(root, temp);
end;

procedure TTree.Walk(memo : Tmemo);
  procedure print_node(node : PNode);
  begin
    memo.Lines.Add(node^.value.name);
    if(node^.left <> nil) then
      print_node(node^.left);
    if(node^.right <> nil) then
      print_node(node^.right);
  end;
begin
  memo.Clear;
  print_node(root);
end;

procedure TTree.PrintToTreeView(treeView : TTreeView);
  function count(node : PNode) : integer;
  begin
    result := 0;
    if(node = nil)then exit;
    inc(result, count(node^.left));
    inc(result, count(node^.right));
    inc(result);
  end;
  procedure print_node(parent_node : TTreeNode; node : PNode);
  var cur_node : TTreeNode;
  begin
    if(node = nil) then exit;
    cur_node := treeView.Items.AddChild(parent_node, node^.value.name + ' - ' + intToStr(node^.value.passport) + '(' +
      intToStr(count(node^.left)) +')');
    print_node(cur_node, node^.left);
    print_node(cur_node, node^.right);
  end;
begin
  treeView.Items.Clear;
  print_node(nil, root);
end;


end.

