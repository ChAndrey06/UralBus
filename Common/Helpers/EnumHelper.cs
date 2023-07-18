using System.ComponentModel;

namespace Common.Helpers;

public class EnumHelper
{
    public static Dictionary<T, string> GetEnumDescription<T>(T enumValue) where T : Enum
    {
        var enumType = typeof(T);
        var fieldInfo = enumType.GetField(enumValue.ToString());
        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

        string description = enumValue.ToString();

        if (attributes.Length > 0)
            description = attributes[0].Description;

        return new Dictionary<T, string>
        {
            { enumValue, description }
        };
    }
    
    public static Dictionary<T, string> GetAllEnumDescriptions<T>() where T : Enum
    {
        var enumType = typeof(T);
        var enumValues = Enum.GetValues(enumType);
        var descriptionsList = new Dictionary<T, string>();

        foreach (var enumValue in enumValues)
        {
            var fieldInfo = enumType.GetField(enumValue.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            string description = enumValue.ToString();

            if (attributes.Length > 0)
                description = attributes[0].Description;

            descriptionsList.Add((T)enumValue, description);
        }

        return descriptionsList;
    }

}