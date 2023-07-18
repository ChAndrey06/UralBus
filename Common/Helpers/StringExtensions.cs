namespace Common.Helpers
{
    public static class StringExtensions
    {
        public static T ParseEnum<T>(this string value)
            where T : Enum
        {
            return (T)Enum.Parse(typeof(T), value, true);
        }

        public static Nullable<int> ParseNulableEnum<T>(this string? value)
            where T : Enum
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
            return new Nullable<int>((int)Enum.Parse(typeof(T), value, true));
        }
    }
}
