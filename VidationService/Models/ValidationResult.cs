using System.Collections.Generic;

namespace ValidationService
{
    public class ValidationResult
    {
        public ValidationResult(bool ok, IEnumerable<string> errors)
        {
            Ok = ok;
            Errors = errors;
        }

        public bool Ok { get; }

        public IEnumerable<string> Errors { get; }
    }
}
