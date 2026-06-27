using System.Reflection;
using CommandLib;
using PluginSystem;

string pluginsPath;
if (args.Length > 0)
{
    pluginsPath = Path.GetFullPath(args[0]);
}
else if (Directory.Exists("plugins"))
{
    pluginsPath = Path.GetFullPath("plugins");
}
else 
{
    pluginsPath = Path.GetFullPath(@"./FileSystemCommands/bin/Debug/net10.0/");
}

if (!Directory.Exists(pluginsPath))
{
    Console.WriteLine($"Ошибка: Папка {pluginsPath} не найдена!");
    return;
}

try
{
    var loader = new PluginLoader();
    loader.LoadAndExecute(pluginsPath);
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка при работе загрузчика: {ex.Message}");
}
