using System.Collections;
using System.Reflection;
using System.Text;

namespace ProtobufGenerator;

public static class GenerateProtobufFromClass
{
    /// <summary>
    /// Generate proto file from c# class.
    /// </summary>
    /// <typeparam name="T">System.Type</typeparam>
    /// <param name="model">System.Type</param>
    /// <param name="protoFilePath">Proto file path which will be appended</param>
    public static void GenerateProtobuf<T>(this T model, string protoFilePath) where T : Type
    {
        List<Type> types = new List<Type>();
        List<Type> typesAddedToProtoFile = new List<Type>();
        List<Type> enums = new List<Type>();
        types.Add(model);
        while (types.Count > 0)
        {
            var type = types[0];
            typesAddedToProtoFile.Add(type);
            var properties = type.GetProperties();
            StringBuilder protoStr = new StringBuilder();
            protoStr.AppendLine();
            string typeName = GetTypeName(type);
            protoStr.AppendLine($"message {typeName} " + "{");
            for (int i = 0; i < properties.Length; i++)
            {
                CheckEnum(enums, properties[i].PropertyType);
                if (CheckCollection(properties[i]))
                {
                    var genericType = properties[i].PropertyType.GenericTypeArguments[0];
                    CheckEnum(enums, genericType);
                    if (!CheckSystemType(genericType))
                    {
                        if (!typesAddedToProtoFile.Contains(genericType) && !genericType.IsEnum)
                        {
                            types.Add(genericType);
                            typesAddedToProtoFile.Add(genericType);
                        }
                    }
                    AppendPropertyToString(protoStr, properties[i], i + 1, " repeated", true);
                }
                else
                {
                    if (!CheckSystemType(properties[i].PropertyType))
                    {
                        if (!typesAddedToProtoFile.Contains(properties[i].PropertyType) && !properties[i].PropertyType.IsEnum)
                        {
                            types.Add(properties[i].PropertyType);
                            typesAddedToProtoFile.Add(properties[i].PropertyType);
                        }
                    }
                    AppendPropertyToString(protoStr, properties[i], i + 1, string.Empty, false);
                }
            }
            protoStr.AppendLine("}");

            File.AppendAllText(protoFilePath, protoStr.ToString());
            types.Remove(type);
        }
        foreach (var item in enums)
        {
            var properties = item.GetMembers(BindingFlags.Public | BindingFlags.Static);
            StringBuilder protoStr = new StringBuilder();
            protoStr.AppendLine();
            protoStr.AppendLine($"enum {item.Name} " + "{");
            for (int i = 0; i < properties.Length; i++)
            {
                protoStr.AppendLine($"  {char.ToLower(properties[i].Name[0]) + properties[i].Name[1..]} = {i};");
            }
            protoStr.AppendLine("}");
            File.AppendAllText(protoFilePath, protoStr.ToString());
        }

    }

    private static string GetTypeName(Type type)
    {
        var typeName = "";
        var customAttr = (ProtofileGenerationMessageNameAttribute)type.GetCustomAttribute(typeof(ProtofileGenerationMessageNameAttribute), false);
        if (customAttr is not null)
            typeName = customAttr.TargetMessageName;

        if (string.IsNullOrEmpty(typeName))
            typeName = type.Name;
        return typeName;
    }

    private static void CheckEnum(List<Type> enums, Type propertyType)
    {
        if (propertyType.IsEnum)
        {
            if (!enums.Contains(propertyType))
                enums.Add(propertyType);
        }
    }

    private static bool CheckSystemType(Type property)
    {
        return property.FullName.StartsWith("System");
    }

    private static void AppendPropertyToString(StringBuilder protoStr, PropertyInfo propertyInfo, int num, string preTypeName, bool isCollection)
    {
        protoStr.AppendLine($"  {preTypeName} {GetTypeName(propertyInfo, isCollection)} {char.ToLower(propertyInfo.Name[0]) + propertyInfo.Name[1..]} = {num};");
    }
    private static bool CheckCollection(System.Reflection.PropertyInfo property)
    {
        return typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string);
    }

    private static string GetTypeName(PropertyInfo propertyInfo, bool isCollection)
    {
        ProtofileGenerationPropertyTypeAttribute customAttr = null;
        Type type = null;
        if (isCollection)
        {
            customAttr = (ProtofileGenerationPropertyTypeAttribute)propertyInfo.PropertyType.GenericTypeArguments[0].GetCustomAttribute(typeof(ProtofileGenerationPropertyTypeAttribute), false);
            type = propertyInfo.PropertyType.GenericTypeArguments[0];
        }

        else
        {
            customAttr = (ProtofileGenerationPropertyTypeAttribute)propertyInfo.GetCustomAttribute(typeof(ProtofileGenerationPropertyTypeAttribute), false);
            type = propertyInfo.PropertyType;
        }



        if (customAttr is not null)
        {
            return customAttr.TargetType;
        }
        else
        {
            switch (type.Name)
            {
                case "Int32":
                    return "int32";
                case "String":
                    return "string";
                case "DateTime":
                    return "double";
                case "Double":
                    return "double";
                case "Single":
                    return "float";
                case "Int64":
                    return "int64";
                case "UInt32":
                    return "uint32";
                case "UInt64":
                    return "uint64";
                case "Boolean":
                    return "bool";
                default:
                    return type.Name;
            }
        }
    }

}
