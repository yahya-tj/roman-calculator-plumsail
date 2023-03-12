namespace YahyaTj.RomanCalculator.Exceptions;

public class SyntaxException : MathExpressionException
{
    public SyntaxException(string message) : base(message)
    {
    }
}