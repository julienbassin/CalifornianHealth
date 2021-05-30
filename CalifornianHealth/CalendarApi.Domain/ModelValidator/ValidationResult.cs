using System.Collections.Generic;

namespace CalendarApi.Domain.ModelValidator
{
    public class ValidationResult
    {
        public ValidationResult()
        {
            ErrorMessages = new Dictionary<string, string>();
        }
        public bool IsValid { get; set; }
        public Dictionary<string, string> ErrorMessages { get; set; }
    }
}
