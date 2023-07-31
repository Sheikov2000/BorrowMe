using System.Collections.Generic;
using System;
using BorrowMe.Models;

namespace BorrowMe.Repositories
{
    public interface IBorrowingRepository
    {
        
        List<Borrowing> GetByUserId(int id);
        Borrowing GetById(int id);
   
     
    }
}

