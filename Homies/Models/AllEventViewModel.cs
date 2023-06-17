using Homies.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Type = Homies.Data.Models.Type;

namespace Homies.Models
{
    public class AllEventViewModel
    {
 
        public int Id { get; set; }


        public string Name { get; set; } = null!;


        public string OrganiserId { get; set; } = null!;


        public IdentityUser Organiser { get; set; } = null!;


        public DateTime Start { get; set; }

        public int TypeId { get; set; }

        [Required]
        [ForeignKey(nameof(TypeId))]
        public Type Type { get; set; } = null!;

        public ICollection<EventParticipant> EventsParticipants { get; set; } = null;
    }
}
