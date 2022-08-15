ProtobufGenerator<br />
<br />Generate .proto file for GRPC from c# class.
<br />Generate double value in .proto file from DateTime value of c#,
Becouse front developers can convert Date to milliseconds and easy for them to work with milliseconds date.
<br />**You must check some of data type in GRPC maybe not supported**.
<br />Usage:<br />
use any class type and pass the proto file path to extension method and it will appends  to exsisting proto file

 `typeof(Person).GenerateProtobufFile(@"C:\Users\UserAccount\Documents\testProto.proto");`

You can override default value type generator by using `ProtofileGenerationPropertyType` attribute above any property:

`[ProtofileGenerationPropertyType("sint32")]`
`public int sint32Value { get; set; }`

and it will generate protofile like this:

`sint32 sint32Value = 1;`

You can override default naming message by using ProtofileGenerationMessageName attribute above the c# class:

`[ProtofileGenerationMessageName("NewAddress")]`
`public class Address`
`{`
    `public string Allay { get; set; }`
`}`

and it will generate message in proto file like this:

`message NewAddress {`
    `string allay = 1;`
`}`
