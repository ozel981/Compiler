
// Uwaga: W wywołaniu generatora gppg należy użyć opcji /gplex

%namespace GardensPoint

%union
{
public List<string> varNames;
public List<Compiler.INode> nodesList;
public string val;
public Compiler.Types types;
public Compiler.INode node;
public Compiler.Pair constantType;
public Compiler.ExpresionNode expresionNode;
}

%token Program OpenBlock Eof CloseBlock Int Bool Double Coma Semicolon Assignment
%token <val> Identificator IntNumber RealNumber Boolean

%type <types> type 
%type <varNames> multideclarations 
%type <node> body declaration statement singleOperation 
%type <constantType> constant
%type <expresionNode> expression
%type <nodesList> declarations statements

%%

start   : Program body Eof
            {
                Compiler.GenBody($2);
            }
        ;

body    : OpenBlock declarations statements CloseBlock
            {
                $$ = new Compiler.BodyNode($2,$3);
            }
        |
        ;

declarations : declarations declaration 
                {
                    $1.Add($2);
                    $$ = $1;
                }
             | { $$ = new List<Compiler.INode>(); }
             ;

declaration : type multideclarations Identificator Semicolon
              {
                    Compiler.actualType = $1;
                    if(Compiler.IsIdentyficatorOccupied($1,$3))
                    {
                        Console.WriteLine("line: error: such variable name already exists");
                    }
                    else
                    {
                        $2.Add($3);
                        $$ = new Compiler.DeclarationNode($1,$2);
                    }
              }
            ;

statements  : statements statement
                {
                    $1.Add($2);
                    $$ = $1;
                }
            | { $$ = new List<Compiler.INode>(); }
            ;

statement   : singleOperation Semicolon { $$ = new Compiler.StatementNode($1); }
            |
            ;

singleOperation : expression { $$ = new Compiler.SingleOperationNode($1); }
                ;

expression  : Identificator Assignment expression 
            {
                if(Compiler.variables.ContainsKey($1))
                {
                    $$ = new Compiler.AssignmentExpresionNode(new Compiler.Pair(Compiler.variables[$1],$1), $3);
                }
                else
                {
                    Console.WriteLine("line: error: such variable not exists");
                }
            }
            | constant
                {
                    $$ = new Compiler.ConstantExpresionNode($1);
                }
            ;

constant : IntNumber { $$ = new Compiler.Pair(Compiler.Types.IntegerType, $1); }
         | RealNumber { $$ = new Compiler.Pair(Compiler.Types.DoubleType, $1); }
         | Boolean { $$ = new Compiler.Pair(Compiler.Types.BooleanType, $1); }
         ;

type         : Int {$$ = Compiler.Types.IntegerType;}
             | Double {$$ = Compiler.Types.DoubleType;}
             | Bool {$$ = Compiler.Types.BooleanType;}
             ;

multideclarations : multideclarations Identificator Coma 
                    {
                        if(Compiler.IsIdentyficatorOccupied(Compiler.actualType,$2))
                        {
                            Console.WriteLine("line: error: such variable name already exists");
                        }
                        else
                        {
                            $1.Add($2);
                            $$ = $1;
                        }
                    }
                  | { $$ = new List<string>(); }
                  ;

%%

public Parser(Scanner scanner) : base(scanner) { }