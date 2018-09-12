using System.Collections.Generic;

namespace ValidationService
{
    public class ValidationResult
    {
        public bool Ok { get; set; }

        public IEnumerable<string> Errors { get; set; }
    }
}
