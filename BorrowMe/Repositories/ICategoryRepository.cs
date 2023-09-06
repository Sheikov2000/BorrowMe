using System.Collections.Generic;
using BorrowMe.Models;

namespace BorrowMe.Repositories
{
    public interface ICategoryRepository
    {
        List<Category> GetAllCategories();
    }
}