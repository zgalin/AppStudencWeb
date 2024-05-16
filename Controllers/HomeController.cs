using Microsoft.AspNetCore.Mvc;
using AppStudencWeb.Models;
using AppStudencWeb.Simulations;
using AppStudencWeb.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace AppStudencWeb.Controllers
{
    public class HomeController : Controller
    {
        private static List<Restaurant> _restaurants = new List<Restaurant>
        {
            new Restaurant
            {
                Id = 1,
                Name = "Gostilna pri Jožetu",
                Description = "Traditional Slovenian cuisine",
                Latitude = 46.056946,
                Longitude = 14.505751,
                Menu = new List<FoodItem>
                {
                    new FoodItem { Name = "Kranjska klobasa", Price = 8.5 },
                    new FoodItem { Name = "Prekmurska gibanica", Price = 5.0 }
                }
            },
            new Restaurant
            {
                Id = 2,
                Name = "Pizza Mario",
                Description = "Italian pizzas and pastas",
                Latitude = 46.05108,
                Longitude = 14.50693,
                Menu = new List<FoodItem>
                {
                    new FoodItem { Name = "Margherita", Price = 7.0 },
                    new FoodItem { Name = "Spaghetti Carbonara", Price = 9.5 }
                }
            },
            new Restaurant
            {
                Id = 3,
                Name = "Sushi Sakura",
                Description = "Authentic Japanese sushi",
                Latitude = 46.04893,
                Longitude = 14.50285,
                Menu = new List<FoodItem>
                {
                    new FoodItem { Name = "California Roll", Price = 12.0 },
                    new FoodItem { Name = "Nigiri", Price = 10.0 }
                }
            }
        };

        private static ShoppingCart _shoppingCart = new ShoppingCart();
        private static DeliveryService_SIM _deliveryService = new DeliveryService_SIM();
        private static PaymentService_SIM _paymentService = new PaymentService_SIM();
        private static OrderHistory _orderHistory = new OrderHistory();

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                Restaurants = _restaurants,
                StudentCouponsLeft = _shoppingCart.StudentCouponsLeft
            };

            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
            var restaurant = _restaurants.FirstOrDefault(r => r.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }

            return View(restaurant);
        }

        [HttpPost]
        public IActionResult AddToCart(int restaurantId, string foodName)
        {
            var restaurant = _restaurants.FirstOrDefault(r => r.Id == restaurantId);
            if (restaurant != null)
            {
                var foodItem = restaurant.Menu.FirstOrDefault(f => f.Name == foodName);
                if (foodItem != null)
                {
                    _shoppingCart.Items.Add(foodItem);
                }
            }

            return RedirectToAction("ViewCart");
        }

        public IActionResult ViewCart()
        {
            var viewModel = new ShoppingCartViewModel
            {
                Items = _shoppingCart.Items.ToList(),
                TotalPrice = _shoppingCart.TotalPrice,
                StudentDiscountApplied = _shoppingCart.StudentDiscountApplied,
                StudentCouponsLeft = _shoppingCart.StudentCouponsLeft,
                EstimatedDeliveryTime = GetEstimatedDeliveryTime() // Simulated estimated delivery time
            };

            return View(viewModel);
        }

        private string GetEstimatedDeliveryTime()
        {
            // Simulate an estimated delivery time
            var estimatedTime = DateTime.Now.AddMinutes(new Random().Next(30, 60));
            return estimatedTime.ToString("hh:mm tt");
        }

        [HttpPost]
        public IActionResult ApplyStudentDiscount()
        {
            if (_shoppingCart.StudentCouponsLeft > 0)
            {
                _shoppingCart.StudentDiscountApplied = true;
                _shoppingCart.StudentCouponsLeft--;
                _shoppingCart.Items.ForEach(item => item.Price *= 0.8); // Apply 20% discount
            }

            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public IActionResult SubmitOrder(string deliveryAddress, string paymentMethod)
        {
            _shoppingCart.DeliveryAddress = deliveryAddress;
            _shoppingCart.PaymentMethod = paymentMethod;

            // Simulate payment processing
            if (!_paymentService.ProcessPayment(paymentMethod, _shoppingCart.TotalPrice))
            {
                return RedirectToAction("ViewCart", new { errorMessage = "Payment failed" });
            }

            // Create and submit the order
            var order = new Naročilo
            {
                Id = new Random().Next(1, 1000), // Simulate order ID generation
                StanjeNarocila = "Being prepared",
                CasNarocila = DateTime.Now,
                DeliveryAddress = deliveryAddress,
                PaymentMethod = paymentMethod
            };

            _orderHistory.Orders.Add(order);

            // Clear the shopping cart
            _shoppingCart.Items.Clear();

            // Simulate order process
            SimulateOrderProcess(order);

            return RedirectToAction("OrderConfirmation", new { orderId = order.Id });
        }

        private async void SimulateOrderProcess(Naročilo order)
        {
            // Simulate being prepared
            await Task.Delay(5000); // Wait for 5 seconds
            order.StanjeNarocila = "Out for delivery";

            // Simulate delivery
            await Task.Delay(5000); // Wait for another 5 seconds
            order.StanjeNarocila = "Delivered";

            // Simulate order completion
            await Task.Delay(5000); // Wait for another 5 seconds
            order.StanjeNarocila = "Completed";
        }

        public IActionResult OrderConfirmation(int orderId)
        {
            var order = _orderHistory.Orders.FirstOrDefault(o => o.Id == orderId);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        public IActionResult TrackOrder()
        {
            // Simulate delivery status
            var orderStatus = _deliveryService.GetDeliveryStatus();
            var viewModel = new TrackOrderViewModel
            {
                OrderStatus = orderStatus,
                Latitude = _shoppingCart.Latitude,
                Longitude = _shoppingCart.Longitude
            };
            return View(viewModel);
        }

        public IActionResult History()
        {
            return View(_orderHistory);
        }
    }
}
