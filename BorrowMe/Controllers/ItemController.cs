using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BorrowMe.Models;
using BorrowMe.Repositories;



namespace BorrowMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAccessoryRepository _accessoryRepository;

        public ItemController(IItemRepository itemRepository,
                              IUserRepository userRepository,
                              IAccessoryRepository accessoryRepository)
        {
            _itemRepository = itemRepository;
            _userRepository = userRepository;
            _accessoryRepository = accessoryRepository;
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = _itemRepository.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            {
                return Ok(item);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Item item)
        {
            // Id 1 is the unmodifiable 'dummy' gear to keep old reservations in the database
            if (id != item.Id || id == 1)
            {
                return BadRequest();
            }

            _itemRepository.Update(item);
            foreach (Accessory accessory in item.Accessories)
            {
                if (accessory.ItemId != item.Id)
                {
                    return BadRequest();
                }
                _accessoryRepository.Update(accessory);
            }

            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // Id 1 is the permanent 'dummy' gear to keep old reservations in the database
            if (id == 1)
            {
                return BadRequest();
            }
            

            _itemRepository.Delete(id);

            return NoContent();
        }
    }
}


