using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace task13
{
    public static class JsonManager
    {
        private static readonly JsonSerializerOptions Options = new()
        {
            WriteIndented = true, 
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            PropertyNameCaseInsensitive = true 
        };

        public static string ToJson(Student student)
        {
            return JsonSerializer.Serialize(student, Options);
        }

        public static Student FromJson(string json)
        {
            if (string.IsNullOrWhiteSpace(json)) 
                throw new ArgumentException("JSON не может быть пустым.");

            var student = JsonSerializer.Deserialize<Student>(json, Options);

            if (student == null || string.IsNullOrEmpty(student.FirstName))
                throw new InvalidOperationException("Ошибка! Имя студента отсутствует.");

            return student;
        }

        public static void SaveToFile(string fileName, Student student)
        {
            string jsonString = ToJson(student);
            File.WriteAllText(fileName, jsonString);
        }

        public static Student ReadFromFile(string fileName)
        {
            if (!File.Exists(fileName)) 
                throw new FileNotFoundException($"Файл {fileName} не найден.");

            string jsonString = File.ReadAllText(fileName);
            return FromJson(jsonString);
        }
    }
}
