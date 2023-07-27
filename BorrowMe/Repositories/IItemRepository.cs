using System.Collections.Generic;
using BorrowMe.Models;


namespace BorrowMe.Repositories
{
    public interface IItemRepository
    {
        List<Item> GetSearchResults(string query);
        List<Item> GetAllItems();
        Item GetById(int id);
        
        void AddItem(Item item);
        void UpdateItem(Item item);
        void DeleteItem(int id);

    }
}
