using System.Collections.Generic;
using BorrowMe.Models;


namespace BorrowMe.Repositories
{
    public interface IAccessoryRepository
    {
     
        Item GetById(int id);

        void Add(Item item);
        void Update(Item item);
        void Delete(int id);

    }
}