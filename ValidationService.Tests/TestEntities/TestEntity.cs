using ValidationService.ValidationAttributes;

namespace ValidationService.Tests
{
    public class TestEntity
    {
        [StringLengthAttribute(10)]
        public string StringTestValue { get; set; }

        [IntRangeAttribute(3,30)]
        public int IntTestValue { get; set; }

        [NotNull]
        public string NotNullTestValue { get; set; }
    }
}
