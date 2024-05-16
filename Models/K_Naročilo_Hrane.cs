using System.Collections.Generic;

namespace AppStudencWeb.Models
{
    public class K_Naročilo_Hrane
    {
        private List<FoodItem> items = new List<FoodItem>();
        private int studentCouponsLeft = 5; // Example initial number of coupons

        public List<FoodItem> Items => items;
        public int StudentCouponsLeft => studentCouponsLeft;
        public bool StudentDiscountApplied { get; private set; }

        public void PrikaziPodrobnostiArtikla(FoodItem item)
        {
            // Logic to display item details
            // This can be implemented to return the item details or display them in the UI
        }

        public List<FoodItem> PregledŠtudentskePonudbe()
        {
            // Logic to review student offers
            // This can return a list of items that are available for students
            return items; // Example logic, should return filtered list based on student offers
        }

        public void DodajArtikel(FoodItem item)
        {
            items.Add(item);
        }

        public void ApplyStudentDiscount()
        {
            if (studentCouponsLeft > 0 && !StudentDiscountApplied)
            {
                StudentDiscountApplied = true;
                studentCouponsLeft--;
            }
        }

        public void OddajNaročilo(string deliveryAddress, string paymentMethod)
        {
            // Logic to submit order
            // This could include saving the order details to a database or processing the payment
        }

        public double IzracunajCeno()
        {
            double totalPrice = 0;
            foreach (var item in items)
            {
                totalPrice += item.Price;
            }

            if (StudentDiscountApplied)
            {
                totalPrice *= 0.8; // Applying a 20% discount
            }

            return totalPrice;
        }

        public void PosodobiVsebino(List<FoodItem> newItems)
        {
            items = newItems;
        }

        public void PocistiKosarico()
        {
            items.Clear();
            StudentDiscountApplied = false;
        }
    }
}
