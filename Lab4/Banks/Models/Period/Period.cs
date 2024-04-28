using Banks.Exceptions;

namespace Banks.Models;

public class Period
{
    public static int ByDateDifferenceMonth(DateOnly newDate, DateOnly oldDate)
    {
        if (newDate < oldDate)
            throw PeriodException.DateDiffereceException();
        return ((newDate.Year - oldDate.Year) * 12) + newDate.Month - oldDate.Month;
    }
}