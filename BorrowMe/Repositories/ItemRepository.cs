using BorrowMe.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using BorrowMe.Models;
using BorrowMe.Repositories;


namespace BorrowMe.Repositories
{
    public class ItemRepository : BaseRepository, IItemRepository
    {
        public ItemRepository(IConfiguration configuration) : base(configuration) { }


        public void GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT i.Id, i.Name, i.Description, i.UserId, i.ItemType, i.ImageUrl,
                    
                    u.FirstName, u.LastName, u.email, u.Phone, u.Zipcode,
                    
                FROM Item i
                JOIN User u ON i.UserId = u.Id
                WHERE i.Id = @Id";
                }
            }
        }

        public void AddItem(Item item) 
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Item (UserId, Description, Name, ItemTypeId, ImageUrl)
                        OUTPUT INSERTED.ID
                                  VALUES (@UserId, @Description, @Name, @ItemTypeId, @ImageUrl);";
                    DbUtils.AddParameter(cmd, "@UserId", item.UserId);
                    DbUtils.AddParameter(cmd, "@Description", item.Description);
                    DbUtils.AddParameter(cmd, "@Name", item.Name);
                    DbUtils.AddParameter(cmd, "@ItemTypeId", item.ItemTypeId);
                    DbUtils.AddParameter(cmd, "@ImageUrl", item.ImageUrl);
                    DbUtils.AddParameter(cmd, "@Id", item.Id);

                    item.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateItem(Item item)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Item 
                                           SET UserId = @UserId,
                                               Name = @Name,
                                               Description = @Description,
                                               ItemTypeId = @ItemTypeId,
                                               ImageUrl = @ImageUrl,                                             
                                         WHERE Id = @Id;";

                    DbUtils.AddParameter(cmd, "@UserId", item.UserId);
                    DbUtils.AddParameter(cmd, "@Description", item.Description);
                    DbUtils.AddParameter(cmd, "@Name", item.Name);
                    DbUtils.AddParameter(cmd, "@ItemTypeId", item.ItemTypeId);
                    DbUtils.AddParameter(cmd, "@ImageUrl", item.ImageUrl);
                    DbUtils.AddParameter(cmd, "@Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteItem(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        UPDATE dbo.Item
                        SET IsDeleted = 1
                        WHERE Id = @Id
                    ";

                    DbUtils.AddParameter(cmd, "@Id", id);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
