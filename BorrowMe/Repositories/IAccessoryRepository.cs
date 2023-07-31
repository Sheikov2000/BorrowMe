using System.Collections.Generic;
using BorrowMe.Models;


namespace BorrowMe.Repositories
{
    public interface IAccessoryRepository
    {
        Accessory GetById(int id);
        void Add(Accessory accessory);
        void Update(Accessory accessory);

    }
}