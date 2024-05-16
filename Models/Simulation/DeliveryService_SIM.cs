namespace AppStudencWeb.Models.Simulation
{
    public class DeliveryService_SIM
    {
        public string GetDeliveryStatus()
        {
            // Simulate delivery status
            string[] statuses = { "Order received", "Preparing", "Out for delivery", "Delivered" };
            Random rand = new Random();
            return statuses[rand.Next(statuses.Length)];
        }
    }
}
