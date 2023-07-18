using System.ComponentModel;

namespace Common.Enums;

public enum PaymentStatus
{
    [Description("Создан")]
    Created,
    [Description("В ожидании")]
    Pending,
    [Description("Успешно")]
    Completed,
    [Description("Отмена")]
    Cancelled
}