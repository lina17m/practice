using System.Reflection;
using CommandLib;
try
{
    string dllPath = Path.GetFullPath(@"../FileSystemCommands/bin/Debug/net10.0/FileSystemCommands.dll");
    
    if (!File.Exists(dllPath))
    {
        Console.WriteLine($"Ошибка: Файл {dllPath} не найден.");
        return;
    }
    Assembly assembly = Assembly.LoadFrom(dllPath);
    Type sizeType = assembly.GetType("FileSystemCommands.DirectorySizeCommand")!;
    object sizeInstance = Activator.CreateInstance(sizeType, Directory.GetCurrentDirectory())!;
    ((ICommand)sizeInstance).Execute();
    var sizeResult = sizeType.GetProperty("TotalSize")?.GetValue(sizeInstance);
    Console.WriteLine($"Размер каталога: {sizeResult} байт\n");
    Type findType = assembly.GetType("FileSystemCommands.FindFilesCommand")!;
    object findInstance = Activator.CreateInstance(findType, Directory.GetCurrentDirectory(), "*.cs")!;
    ((ICommand)findInstance).Execute();
    var findResult = (List<string>)findType.GetProperty("FoundFiles")?.GetValue(findInstance)!;
    
    Console.WriteLine($"Найдено файлов: {findResult.Count}");
    foreach (var file in findResult.Take(5))
        Console.WriteLine($"  - {Path.GetFileName(file)}");
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}
