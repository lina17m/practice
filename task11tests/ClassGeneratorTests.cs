using task11;
using Xunit;

namespace task11tests;

public class ClassGeneratorTests
{
    [Fact]
    public void TestCalculation()
    {
        ICalculator calc = ClassGenerator.CreateCalculator();

        Assert.Equal(8, calc.Add(5, 3));
        Assert.Equal(2, calc.Minus(5, 3));
        Assert.Equal(15, calc.Mul(5, 3));
        Assert.Equal(2, calc.Div(6, 3));
    }
}
