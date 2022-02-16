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
    //[ApiKeyAuth]
    public class CategoryController : ControllerBase
    {
        private readonly SqlContext _context;

        public CategoryController(SqlContext context)
        {
            _context = context;
        }

        #region Get
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryViewModel>>> GetCategories()
        {
            List<CategoryViewModel> categories = new List<CategoryViewModel>();

            foreach (var item in await _context.Categories.ToListAsync())
            {
                categories.Add(new CategoryViewModel(item.Id, item.Name));
            }
            return categories;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryViewModel>> GetCategoryEntity(int id)
        {
            var categoryEntity = await _context.Categories.FindAsync(id);

            CategoryViewModel model = new CategoryViewModel(categoryEntity.Id, categoryEntity.Name);


            if (categoryEntity == null)
            {
                return NotFound();
            }

            return model;
        }
        #endregion
        #region Post
  
        [HttpPost]
        public async Task<ActionResult<CategoryEntity>> PostCategoryEntity(CategoryCreateModel model)
        {

            CategoryEntity categoryEntity = new CategoryEntity(model.Name);

            _context.Categories.Add(categoryEntity);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategoryEntity", new { id = categoryEntity.Id }, categoryEntity);
        }

        [HttpPut]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id,  CategoryEntity category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryEntityExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryEntity(int id)
        {
            var categoryEntity = await _context.Categories.FindAsync(id);
            if (categoryEntity == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categoryEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryEntityExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }

    }
}
