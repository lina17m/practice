using System.Reflection;
using CommandLib;

if (args.Length == 0) return;

try
{
    var assembly = Assembly.LoadFrom(args[0]);
    Console.WriteLine($"\nСБОРКА: {assembly.GetName().Name}");
    foreach (var type in assembly.GetTypes().Where(t => t.IsClass))
    {
        Console.WriteLine($"\n[Класс] {type.FullName}");
        var classAtr = type.GetCustomAttribute<DisplayNameAttribute>();
        if (classAtr != null) 
            Console.WriteLine($"Описание: {classAtr.DisplayName}");
        var verAtr = type.GetCustomAttribute<VersionAttribute>();
        if (verAtr != null) 
            Console.WriteLine($"Версия: {verAtr.Major}.{verAtr.Minor}");

        Console.WriteLine("Конструкторы:");
        foreach (var contor in type.GetConstructors())
        {
            var param = contor.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}");
            Console.WriteLine($"  - ({string.Join(", ", param)})");
        }

        Console.WriteLine("Методы:");
        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        foreach (var m in methods)
        {
            var param = m.GetParameters().Select(p => $"{p.ParameterType.Name} {p.Name}");
            var methodLine = $"  - {m.Name}({string.Join(", ", param)})";
            var methodAtr = m.GetCustomAttribute<DisplayNameAttribute>();
            if (methodAtr != null)
            {
                methodLine += $" {methodAtr.DisplayName}";
            }
            Console.WriteLine(methodLine);
        }
    }
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}