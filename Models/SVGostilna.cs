namespace AppStudencWeb.Models
{
    public class SVGostilna
    {
        public void SprejmiNaročilo(Naročilo order)
        {
            // Accept order logic
            order.StanjeNarocila = "Accepted";
        }

        public void PosodobiStanjeNarocila(Naročilo order, string newStatus)
        {
            // Update order status logic
            order.StanjeNarocila = newStatus;
        }

        public Naročilo VrniPodrobnostiNarocila(int orderId)
        {
            // Return order details logic
            // Simulate returning order details (in real application, this would query a database)
            return new Naročilo { Id = orderId, StanjeNarocila = "Delivered", CasNarocila = DateTime.Now };
        }
    }
}
