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

        public async Task<IActionResult> Address()
        {
            return View();
        }


        public async Task<ActionResult> OrderCreated(AddressCreateModel model)
        {
            if (ModelState.IsValid)
            {

                await _orderService.PostOrder(model);

  

                return RedirectToAction("Products", "Home");
            }



            return RedirectToAction("Address");

            

        }
    }
}
