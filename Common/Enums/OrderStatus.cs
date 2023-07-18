using System.ComponentModel;

namespace Common.Enums;

public enum OrderStatus
{
    [Description("Забронирован")]
    Booked,
    [Description("Активный")]
    Active,
    [Description("Пропущен")]
    Missed,
    [Description("Завершен")]
    Completed,
    [Description("Ворврат 100%")]
    Refund100,
    [Description("Ворврат 75%")]
    Refund75,
    [Description("Ворврат 50%")]
    Refund50,
    [Description("Ворврат 25%")]
    Refund25,
}