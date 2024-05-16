namespace AppStudencWeb.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public int Rating { get; set; } // Rating from 1 to 5
        public string Comment { get; set; } // Optional comment

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
