using BorrowMe.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using BorrowMe.Models;
using BorrowMe.Repositories;
namespace BorrowMe.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IConfiguration configuration) : base(configuration) { }

        public User GetByFirebaseUserId(string firebaseId)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, FirebaseId, FirstName, LastName, Email, Phone, ZipCode   
                        FROM [User]
                        WHERE FirebaseId = @FirebaseId";

                    DbUtils.AddParameter(cmd, "@FirebaseId", firebaseId);

                    User user = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user = new User()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirebaseId = DbUtils.GetString(reader, "FirebaseId"),
                            FirstName = DbUtils.GetString(reader, "FirstName"),
                            LastName = DbUtils.GetString(reader, "LastName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            Phone = DbUtils.GetString(reader, "Phone"),
                            ZipCode = DbUtils.GetInt(reader, "ZipCode")
                        };
                    }
                    reader.Close();

                    return user;
                }
            }
        }

        public User GetUserById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {

                    cmd.CommandText = @"
                        SELECT Id, FirebaseId, FirstName, LastName, Email, Phone, ZipCode         
                        FROM [User]
                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@Id", id);


                    User user = null;

                    var reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user = new User()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            FirebaseId = DbUtils.GetString(reader, "FirebaseId"),
                            FirstName = DbUtils.GetString(reader, "FirstName"),
                            LastName = DbUtils.GetString(reader, "LastName"),
                            Email = DbUtils.GetString(reader, "Email"),
                            Phone = DbUtils.GetString(reader, "Phone"),
                            ZipCode = DbUtils.GetInt(reader, "ZipCode")
                        };
                    }
                    reader.Close();

                    return user;
                }
            }

        }

        public void AddUser(User user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"INSERT INTO User (FirebaseId, FirstName, LastName, Email, Phone, ZipCode)
                                        OUTPUT INSERTED.ID
                                        VALUES (@FirebaseUserId, @FirstName, @LastName, 
                                                @Email, @Phone, @ZipCode)";
                    DbUtils.AddParameter(cmd, "@FirebaseId", user.FirebaseId);
                    DbUtils.AddParameter(cmd, "@FirstName", user.FirstName);
                    DbUtils.AddParameter(cmd, "@LastName", user.LastName);
                    DbUtils.AddParameter(cmd, "@Email", user.Email);
                    DbUtils.AddParameter(cmd, "@Phone", user.Phone);
                    DbUtils.AddParameter(cmd, "@Zipcode", user.ZipCode);

                    user.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateUser(User user)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE User
                                        SET 
                                            FirstName = @firstName,
                                            LastName = @lastName,
                                            Email = @email,
                                            Phone = @phone,
                                            ZipCode = @zipCode,                               
                                        WHERE Id = @id";

                    DbUtils.AddParameter(cmd, "@FirstName", user.FirstName);
                    DbUtils.AddParameter(cmd, "@LastName", user.LastName);
                    DbUtils.AddParameter(cmd, "@Email", user.Email);
                    DbUtils.AddParameter(cmd, "@Phone", user.Phone);
                    DbUtils.AddParameter(cmd, "@Zipcode", user.ZipCode);


                    cmd.ExecuteNonQuery();
                }
            }
        }

        /*
        public UserProfile GetByFirebaseUserId(string firebaseUserId)
        {
            return _context.UserProfile
                       .Include(up => up.UserType) 
                       .FirstOrDefault(up => up.FirebaseUserId == firebaseUserId);
        }

        public void Add(UserProfile userProfile)
        {
            _context.Add(userProfile);
            _context.SaveChanges();
        }
        */
    }
}