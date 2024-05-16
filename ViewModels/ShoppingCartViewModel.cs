using AppStudencWeb.Models;
using System.Collections.Generic;

namespace AppStudencWeb.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<FoodItem> Items { get; set; } = new List<FoodItem>();
        public double TotalPrice { get; set; }
        public bool StudentDiscountApplied { get; set; }
        public int StudentCouponsLeft { get; set; }
        public string EstimatedDeliveryTime { get; set; } = string.Empty;
    }
}
