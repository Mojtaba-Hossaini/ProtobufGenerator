using ProtobufFileGenerator;

namespace ProtobufGenerator;

public class Person
{
    public int? NullId { get; set; }
    public short ShortProp { get; set; }
    public decimal MyProperty { get; set; }
    public byte fffff { get; set; }
    public int Id { get; set; }
    public int[] Ids { get; set; }
    public bool[] boooooolssss { get; set; }

    [ProtofileGenerationPropertyType("sint32")]
    public int[] IdsArr { get; set; }
    public string[] someee { get; set; }
    public Address[] adddddss { get; set; }

    [ProtofileGenerationPropertyType("sint32")]
    public int sint32Propery { get; set; }

    [ProtofileGenerationPropertyType("sfixed32")]
    public int sfixed32Propery { get; set; }
    public string FirstName { get; set; }
    public Address Address { get; set; }
    public List<string> StringTest { get; set; }
    public ICollection<Address> Addresses { get; set; }
    public DateTime From { get; set; }
    public List<SomeEnume> SomeEnumes { get; set; }
    public double DoubleProperty { get; set; }
    public float floatProperty { get; set; }
    public long longProperty { get; set; }

    [ProtofileGenerationPropertyType("sfixed32")]
    public long sint64Property { get; set; }

    [ProtofileGenerationPropertyType("sfixed64")]
    public long sfixed64Property { get; set; }
    public uint uintProperty { get; set; }

    [ProtofileGenerationPropertyType("fixed32")]
    public uint fixed32Property { get; set; }
    public ulong ulongProperty { get; set; }

    [ProtofileGenerationPropertyType("fixed64")]
    public ulong fixed64Property { get; set; }
    public bool boolProperty { get; set; }
    public SomeEnume[] enumeeeeeeeee { get; set; }
}
