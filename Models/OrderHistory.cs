using System.Collections.Generic;

namespace AppStudencWeb.Models
{
    public class OrderHistory
    {
        public List<Naročilo> Orders { get; set; } = new List<Naročilo>();
    }
}
