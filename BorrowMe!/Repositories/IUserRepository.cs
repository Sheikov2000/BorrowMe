using System.Collections.Generic;
using BorrowMe.Models;



namespace BorrowMe.Repositories
{
    public interface IUserRepository
    {
        User GetByFirebaseUserId(string firebaseUserId);
 
        User GetUserById(int id);
        void AddUser(User user);
         
        void UpdateUser(User user);

    }
}
