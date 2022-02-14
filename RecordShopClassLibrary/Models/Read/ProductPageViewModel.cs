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

        public ProductPageViewModel(IEnumerable<ProductViewModel> products, IEnumerable<SelectListItem> categories, SelectListItem? categoryId)
        {
            Products = products;
            Categories = categories;
            CategoryId = categoryId;
        }

        public IEnumerable<ProductViewModel> Products { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; }

        public SelectListItem? CategoryId { get; set; }

    }
}
