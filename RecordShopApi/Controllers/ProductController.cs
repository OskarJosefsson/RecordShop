using Microsoft.AspNetCore.Authorization;
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
    
    public class ProductController : ControllerBase
    {
        private readonly SqlContext _context;
        private readonly IConfiguration _configuration;
        
        public ProductController(SqlContext context, IConfiguration configuration) 
        {
            _context = context;
            _configuration = configuration;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductViewModel>>> GetProducts()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();

           
          
                foreach (var item in await _context.Products.Include(x => x.Category).ToListAsync())
                {
                    CategoryViewModel category = new CategoryViewModel(item.Id, item.Category.Name);
                    products.Add(new ProductViewModel(item.Id, item.Name, item.Description, item.Price, item.Category.Name, item.CategoryID));


                 
                }


            return products;

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> GetProductEntity(int id)
        {
            var item = await _context.Products.Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);

            if (item == null)
            {
                return NotFound();
            }
            CategoryViewModel category = new CategoryViewModel(item.Id, item.Category.Name);
            ProductViewModel product = new ProductViewModel(item.Id, item.Name, item.Description, item.Price, item.Category.Name,item.CategoryID,item.Category.Id);
            return product;
        }
        #endregion
        #region Post
        [ApiKeyAuth]
        [HttpPost]
        public async Task<ActionResult<ProductEntity>> PostProductEntity(ProductCreateModel productModel)
        {

            ProductEntity productEntity = new ProductEntity(productModel.Name, productModel.Description, productModel.Price, productModel.CategoryID);

            _context.Products.Add(productEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [ApiKeyAuth]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductEntity(int id, ProductEditModel model)
        {

            var productEntity = await _context.Products.FindAsync(id);
            productEntity.Name = model.Name;
            productEntity.Description = model.Description;
            productEntity.Price = model.Price;
            productEntity.CategoryID = model.CategoryID;

            _context.Entry(productEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductEntityExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();

        }

        #endregion

        [ApiKeyAuth]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductEntity(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null)
            {
                return NotFound();
            }

            _context.Products.Remove(productEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool ProductEntityExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

    }
}
