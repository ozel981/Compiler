
using System;
using System.IO;
using System.Collections.Generic;
using GardensPoint;

public class Compiler
{
    public enum Types
    {
        NoneType = -1,
        IntegerType = 0,
        DoubleType = 1,
        BooleanType = 2,
        StringType = 3
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
            case Types.StringType: return "string";
            default: return "";
        }
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
        void EmitBodyCode();
        void EmitUnaryMinusCode(Types type, string outputRegisterNumber, string registerNumber);
        void EmitUnaryNegationCode(Types type, string outputRegisterNumber, string registerNumber);
        void EmitLogicalNegationCode(Types type, string outputRegisterNumber, string registerNumber);
        void EmitBinaryMultiplyCode(Types type, string outputRegisterNumber, string registerNumber);
        void EmitBoolToIntConversionCode(string outputRegisterNumber, string registerNumber);
        void EmitIntToDoubleConversionCode(string outputRegisterNumber, string registerNumber);
        void EmitDoubleToIntConversionCode(string outputRegisterNumber, string registerNumber);
        void EmitBinarySumCode(string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        void EmitMultiplyCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        void EmitDivideCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        void EmitPlusCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        void EmitMinusCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
         void EmitVariableCode(Pair variable, string outputRegisterNumber);
        void EmitDeclarationCode(Types type, string variableName);
        void EmitConstantExprestionCode(Pair constant, string outputRegisterNumber);
        #region RELATION
        void EmitEqualCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        void EmitNotEqualCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        void EmitGreaterCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        void EmitGreaterEqualCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        void EmitLessCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        void EmitLessEqualCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        #endregion
        #region LOGICAL
        void EmitRightAndLogicalExpresionCode(string outputRegisterNumber, string rightRegisterNumber, string endLabel);
        void EmitLeftAndLogicalExpresionCode(string outputRegisterNumber, string leftRegisterNumber, string endLabel);
        void EmitRightOrLogicalExpresionCode(string outputRegisterNumber, string rightRegisterNumber, string endLabel);
        void EmitLeftOrLogicalExpresionCode(string outputRegisterNumber, string leftRegisterNumber, string endLabel);
        #endregion
        void EmitAssignmentExpresionCode(Pair variable, string outputRegisterNumber, string rvalueRegisterNumber);
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

    public class LLVMCodeEmiter : ICodeEmiter
    {
        public void EmitBodyCode() { }
        public void EmitConstantExprestionCode(Pair constant, string outputRegisterNumber)
        {
            string typeString = GetTypeString(constant.Type);
            EmitCode($"%tmp{outputRegisterNumber} = add {typeString} 0, {constant.Value}");
        }
        public void EmitDeclarationCode(Types type, string variableName)
        {
            string typeString = GetTypeString(type);
            EmitCode($"%{variableName} = alloca {typeString}");
        }
        public void EmitVariableCode(Pair variable, string outputRegisterNumber)
        {
            string typeString = GetTypeString(variable.Type);
            EmitCode($"%tmp{outputRegisterNumber} = load {typeString}* %{variable.Value}");
        }
        public void EmitPlusCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            EmitCode("Plus");
        }
        public void EmitMinusCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            EmitCode("Minus");
        }
        public void EmitMultiplyCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            EmitCode("Multiply");
        }
        public void EmitDivideCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            EmitCode("Divide");
        }
        public void EmitBinaryMultiplyCode(string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            EmitCode("BinaryMultiply");
        }
        public void EmitBinarySumCode(string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            EmitCode("BinarySum");
        }
        public void EmitUnaryMinusCode(Types type, string outputRegisterNumber, string registerNumber)
        {
            EmitCode("UnaryMinus");
        }
        public void EmitUnaryNegationCode(Types type, string outputRegisterNumber, string registerNumber)
        {
            EmitCode("UnaryNegation");
        }
        public void EmitLogicalNegationCode(Types type, string outputRegisterNumber, string registerNumber)
        {
            EmitCode("LogicalNegation");
        }
        public void EmitBinaryMultiplyCode(Types type, string outputRegisterNumber, string registerNumber)
        {
            EmitCode("BinaryMultiply");
        }
        public void EmitBoolToIntConversionCode(string outputRegisterNumber, string registerNumber)
        {
            EmitCode("BoolToInt");
        }
        public void EmitIntToDoubleConversionCode(string outputRegisterNumber, string registerNumber)
        {
            EmitCode("IntToDouble");
        }
        public void EmitDoubleToIntConversionCode(string outputRegisterNumber, string registerNumber)
        {
            EmitCode("DoubleToInt");
        }
        #region RELATION
        public void EmitEqualCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
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
        public void EmitNotEqualCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
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
        public void EmitGreaterCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
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
        public void EmitGreaterEqualCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
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
        public void EmitLessCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
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
        public void EmitLessEqualCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
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
        public void EmitRightAndLogicalExpresionCode(string outputRegisterNumber, string rightRegisterNumber, string endLabel)
        {
            string rightLabel = $"Right{labelsCount++}";
            EmitCode($"br i1 %tmp{rightRegisterNumber}, label %{rightLabel}, label %{endLabel}");
            EmitCode($"{rightLabel}:");
            EmitCode($"%tmp{outputRegisterNumber} = add i1 0, 1");
            EmitCode($"br label %{endLabel}:");
            EmitCode($"{endLabel}:");
        }

        public void EmitLeftAndLogicalExpresionCode(string outputRegisterNumber, string leftRegisterNumber, string endLabel)
        {
            string leftLabel = $"Left{labelsCount++}";
            EmitCode($"%tmp{outputRegisterNumber} = add i1 0, 0");
            EmitCode($"br i1 %tmp{leftRegisterNumber}, label %{leftLabel}, label %{endLabel}");
            EmitCode($"{leftLabel}:");
        }

        public void EmitRightOrLogicalExpresionCode(string outputRegisterNumber, string rightRegisterNumber, string endLabel)
        {
            string rightLabel = $"Right{labelsCount++}";
            EmitCode($"br i1 %tmp{rightRegisterNumber}, label %{endLabel}, label %{rightLabel}");
            EmitCode($"{rightLabel}:");
            EmitCode($"%tmp{outputRegisterNumber} = add i1 0, 0");
            EmitCode($"br label %{endLabel}:");
            EmitCode($"{endLabel}:");
        }

        public void EmitLeftOrLogicalExpresionCode(string outputRegisterNumber, string leftRegisterNumber, string endLabel)
        {
            string leftLabel = $"Left{labelsCount++}";
            EmitCode($"%tmp{outputRegisterNumber} = add i1 0, 1");
            EmitCode($"br i1 %tmp{leftRegisterNumber}, label %{endLabel}, label %{leftLabel}");
            EmitCode($"{endLabel}:");
        }
        #endregion
        public void EmitAssignmentExpresionCode(Pair variable, string outputRegisterNumber, string rvalueRegisterNumber)
        {
            string typeString = GetTypeString(variable.Type);
            EmitCode($"store {typeString} %tmp{rvalueRegisterNumber}, {typeString}* %{variable.Value}");
            EmitCode($"%tmp{outputRegisterNumber} = load {typeString}, {typeString}* %tmp{rvalueRegisterNumber}");
        }
    }

    public interface INode
    {
        void EmitCode(ICodeEmiter codeEmiter);
    }

    public class BodyNode : INode
    {
        public List<INode> declarationsNodes;
        public List<INode> statementsNodes;
        public BodyNode(List<INode> declarationsNodes, List<INode> statementsNodes)
        {
            this.declarationsNodes = declarationsNodes;
            this.statementsNodes = statementsNodes;
        }
        public void EmitCode(ICodeEmiter codeEmiter)
        {
            codeEmiter.EmitBodyCode();
            declarationsNodes.ForEach((node) => node.EmitCode(codeEmiter));
            statementsNodes.ForEach((node) => node.EmitCode(codeEmiter));
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

    public class StatementNode : INode
    {
        INode singleOperation;
        public StatementNode(INode singleOperation)
        {
            this.singleOperation = singleOperation;
        }
        public void EmitCode(ICodeEmiter codeEmiter)
        {
            singleOperation.EmitCode(codeEmiter);
        }
    }

    public class SingleOperationNode : INode
    {
        INode operation;
        public SingleOperationNode(INode operation)
        {
            this.operation = operation;
        }
        public void EmitCode(ICodeEmiter codeEmiter)
        {
            operation.EmitCode(codeEmiter);
        }
    }

    public abstract class ExpresionNode : INode
    {
        public Types Type;
        public ExpresionNode(Types type)
        {
            this.Type = type;
        }
        public virtual void EmitCode(ICodeEmiter codeEmiter)
        {
            string registerNumber = registersCount++.ToString();
            EmitExpresionCode(codeEmiter, registerNumber);
        }

        public abstract void EmitExpresionCode(ICodeEmiter codeEmiter, string registerName);
    }

    public class ConstantExpresionNode : ExpresionNode
    {
        Pair constant;
        public ConstantExpresionNode(Types type, string value) : base(type)
        {
            this.constant = new Pair(type, value);
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber)
        {
            codeEmiter.EmitConstantExprestionCode(constant, outputRegisterNumber);
        }
    }

    public class VariableExpresionNode : ExpresionNode
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

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber)
        {
            codeEmiter.EmitVariableCode(variable, outputRegisterNumber);
        }
    }

    public class CastExpresionNode : ExpresionNode
    {
        Types fromType;
        ExpresionNode expresionNode;
        public CastExpresionNode(Types from, Types to, ExpresionNode expresionNode) : base(to)
        {
            this.fromType = from;
            this.expresionNode = expresionNode;
            switch (from)
            {
                case Types.BooleanType: 
                    {
                        if (to == Types.IntegerType)
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
                        if (to != Types.DoubleType)
                        {
                            errors++;
                            Console.WriteLine($"line [{lineNumber}] error: can not conver from int to bool type");
                        }
                    } break;
            }
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string registerName)
        {
            string outputRegisterNumber = registersCount++.ToString();
            expresionNode.EmitExpresionCode(codeEmiter, outputRegisterNumber);
            switch(fromType)
            {
                case Types.BooleanType: codeEmiter.EmitBoolToIntConversionCode(registerName, outputRegisterNumber); break;
                case Types.DoubleType: codeEmiter.EmitDoubleToIntConversionCode(registerName, outputRegisterNumber); break;
                case Types.IntegerType: codeEmiter.EmitIntToDoubleConversionCode(registerName, outputRegisterNumber); break;
            }
        }
    }

    #region UNARY
    public class UnaryMinusExpresionNode : ExpresionNode
    {
        ExpresionNode expresionNode;
        public UnaryMinusExpresionNode(ExpresionNode expresionNode) : base(expresionNode.Type)
        {
            this.expresionNode = expresionNode;
            if(expresionNode.Type == Types.BooleanType)
            {
                Console.WriteLine($"line [{lineNumber}] error: value can not be bool type");
                errors++;
            }
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string registerName)
        {
            string outputRegisterNumber = registersCount++.ToString();
            expresionNode.EmitExpresionCode(codeEmiter, outputRegisterNumber);
            codeEmiter.EmitUnaryMinusCode(Type, registerName, outputRegisterNumber);
        }
    }

    public class UnaryNegationExpresionNode : ExpresionNode
    {
        ExpresionNode expresionNode;
        public UnaryNegationExpresionNode(ExpresionNode expresionNode) : base(Types.IntegerType)
        {
            this.expresionNode = expresionNode;
            if (expresionNode.Type != Types.IntegerType)
            {
                Console.WriteLine($"line [{lineNumber}] error: value can not be {TypeName(expresionNode.Type)} type");
                errors++;
            }
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string registerName)
        {
            string outputRegisterNumber = registersCount++.ToString();
            expresionNode.EmitExpresionCode(codeEmiter, outputRegisterNumber);
            codeEmiter.EmitUnaryNegationCode(Type, registerName, outputRegisterNumber);
        }
    }

    public class LogicalNegationExpresionNode : ExpresionNode
    {
        ExpresionNode expresionNode;
        public LogicalNegationExpresionNode(ExpresionNode expresionNode) : base(Types.BooleanType)
        {
            this.expresionNode = expresionNode;
            if (expresionNode.Type != Types.BooleanType)
            {
                Console.WriteLine($"line [{lineNumber}] error: value can not be {TypeName(expresionNode.Type)} type");
                errors++;
            }
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string registerName)
        {
            string outputRegisterNumber = registersCount++.ToString();
            expresionNode.EmitExpresionCode(codeEmiter, outputRegisterNumber);
            codeEmiter.EmitLogicalNegationCode(Type, registerName, outputRegisterNumber);
        }
    }

    public class IntConversionExpresionNode : ExpresionNode
    {
        ExpresionNode expresionNode;
        public IntConversionExpresionNode(ExpresionNode expresionNode) : base(Types.IntegerType)
        {
            this.expresionNode = new CastExpresionNode(expresionNode.Type, Types.IntegerType, expresionNode);
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string registerName)
        {
            expresionNode.EmitExpresionCode(codeEmiter, registerName);
        }
    }

    public class DoubleConversionExpresionNode : ExpresionNode
    {
        ExpresionNode expresionNode;
        public DoubleConversionExpresionNode(ExpresionNode expresionNode) : base(Types.DoubleType)
        {
            this.expresionNode = new CastExpresionNode(expresionNode.Type, Types.DoubleType, expresionNode);
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string registerName)
        {
            expresionNode.EmitExpresionCode(codeEmiter, registerName);
        }
    }
    #endregion

    public abstract class BinaryExpresionNode : ExpresionNode
    {
        protected ExpresionNode leftExpresionNode;
        protected ExpresionNode rightExpresionNode;

        public BinaryExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
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

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber)
        {
            string leftRegisterNumber = registersCount++.ToString();
            string rightRegisterNumber = registersCount++.ToString();
            leftExpresionNode.EmitExpresionCode(codeEmiter, leftRegisterNumber);
            rightExpresionNode.EmitExpresionCode(codeEmiter, rightRegisterNumber);
            EmitBinaryExpresionCode(codeEmiter, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }

        public abstract void EmitBinaryExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
    }

    #region BINARY
    public class BinarySumExpresionNode : BinaryExpresionNode
    {

        public BinarySumExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitBinaryExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            codeEmiter.EmitBinarySumCode(outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class BinaryMultiplyExpresionNode : BinaryExpresionNode
    {
        public BinaryMultiplyExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitBinaryExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            codeEmiter.EmitBinarySumCode(outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }
    #endregion

    public abstract class ReckoningExpresionNode : ExpresionNode
    {
        protected ExpresionNode leftExpresionNode;
        protected ExpresionNode rightExpresionNode;
        protected Types reckoningnType;
        public ReckoningExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
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
        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber)
        {
            string leftRegisterNumber = registersCount++.ToString();
            string rightRegisterNumber = registersCount++.ToString();
            leftExpresionNode.EmitExpresionCode(codeEmiter, leftRegisterNumber);
            rightExpresionNode.EmitExpresionCode(codeEmiter, rightRegisterNumber);
            EmitReckoningExpresionCode(codeEmiter, leftRegisterNumber, rightRegisterNumber, outputRegisterNumber);
        }

        public abstract void EmitReckoningExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
    }

    #region RECKONING
    public class DivideExpresionNode : ReckoningExpresionNode
    {  
        public DivideExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitReckoningExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            codeEmiter.EmitDivideCode(reckoningnType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class MultiplyExpresionNode : ReckoningExpresionNode
    {
        public MultiplyExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitReckoningExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            codeEmiter.EmitMultiplyCode(reckoningnType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class PlusExpresionNode : ReckoningExpresionNode
    {
        public PlusExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitReckoningExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            codeEmiter.EmitPlusCode(reckoningnType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class MinusExpresionNode : ReckoningExpresionNode
    {
        public MinusExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitReckoningExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            codeEmiter.EmitMinusCode(reckoningnType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }
    #endregion

    public abstract class RelationExpresionNode : ExpresionNode
    {
        protected ExpresionNode leftExpresionNode;
        protected ExpresionNode rightExpresionNode;
        protected Types relationType;
        public RelationExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
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

        public override void EmitCode(ICodeEmiter codeEmiter)
        {
            string outputRegisterNumber = registersCount++.ToString();
            EmitExpresionCode(codeEmiter, outputRegisterNumber);
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber)
        {

            string leftRegisterNumber = registersCount++.ToString();
            string rightRegisterNumber = registersCount++.ToString();
            leftExpresionNode.EmitExpresionCode(codeEmiter, leftRegisterNumber);
            rightExpresionNode.EmitExpresionCode(codeEmiter, rightRegisterNumber);
            EmitRelationExpresionCode(codeEmiter, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }

        public abstract void EmitRelationExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
    }

    #region RELATION
    public class EqualExpresionNode : RelationExpresionNode
    {
        public EqualExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitRelationExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            codeEmiter.EmitEqualCode(relationType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class NotEqualExpresionNode : RelationExpresionNode
    {
        public NotEqualExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitRelationExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            codeEmiter.EmitNotEqualCode(relationType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class GreaterExpresionNode : RelationExpresionNode
    {
        public GreaterExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode)
        {
            if (leftExpresionNode.Type == Types.BooleanType || rightExpresionNode.Type == Types.BooleanType)
            {
                Console.WriteLine($"line [{lineNumber}] error: at least one of type is bool type");
                errors++;
            }
        }

        public override void EmitRelationExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            codeEmiter.EmitGreaterCode(relationType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class GreaterEqualExpresionNode : RelationExpresionNode
    {
        public GreaterEqualExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode)
        {
            if (leftExpresionNode.Type == Types.BooleanType || rightExpresionNode.Type == Types.BooleanType)
            {
                Console.WriteLine($"line [{lineNumber}] error: at least one of type is bool type");
                errors++;
            }
        }

        public override void EmitRelationExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            codeEmiter.EmitGreaterEqualCode(relationType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class LessExpresionNode : RelationExpresionNode
    {
        public LessExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode)
        {
            if (leftExpresionNode.Type == Types.BooleanType || rightExpresionNode.Type == Types.BooleanType)
            {
                Console.WriteLine($"line [{lineNumber}] error: at least one of type is bool type");
                errors++;
            }
        }

        public override void EmitRelationExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            codeEmiter.EmitEqualCode(relationType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class LessEqualExpresionNode : RelationExpresionNode
    {
        public LessEqualExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode)
        {
            if (leftExpresionNode.Type == Types.BooleanType || rightExpresionNode.Type == Types.BooleanType)
            {
                Console.WriteLine($"line [{lineNumber}] error: at least one of type is bool type");
                errors++;
            }
        }

        public override void EmitRelationExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            codeEmiter.EmitLessEqualCode(relationType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }
    #endregion

    public abstract class LogicalExpresionNode : ExpresionNode
    {
        protected ExpresionNode leftExpresionNode;
        protected ExpresionNode rightExpresionNode;
        public LogicalExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
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
        public AndLogicalExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber)
        {
            string leftRegisterNumber = registersCount++.ToString();
            string rightRegisterNumber = registersCount++.ToString();
            string endLable = $"End{labelsCount++}";
            leftExpresionNode.EmitExpresionCode(codeEmiter, leftRegisterNumber);
            codeEmiter.EmitRightAndLogicalExpresionCode(outputRegisterNumber, leftRegisterNumber, endLable);
            rightExpresionNode.EmitExpresionCode(codeEmiter, rightRegisterNumber);
            codeEmiter.EmitRightAndLogicalExpresionCode(outputRegisterNumber, rightRegisterNumber, endLable);
        }
    }

    public class OrLogicalExpresionNode : LogicalExpresionNode
    {
        public OrLogicalExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber)
        {
            string leftRegisterNumber = registersCount++.ToString();
            string rightRegisterNumber = registersCount++.ToString();
            string endLable = $"End{labelsCount++}";
            leftExpresionNode.EmitExpresionCode(codeEmiter, leftRegisterNumber);
            codeEmiter.EmitLeftOrLogicalExpresionCode(outputRegisterNumber, leftRegisterNumber, endLable);
            rightExpresionNode.EmitExpresionCode(codeEmiter, rightRegisterNumber);
            codeEmiter.EmitRightOrLogicalExpresionCode(outputRegisterNumber, rightRegisterNumber, endLable); ;
        }
    }
    #endregion

    public class AssignmentExpresionNode : ExpresionNode
    {
        ExpresionNode expresionNode;
        Pair variable;
        public AssignmentExpresionNode(string variableName, ExpresionNode expresionNode) : base(variables[UniqueVariableName(variableName)])
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
            string registerNumber = registersCount++.ToString();
            EmitExpresionCode(codeEmiter, registerNumber);
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber)
        {
            string rvalueRegisterNumber = registersCount++.ToString();
            expresionNode.EmitExpresionCode(codeEmiter, rvalueRegisterNumber);
            codeEmiter.EmitAssignmentExpresionCode(variable, outputRegisterNumber, rvalueRegisterNumber);
        }
    }

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

    private static void GenProlog()
    {
        EmitCode("define i32 @main()\n{");     
    }

    public static void GenBody(INode node)
    {
        LLVMCodeEmiter lLVMCodeEmiter = new LLVMCodeEmiter();
        node.EmitCode(lLVMCodeEmiter);
    }

    private static void GenEpilog()
    {
        EmitCode("ret i32 0\n}");
    }

}

