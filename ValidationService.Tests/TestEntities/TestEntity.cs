using ValidationService.ValidationAttributes;

namespace ValidationService.Tests
{
    public class TestEntity
    {
        [StringMaxLength(10)]
        public string StringTestValue { get; set; }

        [IntRange(3,30)]
        public int IntTestValue { get; set; }

        [RequiredValidation]
        public string RequiredValue { get; set; }
    }
}
