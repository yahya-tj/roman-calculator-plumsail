namespace RomanCalculator;

public class RomanConverter
{
    private readonly Dictionary<char, int> _romanToIntegerDictionary = new()
    {
        { 'I', 1 },
        { 'V', 5 },
        { 'X', 10 },
        { 'L', 50 },
        { 'C', 100 },
        { 'D', 500 },
        { 'M', 1000 }
    };

    private readonly Dictionary<int, string> _romanNumeralsDictionary = new()
    {
        { 1000, "M" },
        { 900, "CM" },
        { 500, "D" },
        { 400, "CD" },
        { 100, "C" },
        { 90, "XC" },
        { 50, "L" },
        { 40, "XL" },
        { 10, "X" },
        { 9, "IX" },
        { 5, "V" },
        { 4, "IV" },
        { 1, "I" }
    };

    public int RomanToInt(string romanNumeral)
    {
        var result = 0;
        for (var i = 0; i < romanNumeral.Length; i++)
        {
            var value = _romanToIntegerDictionary[romanNumeral[i]];
            if (i < romanNumeral.Length - 1 && _romanToIntegerDictionary[romanNumeral[i + 1]] > value)
            {
                result -= value;
            }
            else
            {
                result += value;
            }
        }

        return result;
    }

    public string IntToRoman(int number)
    {
        var result = string.Empty;
        foreach (var pair in _romanNumeralsDictionary)
        {
            while (number >= pair.Key)
            {
                result += pair.Value;
                number -= pair.Key;
            }
        }

        return result;
    }
}