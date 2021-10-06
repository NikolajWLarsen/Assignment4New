using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment4.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        
        [Required]
        //[Index(IsUnique=true)] //todo - how to?
        [StringLength(50)]
        public string Name { get; set; }
        
        public ICollection<Task> tasks { get; set; } 
    }
}
