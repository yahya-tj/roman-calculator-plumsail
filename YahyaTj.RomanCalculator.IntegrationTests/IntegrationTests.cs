using Xunit;

namespace YahyaTj.RomanCalculator.IntegrationTests;

public class IntegrationTests
{
    private readonly RomanCalculator _romanCalculator;

    public IntegrationTests()
    {
        _romanCalculator = new RomanCalculator();
    }

    [Fact]
    public void ExpressionSimpleTest()
    {
        var expression = "X + V - V * II";
        var actual = _romanCalculator.Evaluate(expression);
        var expected = "V";
        Assert.Contains(expected, actual);
    } 
    
    [Fact]
    public void ExpressionWithBracketsTest()
    {
        var expression = "(MMMDCCXXIV - MMCCXXIX) * II";
        var actual = _romanCalculator.Evaluate(expression);
        var expected = "MMCMXC";
        Assert.Contains(expected, actual);
    }
}