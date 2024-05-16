using AppStudencWeb.Models;
using System.Collections.Generic;

namespace AppStudencWeb.ViewModels
{
    public class HomeViewModel
    {
        public List<Restaurant> Restaurants { get; set; }
        public int StudentCouponsLeft { get; set; }
    }
}
