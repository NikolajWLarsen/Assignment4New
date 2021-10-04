using Assignment4.Core;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Assignment4.Entities

{
    public class Task
    {
        public  int Id {get; set;}

        [Required]
        [StringLength(50)]
        public string Title { get; set; }

        
        public int AssignedTo { get; set; }

        public string Description { get; set; }

        [Required]
        public State State { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}
