using System;

namespace AppStudencWeb.Models
{
    public class Naročilo
    {
        public string StanjeNarocila { get; set; }
        public DateTime CasNarocila { get; set; }

        public string VrniStanjeNarocila()
        {
            return StanjeNarocila;
        }

        public TimeSpan VrniPreostaliCas()
        {
            return DateTime.Now - CasNarocila;
        }
    }
}
