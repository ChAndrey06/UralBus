namespace Common.Helpers;

public static class DictionaryExtensions
{
    public static Dictionary<string, object> ToCamelCase(this Dictionary<string, object> dictionary)
    {
        var result = new Dictionary<string, object>();
        foreach (var item in dictionary)
        {
            var key = Char.ToUpperInvariant(item.Key[0]) + item.Key.Substring(1);
            var value = item.Value;

            if (value is Dictionary<string, object> nestedDict)
            {
                value = ToCamelCase(nestedDict);
            }
            else if (value is IEnumerable<object> enumerable)
            {
                var objectList = new List<object>();
                foreach (var element in enumerable)
                {
                    if (element is Dictionary<string, object> nestedElement)
                    {
                        objectList.Add(ToCamelCase(nestedElement));
                    }
                    else
                    {
                        objectList.Add(element);
                    }
                }
                value = objectList;
            }

            result[key] = value;
        }

        return result;
    }
}
