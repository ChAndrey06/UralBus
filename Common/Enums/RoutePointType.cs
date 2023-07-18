using System.ComponentModel;

namespace Common.Enums;

public enum RoutePointType
{
    [Description("Отправление")]
    Departure,
    [Description("Прибытие")]
    Arrival,
    [Description("Передача")]
    Transfer
}