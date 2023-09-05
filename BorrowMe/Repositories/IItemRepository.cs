using System.Collections.Generic;
using BorrowMe.Models;

namespace BorrowMe.Repositories
{
    public interface IItemRepository
    {
        void AddItem(Item item);
        void DeleteItem(int id);
        List<Item> GetAllItems();
        Item GetItemById(int id);
        List<Item> GetItemsByUserId(int id);
        void UpdateItem(Item item);
    }
}