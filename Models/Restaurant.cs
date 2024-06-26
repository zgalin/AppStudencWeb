namespace AppStudencWeb.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<FoodItem> Menu { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();

        public double AverageRating
        {
            get
            {
                if (Reviews.Count == 0) return 0;
                return Reviews.Average(r => r.Rating);
            }
        }
    }
}
