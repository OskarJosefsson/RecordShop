using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using RecordShopClassLibrary.Models.Create;
using RecordShopClassLibrary.Models.Entities;
using RecordShopClassLibrary.Models.Read;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using RecordShopClassLibrary.Models.Create;
using RecordShopClassLibrary.Models.Read;
using RecordShopMvc.Services;
using System.Security.Claims;
using Newtonsoft.Json;

namespace RecordShopMvc.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderViewModel>> GetAllOrders(Guid id);
        Task<OrderViewModel> GetOrder(Guid id);
        Task PostOrder(OrderAddressCreateModel orderAddress);
    }

    public class OrderService : IOrderService
    {

        private readonly ICartService _cartService;

        public OrderService(ICartService cartService)
        {
            _cartService = cartService;
        }

        public async Task<IEnumerable<OrderViewModel>> GetAllOrders(Guid customerId)
        {
            List<OrderViewModel> ordersList = new List<OrderViewModel>();
            ProductService productService = new ProductService();
            IEnumerable<OrderViewModel> orders = new List<OrderViewModel>();
            IEnumerable<ProductViewModel> products = new List<ProductViewModel>();
            IEnumerable<OrderDetailViewModel> orderDetails = new List<OrderDetailViewModel>();
            products = await productService.GetAllProducts();
            using (var client = new HttpClient())
            {
                orders = await client.GetFromJsonAsync<IEnumerable<OrderViewModel>>($"https://localhost:7247/api/Order/?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
            }

            using (var client = new HttpClient())
            {

                orderDetails = await client.GetFromJsonAsync<IEnumerable<OrderDetailViewModel>>("https://localhost:7247/api/OrderDetail?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");

            }
            
                var customerOrders = orders.Where(x => x.CustomerId == customerId);

            foreach (var item in customerOrders)
            {
                List<OrderDetailProductViewModel> listOd = new List<OrderDetailProductViewModel>();
                var details = orderDetails.Where(x => x.OrderId == item.Id);
                OrderViewModel order = new OrderViewModel();

                order.CustomerId = item.Id;
                order.Created = item.Created;
                order.TotalPrice = item.TotalPrice;
                order.Address = item.Address;
                order.Id = item.Id;
                order.CustomerName = item.CustomerName;


                foreach (var d in details)
                {
                    OrderDetailProductViewModel orderDetail = new OrderDetailProductViewModel();
                    ProductViewModel product = new ProductViewModel();


                    orderDetail.OrderId = d.OrderId;
                    orderDetail.Price = d.Price;
                    orderDetail.Quantity = d.Quantity;
                    product.Id = d.Product.Id;
                    product.Name = d.Product.Name;
                    product.CategoryName = d.Product.CategoryName;
                    product.Price = d.Product.Price;

                    orderDetail.Product = product;
                    



                        listOd.Add(orderDetail);
                }







                order.OrderDetails = listOd;
                ordersList.Add(order);

            }

            return ordersList;
        }

        public async Task<OrderViewModel> GetOrder(Guid id)
        {

            OrderViewModel order = new OrderViewModel();

            using (var client = new HttpClient())
            {
                order = await client.GetFromJsonAsync<OrderViewModel>($"https://localhost:7247/api/order/{id}?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c");
            }

            return order;
        }

        public async Task PostOrder(OrderAddressCreateModel orderAddress)
        {


            Guid orderId = Guid.NewGuid();
            orderAddress.Order.OrderId = orderId;
            orderAddress.Address.OrderId = orderId;
            orderAddress.Order.TotalPrice = TotalPrice(orderAddress.CartItems);
      

            using (var client = new HttpClient())
            {
               await client.PostAsJsonAsync<OrderAddressCreateModel>("https://localhost:7247/api/order/?key=SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c", orderAddress);
            }

        }


        public decimal TotalPrice(IEnumerable<CartItemCreateModel> items)
        {
            decimal totalPrice = 0;

            foreach (var item in items)
            {

                totalPrice += item.Quantity * item.Product.Price;
            }


            return totalPrice;
        }

    }
}