namespace Banks.Models;

public class PassportData
{
    public PassportData(Address address, PassportID id, ClientName name)
    {
        ArgumentNullException.ThrowIfNull(address);
        ArgumentNullException.ThrowIfNull(id);
        ArgumentNullException.ThrowIfNull(name);
        Address = address;
        Id = id;
        Name = name;
    }

    public Address Address { get; }

    public PassportID Id { get; }

    public ClientName Name { get; }

    public override string ToString()
    {
        return Id.ToString();
    }
}