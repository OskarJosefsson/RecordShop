#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecordShopApi.Data;
using RecordShopApi.Filters;
using RecordShopClassLibrary.Models.Create;
using RecordShopClassLibrary.Models.Entities;
using RecordShopClassLibrary.Models.Read;

namespace RecordShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class OrderDetailController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrderDetailController(SqlContext context)
        {
            _context = context;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailViewModel>>> GetOrderDetails()
        {
            IEnumerable<OrderDetailEntity> entities = await _context.OrderDetails.Include(x => x.Product).ThenInclude(x => x.Category).ToListAsync();
            List<OrderDetailViewModel> result = new List<OrderDetailViewModel>();

            foreach (var item in entities)
            {
                OrderDetailViewModel orderDetail = new OrderDetailViewModel();
                ProductViewModel product = new ProductViewModel();
                orderDetail.ProductId = item.ProductId;
                orderDetail.Price = item.Price;
                orderDetail.Quantity = item.Quantity;
                orderDetail.OrderId = item.OrderId;
                product.Description = item.Product.Description;
                product.CategoryName = item.Product.Category.Name;
                product.Price = item.Product.Price;
                product.Name = item.Product.Name;

                orderDetail.Product = product;
                result.Add(orderDetail);
            }

            return result;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailViewModel>> GetOrderDetailEntity(int id)
        {
            var orderDetailEntity = await _context.OrderDetails.FindAsync(id);

            if (orderDetailEntity == null)
            {
                return NotFound();
            }

            OrderDetailViewModel orderDetail = new OrderDetailViewModel();

            orderDetail.ProductId = orderDetailEntity.ProductId;
            orderDetail.Price = orderDetailEntity.Price;
            orderDetail.Quantity = orderDetailEntity.Quantity;

            return orderDetail;
        }
        #endregion

        #region PostPut
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrderDetailEntity(int id, OrderDetailEntity orderDetailEntity)
        {
            if (id != orderDetailEntity.Id)
            {
                return BadRequest();
            }

            _context.Entry(orderDetailEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderDetailEntityExists(id))
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


        [HttpPost]
        public async Task PostOrderDetailEntity(OrderDetailCreateModel orderDetail)
        {
            OrderDetailEntity orderDetailEntity = new OrderDetailEntity(orderDetail.Quantity,orderDetail.ProductId,orderDetail.Price, orderDetail.OrderId);

            _context.OrderDetails.Add(orderDetailEntity);




            await _context.SaveChangesAsync();

        }

        #endregion

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderDetailEntity(int id)
        {
            var orderDetailEntity = await _context.OrderDetails.FindAsync(id);
            if (orderDetailEntity == null)
            {
                return NotFound();
            }

            _context.OrderDetails.Remove(orderDetailEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderDetailEntityExists(int id)
        {
            return _context.OrderDetails.Any(e => e.Id == id);
        }
    }
}
