using System.ComponentModel.DataAnnotations;

namespace Homies.Models
{
    public class TypeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;
    }
}
