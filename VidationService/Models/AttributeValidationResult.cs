namespace ValidationService
{
    public class AttributeValidationResult
    {
        public AttributeValidationResult(bool ok)
        {
            Ok = ok;
        }

        public bool Ok { get; }

        public string Error { get; set; }
    }
}
