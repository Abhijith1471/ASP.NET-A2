namespace VintageGameStore.Models
{
    public class Category
    {
        // Assigned primary key
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }

        // For subcategories (can include many games)
        public List<Game>? Games { get; set; } = new List<Game>();
    }
}
