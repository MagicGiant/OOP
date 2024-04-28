namespace BusinessLayer.Exceptions;

public class ReportBuilderException : Exception
{
    public ReportBuilderException(string message)
        : base(message)
    { }

    public static ReportBuilderException PeriodException()
    {
        return new ReportBuilderException("The first date should be less than the second");
    }
}