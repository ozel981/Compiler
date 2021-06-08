
%using QUT.Gppg;
%namespace GardensPoint

Identificator       [a-zA-Z]([a-zA-Z0-9])*
IntNumber           ([1-9]([0-9])*)|0
RealNumber          ([1-9]([0-9])*\.([0-9])+)|(0\.([0-9])+)
Boolean             (true|false)

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
"||"           { return (int)Tokens.Or; }
"&&"           { return (int)Tokens.And; }
{IntNumber}   { yylval.val=yytext; return (int)Tokens.IntNumber; }
{RealNumber}  { yylval.val=yytext; return (int)Tokens.RealNumber; }
{Boolean}     { yylval.val=yytext; return (int)Tokens.Boolean; }
{Identificator} { yylval.val=yytext; return (int)Tokens.Identificator; }
<<EOF>>       { return (int)Tokens.Eof; }