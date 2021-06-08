
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
        void EmitDeclarationCode(Types type, string variableName);
        void EmitAndLogicalExpresionCode();
        void EmitOrLogicalExpresionCode();
        void EmitConstantExprestionCode(Pair constant, string resultRegisterName);
        void EmitAssignmentExpresionCode(Pair variable, string registerName, string resultRegisterName);
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
        public void EmitAndLogicalExpresionCode()
        {
            EmitCode("TODO: emit AND code");
        }

        public void EmitAssignmentExpresionCode(Pair variable, string registerNumber, string resultRegisterNumber)
        {
            string typeString = GetTypeString(variable.Type);
            EmitCode($"store {typeString} %tmp{registerNumber}, {typeString}* %{variable.Value}");
            EmitCode($"%tmp{registerNumber} = load {typeString}* %tmp{resultRegisterNumber}");
        }

        public void EmitBodyCode()
        {
            
        }

        public void EmitConstantExprestionCode(Pair constant, string resultRegisterNumber)
        {
            string typeString = GetTypeString(constant.Type);
            EmitCode($"%tmp{resultRegisterNumber} = add {typeString} 0, {constant.Value}");
        }

        public void EmitDeclarationCode(Types type, string variableName)
        {
            string typeString = GetTypeString(type);
            EmitCode($"%{variableName} = alloca {typeString}");
        }

        public void EmitOrLogicalExpresionCode()
        {
            EmitCode("TODO: emit OR code");
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
        public ConstantExpresionNode(Pair constant) : base(constant.Type)
        {
            this.constant = constant;
        }
        public override void EmitCode(ICodeEmiter codeEmiter)
        {
            string registerNumber = registersCount++.ToString();
            codeEmiter.EmitConstantExprestionCode(constant, registerNumber);
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string registerNumber)
        {
            codeEmiter.EmitConstantExprestionCode(constant, registerNumber);
        }
    }

    public abstract class LogicalExpresionNode : ExpresionNode
    {
        protected ExpresionNode leftExpresionNode;
        protected ExpresionNode rightExpresionNode;
        public LogicalExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode) : base(Types.BooleanType)
        {
            this.leftExpresionNode = leftExpresionNode;
            this.rightExpresionNode = rightExpresionNode;
        }

        public override void EmitCode(ICodeEmiter codeEmiter)
        {
            string registerNumber = registersCount++.ToString();
            EmitExpresionCode(codeEmiter, registerNumber);
        }
    }

    public class AndLogicalExpresionNode : LogicalExpresionNode
    {
        public AndLogicalExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string registerNumber)
        {
            codeEmiter.EmitAndLogicalExpresionCode();
            rightExpresionNode.EmitExpresionCode(codeEmiter, registerNumber);
            leftExpresionNode.EmitExpresionCode(codeEmiter, registerNumber);
        }
    }

    public class OrLogicalExpresionNode : LogicalExpresionNode
    {
        public OrLogicalExpresionNode(ExpresionNode leftExpresionNode, ExpresionNode rightExpresionNode)
            : base(leftExpresionNode, rightExpresionNode) { }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string registerNumber)
        {
            codeEmiter.EmitOrLogicalExpresionCode();
            rightExpresionNode.EmitExpresionCode(codeEmiter, registerNumber);
            leftExpresionNode.EmitExpresionCode(codeEmiter, registerNumber);
        }
    }

    public class AssignmentExpresionNode : ExpresionNode
    {
        ExpresionNode expresionNode;
        Pair variable;
        public AssignmentExpresionNode(Pair variable, ExpresionNode expresionNode) : base(variable.Type)
        {
            this.expresionNode = expresionNode;
            this.variable = variable;
        }

        public override void EmitCode(ICodeEmiter codeEmiter)
        {
            string registerNumber = registersCount++.ToString();
            EmitExpresionCode(codeEmiter, registerNumber);
        }

        public override void EmitExpresionCode(ICodeEmiter codeEmiter, string registerNumber)
        {
            string newRegisterNumber = registersCount++.ToString();
            expresionNode.EmitExpresionCode(codeEmiter, newRegisterNumber);
            codeEmiter.EmitAssignmentExpresionCode(variable, registerNumber, newRegisterNumber);
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

