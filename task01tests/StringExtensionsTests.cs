using Xunit;
using task01;

namespace task01tests;

public class StringExtensionsTests
{
    [Fact]
    public void IsPalindrome_ValidPalindrome_ReturnsTrue()
    {
        string input = "А роза упала на лапу Азора";
        bool result = input.IsPalindrome();
        Console.WriteLine($"\"{input}\" -> {result}");
        Assert.True(result);
    }

    [Fact]
    public void IsPalindrome_NotPalindrome_ReturnsFalse()
    {
        string input = "Hello, world!";
        bool result = input.IsPalindrome();
        Console.WriteLine($"\"{input}\" -> {result}");
        Assert.False(result);
    }

    [Fact]
    public void IsPalindrome_EmptyString_ReturnsFalse()
    {
        string input = "";
        bool result = input.IsPalindrome();
        Console.WriteLine($"\"{input}\" -> {result}");
        Assert.False(result);
    }

    [Fact]
    public void IsPalindrome_WithPunctuation_IgnoresPunctuation()
    {
        string input = "Was it a car or a cat I saw?";
        bool result = input.IsPalindrome();
        Console.WriteLine($"\"{input}\" -> {result}");
        Assert.True(result);
    }
}
