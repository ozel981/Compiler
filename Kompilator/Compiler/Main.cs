
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

    public static bool CanConvertTypes(Types from, Types to)
    {
        if (from == Types.IntegerType && to == Types.DoubleType) return true;
        return from == to;
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
        void EmitEqualCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        void EmitNotEqualCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        void EmitGreaterCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        void EmitGreaterEqualCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        void EmitLessCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        void EmitLessEqualCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        void EmitBodyCode();
        void EmitVariableCode(Pair variable, string outputRegisterNumber);
        void EmitDeclarationCode(Types type, string variableName);
        void EmitAndLogicalExpresionCode(string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        void EmitOrLogicalExpresionCode(string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber);
        void EmitConstantExprestionCode(Pair constant, string outputRegisterNumber);
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
        public void EmitAssignmentExpresionCode(Pair variable, string outputRegisterNumber, string rvalueRegisterNumber)
        {
            string typeString = GetTypeString(variable.Type);
            EmitCode($"store {typeString} %tmp{rvalueRegisterNumber}, {typeString}* %{variable.Value}");
            EmitCode($"%tmp{outputRegisterNumber} = load {typeString}* %tmp{rvalueRegisterNumber}");           
        }

        public void EmitBodyCode()
        {
            
        }

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
        public void EmitAndLogicalExpresionCode(string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            string endLabelName = $"End{labelsCount++}";
            string leftLabelName = $"Left{labelsCount++}";
            string rightLabelName = $"Right{labelsCount++}";
            EmitCode($"%tmp{outputRegisterNumber} = add i1 0, 0");
            EmitCode($"br i1 %tmp{leftRegisterNumber}, label %{leftLabelName}, label %{endLabelName}");
            EmitCode($"{leftLabelName}:");
            EmitCode($"br i1 %tmp{rightRegisterNumber}, label %{rightLabelName}, label %{endLabelName}");
            EmitCode($"{rightLabelName}:");
            EmitCode($"%tmp{outputRegisterNumber} = and i1 %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            EmitCode($"br label %{endLabelName}:");
            EmitCode($"{endLabelName}:");
        }

        public void EmitOrLogicalExpresionCode(string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            string endLabelName = $"End{labelsCount++}";
            string leftLabelName = $"Left{labelsCount++}";
            string rightLabelName = $"Right{labelsCount++}";
            EmitCode($"%tmp{outputRegisterNumber} = add i1 0, 1");
            EmitCode($"br i1 %tmp{leftRegisterNumber}, label %{endLabelName}, label %{leftLabelName}");
            EmitCode($"{leftLabelName}:");
            EmitCode($"br i1 %tmp{rightRegisterNumber}, label %{endLabelName}, label %{rightLabelName}");
            EmitCode($"{rightLabelName}:");
            EmitCode($"%tmp{outputRegisterNumber} = or i1 %tmp{leftRegisterNumber}, %tmp{rightRegisterNumber}");
            EmitCode($"br label %{endLabelName}:");
            EmitCode($"{endLabelName}:");
        }

        public void EmitVariableCode(Pair variable, string outputRegisterNumber)
        {
            string typeString = GetTypeString(variable.Type);
            EmitCode($"%tmp{outputRegisterNumber} = load {typeString}* %{variable.Value}");
        }

        public void EmitEqualCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            EmitCode("Equal");
        }

        public void EmitNotEqualCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            EmitCode("NotEqual");
        }

        public void EmitGreaterCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            EmitCode("Greater");
        }

        public void EmitGreaterEqualCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            EmitCode("GreaterEqual");
        }

        public void EmitLessCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            EmitCode("Less");
        }

        public void EmitLessEqualCode(Types type, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            EmitCode("LessEqual");
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
                codeEmiter.EmitDeclarationCode(declarationType, variableName);
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
        public abstract void EmitCode(ICodeEmiter codeEmiter);

        public abstract void EmitExpresionCode(ICodeEmiter codeEmiter, string registerName);
    }

    public class ConstantExpresionNode : ExpresionNode
    {
        Pair constant;
        public ConstantExpresionNode(Types type, string value) : base(type)
        {
            this.constant = new Pair(type, value);
        }
        public override void EmitCode(ICodeEmiter codeEmiter)
        {
            string registerNumber = registersCount++.ToString();
            codeEmiter.EmitConstantExprestionCode(constant, registerNumber);
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber)
        {
            codeEmiter.EmitConstantExprestionCode(constant, outputRegisterNumber);
        }
    }

    public class VariableExpresionNode : ExpresionNode
    {
        Pair variable;
        public VariableExpresionNode(string variableName) : base(variables[variableName])
        {
            if (!variables.ContainsKey(variableName))
            {
                Console.WriteLine($"line [{lineNumber}]: error: such variable not exists");
                errors++;
            }
            this.variable = new Pair(variables[variableName], variableName);
        }
        public override void EmitCode(ICodeEmiter codeEmiter)
        {
            string registerNumber = registersCount++.ToString();
            EmitExpresionCode(codeEmiter, registerNumber);
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber)
        {
            codeEmiter.EmitVariableCode(variable, outputRegisterNumber);
        }
    }

    public abstract class LogicalExpresionNode : ExpresionNode
    {
        protected ExpresionNode leftExpresionNode;
        protected ExpresionNode rightExpresionNode;
        public LogicalExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode) 
            : base(Types.BooleanType)
        {
            this.leftExpresionNode = leftExpresionNode;
            this.rightExpresionNode = rightExpresionNode;
            if (leftExpresionNode.Type == Types.BooleanType && rightExpresionNode.Type == Types.BooleanType)
            {
                Console.WriteLine($"line [{lineNumber}]: error: types are not bool type");
                errors++;
            }
        }

        public override void EmitCode(ICodeEmiter codeEmiter)
        {
            string registerNumber = registersCount++.ToString();
            EmitExpresionCode(codeEmiter, registerNumber);
        }
    }

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
                Console.WriteLine($"line [{lineNumber}]: error: types are not same type");
                errors++;
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
                Console.WriteLine($"line [{lineNumber}]: error: one of type is bool type");
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
                Console.WriteLine($"line [{lineNumber}]: error: one of type is bool type");
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
                Console.WriteLine($"line [{lineNumber}]: error: one of type is bool type");
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
                Console.WriteLine($"line [{lineNumber}]: error: one of type is bool type");
                errors++;
            }
        }

        public override void EmitRelationExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber, string leftRegisterNumber, string rightRegisterNumber)
        {
            codeEmiter.EmitLessEqualCode(relationType, outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
        }
    }

    public class AndLogicalExpresionNode : LogicalExpresionNode
    {
        public AndLogicalExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string outputRegisterNumber)
        {

            string leftRegisterNumber = registersCount++.ToString();
            string rightRegisterNumber = registersCount++.ToString();
            leftExpresionNode.EmitExpresionCode(codeEmiter, leftRegisterNumber);
            rightExpresionNode.EmitExpresionCode(codeEmiter, rightRegisterNumber);
            codeEmiter.EmitAndLogicalExpresionCode(outputRegisterNumber, leftRegisterNumber, rightRegisterNumber);
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
            leftExpresionNode.EmitExpresionCode(codeEmiter, leftRegisterNumber);
            rightExpresionNode.EmitExpresionCode(codeEmiter, rightRegisterNumber);
            codeEmiter.EmitOrLogicalExpresionCode(outputRegisterNumber, leftRegisterNumber, rightRegisterNumber); ;
        }
    }

    public class AssignmentExpresionNode : ExpresionNode
    {
        ExpresionNode expresionNode;
        Pair variable;
        public AssignmentExpresionNode(string variableName, ExpresionNode expresionNode) : base(variables[variableName])
        {
            this.expresionNode = expresionNode;
            if(!variables.ContainsKey(variableName))
            {
                Console.WriteLine($"line [{lineNumber}]: error: such variable not exists");
                errors++;
            }
            this.variable = new Pair(variables[variableName], variableName);
            if (!CanConvertTypes(expresionNode.Type, variable.Type))
            {
                Console.WriteLine($"line [{lineNumber}]: error: types not match");
                errors++;
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
        if(variables.ContainsKey(identyficator))
        {
            return true;
        }
        variables.Add(identyficator, type);
        return false;
    }

    // arg[0] określa plik źródłowy
    // pozostałe argumenty są ignorowane
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

