using BorrowMe.Models;
using System.Collections.Generic;

namespace BorrowMe.Repositories
{
    public interface IItemTypeRepository
    {
        List<Accessory> GetAll();
    }
}