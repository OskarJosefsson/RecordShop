using Microsoft.AspNetCore.Mvc.Rendering;
using RecordShopClassLibrary.Models.Read;

namespace RecordShopMvc.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductViewModel>> GetAllProducts(int id);
        Task<IEnumerable<ProductViewModel>> GetAllProducts();
        Task<IEnumerable<SelectListItem>> GetAllCategoriesSelectListItem();
        Task<ProductViewModel> GetProduct(int id);
    }
    public class ProductService : IProductService
    {
        public async Task<IEnumerable<ProductViewModel>> GetAllProducts(int id)
        {
            IEnumerable<ProductViewModel> products = new List<ProductViewModel>();
            IEnumerable<CategoryViewModel> categories = new List<CategoryViewModel>();

            if (id == 0)
            {
                using (var client = new HttpClient())
                {
                    products = await client.GetFromJsonAsync<IEnumerable<ProductViewModel>>("https://localhost:7247/api/product?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
                }

                return products;
            }

            using (var client = new HttpClient())
            {
                products = await client.GetFromJsonAsync<IEnumerable<ProductViewModel>>("https://localhost:7247/api/product?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
            }

            using (var client = new HttpClient())
            {
                categories = await client.GetFromJsonAsync<IEnumerable<CategoryViewModel>>("https://localhost:7247/api/Category?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
            }

            var category = categories.Where(y => y.Id == id).First();
            products = products.Where(x => x.CategoryName == category.Name).ToList();

            

            return products;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
        {
            IEnumerable<ProductViewModel> products = new List<ProductViewModel>();


            using (var client = new HttpClient())
                {
                    products = await client.GetFromJsonAsync<IEnumerable<ProductViewModel>>("https://localhost:7247/api/product?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
                }

                return products;
            


        }

        public async Task<ProductViewModel> GetProduct(int id)
        {

            ProductViewModel product = new ProductViewModel();
            using (var client = new HttpClient())
            {
                product = await client.GetFromJsonAsync<ProductViewModel>($"https://localhost:7247/api/product/{id}?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

            }

            return product;
        }
        public async Task<IEnumerable<SelectListItem>> GetAllCategoriesSelectListItem()
        {
            List<SelectListItem> categories = new List<SelectListItem>();
            IEnumerable<CategoryViewModel> categoryViewModels = new List<CategoryViewModel>();
            using(var client = new HttpClient())
            {
                categoryViewModels = await client.GetFromJsonAsync<IEnumerable<CategoryViewModel>>("https://localhost:7247/api/Category?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
            }
            foreach (var item in categoryViewModels)
            {
                SelectListItem category = new SelectListItem();
                category.Value = Convert.ToString(item.Id);
                category.Text = item.Name;
                categories.Add(category);
            }
            return categories;
        }

        


    }
}
