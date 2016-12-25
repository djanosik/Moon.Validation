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

        public ThirdModel[] Thirds { get; set; } = new[] { new ThirdModel() };
    }

    public class ThirdModel
    {
        [Required]
        public string Name { get; set; }

        public ForthModel Forth { get; set; } = new ForthModel();
    }

    public class ForthModel : IValidatableObject
    {
        public string Name { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Name != "Forth")
            {
                yield return new ValidationResult("The Name should be 'Forth'.", new[] { nameof(Name) });
            }
        }
    }
}