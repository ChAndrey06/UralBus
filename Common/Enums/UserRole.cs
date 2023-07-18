namespace Common.Enums;

[Flags]
public enum UserRole
{
    None = 0,
    Client = 1,
    Driver = 2,
    Dispatcher = 4,
    Operator = 8,
    Carrier = 16,
    Admin = 32,
    SuperAdmin = 64
}