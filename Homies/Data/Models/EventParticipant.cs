using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic.CompilerServices;

namespace Homies.Data.Models
{
    public class EventParticipant
    {
        [Required]
        public string HelperId { get; set; }

        
        [Required]
        public int EventId { get; set; }
     
        public Event Event { get; set; }=null!;


    }
}
