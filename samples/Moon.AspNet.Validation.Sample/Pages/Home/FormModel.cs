using System.ComponentModel.DataAnnotations;
using Moon.Validation;

namespace Moon.AspNet.Sample.Pages.Home
{
    public enum Enum1
    {
        Required,
        Other
    }

    public enum Enum2
    {
        First,
        Second
    }

    public class FormModel
    {
        [Digits]
        public string Digits { get; set; }

        [Double]
        public double? Double { get; set; }

        public Enum1? TypeRequired { get; set; }

        [RequiredIfEqual("TypeRequired", Enum1.Required)]
        public Enum2? MigthBeRequired { get; set; }

        [GreaterThan("Min")]
        public int GreaterThanMin { get; set; }

        [Min(5)]
        public int Min { get; set; }

        [MinLength(3), MaxLength(10)]
        public string MinMaxLength { get; set; }

        [Required]
        public string Required { get; set; }

        [RequiredIfNotEmpty("Digits")]
        public string RequiredIfDigitsNotEmpty { get; set; }

        [RequiredIfNotEqual("Min", 11)]
        public string RequiredIfMinNotEqualTo11 { get; set; }
    }
}