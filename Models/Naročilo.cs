using System;

namespace AppStudencWeb.Models
{
    public class Naročilo
    {
        public int Id { get; set; }  // Add this property
        public string StanjeNarocila { get; set; }
        public DateTime CasNarocila { get; set; }
    }
}
