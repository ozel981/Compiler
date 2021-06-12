
using System;
using System.IO;
using System.Collections.Generic;
using GardensPoint;

public class Compiler
{
    public enum Types
    {
        IntegerType = 0,
        DoubleType = 1,
        BooleanType = 2,
    }

    public static int errors = 0;
    public static Dictionary<string, Types> variables = new Dictionary<string, Types>();
    public static List<string> source;
    public static Types actualType;
    public static int registersCount = 0;
    public static int labelsCount = 0;
    public static int lineNumber = 0;

    public static string UniqueVariableName(string name) => $"{name}VAR";

    public static Types GetMoreOverallType(Types first, Types second)
    {
        if (first == Types.DoubleType || second == Types.DoubleType)
            return Types.DoubleType;
        if (first == Types.IntegerType || second == Types.IntegerType)
            return Types.IntegerType;
        return Types.BooleanType;
    }

    public static string TypeName(Types type)
    {
        switch(type)
        {
            case Types.IntegerType: return "int";
            case Types.DoubleType: return "double";
            case Types.BooleanType: return "bool";
            default: return "";
        }
    }

    public static string GetTypeString(Types type)
    {
        switch (type)
        {
            case Types.BooleanType: return "i1";
            case Types.IntegerType: return "i32";
            case Types.DoubleType: return "double";
        }
        return "";
    }

    public struct Pair
    {
        public Types Type;
        public string Value;

        public Pair(Types type, string value)
        {
            this.Type = type;
            this.Value = value;
        }
    }

    public interface ICodeEmiter
    {
        void EmitProgramProlog();
        void EmitProgramEpilog();
        void EmitReturnCode();
        #region IOSTREAM
        void EmitReadCode(Types type, string variableName);
        void EmitHexReadCode(Types type, string variableName);
        void EmitWriteCode(Types type, int registerNumber);
        void EmitHexWriteCode(int registerNumber);
        void EmitStringWriteCode(string inscription);
        #endregion
        #region CONDITIONS
        void EmitIfCode(int registerNumber, int labelNumber);
        void EmitElseCode(int labelNumber);
        void EmitWhileBeginningCode(int labelNumber);
        void EmitWhileCode(int registerNumber, int labelNumber);
        void EmitWhileEndCode(int labelNumber);
        #endregion
        #region EXPRESSIONS
        void EmitVariableCode(Pair variable, int outputRegisterNumber);
        void EmitDeclarationCode(Types type, string variableName);
        void EmitConstantExprestionCode(Pair constant, int outputRegisterNumber);
        #region UNARY
        void EmitUnaryMinusCode(Types type, int outputRegisterNumber, int registerNumber);
        void EmitUnaryNegationCode(int outputRegisterNumber, int registerNumber);
        void EmitLogicalNegationCode(int outputRegisterNumber, int registerNumber);
        #endregion
        #region COVERSIONS
        void EmitBoolToIntConversionCode(int outputRegisterNumber, int registerNumber);
        void EmitIntToDoubleConversionCode(int outputRegisterNumber, int registerNumber);
        void EmitDoubleToIntConversionCode(int outputRegisterNumber, int registerNumber);
        #endregion
        #region BINARY
        void EmitBinaryMultiplyCode(int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber);
        void EmitBinarySumCode(int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber);
        #endregion
        #region RECKONING
        void EmitMultiplyCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber);
        void EmitDivideCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber);
        void EmitPlusCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber);
        void EmitMinusCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber);
        #endregion
        #region RELATION
        void EmitEqualCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber);
        void EmitNotEqualCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber);
        void EmitGreaterCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber);
        void EmitGreaterEqualCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber);
        void EmitLessCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber);
        void EmitLessEqualCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber);
        #endregion
        #region LOGICAL
        void EmitRightAndLogicalExpresionCode(int outputRegisterNumber, int rightRegisterNumber, int labelNumber);
        void EmitLeftAndLogicalExpresionCode(int outputRegisterNumber, int leftRegisterNumber, int labelNumber);
        void EmitRightOrLogicalExpresionCode(int outputRegisterNumber, int rightRegisterNumber, int labelNumber);
        void EmitLeftOrLogicalExpresionCode(int outputRegisterNumber, int leftRegisterNumber, int labelNumber);
        #endregion
        void EmitAssignmentExpresionCode(Pair variable, int outputRegisterNumber, int rvalueRegisterNumber);
        #endregion
    }

    public class LLVMCodeEmiter : ICodeEmiter
    {
        public void EmitProgramProlog()
        {
            EmitCode("define i32 @main()\n{");
        }
        public void EmitProgramEpilog()
        {
            EmitCode($"br label %END_PROGRAM");
            EmitCode($"END_PROGRAM:");
            EmitCode("ret i32 0\n}");
        }
        public void EmitReturnCode()
        {
            EmitCode($"br label %END_PROGRAM");
        }
        #region IOSTREAM     
        public void EmitReadCode(Types type, string variableName)
        {

        }
        public void EmitHexReadCode(Types type, string variableName)
        {

        }
        public void EmitWriteCode(Types type, int registerNumber)
        {

        }
        public void EmitHexWriteCode(int registerNumber)
        {

        }
        public void EmitStringWriteCode(string inscription)
        {

        }
        #endregion
        #region CONDITIONS
        public void EmitIfCode(int registerNumber, int labelNumber)
        {
            string trueLabel = $"";
            EmitCode($"br i1 %tmp{registerNumber}, label %TRUE{labelNumber}, label %FALSE{labelNumber}");
            EmitCode($"TRUE{labelNumber}:");
        }
        public void EmitElseCode(int labelNumber)
        {
            EmitCode($"br label %FALSE{labelNumber}");
            EmitCode($"FALSE{labelNumber}:");
        }
        public void EmitWhileBeginningCode(int labelNumber)
        {
            EmitCode($"br label %WHILE{labelNumber}");
            EmitCode($"WHILE{labelNumber}:");
        }
        public void EmitWhileCode(int registerNumber, int labelNumber)
        {
            EmitCode($"br i1 %tmp{registerNumber}, label %DO{labelNumber}, label %END{labelNumber}");
            EmitCode($"DO{labelNumber}:");
        }
        public void EmitWhileEndCode(int labelNumber)
        {
            EmitCode($"br label %WHILE{labelNumber}");
            EmitCode($"END{labelNumber}:");
        }
        #endregion
        #region EXPRESSIONS
        public void EmitVariableCode(Pair variable, int outputRegisterNumber)
        {
            string typeString = GetTypeString(variable.Type);
            EmitCode($"%tmp{outputRegisterNumber} = load {typeString}, {typeString}* %{variable.Value}");
        }
        public void EmitDeclarationCode(Types type, string variableName)
        {
            string typeString = GetTypeString(type);
            EmitCode($"%{variableName} = alloca {typeString}");
        }
        public void EmitConstantExprestionCode(Pair constant, int outputRegisterNumber)
        {
            string typeString = GetTypeString(constant.Type);
            if(constant.Type == Types.BooleanType)
            {
                EmitCode($"%tmp{outputRegisterNumber} = add {typeString} 0, {(constant.Value == "true" ? 1 : 0)}");
            }
            else
            {
                EmitCode($"%tmp{outputRegisterNumber} = add {typeString} 0, {constant.Value}");
            }
        }
        #region UNARY
        public void EmitUnaryMinusCode(Types type, int outputRegisterNumber, int registerNumber)
        {
            string typeString = GetTypeString(type);
            if (type == Types.DoubleType)
            {
                EmitCode($"%tmp{outputRegisterNumber} = fsub {typeString} 0, %tmp{registerNumber}");
            }
            else
            {
                EmitCode($"%tmp{outputRegisterNumber} = sub {typeString} 0, %tmp{registerNumber}");
            }
        }
        public void EmitUnaryNegationCode(int outputRegisterNumber, int registerNumber)
        {
            EmitCode($"%tmp{outputRegisterNumber} = xor i32 -1, %tmp{registerNumber}");
        }
        public void EmitLogicalNegationCode(int outputRegisterNumber, int registerNumber)
        {
            EmitCode($"%tmp{outputRegisterNumber} = xor i1 1, %tmp{registerNumber}");
        }
        #endregion
        #region CONVERSIONS
        public void EmitBoolToIntConversionCode(int outputRegisterNumber, int registerNumber)
        {
            EmitCode($"%tmp{outputRegisterNumber} = zext i1 %tmp{registerNumber} to i32 ");
        }
        public void EmitIntToDoubleConversionCode(int outputRegisterNumber, int registerNumber)
        {
            EmitCode($"%tmp{outputRegisterNumber} = sitofp i32 %tmp{registerNumber} to double ");
        }
        public void EmitDoubleToIntConversionCode(int outputRegisterNumber, int registerNumber)
        {
            EmitCode($"%tmp{outputRegisterNumber} = fptosi double %tmp{registerNumber} to i32");
        }
        #endregion
        #region BINARY
        public void EmitBinaryMultiplyCode(int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            EmitCode($"%tmp{outputRegisterNumber} = and i32 %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
        }
        public void EmitBinarySumCode(int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            EmitCode($"%tmp{outputRegisterNumber} = or i32 %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
        }
        #endregion
        #region RECKONING
        public void EmitMultiplyCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            string typeString = GetTypeString(type);
            if (type == Types.DoubleType)
            {
                EmitCode($"%tmp{outputRegisterNumber} = fmul {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
            else
            {
                EmitCode($"%tmp{outputRegisterNumber} = mul {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
        }
        public void EmitDivideCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            string typeString = GetTypeString(type);
            if (type == Types.DoubleType)
            {
                EmitCode($"%tmp{outputRegisterNumber} = fdiv  {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
            else
            {
                EmitCode($"%tmp{outputRegisterNumber} = udiv {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
        }
        public void EmitPlusCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            string typeString = GetTypeString(type);
            if (type == Types.DoubleType)
            {
                EmitCode($"%tmp{outputRegisterNumber} = fadd {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
            else
            {
                EmitCode($"%tmp{outputRegisterNumber} = add {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
        }
        public void EmitMinusCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            string typeString = GetTypeString(type);
            if (type == Types.DoubleType)
            {
                EmitCode($"%tmp{outputRegisterNumber} = fsub {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
            else
            {
                EmitCode($"%tmp{outputRegisterNumber} = sub {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
        }

        #endregion
        #region RELATION
        public void EmitEqualCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            string typeString = GetTypeString(type);
            if (type == Types.DoubleType)
            {
                EmitCode($"%tmp{outputRegisterNumber} = fcmp oeq {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
            else
            {
                EmitCode($"%tmp{outputRegisterNumber} = icmp eq {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
        }
        public void EmitNotEqualCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            string typeString = GetTypeString(type);
            if (type == Types.DoubleType)
            {
                EmitCode($"%tmp{outputRegisterNumber} = fcmp one {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
            else
            {
                EmitCode($"%tmp{outputRegisterNumber} = icmp ne {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
        }
        public void EmitGreaterCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            string typeString = GetTypeString(type);
            if (type == Types.DoubleType)
            {
                EmitCode($"%tmp{outputRegisterNumber} = fcmp ogt {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
            else
            {
                EmitCode($"%tmp{outputRegisterNumber} = icmp ugt {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
        }
        public void EmitGreaterEqualCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            string typeString = GetTypeString(type);
            if (type == Types.DoubleType)
            {
                EmitCode($"%tmp{outputRegisterNumber} = fcmp oge {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
            else
            {
                EmitCode($"%tmp{outputRegisterNumber} = icmp uge {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
        }
        public void EmitLessCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            string typeString = GetTypeString(type);
            if (type == Types.DoubleType)
            {
                EmitCode($"%tmp{outputRegisterNumber} = fcmp olt {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
            else
            {
                EmitCode($"%tmp{outputRegisterNumber} = icmp ult {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
        }
        public void EmitLessEqualCode(Types type, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            string typeString = GetTypeString(type);
            if (type == Types.DoubleType)
            {
                EmitCode($"%tmp{outputRegisterNumber} = fcmp ole {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
            else
            {
                EmitCode($"%tmp{outputRegisterNumber} = icmp ule {typeString} %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            }
        }
        #endregion
        #region LOGICL
        public void EmitRightAndLogicalExpresionCode(int outputRegisterNumber, int rightRegisterNumber, int labelNumber)
        {
            EmitCode($"br i1 %tmp{rightRegisterNumber}, label %AND{labelNumber}, label %END{labelNumber}");
            EmitCode($"AND{labelNumber}:");
            EmitCode($"%tmp{outputRegisterNumber} = add i1 0, 1");
            EmitCode($"br label %END{labelNumber}");
            EmitCode($"END{labelNumber}:");
        }

        public void EmitLeftAndLogicalExpresionCode(int outputRegisterNumber, int leftRegisterNumber, int labelNumber)
        {
            EmitCode($"%tmp{outputRegisterNumber} = add i1 0, 0");
            EmitCode($"br i1 %tmp{leftRegisterNumber}, label %RIGHT{labelNumber}, label %END{labelNumber}");
            EmitCode($"RIGHT{labelNumber}:");
        }

        public void EmitRightOrLogicalExpresionCode(int outputRegisterNumber, int rightRegisterNumber, int labelNumber)
        {
            EmitCode($"br i1 %tmp{rightRegisterNumber}, label %END{labelNumber}, label %OR{labelNumber}");
            EmitCode($"OR{labelNumber}:");
            EmitCode($"%tmp{outputRegisterNumber} = add i1 0, 0");
            EmitCode($"br label %END{labelNumber}");
            EmitCode($"END{labelNumber}:");
        }

        public void EmitLeftOrLogicalExpresionCode(int outputRegisterNumber, int leftRegisterNumber, int labelNumber)
        {
            EmitCode($"%tmp{outputRegisterNumber} = add i1 0, 1");
            EmitCode($"br i1 %tmp{leftRegisterNumber}, label %END{labelNumber}, label %RIGHT{labelNumber}");
            EmitCode($"RIGHT{labelNumber}:");
        }
        #endregion
        public void EmitAssignmentExpresionCode(Pair variable, int outputRegisterNumber, int rvalueRegisterNumber)
        {
            string typeString = GetTypeString(variable.Type);
            EmitCode($"store {typeString} %tmp{rvalueRegisterNumber}, {typeString}* %{variable.Value}");
            EmitCode($"%tmp{outputRegisterNumber} = load {typeString}, {typeString}* %tmp{rvalueRegisterNumber}");
        }
        #endregion
    }

    public interface INode
    {
        void EmitCode(ICodeEmiter codeEmiter);
    }

    public class ProgramNode : INode
    {
        INode body;
        public ProgramNode(INode body)
        {
            this.body = body;
        }
        public void EmitCode(ICodeEmiter codeEmiter)
        {
            codeEmiter.EmitProgramProlog();
            body.EmitCode(codeEmiter);
            codeEmiter.EmitProgramEpilog();
        }
    }

    public class BodyNode : INode
    {
        public List<INode> declarations;
        public List<INode> statements;
        public BodyNode(List<INode> declarations, List<INode> statements)
        {
            this.declarations = declarations;
            this.statements = statements;
        }
        public void EmitCode(ICodeEmiter codeEmiter)
        {
            declarations.ForEach((node) => node.EmitCode(codeEmiter));
            statements.ForEach((node) => node.EmitCode(codeEmiter));
        }
    }

    public class DeclarationNode : INode
    {
        Types declarationType;
        List<string> variableNames;
        public DeclarationNode(Types declarationType, List<string> variableNames)
        {
            this.declarationType = declarationType;
            this.variableNames = variableNames;
        }
        public void EmitCode(ICodeEmiter codeEmiter)
        {
            foreach(var variableName in variableNames)
            {
                codeEmiter.EmitDeclarationCode(declarationType, UniqueVariableName(variableName));
            }
        }
    }

    #region IOSTREAM
    public class ReadNode : INode
    {
        protected Pair variable;
        public ReadNode(string variableName)
        {
            string varName = UniqueVariableName(variableName);
            if (!variables.ContainsKey(varName))
            {
                Console.WriteLine($"line [{lineNumber}] error: such variable not exists");
                errors++;
            }
            else
            {
                if (variables[varName] == Types.BooleanType)
                {
                    Console.WriteLine($"line [{lineNumber}] error: can not read bool type");
                    errors++;
                }
            }
            this.variable = new Pair(variables[varName], varName);
        }
        public virtual void EmitCode(ICodeEmiter codeEmiter)
        {
            codeEmiter.EmitReadCode(variable.Type, variable.Value);
        }
    }

    public class HexReadNode : ReadNode
    {
        public HexReadNode(string variableName) : base(variableName)
        {
            if (variables[UniqueVariableName(variableName)] == Types.DoubleType)
            {
                Console.WriteLine($"line [{lineNumber}] error: can not read hex to double type");
                errors++;
            }
        }
        public override void EmitCode(ICodeEmiter codeEmiter)
        {
            codeEmiter.EmitReadCode(variable.Type, variable.Value);
        }
    }

    public class WriteNode : INode
    {
        protected ExpressionNode expressionNode;
        public WriteNode(ExpressionNode expressionNode)
        {
            this.expressionNode = expressionNode;
        }
        public void EmitCode(ICodeEmiter codeEmiter)
        {
            int outputRegisterNumber = registersCount++;
            expressionNode.EmitExpresionCode(codeEmiter, outputRegisterNumber);
            EmitWriteCode(codeEmiter, outputRegisterNumber);
        }

        public virtual void EmitWriteCode(ICodeEmiter codeEmiter, int registerNumber)
        {
            codeEmiter.EmitWriteCode(expressionNode.Type, registerNumber);
        }
    }

    public class HexWriteNode : WriteNode
    {
        public HexWriteNode(ExpressionNode expressionNode) : base(expressionNode)
        {
            if(expressionNode.Type != Types.IntegerType)
            {
                Console.WriteLine($"line [{lineNumber}] error: can not write {TypeName(expressionNode.Type)} as hex");
                errors++;
            }
        }
        public override void EmitWriteCode(ICodeEmiter codeEmiter, int registerNumber)
        {
            codeEmiter.EmitHexWriteCode(registerNumber);
        }
    }

    public class StringWriteNode : INode
    {
        protected string inscription;
        public StringWriteNode(string inscription)
        {
            this.inscription = inscription;
        }
        public void EmitCode(ICodeEmiter codeEmiter)
        {
            codeEmiter.EmitStringWriteCode(inscription);
        }
    }
    #endregion

    public class ReturnNode : INode
    {
        public void EmitCode(ICodeEmiter codeEmiter)
        {
            codeEmiter.EmitReturnCode();
        }
    }

    #region CONDITIONS
    public class IfElseNode : INode
    {
        protected ExpressionNode expresionNode;
        protected INode trueStatement;
        protected INode falseStatement;
        public IfElseNode(ExpressionNode expresionNode, INode trueStatement, INode falseStatement)
        {
            this.expresionNode = expresionNode;
            this.trueStatement = trueStatement;
            this.falseStatement = falseStatement;
            if(expresionNode.Type != Types.BooleanType)
            {
                Console.WriteLine($"line [{lineNumber}] error: if condition must be bool type");
                errors++;
            }
        }
        public void EmitCode(ICodeEmiter codeEmiter)
        {
            int outputRegisterNumber = registersCount++;
            int labelNumber = labelsCount++;
            expresionNode.EmitExpresionCode(codeEmiter, outputRegisterNumber);
            codeEmiter.EmitIfCode(outputRegisterNumber, labelNumber);
            trueStatement.EmitCode(codeEmiter);
            codeEmiter.EmitElseCode(labelNumber);
            if (falseStatement != null) falseStatement.EmitCode(codeEmiter);
        }
    }

    public class IfNode : IfElseNode
    {
        public IfNode(ExpressionNode expresionNode, INode trueStatement) : 
            base(expresionNode, trueStatement, null) { }
    }

    public class WhileNode : INode
    {
        protected ExpressionNode expresionNode;
        protected INode statement;
        public WhileNode(ExpressionNode expresionNode, INode statement)
        {
            this.expresionNode = expresionNode;
            this.statement = statement;
            if (expresionNode.Type != Types.BooleanType)
            {
                Console.WriteLine($"line [{lineNumber}] error: while condition must be bool type");
                errors++;
            }
        }
        public void EmitCode(ICodeEmiter codeEmiter)
        {
            int outputRegisterNumber = registersCount++;
            int labelNumber = labelsCount++;
            codeEmiter.EmitWhileBeginningCode(labelNumber);
            expresionNode.EmitExpresionCode(codeEmiter, outputRegisterNumber);
            codeEmiter.EmitWhileCode(outputRegisterNumber, labelNumber);
            statement.EmitCode(codeEmiter);
            codeEmiter.EmitWhileEndCode(labelNumber);
        }
    }
    #endregion

    public class BlockNode : BodyNode
    {
        public BlockNode(List<INode> statements) : base(new List<INode>(), statements) { }
    }

    public abstract class ExpressionNode : INode
    {
        public Types Type;
        public ExpressionNode(Types type)
        {
            this.Type = type;
        }
        public virtual void EmitCode(ICodeEmiter codeEmiter)
        {
            int outputRegisterNumber = registersCount++;
            EmitExpresionCode(codeEmiter, outputRegisterNumber);
        }

        public abstract void EmitExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber);
    }

    #region EXPRESSIONS
    public class ConstantExpresionNode : ExpressionNode
    {
        Pair constant;
        public ConstantExpresionNode(Types type, string value) : base(type)
        {
            this.constant = new Pair(type, value);
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber)
        {
            codeEmiter.EmitConstantExprestionCode(constant, outputRegisterNumber);
        }
    }

    public class VariableExpresionNode : ExpressionNode
    {
        Pair variable;
        public VariableExpresionNode(string variableName) : base(variables[UniqueVariableName(variableName)])
        {
            string varName = UniqueVariableName(variableName);
            if (!variables.ContainsKey(varName))
            {
                Console.WriteLine($"line [{lineNumber}]: error: such variable not exists");
                errors++;
            }
            this.variable = new Pair(variables[varName], varName);
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber)
        {
            codeEmiter.EmitVariableCode(variable, outputRegisterNumber);
        }
    }

    public class CastExpresionNode : ExpressionNode
    {
        Types fromType;
        ExpressionNode expresionNode;
        public CastExpresionNode(Types from, Types to, ExpressionNode expresionNode) : base(to)
        {
            this.fromType = from;
            this.expresionNode = expresionNode;
            switch (from)
            {
                case Types.BooleanType: 
                    {
                        if (to == Types.DoubleType)
                        {
                            errors++;
                            Console.WriteLine($"line [{lineNumber}] error: can not conver from bool to double type");
                        }
                    } break;
                case Types.DoubleType:
                    {
                        if (to == Types.BooleanType)
                        {
                            errors++;
                            Console.WriteLine($"line [{lineNumber}] error: can not conver from double to bool type");
                        }
                        if (to == Types.DoubleType)
                        {
                            errors++;
                            Console.WriteLine($"line [{lineNumber}] error: can not conver from double to double type");
                        }
                    } break;
                case Types.IntegerType:
                    {
                        if (to == Types.BooleanType)
                        {
                            errors++;
                            Console.WriteLine($"line [{lineNumber}] error: can not conver from int to bool type");
                        }
                    } break;
            }
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber)
        {
            int registerNumber = registersCount++;
            expresionNode.EmitExpresionCode(codeEmiter, registerNumber);
            switch(fromType)
            {
                case Types.BooleanType: codeEmiter.EmitBoolToIntConversionCode(outputRegisterNumber, registerNumber); break;
                case Types.DoubleType: codeEmiter.EmitDoubleToIntConversionCode(outputRegisterNumber, registerNumber); break;
                case Types.IntegerType: codeEmiter.EmitIntToDoubleConversionCode(outputRegisterNumber, registerNumber); break;
            }
        }
    }

    #region UNARY
    public class UnaryMinusExpresionNode : ExpressionNode
    {
        ExpressionNode expresionNode;
        public UnaryMinusExpresionNode(ExpressionNode expresionNode) : base(expresionNode.Type)
        {
            this.expresionNode = expresionNode;
            if(expresionNode.Type == Types.BooleanType)
            {
                Console.WriteLine($"line [{lineNumber}] error: value can not be bool type");
                errors++;
            }
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber)
        {
            int registerName = registersCount++;
            expresionNode.EmitExpresionCode(codeEmiter, registerName);
            codeEmiter.EmitUnaryMinusCode(Type, outputRegisterNumber, registerName);
        }
    }

    public class UnaryNegationExpresionNode : ExpressionNode
    {
        ExpressionNode expresionNode;
        public UnaryNegationExpresionNode(ExpressionNode expresionNode) : base(Types.IntegerType)
        {
            this.expresionNode = expresionNode;
            if (expresionNode.Type != Types.IntegerType)
            {
                Console.WriteLine($"line [{lineNumber}] error: value can not be {TypeName(expresionNode.Type)} type");
                errors++;
            }
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber)
        {
            int registerName = registersCount++;
            expresionNode.EmitExpresionCode(codeEmiter, registerName);
            codeEmiter.EmitUnaryNegationCode(outputRegisterNumber, registerName);
        }
    }

    public class LogicalNegationExpresionNode : ExpressionNode
    {
        ExpressionNode expresionNode;
        public LogicalNegationExpresionNode(ExpressionNode expresionNode) : base(Types.BooleanType)
        {
            this.expresionNode = expresionNode;
            if (expresionNode.Type != Types.BooleanType)
            {
                Console.WriteLine($"line [{lineNumber}] error: value can not be {TypeName(expresionNode.Type)} type");
                errors++;
            }
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber)
        {
            int registerName = registersCount++;
            expresionNode.EmitExpresionCode(codeEmiter, registerName);
            codeEmiter.EmitLogicalNegationCode(outputRegisterNumber, registerName);
        }
    }

    public class IntConversionExpresionNode : ExpressionNode
    {
        ExpressionNode expresionNode;
        public IntConversionExpresionNode(ExpressionNode expresionNode) : base(Types.IntegerType)
        {
            this.expresionNode = new CastExpresionNode(expresionNode.Type, Types.IntegerType, expresionNode);
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber)
        {
            expresionNode.EmitExpresionCode(codeEmiter, outputRegisterNumber);
        }
    }

    public class DoubleConversionExpresionNode : ExpressionNode
    {
        ExpressionNode expresionNode;
        public DoubleConversionExpresionNode(ExpressionNode expresionNode) : base(Types.DoubleType)
        {
            this.expresionNode = new CastExpresionNode(expresionNode.Type, Types.DoubleType, expresionNode);
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber)
        {
            expresionNode.EmitExpresionCode(codeEmiter, outputRegisterNumber);
        }
    }
    #endregion

    public abstract class BinaryExpresionNode : ExpressionNode
    {
        protected ExpressionNode leftExpresionNode;
        protected ExpressionNode rightExpresionNode;

        public BinaryExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(Types.IntegerType) 
        {
            this.leftExpresionNode = leftExpresionNode;
            this.rightExpresionNode = rightExpresionNode;
            if(leftExpresionNode.Type != Types.IntegerType || rightExpresionNode.Type != Types.IntegerType)
            {
                Console.WriteLine($"line [{lineNumber}] error: operation is not allowed for not int types");
                errors++;
            }
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber)
        {
            int leftRegisterNumber = registersCount++;
            int rightRegisterNumber = registersCount++;
            leftExpresionNode.EmitExpresionCode(codeEmiter, leftRegisterNumber);
            rightExpresionNode.EmitExpresionCode(codeEmiter, rightRegisterNumber);
            EmitBinaryExpresionCode(codeEmiter, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }

        public abstract void EmitBinaryExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber);
    }

    #region BINARY
    public class BinarySumExpresionNode : BinaryExpresionNode
    {

        public BinarySumExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitBinaryExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            codeEmiter.EmitBinarySumCode(outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class BinaryMultiplyExpresionNode : BinaryExpresionNode
    {
        public BinaryMultiplyExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitBinaryExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            codeEmiter.EmitBinaryMultiplyCode(outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }
    #endregion

    public abstract class ReckoningExpresionNode : ExpressionNode
    {
        protected ExpressionNode leftExpresionNode;
        protected ExpressionNode rightExpresionNode;
        protected Types reckoningnType;
        public ReckoningExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(GetMoreOverallType(leftExpresionNode.Type, rightExpresionNode.Type))
        {
            this.leftExpresionNode = leftExpresionNode;
            this.rightExpresionNode = rightExpresionNode;
            reckoningnType = GetMoreOverallType(leftExpresionNode.Type, rightExpresionNode.Type);
            if (leftExpresionNode.Type != Types.BooleanType && rightExpresionNode.Type != Types.BooleanType)
            {
                if (leftExpresionNode.Type != rightExpresionNode.Type)
                {
                    if (leftExpresionNode.Type == Types.IntegerType && rightExpresionNode.Type == Types.DoubleType)
                    {
                        this.leftExpresionNode = new CastExpresionNode(Types.IntegerType, Types.DoubleType, leftExpresionNode);
                    }
                    else 
                    {
                        this.rightExpresionNode = new CastExpresionNode(Types.IntegerType, Types.DoubleType, rightExpresionNode);
                    }
                }
            }
            else
            {
                Console.WriteLine($"line [{lineNumber}] error: operation not allowed for bool types");
                errors++;
            }
        }
        public override void EmitExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber)
        {
            int leftRegisterNumber = registersCount++;
            int rightRegisterNumber = registersCount++;
            leftExpresionNode.EmitExpresionCode(codeEmiter, leftRegisterNumber);
            rightExpresionNode.EmitExpresionCode(codeEmiter, rightRegisterNumber);
            EmitReckoningExpresionCode(codeEmiter, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }

        public abstract void EmitReckoningExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber);
    }

    #region RECKONING
    public class DivideExpresionNode : ReckoningExpresionNode
    {  
        public DivideExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitReckoningExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            codeEmiter.EmitDivideCode(reckoningnType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class MultiplyExpresionNode : ReckoningExpresionNode
    {
        public MultiplyExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitReckoningExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            codeEmiter.EmitMultiplyCode(reckoningnType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class PlusExpresionNode : ReckoningExpresionNode
    {
        public PlusExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitReckoningExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            codeEmiter.EmitPlusCode(reckoningnType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class MinusExpresionNode : ReckoningExpresionNode
    {
        public MinusExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitReckoningExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            codeEmiter.EmitMinusCode(reckoningnType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }
    #endregion

    public abstract class RelationExpresionNode : ExpressionNode
    {
        protected ExpressionNode leftExpresionNode;
        protected ExpressionNode rightExpresionNode;
        protected Types relationType;
        public RelationExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(Types.BooleanType)
        {
            this.leftExpresionNode = leftExpresionNode;
            this.rightExpresionNode = rightExpresionNode;
            this.relationType = leftExpresionNode.Type;
            if (leftExpresionNode.Type != rightExpresionNode.Type)
            {
                if (leftExpresionNode.Type == Types.IntegerType && rightExpresionNode.Type == Types.DoubleType)
                {
                    this.leftExpresionNode = new CastExpresionNode(Types.IntegerType, Types.DoubleType, leftExpresionNode);
                }
                else if (leftExpresionNode.Type == Types.DoubleType && rightExpresionNode.Type == Types.IntegerType)
                {
                    this.rightExpresionNode = new CastExpresionNode(Types.IntegerType, Types.DoubleType, rightExpresionNode);
                }
                else
                {
                    Console.WriteLine($"line [{lineNumber}]: error: can not compare {TypeName(leftExpresionNode.Type)} type with {TypeName(rightExpresionNode.Type)} type");
                    errors++;
                }
            }
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber)
        {

            int leftRegisterNumber = registersCount++;
            int rightRegisterNumber = registersCount++;
            leftExpresionNode.EmitExpresionCode(codeEmiter, leftRegisterNumber);
            rightExpresionNode.EmitExpresionCode(codeEmiter, rightRegisterNumber);
            EmitRelationExpresionCode(codeEmiter, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }

        public abstract void EmitRelationExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber);
    }

    #region RELATION
    public class EqualExpresionNode : RelationExpresionNode
    {
        public EqualExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitRelationExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            codeEmiter.EmitEqualCode(relationType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class NotEqualExpresionNode : RelationExpresionNode
    {
        public NotEqualExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitRelationExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            codeEmiter.EmitNotEqualCode(relationType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class GreaterExpresionNode : RelationExpresionNode
    {
        public GreaterExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode)
        {
            if (leftExpresionNode.Type == Types.BooleanType || rightExpresionNode.Type == Types.BooleanType)
            {
                Console.WriteLine($"line [{lineNumber}] error: at least one of type is bool type");
                errors++;
            }
        }

        public override void EmitRelationExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            codeEmiter.EmitGreaterCode(relationType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class GreaterEqualExpresionNode : RelationExpresionNode
    {
        public GreaterEqualExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode)
        {
            if (leftExpresionNode.Type == Types.BooleanType || rightExpresionNode.Type == Types.BooleanType)
            {
                Console.WriteLine($"line [{lineNumber}] error: at least one of type is bool type");
                errors++;
            }
        }

        public override void EmitRelationExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            codeEmiter.EmitGreaterEqualCode(relationType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class LessExpresionNode : RelationExpresionNode
    {
        public LessExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode)
        {
            if (leftExpresionNode.Type == Types.BooleanType || rightExpresionNode.Type == Types.BooleanType)
            {
                Console.WriteLine($"line [{lineNumber}] error: at least one of type is bool type");
                errors++;
            }
        }

        public override void EmitRelationExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            codeEmiter.EmitEqualCode(relationType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class LessEqualExpresionNode : RelationExpresionNode
    {
        public LessEqualExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode)
        {
            if (leftExpresionNode.Type == Types.BooleanType || rightExpresionNode.Type == Types.BooleanType)
            {
                Console.WriteLine($"line [{lineNumber}] error: at least one of type is bool type");
                errors++;
            }
        }

        public override void EmitRelationExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber, int leftRegisterNumber, int rightRegisterNumber)
        {
            codeEmiter.EmitLessEqualCode(relationType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }
    #endregion

    public abstract class LogicalExpresionNode : ExpressionNode
    {
        protected ExpressionNode leftExpresionNode;
        protected ExpressionNode rightExpresionNode;
        public LogicalExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(Types.BooleanType)
        {
            this.leftExpresionNode = leftExpresionNode;
            this.rightExpresionNode = rightExpresionNode;
            if (leftExpresionNode.Type != Types.BooleanType || rightExpresionNode.Type != Types.BooleanType)
            {
                Console.WriteLine($"line [{lineNumber}] error: types are not bool type");
                errors++;
            }
        }
    }

    #region LOGICAL
    public class AndLogicalExpresionNode : LogicalExpresionNode
    {
        public AndLogicalExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber)
        {
            int leftRegisterNumber = registersCount++;
            int rightRegisterNumber = registersCount++;
            int labelNumber = labelsCount++;
            leftExpresionNode.EmitExpresionCode(codeEmiter, leftRegisterNumber);
            codeEmiter.EmitRightAndLogicalExpresionCode(outputRegisterNumber, leftRegisterNumber, labelNumber);
            rightExpresionNode.EmitExpresionCode(codeEmiter, rightRegisterNumber);
            codeEmiter.EmitRightAndLogicalExpresionCode(outputRegisterNumber, rightRegisterNumber, labelNumber);
        }
    }

    public class OrLogicalExpresionNode : LogicalExpresionNode
    {
        public OrLogicalExpresionNode(ExpressionNode leftExpresionNode, ExpressionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber)
        {
            int leftRegisterNumber = registersCount++;
            int rightRegisterNumber = registersCount++;
            int labelNumber = labelsCount++;
            leftExpresionNode.EmitExpresionCode(codeEmiter, leftRegisterNumber);
            codeEmiter.EmitLeftOrLogicalExpresionCode(outputRegisterNumber, leftRegisterNumber, labelNumber);
            rightExpresionNode.EmitExpresionCode(codeEmiter, rightRegisterNumber);
            codeEmiter.EmitRightOrLogicalExpresionCode(outputRegisterNumber, rightRegisterNumber, labelNumber); ;
        }
    }
    #endregion

    public class AssignmentExpresionNode : ExpressionNode
    {
        ExpressionNode expresionNode;
        Pair variable;
        public AssignmentExpresionNode(string variableName, ExpressionNode expresionNode) : base(variables[UniqueVariableName(variableName)])
        {
            string varName = UniqueVariableName(variableName);
            if (!variables.ContainsKey(varName))
            {
                Console.WriteLine($"line [{lineNumber}] error: such variable not exists");
                errors++;
            }
            this.variable = new Pair(variables[varName], varName);
            this.expresionNode = expresionNode;
            if (variable.Type != expresionNode.Type)
            {
                if(variable.Type == Types.DoubleType && expresionNode.Type == Types.IntegerType)
                {
                    this.expresionNode = new CastExpresionNode(Types.IntegerType, Types.DoubleType, expresionNode);
                }
                else
                {
                    Console.WriteLine($"line [{lineNumber}] error: can not assign {TypeName(expresionNode.Type)} to {TypeName(variable.Type)} type");
                    errors++;
                }
            }     
        }

        public override void EmitCode(ICodeEmiter codeEmiter)
        {
            int registerNumber = registersCount++;
            EmitExpresionCode(codeEmiter, registerNumber);
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, int outputRegisterNumber)
        {
            int rvalueRegisterNumber = registersCount++;
            expresionNode.EmitExpresionCode(codeEmiter, rvalueRegisterNumber);
            codeEmiter.EmitAssignmentExpresionCode(variable, outputRegisterNumber, rvalueRegisterNumber);
        }
    }
    #endregion

    public static bool IsIdentyficatorOccupied(Types type, string identyficator)
    {
        string ident = UniqueVariableName(identyficator);
        if (variables.ContainsKey(ident))
        {
            Console.WriteLine($"line [{lineNumber}]: error: such variable name already exists");
            errors++;
            return true;
        }
        variables.Add(ident, type);
        return false;
    }

    public static int Main(string[] args)
    {
        string file;
        FileStream source;
        Console.WriteLine("\nSingle-Pass LLVM Code Generator for Multiline Calculator - Gardens Point");
        if (args.Length >= 1)
            file = args[0];
        else
        {
            Console.Write("\nsource file:  ");
            file = Console.ReadLine();
        }
        try
        {
            var sr = new StreamReader(file);
            string str = sr.ReadToEnd();
            sr.Close();
            Compiler.source = new System.Collections.Generic.List<string>(str.Split(new string[] { "\r\n" }, System.StringSplitOptions.None));
            source = new FileStream(file, FileMode.Open);
        }
        catch (Exception e)
        {
            Console.WriteLine("\n" + e.Message);
            return 1;
        }
        Scanner scanner = new Scanner(source);
        Parser parser = new Parser(scanner);
        Console.WriteLine();
        sw = new StreamWriter(file + ".il");
        GenProlog();
        parser.Parse();
        GenEpilog();
        sw.Close();
        source.Close();
        if (errors == 0)
            Console.WriteLine("  compilation successful\n");
        else
        {
            Console.WriteLine($"\n  {errors} errors detected\n");
            File.Delete(file + ".il");
        }
        return errors == 0 ? 0 : 2;
    }

    public static void EmitCode(string instr = null)
    {
        sw.WriteLine(instr);
    }

    public static void EmitCode(string instr, params object[] args)
    {
        sw.WriteLine(instr, args);
    }

    private static StreamWriter sw;

    private static void GenProlog() { }

    public static void GenProgram(INode program)
    {
        LLVMCodeEmiter lLVMCodeEmiter = new LLVMCodeEmiter();
        program.EmitCode(lLVMCodeEmiter);
    }

    private static void GenEpilog() { }

}

