using System.Collections.Generic;

namespace AppStudencWeb.Models
{
    public class Restaurant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public List<FoodItem> Menu { get; set; } = new List<FoodItem>();
    }
}
