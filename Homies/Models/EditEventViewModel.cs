using System.ComponentModel.DataAnnotations;
using static Homies.Common.ModelValidationConstants;

namespace Homies.Models
{
    public class EditEventViewModel
    {
 
        [StringLength(EventNameMaxLength, MinimumLength = EventNameMinLength, ErrorMessage = "Value must be between 5 and 20")]
        public string Name { get; set; } = null!;

        [StringLength(EventDescriptionMaxLength, MinimumLength = EventDescriptionMinLength, ErrorMessage = "Text characters must be between 15 and 15")]
        public string Description { get; set; } = null!;

        public string Start { get; set; }

        public string End { get; set; }

        [Range(0, int.MaxValue)]
        public int TypeId { get; set; }

        public ICollection<TypeViewModel> Types { get; set; } = new HashSet<TypeViewModel>();
    }
}
