using System.Runtime.Serialization;

namespace RomanCalculator.Exceptions;

[Serializable]
public class MathExpressionException : Exception
{
    public MathExpressionException()
    {
    }

    public MathExpressionException(string message) : base(message)
    {
    }

    public MathExpressionException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    protected MathExpressionException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}