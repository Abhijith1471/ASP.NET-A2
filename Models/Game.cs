namespace VintageGameStore.Models
{
    public class Game
    {
        // Assigned primary key
        public int Id { get; set; }

        public string? Title { get; set; }
        public decimal Price { get; set; }
        public int ReleaseYear { get; set; }

        public int CategoryId { get; set; }

       
        public Category? Category { get; set; }
    }
}
