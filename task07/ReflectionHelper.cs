using System;
using System.Reflection;

namespace task07
{
    public static class ReflectionHelper
    {
        public static void PrintTypeInfo(Type type)
        {
            var nameAtr = type.GetCustomAttribute<DisplayNameAttribute>();
            if (nameAtr != null) Console.WriteLine($"Класс: {nameAtr.DisplayName}");

            var verAtr = type.GetCustomAttribute<VersionAttribute>();
            if (verAtr != null) Console.WriteLine($"Версия: {verAtr.Major}.{verAtr.Minor}");

            Console.WriteLine("Методы:");
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                var atr = method.GetCustomAttribute<DisplayNameAttribute>();
                if (atr != null) Console.WriteLine($"- {method.Name}: {atr.DisplayName}");
            }

            Console.WriteLine("Свойства:");
            foreach (var prop in type.GetProperties())
            {
                var atr = prop.GetCustomAttribute<DisplayNameAttribute>();
                if (atr != null) Console.WriteLine($"- {prop.Name}: {atr.DisplayName}");
            }
        }
    }
}
