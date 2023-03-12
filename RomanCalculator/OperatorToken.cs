using RomanCalculator.Contracts;
using RomanCalculator.Enums;

namespace RomanCalculator;

public class OperatorToken : IToken
{
    public OperatorToken(OperatorType operatorType)
    {
        OperatorType = operatorType;
    }

    public OperatorType OperatorType { get; }
}