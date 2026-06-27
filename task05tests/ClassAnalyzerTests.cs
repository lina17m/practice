using Xunit;
using System;
using task05;

namespace task05tests
{
    public class TestClass
    {
        public int PublicField;
        private readonly string _privateField;
        public int Property { get; set; }
        public void Method() { }

        public TestClass()
        {
            _privateField = string.Empty;
        }
        public string PrivateFieldValue => _privateField;
    }

    [Serializable]
    public class AttributedClass { }

    public class ClassAnalyzerTests
    {
        [Fact]
        public void GetPublicMethods_ReturnsCorrectMethods()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var methods = analyzer.GetPublicMethods();
            Assert.Contains("Method", methods);
        }

        [Fact]
        public void GetMethodParams_ReturnsParametersAndReturnType()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var result = analyzer.GetMethodParams("Method").ToList();
            Assert.Contains("ReturnType: Void", result);
        }   

        [Fact]
        public void GetAllFields_IncludesPrivateFields()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var fields = analyzer.GetAllFields();
            Assert.Contains("_privateField", fields);
        }

        [Fact]
        public void GetProperties_ReturnsCorrectProperties()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var properties = analyzer.GetProperties();
            Assert.Contains("Property", properties);
        }

        [Fact]
        public void HasAttribute_ReturnsTrue_WhenAttributeExists()
        {
            var analyzer = new ClassAnalyzer(typeof(AttributedClass));
            var hasAttr = analyzer.HasAttribute<SerializableAttribute>();
            Assert.True(hasAttr);
        }

        [Fact]
        public void HasAttribute_ReturnsFalse_WhenAttributeMissing()
        {
            var analyzer = new ClassAnalyzer(typeof(TestClass));
            var hasAttr = analyzer.HasAttribute<SerializableAttribute>();
            Assert.False(hasAttr);
        }
    }
}
