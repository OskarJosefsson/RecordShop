using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecordShopApi.Data;
using RecordShopApi.Filters;
using RecordShopClassLibrary.Models.Create;
using RecordShopClassLibrary.Models.Entities;
using RecordShopClassLibrary.Models.Read;
using RecordShopClassLibrary.Models.Update;

namespace RecordShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class OrderController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrderController(SqlContext context)
        {
            _context = context;
        }



        #region Gets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderViewModel>>> GetOrderEntities()
        {
            
            var result = await _context.Orders.Include(x => x.OrderDetails).Include(x => x.Address).ToListAsync();

            
            List<OrderViewModel> orders = new List<OrderViewModel>();

            foreach (var item in result)
            {
                OrderViewModel order = new OrderViewModel();

                AddressViewModel address = new AddressViewModel();



                order.Id = item.Id;
                order.CustomerId = item.CustomerId;
                order.TotalPrice = item.TotalPrice;
                order.Created = item.Created;
                order.Id = item.Id;
                order.CustomerName = item.CustomerName;
                address.City = item.Address.City;
                address.StreetAdress = item.Address.StreetAdress;
                address.Postalcode = item.Address.Postalcode;

                order.Address = address;
                orders.Add(order);
            }


            return orders;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OrderEntity>> GetOrder(Guid orderId)
        {
            var result = await _context.Orders.Include(x => x.Address).ToListAsync();

            var customerOrders = result.Where(x => x.Id == orderId);

            return Ok(customerOrders);
        }

        #endregion
        #region PostPut
        [HttpPost]
        public async Task<IActionResult> PostOrderEntity(OrderAddressCreateModel model)
        {

            //OrderDetailController orderDetailController = new OrderDetailController(_context);

            if (!OrderEntityExists(model.Order.OrderId)) 
            {
                //AddressController addressController = new AddressController(_context);
                //int addressId = await addressController.PostAddress(model.Address);

                AddressEntity addressEntity = new AddressEntity(model.Address.StreetAddress, model.Address.PostalCode, model.Address.City);

                _context.Addresses.Add(addressEntity);
                await _context.SaveChangesAsync();

                OrderEntity orderEntity = new OrderEntity(model.Order.OrderId, model.Order.CustomerId, model.Order.CustomerName, DateTime.Now, model.Order.TotalPrice, addressEntity.Id);
                
                _context.Orders.Add(orderEntity);

                await _context.SaveChangesAsync();


                foreach (var item in model.CartItems)
                {
                    //OrderDetailCreateModel odcm = new OrderDetailCreateModel();

                    //odcm.OrderId = orderEntity.Id;
                    //odcm.ProductId = item.Product.Id;
                    //odcm.Quantity = item.Quantity;
                    //odcm.Price = item.Product.Price;

                    OrderDetailEntity orderDetailEntity = new OrderDetailEntity(item.Quantity, item.Product.Id, item.Product.Price, orderEntity.Id);

                    _context.OrderDetails.Add(orderDetailEntity);


                    

                }


                await _context.SaveChangesAsync();

                return NoContent();
            }

            return BadRequest();
          
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(Guid id, OrderEditEntity orderEditModel)
        {



            var result = await _context.Orders.FindAsync(id);
            result.TotalPrice = orderEditModel.TotalPrice;
            if (id != result.Id)
            {
                return BadRequest();
            }

            _context.Entry(result).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        #endregion
        // DELETE: api/Order/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(Guid id)
        {
            var orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(orderEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderEntityExists(Guid id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }

    }
}
