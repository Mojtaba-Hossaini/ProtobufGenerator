using System;

namespace ProtobufFileGenerator
{
    /// <summary>
    /// determine the custom type of property for proto file.
    /// default taype generates for DateTime type in c# is double.
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class ProtofileGenerationPropertyTypeAttribute : Attribute
    {
        public ProtofileGenerationPropertyTypeAttribute(string typename)
        {
            TargetType = typename;
        }
        public string TargetType { get; set; }
    }
}
