namespace Common.Configuration;

public static class Constants
{
    public static readonly Guid BasicFromId = new Guid("00000000-0000-0000-0000-000000000001");
    public static readonly Guid BasicToId = new Guid("00000000-0000-0000-0000-000000000002");
    public static Guid DefaultUserId => Guid.Parse("82448FB7-CBB9-4054-B871-695DE72FD61D");
    public static Guid DefaultClientId => Guid.Parse("968F48A5-50BA-4EEA-B312-E1072DD062B6");
}