using Banks.Exceptions;

namespace Banks.Entities;

public class Percent
{
    public Percent(double value)
    {
        if (value < 0)
            throw PercentException.ValueException();
        Value = value;
    }

    public double Value { get; set; }

    public static double OfNumber(double number, int percentValue)
    {
        if (percentValue < 0 || percentValue > 100)
            throw PercentException.ValueException();
        return number / 100 * percentValue;
    }

    public static double ToNumber(double number, int percentValue)
    {
        return number + Percent.OfNumber(number, percentValue);
    }

    public double PercentageOfNumber(double number)
    {
        return number / 100 * Value;
    }

    public double PercentageToNumber(double number)
    {
        return number + PercentageOfNumber(number);
    }
}