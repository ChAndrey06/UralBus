using System.ComponentModel;

namespace Common.Enums;

public enum IdentificationDocumentType
{
    [Description("Паспорт")]
    Passport,
    [Description("В/У")]
    DriverLicense,
    [Description("Загран. паспорт")]
    InternationalPassport
}