using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Moon.Validation.Sample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var validator = new DataValidator();
            var results = validator.Validate(new FirstModel(), true);

            Console.WriteLine(JsonConvert.SerializeObject(results, Formatting.Indented));
        }
    }

    public class FirstModel
    {
        [Required]
        public string Name { get; set; }

        public SecondModel Second { get; set; } = new SecondModel();
    }

    public class SecondModel
    {
        [Required]
        public string Name { get; set; }

        public ThirdModel Third { get; set; } = new ThirdModel();
    }

    public class ThirdModel : IValidatableObject
    {
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name != "Third")
            {
                yield return new ValidationResult("The Name should be 'Third'.", new[] { nameof(Name) });
            }
        }
    }
}