using YahyaTj.RomanCalculator.Contracts;

namespace YahyaTj.RomanCalculator;

public class OperandToken : IToken
{
    public OperandToken(string value)
    {
        Value = value;
    }

    public string Value { get; }
}