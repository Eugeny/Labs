unit Unit2;

Interface

Uses  math, stack;

Type
  Tpz=class(TObject)
    mszn : array['a'..'ÿ'] of extended;
		stack:TStack;
    Function OBP(str:string): string;
    Function AV(strp:string) : extended;
    constructor create;
  end;

Implementation

var i:byte;

constructor Tpz.create;
begin
  stack := TStack.Create;
end;

Function Tpz.OBP(str : string) : string;
var pc : 0..3;
  ch,ch1 : char;

Function prior(ch:char):byte;
begin
  case ch of
    '(', ')': Result:=0;
    '+', '-': Result:=1;
    '*', '/': Result:=2;
    '^'   : Result:=3;
  end;
end;

begin
  Result:='';
	for i:=1 to length(str) do
  begin
	  ch:=str[i];
    if not (ch in ['+','-','*','/','(',')','^']) then
      Result:=Result+ch
    else if stack.top=Nil then
      stack.Adds(ch)
    else if ch='(' then
      stack.Adds(ch)
    else if ch=')' then
    begin
		  stack.Reads(ch);
		  While ch<>'(' do
      begin
			 Result:=Result+ch;
  		 stack.Reads(ch);
      End;
    End else
    begin
      pc:=prior(ch);
      While (stack.top <> nil) and (pc<= prior(stack.top^.value)) do
      begin
        stack.Reads(ch1);
        Result:=Result+ch1;
      end;
		 stack.Adds(ch);
		end;
  end;
  While stack.top<>nil do
  begin
    stack.Reads(ch);
    Result:=Result+ch
  end;
end;

Function Tpz.AV(strp:string) : extended;
	 Var ch,ch1,ch2,chr : char;
		    op1,op2 : extended;
begin
    chr:=Succ('z');
		for i:=1 to length(strp) do
    begin
			ch:=strp[i];
		  if not (ch in ['*','/','+','-','^']) then
        stack.Adds(ch)
	  	else begin
  			stack.Reads(ch2);
        stack.Reads(ch1);
		  	op1:=mszn[ch1];
        op2:=mszn[ch2];
		 	  case ch of
			   '+': Result:=op1+op2;
			   '-': Result:=op1-op2;
			   '*': Result:=op1*op2;
			   '/': Result:=op1/op2;
			   '^': Result:=power(op1,op2);
			  end;
	      mszn[chr]:= Result; 
        stack.Adds(chr);
        Inc(chr);
      end;
    end;
end;

end.
 
