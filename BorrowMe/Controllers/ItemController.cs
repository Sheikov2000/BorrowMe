using BorrowMe.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;
using BorrowMe.Models;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace BorrowMe.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemRepository _itemRepository;

        public ItemController(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpGet]
        public IActionResult GetAllItems()
        {
            return Ok(_itemRepository.GetAllItems());
        }

        [HttpGet("{id}")]
        public IActionResult GetItemById(int id)
        {
            Item item = _itemRepository.GetItemById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpGet("User/{id}")]
        public IActionResult GetItemByUserId(int id)
        {
            List<Item> items = _itemRepository.GetItemsByUserId(id);
            if (items == null)
            {
                return NotFound();
            }
            return Ok(items);
        }

        [HttpPost]
        public IActionResult AddItem(Item item)
        {
            _itemRepository.AddItem(item);
            return CreatedAtAction("GetItemById", new { id = item.Id }, item);
        }

        [HttpPut]
        public IActionResult UpdateItem(Item item)
        {
            if (item == null)
            {
                return BadRequest();
            }
            _itemRepository.UpdateItem(item);
            return CreatedAtAction("GetItemById", new { id = item.Id }, item);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            _itemRepository.DeleteItem(id);
            return Ok();
        }
    }
}

    



    


