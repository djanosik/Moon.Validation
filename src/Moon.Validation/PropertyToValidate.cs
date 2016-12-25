using System.ComponentModel.DataAnnotations;

namespace Moon.Validation
{
    internal class PropertyToValidate
    {
        public object Value { get; set; }

        public ValidationContext Context { get; set; }
    }
}