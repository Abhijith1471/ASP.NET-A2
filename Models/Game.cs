namespace VintageGameStore.Models
{
    public class Game
    {
        // Assigned primary key
        public int Id { get; set; }

        public string Title { get; set; }
        public double Price { get; set; }
        public int ReleaseYear { get; set; }

        // Foreign key to the Category model
        public int CategoryId { get; set; }

        // Child class linked to parent class (Category)
        public Category Category { get; set; }
    }
}
