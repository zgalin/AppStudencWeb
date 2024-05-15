using System;
using System.Collections.Generic;

namespace AppStudencWeb.Models
{
    public class Košarica
    {
        public int KosaricaId { get; set; }
        public double CenaDostave { get; set; }
        public int NarociloId { get; set; }
        public List<string> Vsebina { get; set; } = new List<string>();
        public double Cena { get; set; }

        public void DodajArtikel(string artikel)
        {
            Vsebina.Add(artikel);
        }

        public double IzračunajCenoDostave()
        {
            return Vsebina.Count * 2.0;
        }

        public void OddajNaročilo()
        {
            // Placeholder for order submission logic
        }

        public void PosodobiVsebino(string artikel, int index)
        {
            if (index >= 0 && index < Vsebina.Count)
            {
                Vsebina[index] = artikel;
            }
        }
    }
}
