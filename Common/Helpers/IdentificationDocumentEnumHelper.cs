using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helpers
{
    public static class IdentificationDocumentEnumHelper
    {
        private static readonly Dictionary<IdentificationDocumentType, string> identificationDocumentTypeNames = new Dictionary<IdentificationDocumentType, string>
    {
        { IdentificationDocumentType.Passport, "Паспорт" },
        { IdentificationDocumentType.DriverLicense, "Лицензия на вождения" },
        { IdentificationDocumentType.InternationalPassport, "Заграничный паспорт" }
    };

        public static string GetName(IdentificationDocumentType value)
        {
            return identificationDocumentTypeNames[value];
        }
    }
}
