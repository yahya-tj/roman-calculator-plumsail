using System.Text;
using YahyaTj.RomanCalculator.Contracts;
using YahyaTj.RomanCalculator.Enums;
using YahyaTj.RomanCalculator.Exceptions;

namespace YahyaTj.RomanCalculator;

public class Tokenizer
{
    private readonly List<IToken> _infixNotationTokens;
    private readonly StringBuilder _valueTokenBuilder;

    public Tokenizer()
    {
        _valueTokenBuilder = new StringBuilder();
        _infixNotationTokens = new List<IToken>();
    }

    public IEnumerable<IToken> Parse(string expression)
    {
        Reset();
        foreach (var next in expression) FeedCharacter(next);
        return GetResult();
    }

    private void Reset()
    {
        _valueTokenBuilder.Clear();
        _infixNotationTokens.Clear();
    }

    private void FeedCharacter(char next)
    {
        if (IsSpacingCharacter(next))
        {
            if (_valueTokenBuilder.Length <= 0) return;
            var token = new OperandToken(_valueTokenBuilder.ToString());
            _valueTokenBuilder.Clear();
            _infixNotationTokens.Add(token);
        }
        else if (IsOperatorCharacter(next))
        {
            if (_valueTokenBuilder.Length > 0)
            {
                var token = new OperandToken(_valueTokenBuilder.ToString());
                _valueTokenBuilder.Clear();
                _infixNotationTokens.Add(token);
            }

            var operatorToken = CreateOperatorToken(next);
            _infixNotationTokens.Add(operatorToken);
        }
        else
        {
            _valueTokenBuilder.Append(next);
        }
    }

    private static bool IsOperatorCharacter(char c)
    {
        return c switch
        {
            _ when new[] { '(', ')', '+', '-', '*', '/' }.Contains(c) => true,
            _ => false
        };
    }

    private static bool IsSpacingCharacter(char c)
    {
        return c switch
        {
            ' ' => true,
            _ => false
        };
    }


    private static OperatorToken CreateOperatorToken(char c)
    {
        return c switch
        {
            '(' => new OperatorToken(OperatorType.OpeningBracket),
            ')' => new OperatorToken(OperatorType.ClosingBracket),
            '+' => new OperatorToken(OperatorType.Addition),
            '-' => new OperatorToken(OperatorType.Subtraction),
            '*' => new OperatorToken(OperatorType.Multiplication),
            '/' => new OperatorToken(OperatorType.Division),
            _ => throw new SyntaxException($"There's no a suitable operator for the char {c}")
        };
    }

    private IEnumerable<IToken> GetResult()
    {
        if (_valueTokenBuilder.Length <= 0) return _infixNotationTokens.ToList();
        var token = new OperandToken(_valueTokenBuilder.ToString());
        _valueTokenBuilder.Clear();
        _infixNotationTokens.Add(token);

        return _infixNotationTokens.ToList();
    }
}