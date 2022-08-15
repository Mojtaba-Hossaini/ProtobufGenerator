namespace ProtobufGenerator;

public class ProtofileGenerationMessageNameAttribute : Attribute
{
    public ProtofileGenerationMessageNameAttribute(string messageName)
    {
        TargetMessageName = messageName;
    }
    public string TargetMessageName { get; set; }
}
