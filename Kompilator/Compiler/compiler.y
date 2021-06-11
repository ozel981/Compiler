
// Uwaga: W wywołaniu generatora gppg należy użyć opcji /gplex

%namespace GardensPoint

%union
{
public List<string> varNames;
public List<Compiler.INode> nodesList;
public string val;
public Compiler.Types types;
public Compiler.INode node;
public Compiler.ExpressionNode expresionNode;
}

%token Program OpenBlock Eof CloseBlock Int Bool Double Coma Semicolon Assignment And Or Equal NotEqual Greater GreaterEqual Less LessEqual Plus Minus Multiply Divide BinaryMultiply BinarySum UnaryNegation LogicalNegation IntConversion DoubleConversion OpenParenthesis CloseParenthesis If Else While Read Write Hex Return
%token <val> Identificator IntNumber RealNumber Boolean String

%type <types> type 
%type <varNames> multideclarations 
%type <node> body declaration statement singleOperation if while block read write return
%type <expresionNode> expressionAssig, expressionLogic, expressionRelat, expressionAddit, expressionMulti, expressionBinar, expressionUnary, expression, variable
%type <nodesList> declarations statements

%%

start           : Program body Eof { Compiler.GenProgram(new Compiler.ProgramNode($2)); }
                ;

body            : OpenBlock declarations statements CloseBlock { $$ = new Compiler.BodyNode($2,$3); }
                ;

declarations    : declarations declaration { if($2 != null) $1.Add($2); $$ = $1; }
                | { $$ = new List<Compiler.INode>(); }
                ;

declaration     : type { Compiler.actualType = $1; } multideclarations Identificator Semicolon
                  {
                        if(!Compiler.IsIdentyficatorOccupied($1,$4))
                        {
                            $3.Add($4);
                            $$ = new Compiler.DeclarationNode($1,$3);
                        }
                  }
                ;

multideclarations : multideclarations Identificator Coma 
                    {
                        if(!Compiler.IsIdentyficatorOccupied(Compiler.actualType,$2))
                        {
                            $1.Add($2);
                            $$ = $1;
                        }
                    }
                | { $$ = new List<string>(); }
                ;

statements      : statements statement { $1.Add($2); $$ = $1; }
                | { $$ = new List<Compiler.INode>(); }
                ;

statement       : singleOperation Semicolon { $$ = $1; }
                | if { $$ = $1; }
                | while { $$ = $1; }
                | block { $$ = $1; }
                ;

if              : If OpenParenthesis expressionAssig CloseParenthesis statement 
                    { $$ = new Compiler.IfNode($3, $5); }
                | If OpenParenthesis expressionAssig CloseParenthesis statement Else statement 
                    { $$ = new Compiler.IfElseNode($3, $5, $7); }
                ;

while           : While OpenParenthesis expressionAssig CloseParenthesis statement 
                    { $$ = new Compiler.WhileNode($3, $5); }
                ;

block           : OpenBlock statements CloseBlock { $$ = new Compiler.BlockNode($2); }
                ;

singleOperation : expressionAssig { $$ = $1; }
                | read { $$ = $1; }
                | write { $$ = $1; }
                | return { $$ = $1; }
                ;

read            : Read Identificator { $$ = new Compiler.ReadNode($2); }
                | Read Identificator Coma Hex { $$ = new Compiler.HexReadNode($2); }
                ;

write           : Write expressionAssig { $$ = new Compiler.WriteNode($2); }
                | Write expressionAssig Coma Hex { $$ = new Compiler.HexWriteNode($2); }
                | Write String { $$ = new Compiler.StringWriteNode($2); }
                ;

return          : Return { $$ = new Compiler.ReturnNode(); }
                ;

expressionAssig : Identificator Assignment expressionAssig 
                {
                   $$ = new Compiler.AssignmentExpresionNode($1, $3);
                }
                | expressionLogic { $$ = $1; }
                ;

expressionLogic : expressionLogic And expressionRelat { $$ = new Compiler.AndLogicalExpresionNode($1,$3); }
                | expressionLogic Or expressionRelat { $$ = new Compiler.OrLogicalExpresionNode($1,$3); }
                | expressionRelat { $$ = $1; }
                ;

expressionRelat : expressionRelat Equal expressionAddit {  $$ = new Compiler.EqualExpresionNode($1, $3); }
                | expressionRelat NotEqual expressionAddit {  $$ = new Compiler.NotEqualExpresionNode($1, $3); }
                | expressionRelat Greater expressionAddit {  $$ = new Compiler.GreaterExpresionNode($1, $3); }
                | expressionRelat GreaterEqual expressionAddit {  $$ = new Compiler.GreaterEqualExpresionNode($1, $3); }
                | expressionRelat Less expressionAddit {  $$ = new Compiler.LessExpresionNode($1, $3); }
                | expressionRelat LessEqual expressionAddit {  $$ = new Compiler.LessEqualExpresionNode($1, $3); }
                | expressionAddit { $$ = $1; }
                ;

expressionAddit : expressionAddit Plus expressionMulti { $$ = new Compiler.PlusExpresionNode($1,$3); }
                | expressionAddit Minus expressionMulti { $$ = new Compiler.MinusExpresionNode($1,$3); }
                | expressionMulti { $$ = $1; }
                ;

expressionMulti : expressionMulti Multiply expressionBinar { $$ = new Compiler.MultiplyExpresionNode($1,$3); }
                | expressionMulti Divide expressionBinar { $$ = new Compiler.DivideExpresionNode($1,$3); }
                | expressionBinar { $$ = $1; }
                ;

expressionBinar : expressionBinar BinaryMultiply expressionUnary { $$ = new Compiler.BinaryMultiplyExpresionNode($1,$3); }
                | expressionBinar BinarySum expressionUnary { $$ = new Compiler.BinarySumExpresionNode($1,$3); }
                | expressionUnary { $$ = $1; }
                ;

expressionUnary : Minus expressionUnary { $$ = new Compiler.UnaryMinusExpresionNode($2); }
                | UnaryNegation expressionUnary { $$ = new Compiler.UnaryNegationExpresionNode($2); }
                | LogicalNegation expressionUnary { $$ = new Compiler.LogicalNegationExpresionNode($2); }
                | IntConversion expressionUnary { $$ = new Compiler.IntConversionExpresionNode($2); }
                | DoubleConversion expressionUnary { $$ = new Compiler.DoubleConversionExpresionNode($2); }
                | expression { $$ = $1; }
                ;

expression      : OpenParenthesis expressionAssig CloseParenthesis { $$ = $2; }
                | variable { $$ = $1; }
                ;

variable        : IntNumber { $$ = new Compiler.ConstantExpresionNode(Compiler.Types.IntegerType, $1); }
                | RealNumber { $$ = new Compiler.ConstantExpresionNode(Compiler.Types.DoubleType, $1); }
                | Boolean { $$ = new Compiler.ConstantExpresionNode(Compiler.Types.BooleanType, $1); }
                | Identificator { $$ = new Compiler.VariableExpresionNode($1); }
                ;

type            : Int {$$ = Compiler.Types.IntegerType;}
                | Double {$$ = Compiler.Types.DoubleType;}
                | Bool {$$ = Compiler.Types.BooleanType;}
                ;

%%

public Parser(Scanner scanner) : base(scanner) { }