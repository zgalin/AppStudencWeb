using Microsoft.AspNetCore.Mvc;
using AppStudencWeb.Models;
using AppStudencWeb.Models.Simulation;
using System.Collections.Generic;
using System.Linq;

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
        private static K_Naročilo_Hrane _boundary = new K_Naročilo_Hrane();
        private static SV_Studetska_Prehrana _studentService = new SV_Studetska_Prehrana();
        private static SVGostilna _restaurantService = new SVGostilna();

        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                Restaurants = _restaurants,
                StudentCouponsLeft = _boundary.StudentCouponsLeft
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
                    _boundary.DodajArtikel(foodItem);
                }
            }

            return RedirectToAction("ViewCart");
        }

        public IActionResult ViewCart()
        {
            var viewModel = new ShoppingCartViewModel
            {
                Items = _boundary.Items,
                TotalPrice = _boundary.IzracunajCeno(),
                StudentDiscountApplied = _boundary.StudentDiscountApplied,
                StudentCouponsLeft = _boundary.StudentCouponsLeft
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult ApplyStudentDiscount()
        {
            if (_studentService.ValidirajŠtudetskiStatus())
            {
                _studentService.PotrdiBon();
                _boundary.ApplyStudentDiscount();
            }

            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public IActionResult SubmitOrder(string deliveryAddress, string paymentMethod, double latitude, double longitude)
        {
            _shoppingCart.DeliveryAddress = deliveryAddress;
            _shoppingCart.PaymentMethod = paymentMethod;
            _shoppingCart.Latitude = latitude;
            _shoppingCart.Longitude = longitude;

            // Simulate payment processing
            if (!_paymentService.ProcessPayment(paymentMethod, _boundary.IzracunajCeno()))
            {
                return RedirectToAction("ViewCart", new { errorMessage = "Payment failed" });
            }

            // Create and submit the order
            var order = new Naročilo
            {
                Id = new Random().Next(1, 1000), // Simulate order ID generation
                StanjeNarocila = "Submitted",
                CasNarocila = DateTime.Now
            };
            _restaurantService.SprejmiNaročilo(order);

            // Clear the shopping cart
            _boundary.PocistiKosarico();

            return RedirectToAction("OrderConfirmation", new { orderId = order.Id });
        }

        public IActionResult OrderConfirmation(int orderId)
        {
            var orderDetails = _restaurantService.VrniPodrobnostiNarocila(orderId);
            return View(orderDetails);
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
    }

    public class HomeViewModel
    {
        public List<Restaurant> Restaurants { get; set; } = new List<Restaurant>();  // Ensure it has a default value
        public int StudentCouponsLeft { get; set; }
    }

    public class ShoppingCartViewModel
    {
        public List<FoodItem> Items { get; set; } = new List<FoodItem>();  // Ensure it has a default value
        public double TotalPrice { get; set; }
        public bool StudentDiscountApplied { get; set; }
        public int StudentCouponsLeft { get; set; }
    }

    public class TrackOrderViewModel
    {
        public string OrderStatus { get; set; } = string.Empty;  // Ensure it has a default value
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
