using System.ComponentModel;
using System.Reflection;

public static class EnumExtensions
{
    public static TEnum ConvertToEnum<TEnum>(this string value) where TEnum : Enum
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentNullException(nameof(value));
        }

        return (TEnum)Enum.Parse(typeof(TEnum), value, true);
    }

    public static string ConvertToString<TEnum>(this TEnum value) where TEnum : Enum
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        return value.ToString();
    }

    public static TEnum ParseFlags<TSource, TEnum>(TSource sourceValue) where TEnum : Enum
    {
        var values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
        int result = values
            .Where(x => Convert.ToInt32(x) != 0 && (sourceValue?.ToString()?.Split(new char[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries).Contains(x.ToString()) ?? false))
            .Select(x => Convert.ToInt32(x))
            .Aggregate(0, (current, value) => current | value);
        return (TEnum)Enum.ToObject(typeof(TEnum), result);
    }
    
    public static string ConvertFlagsToString<TEnum>(this TEnum value) where TEnum : Enum
    {
        var names = Enum.GetNames(typeof(TEnum));
        var flags = names.Select(name => (Enum.Parse(typeof(TEnum), name), name)).ToList();

        var parts = flags.Where(f => value.HasFlag((Enum)f.Item1)).Select(f => f.name);

        return string.Join(", ", parts);
    }

    public static string GetDisplayName(this System.Enum value)
    {
        var displayNameAttr = value.GetType()
                                   .GetMember(value.ToString())[0]
                                   .GetCustomAttribute<DisplayNameAttribute>();

        if (displayNameAttr != null)
        {
            return displayNameAttr.DisplayName;
        }
        else
        {
            return value.ToString();
        }
    }
}