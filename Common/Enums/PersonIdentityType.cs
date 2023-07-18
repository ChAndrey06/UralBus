using System.ComponentModel;

namespace Common.Enums;

public enum PersonIdentityType
{
    [Description("Клиент")]
    Client,
    [Description("Водитель")]
    Driver,
    [Description("Диспетчер")]
    Dispatcher,
    [Description("Оператор")]
    Operator
}