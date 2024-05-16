using System.Collections.Generic;

namespace AppStudencWeb.Models
{
    public class ShoppingCart
    {
        public List<FoodItem> Items { get; set; } = new List<FoodItem>();
        public string DeliveryAddress { get; set; } = string.Empty;  // Ensure it has a default value
        public string PaymentMethod { get; set; } = string.Empty;  // Ensure it has a default value
        public bool StudentDiscountApplied { get; set; }
        public int StudentCouponsLeft { get; set; } = 5;
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public double TotalPrice
        {
            get
            {
                double totalPrice = 0;
                foreach (var item in Items)
                {
                    totalPrice += item.Price;
                }

                if (StudentDiscountApplied)
                {
                    totalPrice *= 0.8; // Applying a 20% discount
                }

                return totalPrice;
            }
        }
    }
}
