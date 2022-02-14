using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using RecordShopClassLibrary.Models;
using RecordShopClassLibrary.Models.Create;
using RecordShopClassLibrary.Models.Read;
using RecordShopMvc.Models;
using RecordShopMvc.Services;
using System.Diagnostics;

namespace RecordShopMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;

        public HomeController(ILogger<HomeController> logger, IProductService productService, ICartService cartService) 
        {
            _cartService = cartService;
            _logger = logger;
            _productService = productService;
        }

        public async Task<IActionResult> Products(ProductPageViewModel model)
        {
            IEnumerable<ProductViewModel> products = new List<ProductViewModel> ();
            IEnumerable <SelectListItem> categories = new List<SelectListItem>();
            if (model.CategoryId == null)
            {
                 products = await _productService.GetAllProducts();

                 categories = await _productService.GetAllCategoriesSelectListItem();

                ProductPageViewModel productPageViewModel = new ProductPageViewModel(products, categories);

                return View(productPageViewModel);
            }

            int id = Convert.ToInt32(model.CategoryId.Value);

            products = await _productService.GetAllProducts(id);

            categories = await _productService.GetAllCategoriesSelectListItem();

            ProductPageViewModel productPageViewModelSort = new ProductPageViewModel(products, categories);

            return View(productPageViewModelSort);
        }


        public async Task<IActionResult> ProductDetail(int id)
        {
            ProductViewModel product = new ProductViewModel();
            if(id != null)
            {
                product = await _productService.GetProduct(id);
            }
            

            return View(product);
        }

        public async Task<IActionResult> BuyProduct(ProductViewModel model)
        {
            if(SessionService.GetObjectAsJson<List<CartItemCreateModel>>(HttpContext.Session, "shoppingCart") == null)
            {
                List<CartItemCreateModel> cartItems = new List<CartItemCreateModel>();
                ProductModel pm = new ProductModel(model.Id,model.Name,model.Description,model.Price,model.CategoryName);
                CartItemCreateModel item = new CartItemCreateModel(pm, model.Quantity);
                cartItems.Add(item);
                SessionService.SetObjectAsJson(HttpContext.Session, "shoppingCart", cartItems);
            }
            else
            {
                List<CartItemCreateModel> cartItems = new List<CartItemCreateModel>();
                cartItems = SessionService.GetObjectAsJson<List<CartItemCreateModel>>( HttpContext.Session, "shoppingCart");
                int index = ItemExists(model.Id);
                if(index != -1)
                {
                    cartItems[index].Quantity += model.Quantity;
                    SessionService.SetObjectAsJson(HttpContext.Session, "shoppingCart", cartItems);
                }
                else
                {

                    ProductModel pm = new ProductModel(model.Id, model.Name, model.Description,model.Price,model.CategoryName);
                    CartItemCreateModel item = new CartItemCreateModel(pm, model.Quantity);
                    cartItems.Add(item);
                    SessionService.SetObjectAsJson(HttpContext.Session, "shoppingCart", cartItems);
                }

            }

            return RedirectToAction("Products");
        }

        public async Task<ActionResult> RemoveProduct(CartItemCreateModel model)
        {
            int index = ItemExists(model.Product.Id);

                List<CartItemCreateModel> cartItems = new List<CartItemCreateModel>();

                cartItems = SessionService.GetObjectAsJson<List<CartItemCreateModel>>(HttpContext.Session, "shoppingCart");

                ProductModel pm = new ProductModel(model.Product.Id, model.Product.Name, model.Product.Description, model.Product.Price, model.Product.CategoryName);
                CartItemCreateModel item = new CartItemCreateModel(pm, model.Quantity);

                var itemToRemove = cartItems.SingleOrDefault(x => x.Product.Id == model.Product.Id);
                cartItems.Remove(itemToRemove);

           SessionService.SetObjectAsJson(HttpContext.Session, "shoppingCart", cartItems);

            return RedirectToAction("Cart");

        }

        public async Task<IActionResult> Cart()
        {
            List<CartItemCreateModel> cartItems = SessionService.GetObjectAsJson<List<CartItemCreateModel>>(HttpContext.Session,"shoppingCart");
            CartPageViewModel model = new CartPageViewModel();
            model.TotalPrice = await _cartService.Price(cartItems);
            model.CartItems = cartItems;
            return View(model);
        }

        public int ItemExists(int id)
        {
             List<CartItemCreateModel> cartItems = new List<CartItemCreateModel>();

            cartItems = SessionService.GetObjectAsJson<List<CartItemCreateModel>>(HttpContext.Session, "shoppingCart");
            for(int i = 0; i < cartItems.Count; i++)
            {
                if (cartItems[i].Product.Id == id)
                {
                    return i;
                }
            }
            return -1;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}