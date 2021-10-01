namespace Assignment4.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        
        [Required]
        [IsUnique = true]
        [StringLength(50)]
        public string Name { get; set; }
        
        public ICollection<Task> tasks { get; set; }
    }
}
