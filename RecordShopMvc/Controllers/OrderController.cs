using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RecordShopClassLibrary.Models.Create;
using RecordShopClassLibrary.Models.Read;
using RecordShopMvc.Models.Entities;
using RecordShopMvc.Services;
using System.Security.Claims;

namespace RecordShopMvc.Controllers
{
    public class OrderController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ILogger<HomeController> logger, IProductService productService, ICartService cartService, IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _productService = productService;
            _cartService = cartService;
            _orderService = orderService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Orders()
        {
            IEnumerable<OrderViewModel> orders = new List<OrderViewModel>();

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            Guid id = new Guid(claim.Value);

            orders = await _orderService.GetAllOrders(id);
            return View(orders);
        }

        public async Task<IActionResult> Address(OrderAddressCreateModel model)
        {
            return View(model);
        }


        public async Task<ActionResult> OrderCreated(OrderAddressCreateModel model)
        {


            List<CartItemCreateModel> cartItems = new List<CartItemCreateModel>();
            cartItems = SessionService.GetObjectAsJson<List<CartItemCreateModel>>(HttpContext.Session, "shoppingCart");
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            ApplicationUser currentUser = _userManager.Users.FirstOrDefault(x => x.Id == claim.Value);



            string myName = currentUser.FirstName + " " + currentUser.LastName;
            model.Order.CustomerName = myName;
        
            model.CartItems = cartItems;


            await _orderService.PostOrder(model);

            foreach (var item in cartItems)
            {
                Console.WriteLine(item.Product.Name);
            }
            
            cartItems.Clear();
            SessionService.SetObjectAsJson(HttpContext.Session, "shoppingCart", cartItems);



            return RedirectToAction("Products", "Home");

        }
    }
}
