using System.Text;

namespace task01;

public static class StringExtensions
{
    public static bool IsPalindrome(this string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return false;
        }

        var builder = new StringBuilder();

        foreach (char c in input)
        {
            if (!char.IsWhiteSpace(c) && !char.IsPunctuation(c))
            {
                builder.Append(char.ToLower(c));
            }
        }

        string builderst = builder.ToString();

        char[] array = builderst.ToCharArray();
        Array.Reverse(array);
        string reverse = new string(array);

        return builderst == reverse;
    }
}