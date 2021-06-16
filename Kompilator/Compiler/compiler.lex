
%using QUT.Gppg;
%namespace GardensPoint

Comment            \/\/.*/\n
String              \"([^\n\\"]|\\.)*\"
Identificator       [a-zA-Z]([a-zA-Z0-9])*
IntNumber           ([1-9]([0-9])*)|0
RealNumber          ([1-9]([0-9])*\.([0-9])+)|(0\.([0-9])+)
Boolean             (true|false)
HexNumber           ("0x"|"0X")[0-9a-fA-F]*

%%

"program"     { return (int)Tokens.Program; }
"int"         { return (int)Tokens.Int; }
"double"      { return (int)Tokens.Double; }
"bool"        { return (int)Tokens.Bool; }
"{"           { return (int)Tokens.OpenBlock; }
"}"           { return (int)Tokens.CloseBlock; }
","           { return (int)Tokens.Coma; }
";"           { return (int)Tokens.Semicolon; }
"="           { return (int)Tokens.Assignment; }
"||"          { return (int)Tokens.Or; }
"&&"          { return (int)Tokens.And; }
"=="          { return (int)Tokens.Equal; }
"!="          { return (int)Tokens.NotEqual; }
">"           { return (int)Tokens.Greater; }
">="          { return (int)Tokens.GreaterEqual; }
"<"           { return (int)Tokens.Less; }
"<="          { return (int)Tokens.LessEqual; }
"+"           { return (int)Tokens.Plus; }
"-"           { return (int)Tokens.Minus; }
"*"           { return (int)Tokens.Multiply; }
"/"           { return (int)Tokens.Divide ; }
"|"           { return (int)Tokens.BinarySum; }
"&"           { return (int)Tokens.BinaryMultiply; }
"~"           { return (int)Tokens.UnaryNegation; }
"!"           { return (int)Tokens.LogicalNegation; }
"("           { return (int)Tokens.OpenParenthesis; }
")"           { return (int)Tokens.CloseParenthesis; }
"if"          { return (int)Tokens.If; }
"else"        { return (int)Tokens.Else; }
"while"       { return (int)Tokens.While; }
"read"        { return (int)Tokens.Read; }
"write"       { return (int)Tokens.Write; }
"hex"         { return (int)Tokens.Hex; }
"return"      { return (int)Tokens.Return; }
"\n"          { Compiler.lineNumber++; }
{Comment}     {  }
{HexNumber}   { yylval.val=int.Parse(yytext.Substring(2), System.Globalization.NumberStyles.HexNumber).ToString(); return (int)Tokens.IntNumber; }
{String}      { yylval.val=yytext; return (int)Tokens.String; }
{IntNumber}   { yylval.val=yytext; return (int)Tokens.IntNumber; }
{RealNumber}  { yylval.val=yytext; return (int)Tokens.RealNumber; }
{Boolean}     { yylval.val=yytext; return (int)Tokens.Boolean; }
{Identificator} { yylval.val=yytext; return (int)Tokens.Identificator; }
<<EOF>>       { return (int)Tokens.Eof; }
" "           {  }
"\t"          {  }
"\r"          {  }
.             { Compiler.errors++; Console.WriteLine("line [" + Compiler.lineNumber.ToString() + "] error: unknown character"); }