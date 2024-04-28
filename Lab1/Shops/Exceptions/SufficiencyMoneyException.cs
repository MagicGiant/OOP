namespace Shops.Exceptions;

public class SufficiencyMoneyException : Exception
{
    public SufficiencyMoneyException()
        : base("A person does not have enough money to buy a product")
    { }
}