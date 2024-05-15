using Microsoft.AspNetCore.Mvc;
using AppStudencWeb.Models;
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
            return View(_shoppingCart);
        }

        [HttpPost]
        public IActionResult ApplyStudentDiscount()
        {
            if (_shoppingCart.StudentCouponsLeft > 0 && !_shoppingCart.StudentDiscountApplied)
            {
                _shoppingCart.StudentDiscountApplied = true;
                _shoppingCart.StudentCouponsLeft--;
            }

            return RedirectToAction("ViewCart");
        }

        [HttpPost]
        public IActionResult SubmitOrder(string deliveryAddress, string paymentMethod)
        {
            _shoppingCart.DeliveryAddress = deliveryAddress;
            _shoppingCart.PaymentMethod = paymentMethod;

            // Here you would normally process the order (e.g., save to database, send email, etc.)
            // For simplicity, we will just clear the cart.

            _shoppingCart = new ShoppingCart();

            return RedirectToAction("OrderConfirmation");
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }

        public IActionResult TrackOrder()
        {
            // This would normally track the real order, for now we just display a placeholder
            var orderStatus = "Your order is being prepared and will be delivered soon.";
            return View("TrackOrder", orderStatus);
        }
    }

    public class HomeViewModel
    {
        public List<Restaurant> Restaurants { get; set; }
        public int StudentCouponsLeft { get; set; }
    }
}
