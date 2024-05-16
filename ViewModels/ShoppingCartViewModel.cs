using AppStudencWeb.Models;
using System.Collections.Generic;

namespace AppStudencWeb.ViewModels
{
    public class ShoppingCartViewModel
    {
        public List<FoodItem> Items { get; set; }
        public double TotalPrice { get; set; }
        public bool StudentDiscountApplied { get; set; }
        public int StudentCouponsLeft { get; set; }
    }
}
