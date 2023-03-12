using YahyaTj.RomanCalculator.Contracts;
using YahyaTj.RomanCalculator.Enums;

namespace YahyaTj.RomanCalculator;

public class OperatorToken : IToken
{
    public OperatorToken(OperatorType operatorType)
    {
        OperatorType = operatorType;
    }

    public OperatorType OperatorType { get; }
}