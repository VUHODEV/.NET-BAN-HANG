using Lab03.Models;
using Lab03.Repositories;
using Microsoft.AspNetCore.Mvc;
using Lab03.Extensions;
using Microsoft.AspNetCore.Authorization;
using Lab03.DataAccess;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;

namespace Lab03.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IOrderHistoryRepository _orderHistoryRepository;

        public ShoppingCartController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IProductRepository productRepository, IOrderHistoryRepository orderHistoryRepository)
        {
            _productRepository = productRepository;
            _context = context;
            _userManager = userManager;
            _orderHistoryRepository = orderHistoryRepository;
        }

        public async Task<IActionResult> AddToCart(int productId, int quantity)
        {
            var product = await GetProductFromDatabase(productId);
            var cartItem = new CartItem
            {
                ProductId = productId,
                Name = product.Name,
                Price = product.Price,
                Quantity = quantity
            };
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ??
            new ShoppingCart();
            cart.AddItem(cartItem);
            HttpContext.Session.SetObjectAsJson("Cart", cart);
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart") ?? new ShoppingCart();
            return View(cart);
        }

        private async Task<Product> GetProductFromDatabase(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return product;
        }

        public IActionResult RemoveFromCart(int productId)
        {
            var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
            if (cart is not null)
            {
                cart.RemoveItem(productId);
                HttpContext.Session.SetObjectAsJson("Cart", cart);
            }
            return RedirectToAction("Index");
        }

        public IActionResult Checkout()
        {
            return View(new Order());
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(Order order)
        {
            if (ModelState.IsValid)
            {
                var cart = HttpContext.Session.GetObjectFromJson<ShoppingCart>("Cart");
                if (cart == null || !cart.Items.Any())
                {
                    return RedirectToAction("Index");
                }
                var user = await _userManager.GetUserAsync(User);
                order.UserId = user.Id;
                order.OrderDate = DateTime.UtcNow;
                order.TotalPrice = cart.Items.Sum(i => i.Price * i.Quantity);
                order.OrderDetails = cart.Items.Select(i => new OrderDetail
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    Price = i.Price
                }).ToList();
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                HttpContext.Session.Remove("Cart");

                var orderHistory = new OrderHistory
                {
                    OrderDate = DateTime.Now,
                    CustomerName = user.UserName,
                    ShippingAddress = order.ShippingAddress,
                    Notes = order.Notes,
                    TotalAmount = order.TotalPrice
                };

                await _orderHistoryRepository.AddAsync(orderHistory);

                return RedirectToAction("OrderCompleted", new { id = orderHistory.Id });
            }

            return View(order);
        }

        public async Task<IActionResult> OrderHistory()
        {
            var orderHistories = await _orderHistoryRepository.GetAllAsync();
            return View(orderHistories);
        }
    }
}