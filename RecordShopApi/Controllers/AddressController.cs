using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using RecordShopApi.Data;
using RecordShopClassLibrary.Models.Read;
using RecordShopClassLibrary.Models.Entities;
using RecordShopClassLibrary.Models.Create;
using Microsoft.AspNetCore.Authorization;
using RecordShopApi.Filters;

namespace RecordShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiKeyAuth]
    public class AddressController : ControllerBase
    {
        private readonly SqlContext _context;
       

        public AddressController(SqlContext context)
        {
            _context = context;
        }

        #region Get

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AddressViewModel>>> GetAddresses()
        {
            List<AddressViewModel> addresses = new List<AddressViewModel>();

            foreach (var item in await _context.Addresses.ToListAsync())
            {
                addresses.Add(new AddressViewModel(item.StreetAdress, item.Postalcode, item.City));
            }

            return addresses;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AddressEntity>> GetAddressEntity(int id)
        {
            var addressEntity = await _context.Addresses.FindAsync(id);


            if (addressEntity == null)
            {
                return NotFound();
            }

            return addressEntity;
        }
        #endregion
        #region Post
        [HttpPost]
        public async Task<int> PostAddress(AddressCreateModel model)
        {

            AddressEntity addressEntity = new AddressEntity(model.StreetAddress, model.PostalCode, model.City);

            _context.Addresses.Add(addressEntity);
            await _context.SaveChangesAsync();

            return addressEntity.Id;
        }

        #endregion

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAddressEntity(int id)
        {
            var addressEntity = await _context.Addresses.FindAsync(id);
            if (addressEntity == null)
            {
                return NotFound();
            }

            _context.Addresses.Remove(addressEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AddressEntityExists(int id)
        {
            return _context.Addresses.Any(e => e.Id == id);
        }
    }
}
