using System.Collections.Generic;

namespace AppStudencWeb.Models
{
    public class ShoppingCart
    {
        public List<FoodItem> Items { get; set; } = new List<FoodItem>();
        public string DeliveryAddress { get; set; }
        public string PaymentMethod { get; set; }
        public bool StudentDiscountApplied { get; set; }
        public int StudentCouponsLeft { get; set; } = 5; // Initial number of coupons

        public double TotalPrice
        {
            get
            {
                double total = 0;
                foreach (var item in Items)
                {
                    total += item.Price;
                }
                if (StudentDiscountApplied)
                {
                    total *= 0.8; // 20% discount
                }
                return total;
            }
        }
    }
}
