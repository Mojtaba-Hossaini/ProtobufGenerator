using System;
using System.Collections;
using System.Globalization;
using System.Security.AccessControl;
using System.Text;

namespace ProtobufGenerator;

public static class GenerateProtobufFromClass
{
    public static void GenerateProtobuf<T>(this T model) where T : Type
    {
        List<Type> types = new List<Type>();
        types.Add(model);
        while (types.Count > 0)
        {
            var type = types[0];
            var properties = type.GetProperties();
            StringBuilder protoStr = new StringBuilder();
            protoStr.AppendLine();
            protoStr.AppendLine($"message {type.Name} " + "{");
            for (int i = 0; i < properties.Length; i++)
            {
                if (CheckCollection(properties[i]))
                {
                    if (CheckSystemType(properties[i].PropertyType.GenericTypeArguments[0]))
                        AddTypeCamelCase(protoStr, "repeated ", properties[i].PropertyType.GenericTypeArguments[0].Name, properties[i].Name, i + 1);
                    else
                    {
                        AddTypePascalCase(protoStr, "repeated ", properties[i].PropertyType.GenericTypeArguments[0].Name, properties[i].Name, i + 1);
                        if(!types.Contains(properties[i].PropertyType.GenericTypeArguments[0]))
                            types.Add(properties[i].PropertyType.GenericTypeArguments[0]);
                    }
                        
                }
                else if (CheckDateTime(properties[i]))
                    AddTypePascalCase(protoStr, string.Empty, "double", properties[i].Name, i + 1);
                else if (CheckSystemType(properties[i].PropertyType))
                    AddTypeCamelCase(protoStr, string.Empty, properties[i].PropertyType.Name, properties[i].Name, i + 1);
                else
                {
                    AddTypePascalCase(protoStr, string.Empty, properties[i].PropertyType.Name, properties[i].Name, i + 1);
                    if(!types.Contains(properties[i].PropertyType))
                        types.Add(properties[i].PropertyType);
                }
                    
            }
            protoStr.AppendLine("}");

            File.AppendAllText(@"C:\Users\M.Hussaini\Documents\testProto.proto", protoStr.ToString());
            types.Remove(type);
        }
        
    }

    private static bool CheckSystemType(Type property)
    {
        return property.FullName.StartsWith("System");
    }

    private static void AddTypeCamelCase(StringBuilder protoStr, string preType, string typeName, string propertyName, int num)
    {
        protoStr.AppendLine($"  {preType}{char.ToLower(typeName[0]) + typeName[1..]} {char.ToLower(propertyName[0]) + propertyName[1..]} = {num};");
    }

    private static void AddTypePascalCase(StringBuilder protoStr, string preType, string typeName, string propertyName, int num)
    {
        protoStr.AppendLine($"  {preType}{typeName} {char.ToLower(propertyName[0]) + propertyName[1..]} = {num};");
    }

    private static bool CheckCollection(System.Reflection.PropertyInfo property)
    {
        return typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string);
    }

    private static bool CheckDateTime(System.Reflection.PropertyInfo property)
    {
        return typeof(DateTime).IsAssignableFrom(property.PropertyType);
    }

}
