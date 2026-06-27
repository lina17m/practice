using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp; 
using System.Reflection;

namespace task11;

public static class ClassGenerator
{
    public static ICalculator CreateCalculator()
    {
        string code = @"
            using task11;
            public class Calculator : ICalculator
            {
                public int Add(int a, int b) => a + b;
                public int Minus(int a, int b) => a - b;
                public int Mul(int a, int b) => a * b;
                public int Div(int a, int b) => a / b;
            }";

        var syntaxTree = CSharpSyntaxTree.ParseText(code);

        var refs = new[] {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(ICalculator).Assembly.Location),
            MetadataReference.CreateFromFile(Assembly.Load("System.Runtime").Location)
        };
        var compilation = CSharpCompilation.Create("MyCalc")
            .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
            .AddReferences(refs)
            .AddSyntaxTrees(syntaxTree);

        using var stream = new MemoryStream();
        var result = compilation.Emit(stream);
        if (!result.Success) throw new Exception("Ошибка в коде!");
        var assembly = Assembly.Load(stream.ToArray());
        var type = assembly.GetType("Calculator")!;
        
        return (ICalculator)Activator.CreateInstance(type)!;
    }
}
