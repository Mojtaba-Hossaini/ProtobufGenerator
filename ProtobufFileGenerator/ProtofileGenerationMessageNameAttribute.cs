using System;

namespace ProtobufFileGenerator
{
    /// <summary>
    /// determine the name of message generated from class in proto file in GRPC.
    /// if you not specefied the name, default message name will be the name of c# class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ProtofileGenerationMessageNameAttribute : Attribute
    {
        /// <summary>
        /// Name of message generated from c# class.
        /// </summary>
        /// <param name="messageName">Message name generated with this name you pass into</param>
        public ProtofileGenerationMessageNameAttribute(string messageName)
        {
            TargetMessageName = messageName;
        }
        public string TargetMessageName { get; set; }
    }
}
