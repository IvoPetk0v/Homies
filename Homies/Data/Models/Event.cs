﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;
using static Homies.Common.ModelValidationConstants;
namespace Homies.Data.Models
{
    public class Event
    {
        public Event()
        {
            this.EventsParticipants = new HashSet<EventParticipant>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(EventNameMaxLength)]
        public string Name { get; set; } = null!;

        [Required]
        [MaxLength(EventDescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        public string OrganiserId { get; set; } = null!;

        [Required]
        public IdentityUser Organiser { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public DateTime Start { get; set; }

        [Required]
        public DateTime End { get; set; }

        [Required]
        public int TypeId { get; set; }

        [Required]
        [ForeignKey(nameof(TypeId))]
        public Type Type { get; set; }=null!;

        public ICollection<EventParticipant> EventsParticipants { get; set; } 


        //⦁	Has Id – a unique integer, Primary Key
        //⦁	Has Name – a string with min length 5 and max length 20 (required)
        //⦁	Has Description – a string with min length 15 and max length 150 (required)
        //⦁	Has OrganiserId – an string (required)
        //⦁	Has Organiser – an IdentityUser(required)
        //    ⦁	Has CreatedOn – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
        //⦁	Has Start – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
        //⦁	Has End – a DateTime with format "yyyy-MM-dd H:mm" (required) (the DateTime format is recommended, if you are having troubles with this one, you are free to use another one)
        //⦁	Has TypeId – an integer, foreign key (required)
        //    ⦁	Has Type – a Type(required)
        //    ⦁	Has EventsParticipants – a collection of type EventParticipant

    }
}
