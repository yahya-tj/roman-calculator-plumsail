namespace YahyaTj.RomanCalculator;

public class RomanCalculator
{
    private readonly Tokenizer _tokenizer;
    private readonly ShuntingYardAlgorithm _algorithm;
    private readonly PostfixNotationCalculator _calculator;

    public RomanCalculator()
    {
        _tokenizer = new Tokenizer();
        _algorithm = new ShuntingYardAlgorithm();
        _calculator = new PostfixNotationCalculator();
    }

    public string Evaluate(string input)
    {
        var infixNotationTokens = _tokenizer.Parse(input);
        var postfixNotationTokens = _algorithm.Apply(infixNotationTokens);

        return _calculator.Calculate(postfixNotationTokens).Value;
    }
}