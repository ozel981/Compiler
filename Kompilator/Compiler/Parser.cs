// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  DESKTOP-PIILLQS
// DateTime: 11.06.2021 19:23:34
// UserName: wpodm
// Input file <.\compiler.y - 11.06.2021 18:54:44>

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
    Divide=25,BinaryMultiply=26,BinarySum=27,UnaryNegation=28,LogicalNegation=29,IntConversion=30,
    DoubleConversion=31,OpenParenthesis=32,CloseParenthesis=33,If=34,Else=35,While=36,
    Identificator=37,IntNumber=38,RealNumber=39,Boolean=40};

public struct ValueType
#line 7 ".\compiler.y"
{
public List<string> varNames;
public List<Compiler.INode> nodesList;
public string val;
public Compiler.Types types;
public Compiler.INode node;
public Compiler.ExpresionNode expresionNode;
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
  private static Rule[] rules = new Rule[57];
  private static State[] states = new State[98];
  private static string[] nonTerms = new string[] {
      "type", "multideclarations", "body", "declaration", "statement", "singleOperation", 
      "if", "while", "block", "expressionAssig", "expressionLogic", "expressionRelat", 
      "expressionAddit", "expressionMulti", "expressionBinar", "expressionUnary", 
      "expression", "variable", "declarations", "statements", "start", "$accept", 
      "Anon@1", };

  static Parser() {
    states[0] = new State(new int[]{4,3},new int[]{-21,1});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{5,6},new int[]{-3,4});
    states[4] = new State(new int[]{6,5});
    states[5] = new State(-2);
    states[6] = new State(-5,new int[]{-19,7});
    states[7] = new State(new int[]{8,95,10,96,9,97,7,-11,37,-11,23,-11,28,-11,29,-11,30,-11,31,-11,32,-11,38,-11,39,-11,40,-11,34,-11,36,-11,5,-11},new int[]{-20,8,-4,88,-1,89});
    states[8] = new State(new int[]{7,9,37,14,23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,34,71,36,79,5,85},new int[]{-5,10,-6,11,-10,13,-11,17,-12,42,-13,67,-14,58,-15,57,-16,56,-17,38,-18,51,-7,70,-8,78,-9,84});
    states[9] = new State(-3);
    states[10] = new State(-10);
    states[11] = new State(new int[]{12,12});
    states[12] = new State(-12);
    states[13] = new State(-20);
    states[14] = new State(new int[]{13,15,26,-53,27,-53,24,-53,25,-53,22,-53,23,-53,16,-53,17,-53,18,-53,19,-53,20,-53,21,-53,14,-53,15,-53,12,-53,33,-53});
    states[15] = new State(new int[]{37,14,23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54},new int[]{-10,16,-11,17,-12,42,-13,67,-14,58,-15,57,-16,56,-17,38,-18,51});
    states[16] = new State(-21);
    states[17] = new State(new int[]{14,18,15,68,12,-22,33,-22});
    states[18] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-12,19,-13,67,-14,58,-15,57,-16,56,-17,38,-18,51});
    states[19] = new State(new int[]{16,20,17,43,18,59,19,61,20,63,21,65,14,-23,15,-23,12,-23,33,-23});
    states[20] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-13,21,-14,58,-15,57,-16,56,-17,38,-18,51});
    states[21] = new State(new int[]{22,22,23,45,16,-26,17,-26,18,-26,19,-26,20,-26,21,-26,14,-26,15,-26,12,-26,33,-26});
    states[22] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-14,23,-15,57,-16,56,-17,38,-18,51});
    states[23] = new State(new int[]{24,24,25,47,22,-33,23,-33,16,-33,17,-33,18,-33,19,-33,20,-33,21,-33,14,-33,15,-33,12,-33,33,-33});
    states[24] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-15,25,-16,56,-17,38,-18,51});
    states[25] = new State(new int[]{26,26,27,49,24,-36,25,-36,22,-36,23,-36,16,-36,17,-36,18,-36,19,-36,20,-36,21,-36,14,-36,15,-36,12,-36,33,-36});
    states[26] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-16,27,-17,38,-18,51});
    states[27] = new State(-39);
    states[28] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-16,29,-17,38,-18,51});
    states[29] = new State(-42);
    states[30] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-16,31,-17,38,-18,51});
    states[31] = new State(-43);
    states[32] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-16,33,-17,38,-18,51});
    states[33] = new State(-44);
    states[34] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-16,35,-17,38,-18,51});
    states[35] = new State(-45);
    states[36] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-16,37,-17,38,-18,51});
    states[37] = new State(-46);
    states[38] = new State(-47);
    states[39] = new State(new int[]{37,14,23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54},new int[]{-10,40,-11,17,-12,42,-13,67,-14,58,-15,57,-16,56,-17,38,-18,51});
    states[40] = new State(new int[]{33,41});
    states[41] = new State(-48);
    states[42] = new State(new int[]{16,20,17,43,18,59,19,61,20,63,21,65,14,-25,15,-25,12,-25,33,-25});
    states[43] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-13,44,-14,58,-15,57,-16,56,-17,38,-18,51});
    states[44] = new State(new int[]{22,22,23,45,16,-27,17,-27,18,-27,19,-27,20,-27,21,-27,14,-27,15,-27,12,-27,33,-27});
    states[45] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-14,46,-15,57,-16,56,-17,38,-18,51});
    states[46] = new State(new int[]{24,24,25,47,22,-34,23,-34,16,-34,17,-34,18,-34,19,-34,20,-34,21,-34,14,-34,15,-34,12,-34,33,-34});
    states[47] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-15,48,-16,56,-17,38,-18,51});
    states[48] = new State(new int[]{26,26,27,49,24,-37,25,-37,22,-37,23,-37,16,-37,17,-37,18,-37,19,-37,20,-37,21,-37,14,-37,15,-37,12,-37,33,-37});
    states[49] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-16,50,-17,38,-18,51});
    states[50] = new State(-40);
    states[51] = new State(-49);
    states[52] = new State(-50);
    states[53] = new State(-51);
    states[54] = new State(-52);
    states[55] = new State(-53);
    states[56] = new State(-41);
    states[57] = new State(new int[]{26,26,27,49,24,-38,25,-38,22,-38,23,-38,16,-38,17,-38,18,-38,19,-38,20,-38,21,-38,14,-38,15,-38,12,-38,33,-38});
    states[58] = new State(new int[]{24,24,25,47,22,-35,23,-35,16,-35,17,-35,18,-35,19,-35,20,-35,21,-35,14,-35,15,-35,12,-35,33,-35});
    states[59] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-13,60,-14,58,-15,57,-16,56,-17,38,-18,51});
    states[60] = new State(new int[]{22,22,23,45,16,-28,17,-28,18,-28,19,-28,20,-28,21,-28,14,-28,15,-28,12,-28,33,-28});
    states[61] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-13,62,-14,58,-15,57,-16,56,-17,38,-18,51});
    states[62] = new State(new int[]{22,22,23,45,16,-29,17,-29,18,-29,19,-29,20,-29,21,-29,14,-29,15,-29,12,-29,33,-29});
    states[63] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-13,64,-14,58,-15,57,-16,56,-17,38,-18,51});
    states[64] = new State(new int[]{22,22,23,45,16,-30,17,-30,18,-30,19,-30,20,-30,21,-30,14,-30,15,-30,12,-30,33,-30});
    states[65] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-13,66,-14,58,-15,57,-16,56,-17,38,-18,51});
    states[66] = new State(new int[]{22,22,23,45,16,-31,17,-31,18,-31,19,-31,20,-31,21,-31,14,-31,15,-31,12,-31,33,-31});
    states[67] = new State(new int[]{22,22,23,45,16,-32,17,-32,18,-32,19,-32,20,-32,21,-32,14,-32,15,-32,12,-32,33,-32});
    states[68] = new State(new int[]{23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,37,55},new int[]{-12,69,-13,67,-14,58,-15,57,-16,56,-17,38,-18,51});
    states[69] = new State(new int[]{16,20,17,43,18,59,19,61,20,63,21,65,14,-24,15,-24,12,-24,33,-24});
    states[70] = new State(-13);
    states[71] = new State(new int[]{32,72});
    states[72] = new State(new int[]{37,14,23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54},new int[]{-10,73,-11,17,-12,42,-13,67,-14,58,-15,57,-16,56,-17,38,-18,51});
    states[73] = new State(new int[]{33,74});
    states[74] = new State(new int[]{37,14,23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,34,71,36,79,5,85},new int[]{-5,75,-6,11,-10,13,-11,17,-12,42,-13,67,-14,58,-15,57,-16,56,-17,38,-18,51,-7,70,-8,78,-9,84});
    states[75] = new State(new int[]{35,76,7,-16,37,-16,23,-16,28,-16,29,-16,30,-16,31,-16,32,-16,38,-16,39,-16,40,-16,34,-16,36,-16,5,-16});
    states[76] = new State(new int[]{37,14,23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,34,71,36,79,5,85},new int[]{-5,77,-6,11,-10,13,-11,17,-12,42,-13,67,-14,58,-15,57,-16,56,-17,38,-18,51,-7,70,-8,78,-9,84});
    states[77] = new State(-17);
    states[78] = new State(-14);
    states[79] = new State(new int[]{32,80});
    states[80] = new State(new int[]{37,14,23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54},new int[]{-10,81,-11,17,-12,42,-13,67,-14,58,-15,57,-16,56,-17,38,-18,51});
    states[81] = new State(new int[]{33,82});
    states[82] = new State(new int[]{37,14,23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,34,71,36,79,5,85},new int[]{-5,83,-6,11,-10,13,-11,17,-12,42,-13,67,-14,58,-15,57,-16,56,-17,38,-18,51,-7,70,-8,78,-9,84});
    states[83] = new State(-18);
    states[84] = new State(-15);
    states[85] = new State(-11,new int[]{-20,86});
    states[86] = new State(new int[]{7,87,37,14,23,28,28,30,29,32,30,34,31,36,32,39,38,52,39,53,40,54,34,71,36,79,5,85},new int[]{-5,10,-6,11,-10,13,-11,17,-12,42,-13,67,-14,58,-15,57,-16,56,-17,38,-18,51,-7,70,-8,78,-9,84});
    states[87] = new State(-19);
    states[88] = new State(-4);
    states[89] = new State(-6,new int[]{-23,90});
    states[90] = new State(-9,new int[]{-2,91});
    states[91] = new State(new int[]{37,92});
    states[92] = new State(new int[]{12,93,11,94});
    states[93] = new State(-7);
    states[94] = new State(-8);
    states[95] = new State(-54);
    states[96] = new State(-55);
    states[97] = new State(-56);

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-22, new int[]{-21,3});
    rules[2] = new Rule(-21, new int[]{4,-3,6});
    rules[3] = new Rule(-3, new int[]{5,-19,-20,7});
    rules[4] = new Rule(-19, new int[]{-19,-4});
    rules[5] = new Rule(-19, new int[]{});
    rules[6] = new Rule(-23, new int[]{});
    rules[7] = new Rule(-4, new int[]{-1,-23,-2,37,12});
    rules[8] = new Rule(-2, new int[]{-2,37,11});
    rules[9] = new Rule(-2, new int[]{});
    rules[10] = new Rule(-20, new int[]{-20,-5});
    rules[11] = new Rule(-20, new int[]{});
    rules[12] = new Rule(-5, new int[]{-6,12});
    rules[13] = new Rule(-5, new int[]{-7});
    rules[14] = new Rule(-5, new int[]{-8});
    rules[15] = new Rule(-5, new int[]{-9});
    rules[16] = new Rule(-7, new int[]{34,32,-10,33,-5});
    rules[17] = new Rule(-7, new int[]{34,32,-10,33,-5,35,-5});
    rules[18] = new Rule(-8, new int[]{36,32,-10,33,-5});
    rules[19] = new Rule(-9, new int[]{5,-20,7});
    rules[20] = new Rule(-6, new int[]{-10});
    rules[21] = new Rule(-10, new int[]{37,13,-10});
    rules[22] = new Rule(-10, new int[]{-11});
    rules[23] = new Rule(-11, new int[]{-11,14,-12});
    rules[24] = new Rule(-11, new int[]{-11,15,-12});
    rules[25] = new Rule(-11, new int[]{-12});
    rules[26] = new Rule(-12, new int[]{-12,16,-13});
    rules[27] = new Rule(-12, new int[]{-12,17,-13});
    rules[28] = new Rule(-12, new int[]{-12,18,-13});
    rules[29] = new Rule(-12, new int[]{-12,19,-13});
    rules[30] = new Rule(-12, new int[]{-12,20,-13});
    rules[31] = new Rule(-12, new int[]{-12,21,-13});
    rules[32] = new Rule(-12, new int[]{-13});
    rules[33] = new Rule(-13, new int[]{-13,22,-14});
    rules[34] = new Rule(-13, new int[]{-13,23,-14});
    rules[35] = new Rule(-13, new int[]{-14});
    rules[36] = new Rule(-14, new int[]{-14,24,-15});
    rules[37] = new Rule(-14, new int[]{-14,25,-15});
    rules[38] = new Rule(-14, new int[]{-15});
    rules[39] = new Rule(-15, new int[]{-15,26,-16});
    rules[40] = new Rule(-15, new int[]{-15,27,-16});
    rules[41] = new Rule(-15, new int[]{-16});
    rules[42] = new Rule(-16, new int[]{23,-16});
    rules[43] = new Rule(-16, new int[]{28,-16});
    rules[44] = new Rule(-16, new int[]{29,-16});
    rules[45] = new Rule(-16, new int[]{30,-16});
    rules[46] = new Rule(-16, new int[]{31,-16});
    rules[47] = new Rule(-16, new int[]{-17});
    rules[48] = new Rule(-17, new int[]{32,-10,33});
    rules[49] = new Rule(-17, new int[]{-18});
    rules[50] = new Rule(-18, new int[]{38});
    rules[51] = new Rule(-18, new int[]{39});
    rules[52] = new Rule(-18, new int[]{40});
    rules[53] = new Rule(-18, new int[]{37});
    rules[54] = new Rule(-1, new int[]{8});
    rules[55] = new Rule(-1, new int[]{10});
    rules[56] = new Rule(-1, new int[]{9});
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
                                   { Compiler.GenBody(ValueStack[ValueStack.Depth-2].node); }
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
                    { CurrentSemanticValue.node = new Compiler.IfElseNode(ValueStack[ValueStack.Depth-5].expresionNode, ValueStack[ValueStack.Depth-3].node, ValueStack[ValueStack.Depth-1].node); }
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
                                  { CurrentSemanticValue.node = new Compiler.SingleOperationNode(ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 21: // expressionAssig -> Identificator, Assignment, expressionAssig
#line 85 ".\compiler.y"
                {
                   CurrentSemanticValue.expresionNode = new Compiler.AssignmentExpresionNode(ValueStack[ValueStack.Depth-3].val, ValueStack[ValueStack.Depth-1].expresionNode);
                }
#line default
        break;
      case 22: // expressionAssig -> expressionLogic
#line 88 ".\compiler.y"
                                  { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-1].expresionNode; }
#line default
        break;
      case 23: // expressionLogic -> expressionLogic, And, expressionRelat
#line 91 ".\compiler.y"
                                                      { CurrentSemanticValue.expresionNode = new Compiler.AndLogicalExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode,ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 24: // expressionLogic -> expressionLogic, Or, expressionRelat
#line 92 ".\compiler.y"
                                                     { CurrentSemanticValue.expresionNode = new Compiler.OrLogicalExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode,ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 25: // expressionLogic -> expressionRelat
#line 93 ".\compiler.y"
                                  { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-1].expresionNode; }
#line default
        break;
      case 26: // expressionRelat -> expressionRelat, Equal, expressionAddit
#line 96 ".\compiler.y"
                                                        {  CurrentSemanticValue.expresionNode = new Compiler.EqualExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode, ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 27: // expressionRelat -> expressionRelat, NotEqual, expressionAddit
#line 97 ".\compiler.y"
                                                           {  CurrentSemanticValue.expresionNode = new Compiler.NotEqualExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode, ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 28: // expressionRelat -> expressionRelat, Greater, expressionAddit
#line 98 ".\compiler.y"
                                                          {  CurrentSemanticValue.expresionNode = new Compiler.GreaterExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode, ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 29: // expressionRelat -> expressionRelat, GreaterEqual, expressionAddit
#line 99 ".\compiler.y"
                                                               {  CurrentSemanticValue.expresionNode = new Compiler.GreaterEqualExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode, ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 30: // expressionRelat -> expressionRelat, Less, expressionAddit
#line 100 ".\compiler.y"
                                                       {  CurrentSemanticValue.expresionNode = new Compiler.LessExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode, ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 31: // expressionRelat -> expressionRelat, LessEqual, expressionAddit
#line 101 ".\compiler.y"
                                                            {  CurrentSemanticValue.expresionNode = new Compiler.LessEqualExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode, ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 32: // expressionRelat -> expressionAddit
#line 102 ".\compiler.y"
                                  { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-1].expresionNode; }
#line default
        break;
      case 33: // expressionAddit -> expressionAddit, Plus, expressionMulti
#line 105 ".\compiler.y"
                                                       { CurrentSemanticValue.expresionNode = new Compiler.PlusExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode,ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 34: // expressionAddit -> expressionAddit, Minus, expressionMulti
#line 106 ".\compiler.y"
                                                        { CurrentSemanticValue.expresionNode = new Compiler.MinusExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode,ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 35: // expressionAddit -> expressionMulti
#line 107 ".\compiler.y"
                                  { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-1].expresionNode; }
#line default
        break;
      case 36: // expressionMulti -> expressionMulti, Multiply, expressionBinar
#line 110 ".\compiler.y"
                                                           { CurrentSemanticValue.expresionNode = new Compiler.MultiplyExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode,ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 37: // expressionMulti -> expressionMulti, Divide, expressionBinar
#line 111 ".\compiler.y"
                                                         { CurrentSemanticValue.expresionNode = new Compiler.DivideExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode,ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 38: // expressionMulti -> expressionBinar
#line 112 ".\compiler.y"
                                  { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-1].expresionNode; }
#line default
        break;
      case 39: // expressionBinar -> expressionBinar, BinaryMultiply, expressionUnary
#line 115 ".\compiler.y"
                                                                 { CurrentSemanticValue.expresionNode = new Compiler.BinaryMultiplyExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode,ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 40: // expressionBinar -> expressionBinar, BinarySum, expressionUnary
#line 116 ".\compiler.y"
                                                            { CurrentSemanticValue.expresionNode = new Compiler.BinarySumExpresionNode(ValueStack[ValueStack.Depth-3].expresionNode,ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 41: // expressionBinar -> expressionUnary
#line 117 ".\compiler.y"
                                  { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-1].expresionNode; }
#line default
        break;
      case 42: // expressionUnary -> Minus, expressionUnary
#line 120 ".\compiler.y"
                                        { CurrentSemanticValue.expresionNode = new Compiler.UnaryMinusExpresionNode(ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 43: // expressionUnary -> UnaryNegation, expressionUnary
#line 121 ".\compiler.y"
                                                { CurrentSemanticValue.expresionNode = new Compiler.UnaryNegationExpresionNode(ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 44: // expressionUnary -> LogicalNegation, expressionUnary
#line 122 ".\compiler.y"
                                                  { CurrentSemanticValue.expresionNode = new Compiler.LogicalNegationExpresionNode(ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 45: // expressionUnary -> IntConversion, expressionUnary
#line 123 ".\compiler.y"
                                                { CurrentSemanticValue.expresionNode = new Compiler.IntConversionExpresionNode(ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 46: // expressionUnary -> DoubleConversion, expressionUnary
#line 124 ".\compiler.y"
                                                   { CurrentSemanticValue.expresionNode = new Compiler.DoubleConversionExpresionNode(ValueStack[ValueStack.Depth-1].expresionNode); }
#line default
        break;
      case 47: // expressionUnary -> expression
#line 125 ".\compiler.y"
                             { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-1].expresionNode; }
#line default
        break;
      case 48: // expression -> OpenParenthesis, expressionAssig, CloseParenthesis
#line 128 ".\compiler.y"
                                                                   { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-2].expresionNode; }
#line default
        break;
      case 49: // expression -> variable
#line 129 ".\compiler.y"
                           { CurrentSemanticValue.expresionNode = ValueStack[ValueStack.Depth-1].expresionNode; }
#line default
        break;
      case 50: // variable -> IntNumber
#line 132 ".\compiler.y"
                            { CurrentSemanticValue.expresionNode = new Compiler.ConstantExpresionNode(Compiler.Types.IntegerType, ValueStack[ValueStack.Depth-1].val); }
#line default
        break;
      case 51: // variable -> RealNumber
#line 133 ".\compiler.y"
                             { CurrentSemanticValue.expresionNode = new Compiler.ConstantExpresionNode(Compiler.Types.DoubleType, ValueStack[ValueStack.Depth-1].val); }
#line default
        break;
      case 52: // variable -> Boolean
#line 134 ".\compiler.y"
                          { CurrentSemanticValue.expresionNode = new Compiler.ConstantExpresionNode(Compiler.Types.BooleanType, ValueStack[ValueStack.Depth-1].val); }
#line default
        break;
      case 53: // variable -> Identificator
#line 135 ".\compiler.y"
                                { CurrentSemanticValue.expresionNode = new Compiler.VariableExpresionNode(ValueStack[ValueStack.Depth-1].val); }
#line default
        break;
      case 54: // type -> Int
#line 138 ".\compiler.y"
                      {CurrentSemanticValue.types = Compiler.Types.IntegerType;}
#line default
        break;
      case 55: // type -> Double
#line 139 ".\compiler.y"
                         {CurrentSemanticValue.types = Compiler.Types.DoubleType;}
#line default
        break;
      case 56: // type -> Bool
#line 140 ".\compiler.y"
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

#line 144 ".\compiler.y"

public Parser(Scanner scanner) : base(scanner) { }
#line default
}
}
