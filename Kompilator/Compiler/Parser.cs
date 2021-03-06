// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  DESKTOP-PIILLQS
// DateTime: 16.06.2021 23:33:24
// UserName: wpodm
// Input file <.\compiler.y - 16.06.2021 11:38:59>

// options: lines gplex

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;

namespace GardensPoint
{
public enum Tokens {error=2,EOF=3,Program=4,OpenBlock=5,Eof=6,
    CloseBlock=7,Int=8,Bool=9,Double=10,Coma=11,Semicolon=12,
    Assignment=13,And=14,Or=15,Equal=16,NotEqual=17,Greater=18,
    GreaterEqual=19,Less=20,LessEqual=21,Plus=22,Minus=23,Multiply=24,
    Divide=25,BinaryMultiply=26,BinarySum=27,UnaryNegation=28,LogicalNegation=29,OpenParenthesis=30,
    CloseParenthesis=31,If=32,Else=33,While=34,Read=35,Write=36,
    Hex=37,Return=38,String=39,Identificator=40,IntNumber=41,RealNumber=42,
    Boolean=43};

public struct ValueType
#line 7 ".\compiler.y"
{
public List<string> varNames;
public List<Compiler.INode> nodesList;
public string val;
public Compiler.Types types;
public Compiler.INode node;
public Compiler.ExpressionNode expresionNode;
}
#line default
// Abstract base class for GPLEX scanners
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public abstract class ScanBase : AbstractScanner<ValueType,LexLocation> {
  private LexLocation __yylloc = new LexLocation();
  public override LexLocation yylloc { get { return __yylloc; } set { __yylloc = value; } }
  protected virtual bool yywrap() { return true; }
}

// Utility class for encapsulating token information
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class ScanObj {
  public int token;
  public ValueType yylval;
  public LexLocation yylloc;
  public ScanObj( int t, ValueType val, LexLocation loc ) {
    this.token = t; this.yylval = val; this.yylloc = loc;
  }
}

[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class Parser: ShiftReduceParser<ValueType, LexLocation>
{
#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[66];
  private static State[] states = new State[113];
  private static string[] nonTerms = new string[] {
      "type", "multideclarations", "body", "declaration", "statement", "singleOperation", 
      "if", "while", "block", "read", "write", "return", "expressionAssig", "expressionLogic", 
      "expressionRelat", "expressionAddit", "expressionMulti", "expressionBinar", 
      "expressionUnary", "expression", "variable", "declarations", "statements", 
      "start", "$accept", "Anon@1", };

  static Parser() {
    states[0] = new State(new int[]{4,3},new int[]{-24,1});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{5,6},new int[]{-3,4});
    states[4] = new State(new int[]{6,5});
    states[5] = new State(-2);
    states[6] = new State(-5,new int[]{-22,7});
    states[7] = new State(new int[]{8,110,10,111,9,112,7,-11,40,-11,23,-11,28,-11,29,-11,30,-11,41,-11,42,-11,43,-11,35,-11,36,-11,38,-11,32,-11,34,-11,5,-11},new int[]{-23,8,-4,103,-1,104});
    states[8] = new State(new int[]{7,9,40,14,23,28,28,30,29,32,30,34,41,40,42,41,43,42,35,73,36,78,38,84,32,86,34,94,5,100},new int[]{-5,10,-6,11,-13,13,-14,17,-15,49,-16,69,-17,60,-18,59,-19,58,-20,38,-21,39,-10,72,-11,77,-12,83,-7,85,-8,93,-9,99});
    states[9] = new State(-3);
    states[10] = new State(-10);
    states[11] = new State(new int[]{12,12});
    states[12] = new State(-12);
    states[13] = new State(-20);
    states[14] = new State(new int[]{13,15,26,-62,27,-62,24,-62,25,-62,22,-62,23,-62,16,-62,17,-62,18,-62,19,-62,20,-62,21,-62,14,-62,15,-62,12,-62,31,-62,11,-62});
    states[15] = new State(new int[]{40,14,23,28,28,30,29,32,30,34,41,40,42,41,43,42},new int[]{-13,16,-14,17,-15,49,-16,69,-17,60,-18,59,-19,58,-20,38,-21,39});
    states[16] = new State(-30);
    states[17] = new State(new int[]{14,18,15,70,12,-31,31,-31,11,-31});
    states[18] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-15,19,-16,69,-17,60,-18,59,-19,58,-20,38,-21,39});
    states[19] = new State(new int[]{16,20,17,50,18,61,19,63,20,65,21,67,14,-32,15,-32,12,-32,31,-32,11,-32});
    states[20] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-16,21,-17,60,-18,59,-19,58,-20,38,-21,39});
    states[21] = new State(new int[]{22,22,23,52,16,-35,17,-35,18,-35,19,-35,20,-35,21,-35,14,-35,15,-35,12,-35,31,-35,11,-35});
    states[22] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-17,23,-18,59,-19,58,-20,38,-21,39});
    states[23] = new State(new int[]{24,24,25,54,22,-42,23,-42,16,-42,17,-42,18,-42,19,-42,20,-42,21,-42,14,-42,15,-42,12,-42,31,-42,11,-42});
    states[24] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-18,25,-19,58,-20,38,-21,39});
    states[25] = new State(new int[]{26,26,27,56,24,-45,25,-45,22,-45,23,-45,16,-45,17,-45,18,-45,19,-45,20,-45,21,-45,14,-45,15,-45,12,-45,31,-45,11,-45});
    states[26] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-19,27,-20,38,-21,39});
    states[27] = new State(-48);
    states[28] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-19,29,-20,38,-21,39});
    states[29] = new State(-51);
    states[30] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-19,31,-20,38,-21,39});
    states[31] = new State(-52);
    states[32] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-19,33,-20,38,-21,39});
    states[33] = new State(-53);
    states[34] = new State(new int[]{8,35,10,44,40,14,23,28,28,30,29,32,30,34,41,40,42,41,43,42},new int[]{-13,47,-14,17,-15,49,-16,69,-17,60,-18,59,-19,58,-20,38,-21,39});
    states[35] = new State(new int[]{31,36});
    states[36] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-19,37,-20,38,-21,39});
    states[37] = new State(-54);
    states[38] = new State(-56);
    states[39] = new State(-58);
    states[40] = new State(-59);
    states[41] = new State(-60);
    states[42] = new State(-61);
    states[43] = new State(-62);
    states[44] = new State(new int[]{31,45});
    states[45] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-19,46,-20,38,-21,39});
    states[46] = new State(-55);
    states[47] = new State(new int[]{31,48});
    states[48] = new State(-57);
    states[49] = new State(new int[]{16,20,17,50,18,61,19,63,20,65,21,67,14,-34,15,-34,12,-34,31,-34,11,-34});
    states[50] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-16,51,-17,60,-18,59,-19,58,-20,38,-21,39});
    states[51] = new State(new int[]{22,22,23,52,16,-36,17,-36,18,-36,19,-36,20,-36,21,-36,14,-36,15,-36,12,-36,31,-36,11,-36});
    states[52] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-17,53,-18,59,-19,58,-20,38,-21,39});
    states[53] = new State(new int[]{24,24,25,54,22,-43,23,-43,16,-43,17,-43,18,-43,19,-43,20,-43,21,-43,14,-43,15,-43,12,-43,31,-43,11,-43});
    states[54] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-18,55,-19,58,-20,38,-21,39});
    states[55] = new State(new int[]{26,26,27,56,24,-46,25,-46,22,-46,23,-46,16,-46,17,-46,18,-46,19,-46,20,-46,21,-46,14,-46,15,-46,12,-46,31,-46,11,-46});
    states[56] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-19,57,-20,38,-21,39});
    states[57] = new State(-49);
    states[58] = new State(-50);
    states[59] = new State(new int[]{26,26,27,56,24,-47,25,-47,22,-47,23,-47,16,-47,17,-47,18,-47,19,-47,20,-47,21,-47,14,-47,15,-47,12,-47,31,-47,11,-47});
    states[60] = new State(new int[]{24,24,25,54,22,-44,23,-44,16,-44,17,-44,18,-44,19,-44,20,-44,21,-44,14,-44,15,-44,12,-44,31,-44,11,-44});
    states[61] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-16,62,-17,60,-18,59,-19,58,-20,38,-21,39});
    states[62] = new State(new int[]{22,22,23,52,16,-37,17,-37,18,-37,19,-37,20,-37,21,-37,14,-37,15,-37,12,-37,31,-37,11,-37});
    states[63] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-16,64,-17,60,-18,59,-19,58,-20,38,-21,39});
    states[64] = new State(new int[]{22,22,23,52,16,-38,17,-38,18,-38,19,-38,20,-38,21,-38,14,-38,15,-38,12,-38,31,-38,11,-38});
    states[65] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-16,66,-17,60,-18,59,-19,58,-20,38,-21,39});
    states[66] = new State(new int[]{22,22,23,52,16,-39,17,-39,18,-39,19,-39,20,-39,21,-39,14,-39,15,-39,12,-39,31,-39,11,-39});
    states[67] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-16,68,-17,60,-18,59,-19,58,-20,38,-21,39});
    states[68] = new State(new int[]{22,22,23,52,16,-40,17,-40,18,-40,19,-40,20,-40,21,-40,14,-40,15,-40,12,-40,31,-40,11,-40});
    states[69] = new State(new int[]{22,22,23,52,16,-41,17,-41,18,-41,19,-41,20,-41,21,-41,14,-41,15,-41,12,-41,31,-41,11,-41});
    states[70] = new State(new int[]{23,28,28,30,29,32,30,34,41,40,42,41,43,42,40,43},new int[]{-15,71,-16,69,-17,60,-18,59,-19,58,-20,38,-21,39});
    states[71] = new State(new int[]{16,20,17,50,18,61,19,63,20,65,21,67,14,-33,15,-33,12,-33,31,-33,11,-33});
    states[72] = new State(-21);
    states[73] = new State(new int[]{40,74});
    states[74] = new State(new int[]{11,75,12,-24});
    states[75] = new State(new int[]{37,76});
    states[76] = new State(-25);
    states[77] = new State(-22);
    states[78] = new State(new int[]{39,82,40,14,23,28,28,30,29,32,30,34,41,40,42,41,43,42},new int[]{-13,79,-14,17,-15,49,-16,69,-17,60,-18,59,-19,58,-20,38,-21,39});
    states[79] = new State(new int[]{11,80,12,-26});
    states[80] = new State(new int[]{37,81});
    states[81] = new State(-27);
    states[82] = new State(-28);
    states[83] = new State(-23);
    states[84] = new State(-29);
    states[85] = new State(-13);
    states[86] = new State(new int[]{30,87});
    states[87] = new State(new int[]{40,14,23,28,28,30,29,32,30,34,41,40,42,41,43,42},new int[]{-13,88,-14,17,-15,49,-16,69,-17,60,-18,59,-19,58,-20,38,-21,39});
    states[88] = new State(new int[]{31,89});
    states[89] = new State(new int[]{40,14,23,28,28,30,29,32,30,34,41,40,42,41,43,42,35,73,36,78,38,84,32,86,34,94,5,100},new int[]{-5,90,-6,11,-13,13,-14,17,-15,49,-16,69,-17,60,-18,59,-19,58,-20,38,-21,39,-10,72,-11,77,-12,83,-7,85,-8,93,-9,99});
    states[90] = new State(new int[]{33,91,7,-16,40,-16,23,-16,28,-16,29,-16,30,-16,41,-16,42,-16,43,-16,35,-16,36,-16,38,-16,32,-16,34,-16,5,-16});
    states[91] = new State(new int[]{40,14,23,28,28,30,29,32,30,34,41,40,42,41,43,42,35,73,36,78,38,84,32,86,34,94,5,100},new int[]{-5,92,-6,11,-13,13,-14,17,-15,49,-16,69,-17,60,-18,59,-19,58,-20,38,-21,39,-10,72,-11,77,-12,83,-7,85,-8,93,-9,99});
    states[92] = new State(-17);
    states[93] = new State(-14);
    states[94] = new State(new int[]{30,95});
    states[95] = new State(new int[]{40,14,23,28,28,30,29,32,30,34,41,40,42,41,43,42},new int[]{-13,96,-14,17,-15,49,-16,69,-17,60,-18,59,-19,58,-20,38,-21,39});
    states[96] = new State(new int[]{31,97});
    states[97] = new State(new int[]{40,14,23,28,28,30,29,32,30,34,41,40,42,41,43,42,35,73,36,78,38,84,32,86,34,94,5,100},new int[]{-5,98,-6,11,-13,13,-14,17,-15,49,-16,69,-17,60,-18,59,-19,58,-20,38,-21,39,-10,72,-11,77,-12,83,-7,85,-8,93,-9,99});
    states[98] = new State(-18);
    states[99] = new State(-15);
    states[100] = new State(-11,new int[]{-23,101});
    states[101] = new State(new int[]{7,102,40,14,23,28,28,30,29,32,30,34,41,40,42,41,43,42,35,73,36,78,38,84,32,86,34,94,5,100},new int[]{-5,10,-6,11,-13,13,-14,17,-15,49,-16,69,-17,60,-18,59,-19,58,-20,38,-21,39,-10,72,-11,77,-12,83,-7,85,-8,93,-9,99});
    states[102] = new State(-19);
    states[103] = new State(-4);
    states[104] = new State(-6,new int[]{-26,105});
    states[105] = new State(-9,new int[]{-2,106});
    states[106] = new State(new int[]{40,107});
    states[107] = new State(new int[]{12,108,11,109});
    states[108] = new State(-7);
    states[109] = new State(-8);
    states[110] = new State(-63);
    states[111] = new State(-64);
    states[112] = new State(-65);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-25, new int[]{-24,3});
    rules[2] = new Rule(-24, new int[]{4,-3,6});
    rules[3] = new Rule(-3, new int[]{5,-22,-23,7});
    rules[4] = new Rule(-22, new int[]{-22,-4});
    rules[5] = new Rule(-22, new int[]{});
    rules[6] = new Rule(-26, new int[]{});
    rules[7] = new Rule(-4, new int[]{-1,-26,-2,40,12});
    rules[8] = new Rule(-2, new int[]{-2,40,11});
    rules[9] = new Rule(-2, new int[]{});
    rules[10] = new Rule(-23, new int[]{-23,-5});
    rules[11] = new Rule(-23, new int[]{});
    rules[12] = new Rule(-5, new int[]{-6,12});
    rules[13] = new Rule(-5, new int[]{-7});
    rules[14] = new Rule(-5, new int[]{-8});
    rules[15] = new Rule(-5, new int[]{-9});
    rules[16] = new Rule(-7, new int[]{32,30,-13,31,-5});
    rules[17] = new Rule(-7, new int[]{32,30,-13,31,-5,33,-5});
    rules[18] = new Rule(-8, new int[]{34,30,-13,31,-5});
    rules[19] = new Rule(-9, new int[]{5,-23,7});
    rules[20] = new Rule(-6, new int[]{-13});
    rules[21] = new Rule(-6, new int[]{-10});
    rules[22] = new Rule(-6, new int[]{-11});
    rules[23] = new Rule(-6, new int[]{-12});
    rules[24] = new Rule(-10, new int[]{35,40});
    rules[25] = new Rule(-10, new int[]{35,40,11,37});
    rules[26] = new Rule(-11, new int[]{36,-13});
    rules[27] = new Rule(-11, new int[]{36,-13,11,37});
    rules[28] = new Rule(-11, new int[]{36,39});
    rules[29] = new Rule(-12, new int[]{38});
    rules[30] = new Rule(-13, new int[]{40,13,-13});
    rules[31] = new Rule(-13, new int[]{-14});
    rules[32] = new Rule(-14, new int[]{-14,14,-15});
    rules[33] = new Rule(-14, new int[]{-14,15,-15});
    rules[34] = new Rule(-14, new int[]{-15});
    rules[35] = new Rule(-15, new int[]{-15,16,-16});
    rules[36] = new Rule(-15, new int[]{-15,17,-16});
    rules[37] = new Rule(-15, new int[]{-15,18,-16});
    rules[38] = new Rule(-15, new int[]{-15,19,-16});
    rules[39] = new Rule(-15, new int[]{-15,20,-16});
    rules[40] = new Rule(-15, new int[]{-15,21,-16});
    rules[41] = new Rule(-15, new int[]{-16});
    rules[42] = new Rule(-16, new int[]{-16,22,-17});
    rules[43] = new Rule(-16, new int[]{-16,23,-17});
    rules[44] = new Rule(-16, new int[]{-17});
    rules[45] = new Rule(-17, new int[]{-17,24,-18});
    rules[46] = new Rule(-17, new int[]{-17,25,-18});
    rules[47] = new Rule(-17, new int[]{-18});
    rules[48] = new Rule(-18, new int[]{-18,26,-19});
    rules[49] = new Rule(-18, new int[]{-18,27,-19});
    rules[50] = new Rule(-18, new int[]{-19});
    rules[51] = new Rule(-19, new int[]{23,-19});
    rules[52] = new Rule(-19, new int[]{28,-19});
    rules[53] = new Rule(-19, new int[]{29,-19});
    rules[54] = new Rule(-19, new int[]{30,8,31,-19});
    rules[55] = new Rule(-19, new int[]{30,10,31,-19});
    rules[56] = new Rule(-19, new int[]{-20});
    rules[57] = new Rule(-20, new int[]{30,-13,31});
    rules[58] = new Rule(-20, new int[]{-21});
    rules[59] = new Rule(-21, new int[]{41});
    rules[60] = new Rule(-21, new int[]{42});
    rules[61] = new Rule(-21, new int[]{43});
    rules[62] = new Rule(-21, new int[]{40});
    rules[63] = new Rule(-1, new int[]{8});
    rules[64] = new Rule(-1, new int[]{10});
    rules[65] = new Rule(-1, new int[]{9});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 2: // start -> Program, body, Eof
#line 27 ".\compiler.y"
                                   { Compiler.GenProgram(new Compiler.ProgramNode(ValueStack[ValueStack.Depth-2].node)); }
#line default
        break;
      case 3: // body -> OpenBlock, declarations, statements, CloseBlock
#line 30 ".\compiler.y"
                                                               { CurrentSemanticValue.node = new Compiler.BodyNode(ValueStack[ValueStack.Depth-3].nodesList,ValueStack[ValueStack.Depth-2].nodesList); }
#line default
        break;
      case 4: // declarations -> declarations, declaration
#line 33 ".\compiler.y"
                                           { if(ValueStack[ValueStack.Depth-1].node != null) ValueStack[ValueStack.Depth-2].nodesList.Add(ValueStack[ValueStack.Depth-1].node); CurrentSemanticValue.nodesList = ValueStack[ValueStack.Depth-2].nodesList; }
#line default
        break;
      case 5: // declarations -> /* empty */
#line 34 ".\compiler.y"
                  { CurrentSemanticValue.nodesList = new List<Compiler.INode>(); }
#line default
        break;
      case 6: // Anon@1 -> /* empty */
#line 37 ".\compiler.y"
                       { Compiler.actualType = ValueStack[ValueStack.Depth-1].types; }
#line default
        break;
      case 7: // declaration -> type, Anon@1, multideclarations, Identificator, Semicolon
#line 38 ".\compiler.y"
                  {
                        if(!Compiler.IsIdentyficatorOccupied(ValueStack[ValueStack.Depth-5].types,ValueStack[ValueStack.Depth-2].val))
                        {
                            ValueStack[ValueStack.Depth-3].varNames.Add(ValueStack[ValueStack.Depth-2].val);
                            CurrentSemanticValue.node = new Compiler.DeclarationNode(ValueStack[ValueStack.Depth-5].types,ValueStack[ValueStack.Depth-3].varNames);
                        }
                  }
#line default
        break;
      case 8: // multideclarations -> multideclarations, Identificator, Coma
#line 48 ".\compiler.y"
                    {
                        if(!Compiler.IsIdentyficatorOccupied(Compiler.actualType,ValueStack[ValueStack.Depth-2].val))
                        {
                            ValueStack[ValueStack.Depth-3].varNames.Add(ValueStack[ValueStack.Depth-2].val);
                            CurrentSemanticValue.varNames = ValueStack[ValueStack.Depth-3].varNames;
                        }
                    }
#line default
        break;
      case 9: // multideclarations -> /* empty */
#line 55 ".\compiler.y"
                  { CurrentSemanticValue.varNames = new List<string>(); }
#line default
        break;
      case 10: // statements -> statements, statement
#line 58 ".\compiler.y"
                                       { ValueStack[ValueStack.Depth-2].nodesList.Add(ValueStack[ValueStack.Depth-1].node); CurrentSemanticValue.nodesList = ValueStack[ValueStack.Depth-2].nodesList; }
#line default
        break;
      case 11: // statements -> /* empty */
#line 59 ".\compiler.y"
                  { CurrentSemanticValue.nodesList = new List<Compiler.INode>(); }
#line default
        break;
      case 12: // statement -> singleOperation, Semicolon
#line 62 ".\compiler.y"
                                            { CurrentSemanticValue.node = ValueStack[ValueStack.Depth-2].node; }
#line default
        break;
      case 13: // statement -> if
#line 63 ".\compiler.y"
                     { CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node; }
#line default
        break;
      case 14: // statement -> while
#line 64 ".\compiler.y"
                        { CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node; }
#line default
        break;
      case 15: // statement -> block
#line 65 ".\compiler.y"
                        { CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node; }
#line default
        break;
      case 16: // if -> If, OpenParenthesis, expressionAssig, CloseParenthesis, statement
#line 69 ".\compiler.y"
                    { CurrentSemanticValue.node = new Compiler.IfNode(ValueStack[ValueStack.Depth-3].expresionNode, ValueStack[ValueStack.Depth-1].node); }
#line default
        break;
      case 17: // if -> If, OpenParenthesis, expressionAssig, CloseParenthesis, statement, Else, 
               //       statement
#line 71 ".\compiler.y"
                    { CurrentSemanticValue.node = new Compiler.IfElseNode(ValueStack[ValueStack.Depth-5].expresionNode, ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node);  }
#line default
        break;
      case 18: // while -> While, OpenParenthesis, expressionAssig, CloseParenthesis, statement
#line 75 ".\compiler.y"
                    { CurrentSemanticValue.node = new Compiler.WhileNode(ValueStack[ValueStack.Depth-3].expresionNode, ValueStack[ValueStack.Depth-1].node); }
#line default
        break;
      case 19: // block -> OpenBlock, statements, CloseBlock
#line 78 ".\compiler.y"
                                                  { CurrentSemanticValue.node = new Compiler.BlockNode(ValueStack[ValueStack.Depth-2].nodesList); }
#line default
        break;
      case 20: // singleOperation -> expressionAssig
#line 81 ".\compiler.y"
                                  { CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].expresionNode; }
#line default
        break;
      case 21: // singleOperation -> read
#line 82 ".\compiler.y"
                       { CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node; }
#line default
        break;
      case 22: // singleOperation -> write
#line 83 ".\compiler.y"
                        { CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node; }
#line default
        break;
      case 23: // singleOperation -> return
#line 84 ".\compiler.y"
                         { CurrentSemanticValue.node = ValueStack[ValueStack.Depth-1].node; }
#line default
        break;
      case 24: // read -> Read, Identificator
#line 87 ".\compiler.y"
                                     { CurrentSemanticValue.node = new Compiler.ReadNode(ValueStack[ValueStack.Depth-1].val); }
#line default
        break;
      case 25: // read -> Read, Identificator, Coma, Hex
#line 88 ".\compiler.y"
                                              { CurrentSemanticValue.node = new Compiler.HexReadNode(ValueStack[ValueStack.Depth-3].val); }
#line default
        break;
      case 26: // write -> Write, expressionAssig
#line 91 ".\compiler.y"
                                        { CurrentSemanticValue.node = new Compiler.WriteNode(ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 27: // write -> Write, expressionAssig, Coma, Hex
#line 92 ".\compiler.y"
                                                 { CurrentSemanticValue.node = new Compiler.HexWriteNode(ValueStack[ValueStack.Depth-3].expresionNode); }
#line default
        break;
      case 28: // write -> Write, String
#line 93 ".\compiler.y"
                               { CurrentSemanticValue.node = new Compiler.StringWriteNode(ValueStack[ValueStack.Depth-1].val); }
#line default
        break;
      case 29: // return -> Return
#line 96 ".\compiler.y"
                         { CurrentSemanticValue.node = new Compiler.ReturnNode(); }
#line default
        break;
      case 30: // expressionAssig -> Identificator, Assignment, expressionAssig
#line 100 ".\compiler.y"
                {
                   CurrentSemanticValue.expresionNode = new Compiler.AssignmentExpresionNode(ValueStack[ValueStack.Depth-3].val, ValueStack[ValueStack.Depth-1].expresionNode);
                }
#line default
        break;
      case 31: // expressionAssig -> expressionLogic
#line 103 ".\compiler.y"
                                  { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-1].expresionNode; }
#line default
        break;
      case 32: // expressionLogic -> expressionLogic, And, expressionRelat
#line 106 ".\compiler.y"
                                                      { CurrentSemanticValue.expresionNode = new Compiler.AndLogicalExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode,ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 33: // expressionLogic -> expressionLogic, Or, expressionRelat
#line 107 ".\compiler.y"
                                                     { CurrentSemanticValue.expresionNode = new Compiler.OrLogicalExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode,ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 34: // expressionLogic -> expressionRelat
#line 108 ".\compiler.y"
                                  { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-1].expresionNode; }
#line default
        break;
      case 35: // expressionRelat -> expressionRelat, Equal, expressionAddit
#line 111 ".\compiler.y"
                                                        {  CurrentSemanticValue.expresionNode = new Compiler.EqualExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode, ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 36: // expressionRelat -> expressionRelat, NotEqual, expressionAddit
#line 112 ".\compiler.y"
                                                           {  CurrentSemanticValue.expresionNode = new Compiler.NotEqualExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode, ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 37: // expressionRelat -> expressionRelat, Greater, expressionAddit
#line 113 ".\compiler.y"
                                                          {  CurrentSemanticValue.expresionNode = new Compiler.GreaterExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode, ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 38: // expressionRelat -> expressionRelat, GreaterEqual, expressionAddit
#line 114 ".\compiler.y"
                                                               {  CurrentSemanticValue.expresionNode = new Compiler.GreaterEqualExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode, ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 39: // expressionRelat -> expressionRelat, Less, expressionAddit
#line 115 ".\compiler.y"
                                                       {  CurrentSemanticValue.expresionNode = new Compiler.LessExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode, ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 40: // expressionRelat -> expressionRelat, LessEqual, expressionAddit
#line 116 ".\compiler.y"
                                                            {  CurrentSemanticValue.expresionNode = new Compiler.LessEqualExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode, ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 41: // expressionRelat -> expressionAddit
#line 117 ".\compiler.y"
                                  { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-1].expresionNode; }
#line default
        break;
      case 42: // expressionAddit -> expressionAddit, Plus, expressionMulti
#line 120 ".\compiler.y"
                                                       { CurrentSemanticValue.expresionNode = new Compiler.PlusExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode,ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 43: // expressionAddit -> expressionAddit, Minus, expressionMulti
#line 121 ".\compiler.y"
                                                        { CurrentSemanticValue.expresionNode = new Compiler.MinusExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode,ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 44: // expressionAddit -> expressionMulti
#line 122 ".\compiler.y"
                                  { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-1].expresionNode; }
#line default
        break;
      case 45: // expressionMulti -> expressionMulti, Multiply, expressionBinar
#line 125 ".\compiler.y"
                                                           { CurrentSemanticValue.expresionNode = new Compiler.MultiplyExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode,ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 46: // expressionMulti -> expressionMulti, Divide, expressionBinar
#line 126 ".\compiler.y"
                                                         { CurrentSemanticValue.expresionNode = new Compiler.DivideExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode,ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 47: // expressionMulti -> expressionBinar
#line 127 ".\compiler.y"
                                  { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-1].expresionNode; }
#line default
        break;
      case 48: // expressionBinar -> expressionBinar, BinaryMultiply, expressionUnary
#line 130 ".\compiler.y"
                                                                 { CurrentSemanticValue.expresionNode = new Compiler.BinaryMultiplyExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode,ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 49: // expressionBinar -> expressionBinar, BinarySum, expressionUnary
#line 131 ".\compiler.y"
                                                            { CurrentSemanticValue.expresionNode = new Compiler.BinarySumExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode,ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 50: // expressionBinar -> expressionUnary
#line 132 ".\compiler.y"
                                  { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-1].expresionNode; }
#line default
        break;
      case 51: // expressionUnary -> Minus, expressionUnary
#line 135 ".\compiler.y"
                                        { CurrentSemanticValue.expresionNode = new Compiler.UnaryMinusExpresionNode(ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 52: // expressionUnary -> UnaryNegation, expressionUnary
#line 136 ".\compiler.y"
                                                { CurrentSemanticValue.expresionNode = new Compiler.UnaryNegationExpresionNode(ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 53: // expressionUnary -> LogicalNegation, expressionUnary
#line 137 ".\compiler.y"
                                                  { CurrentSemanticValue.expresionNode = new Compiler.LogicalNegationExpresionNode(ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 54: // expressionUnary -> OpenParenthesis, Int, CloseParenthesis, expressionUnary
#line 138 ".\compiler.y"
                                                                       { CurrentSemanticValue.expresionNode = new Compiler.IntConversionExpresionNode(ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 55: // expressionUnary -> OpenParenthesis, Double, CloseParenthesis, expressionUnary
#line 139 ".\compiler.y"
                                                                          { CurrentSemanticValue.expresionNode = new Compiler.DoubleConversionExpresionNode(ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 56: // expressionUnary -> expression
#line 140 ".\compiler.y"
                             { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-1].expresionNode; }
#line default
        break;
      case 57: // expression -> OpenParenthesis, expressionAssig, CloseParenthesis
#line 143 ".\compiler.y"
                                                                   { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-2].expresionNode; }
#line default
        break;
      case 58: // expression -> variable
#line 144 ".\compiler.y"
                           { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-1].expresionNode; }
#line default
        break;
      case 59: // variable -> IntNumber
#line 147 ".\compiler.y"
                            { CurrentSemanticValue.expresionNode = new Compiler.ConstantExpresionNode(Compiler.Types.IntegerType, ValueStack[ValueStack.Depth-1].val); }
#line default
        break;
      case 60: // variable -> RealNumber
#line 148 ".\compiler.y"
                             { CurrentSemanticValue.expresionNode = new Compiler.ConstantExpresionNode(Compiler.Types.DoubleType, ValueStack[ValueStack.Depth-1].val); }
#line default
        break;
      case 61: // variable -> Boolean
#line 149 ".\compiler.y"
                          { CurrentSemanticValue.expresionNode = new Compiler.ConstantExpresionNode(Compiler.Types.BooleanType, ValueStack[ValueStack.Depth-1].val); }
#line default
        break;
      case 62: // variable -> Identificator
#line 150 ".\compiler.y"
                                { CurrentSemanticValue.expresionNode = new Compiler.VariableExpresionNode(ValueStack[ValueStack.Depth-1].val); }
#line default
        break;
      case 63: // type -> Int
#line 153 ".\compiler.y"
                      {CurrentSemanticValue.types = Compiler.Types.IntegerType;}
#line default
        break;
      case 64: // type -> Double
#line 154 ".\compiler.y"
                         {CurrentSemanticValue.types = Compiler.Types.DoubleType;}
#line default
        break;
      case 65: // type -> Bool
#line 155 ".\compiler.y"
                       {CurrentSemanticValue.types = Compiler.Types.BooleanType;}
#line default
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

#line 159 ".\compiler.y"

public Parser(Scanner scanner) : base(scanner) { }
#line default
}
}
