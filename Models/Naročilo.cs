using System;

namespace AppStudencWeb.Models
{
    public class Naročilo
    {
        public int Id { get; set; }
        public string StanjeNarocila { get; set; } = "Pending";
        public DateTime CasNarocila { get; set; } = DateTime.Now;
        public string DeliveryAddress { get; set; }
        public string PaymentMethod { get; set; }
    }
}
