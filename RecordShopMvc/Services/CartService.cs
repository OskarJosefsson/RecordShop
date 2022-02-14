using RecordShopClassLibrary.Models.Create;
using RecordShopClassLibrary.Models.Read;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecordShopMvc.Services
{

    public interface ICartService
    {
        Task<decimal> Price(List<CartItemCreateModel> cartItems);
    }


    public class CartService : ICartService
    {
        private readonly IProductService _productService;

        public CartService(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<decimal> Price(List<CartItemCreateModel> cartItems)
        {
            decimal price = 0;

            if(cartItems != null)
            {
                foreach (var item in cartItems)
                {

                    price += item.Quantity * item.Product.Price;
                }

            }



            return price;
        }




    }

}
