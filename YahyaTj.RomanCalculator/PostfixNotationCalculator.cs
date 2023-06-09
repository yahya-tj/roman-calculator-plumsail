﻿using YahyaTj.RomanCalculator.Contracts;
using YahyaTj.RomanCalculator.Enums;
using YahyaTj.RomanCalculator.Exceptions;

namespace YahyaTj.RomanCalculator;

public class PostfixNotationCalculator
{
    private readonly Stack<OperandToken> _operandTokensStack;
    private readonly RomanConverter _romanConverter;

    public PostfixNotationCalculator()
    {
        _operandTokensStack = new Stack<OperandToken>();
        _romanConverter = new RomanConverter();
    }

    public OperandToken Calculate(IEnumerable<IToken> tokens)
    {
        Reset();
        foreach (var token in tokens)
        {
            ProcessToken(token);
        }

        return GetResult();
    }

    private void Reset()
    {
        _operandTokensStack.Clear();
    }

    private void ProcessToken(IToken token)
    {
        switch (token)
        {
            case OperandToken operandToken:
                StoreOperand(operandToken);
                break;
            case OperatorToken operatorToken:
                ApplyOperator(operatorToken);
                break;
            default:
                throw new SyntaxException($"An unknown token type: {token.GetType()}.");
        }
    }

    private void StoreOperand(OperandToken operandToken)
    {
        _operandTokensStack.Push(operandToken);
    }

    private void ApplyOperator(OperatorToken operatorToken)
    {
        switch (operatorToken.OperatorType)
        {
            case OperatorType.Addition:
                ApplyAdditionOperator();
                break;
            case OperatorType.Subtraction:
                ApplySubtractionOperator();
                break;
            case OperatorType.Multiplication:
                ApplyMultiplicationOperator();
                break;
            case OperatorType.Division:
                ApplyDivisionOperator();
                break;
            default:
                throw new SyntaxException("An unknown operator type: {operatorToken.OperatorType}.");
        }
    }

    private void ApplyAdditionOperator()
    {
        var operands = GetBinaryOperatorArguments();
        var leftNumber = _romanConverter.RomanToInt(operands.Item1.Value);
        var rightNumber = _romanConverter.RomanToInt(operands.Item2.Value);
        var operandTokenValue = _romanConverter.IntToRoman(leftNumber + rightNumber);
        _operandTokensStack.Push( new OperandToken(operandTokenValue));
    }

    private void ApplySubtractionOperator()
    {
        var operands = GetBinaryOperatorArguments();
        var leftNumber = _romanConverter.RomanToInt(operands.Item1.Value);
        var rightNumber = _romanConverter.RomanToInt(operands.Item2.Value);
        var operandTokenValue = _romanConverter.IntToRoman(leftNumber - rightNumber);
        _operandTokensStack.Push( new OperandToken(operandTokenValue));
    }

    private void ApplyMultiplicationOperator()
    {
        var operands = GetBinaryOperatorArguments();
        var leftNumber = _romanConverter.RomanToInt(operands.Item1.Value);
        var rightNumber = _romanConverter.RomanToInt(operands.Item2.Value);
        var operandTokenValue = _romanConverter.IntToRoman(leftNumber * rightNumber);
        _operandTokensStack.Push( new OperandToken(operandTokenValue));
    }

    private void ApplyDivisionOperator()
    {
        var operands = GetBinaryOperatorArguments();
        var leftNumber = _romanConverter.RomanToInt(operands.Item1.Value);
        var rightNumber = _romanConverter.RomanToInt(operands.Item2.Value);
        var operandTokenValue = _romanConverter.IntToRoman(leftNumber / rightNumber);
        _operandTokensStack.Push( new OperandToken(operandTokenValue));
    }

    private Tuple<OperandToken, OperandToken> GetBinaryOperatorArguments()
    {
        if (_operandTokensStack.Count < 2)
            throw new SyntaxException("Not enough arguments for applying a binary operator.");

        var right = _operandTokensStack.Pop();
        var left = _operandTokensStack.Pop();

        return Tuple.Create(left, right);
    }

    private OperandToken GetResult()
    {
        if (_operandTokensStack.Count == 0)
            throw new SyntaxException("The expression is invalid. " +
                                      "Check, please, that the expression is not empty.");

        if (_operandTokensStack.Count != 1)
            throw new SyntaxException("The expression is invalid. " +
                                      "Check, please, that you're providing " +
                                      "the full expression and the tokens have a correct order.");

        return _operandTokensStack.Pop();
    }
}