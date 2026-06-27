using System.Reflection;
using CommandLib;

namespace PluginSystem;

public class PluginLoader
{
    public void LoadAndExecute(string folderPath)
    {
        var plugins = new Dictionary<string, Type>();
        
        foreach (var dll in Directory.GetFiles(folderPath, "*.dll"))
        {
            try
            {
                var types = Assembly.LoadFrom(dll).GetTypes()
                    .Where(t => t.GetCustomAttribute<PluginLoadAttribute>() != null);
                
                foreach (var t in types) plugins[t.FullName!] = t;
            }
            catch {}
        }

        var sorted = new List<Type>();
        var visited = new HashSet<string>();

        void Visit(Type type)
        {
            if (visited.Contains(type.FullName!)) return;
            visited.Add(type.FullName!);

            var attr = type.GetCustomAttribute<PluginLoadAttribute>();
            if (attr?.Dependencies != null)
            {
                foreach (var depName in attr.Dependencies)
                {
                    if (plugins.TryGetValue(depName, out var depType))
                        Visit(depType);
                }
            }
            sorted.Add(type); 
        }

        foreach (var type in plugins.Values) Visit(type);
        foreach (var type in sorted)
        {
            Console.WriteLine($"[PluginSystem] Запуск: {type.Name}");
            var cmd = Activator.CreateInstance(type) as ICommand;
            cmd?.Execute();
        }
    }
}
