using System.Collections.Generic;

namespace AppStudencWeb.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;  // Ensure it has a default value
        public string Description { get; set; } = string.Empty;  // Ensure it has a default value
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<FoodItem> Menu { get; set; } = new List<FoodItem>();  // Ensure it has a default value
    }
}
