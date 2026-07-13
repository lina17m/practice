using Xunit;
using task13;
using System;
using System.IO;

namespace task13tests
{
    public class StudentTests
    {
        [Fact]
        public void Serialization_ShouldIgnoreNullGrades()
        {
            var student = new Student { FirstName = "Oleg", LastName = "Ivanov", Grades = null };
            
            string json = JsonManager.ToJson(student);

            Assert.DoesNotContain("Grades", json);
        }

        [Fact]
        public void Deserialization_ShouldValidateData()
        {
            string invalidJson = "{\"LastName\":\"Ivanov\"}";

            Assert.Throws<InvalidOperationException>(() => JsonManager.FromJson(invalidJson));
        }

        [Fact]
        public void FileIO_ShouldSaveAndLoadCorrectly()
        {
            string path = "test_student.json";
            var student = new Student { FirstName = "Polina", LastName = "Mosina", BirthDate = new DateTime(2007, 9, 17) };

            try {
                JsonManager.SaveToFile(path, student);
                var loaded = JsonManager.ReadFromFile(path);

                Assert.Equal(student.FirstName, loaded.FirstName);
                Assert.Equal(student.LastName, loaded.LastName);
            }
            finally {
                if (File.Exists(path)) File.Delete(path);
            }
        }
    }
}
