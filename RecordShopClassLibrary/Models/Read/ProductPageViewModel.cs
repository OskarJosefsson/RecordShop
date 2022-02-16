using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordShopClassLibrary.Models.Read
{
    public class ProductPageViewModel
    {
        public ProductPageViewModel()
        {

        }

        public ProductPageViewModel(IEnumerable<ProductViewModel> products, IEnumerable<SelectListItem> categories)
        {
            Products = products;
            Categories = categories;
        }

        public ProductPageViewModel(IEnumerable<ProductViewModel> products, IEnumerable<ProductViewModel> topProducts, IEnumerable<SelectListItem> categories)
        {
            Products = products;
            TopProducts = topProducts;
            Categories = categories;
        }

        public IEnumerable<ProductViewModel> Products { get; set; }
        public IEnumerable<ProductViewModel> TopProducts { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public SelectListItem? CategoryId { get; set; }

    }
}
