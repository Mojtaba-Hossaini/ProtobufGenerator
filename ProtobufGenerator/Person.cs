namespace ProtobufGenerator;

public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public Address Address { get; set; }
    public List<string> StringTest { get; set; }
    public ICollection<Address> Addresses { get; set; }
    public DateTime From { get; set; }
}
