using System.ComponentModel.DataAnnotations;
using static Homies.Common.ModelValidationConstants;

namespace Homies.Models
{
    public class TypeViewModel
    {
        public int Id { get; set; }

        [StringLength(TypeNameMaxLength, MinimumLength = TypeNameMinLength,
            ErrorMessage = "Type length must be between 5 and 15 characters.")]
        public string Name { get; set; } = null!;
    }
}
