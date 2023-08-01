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


        public Item GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                    SELECT i.Id AS ItemId, i.UserId, i.Name, i.Description, i.ImageURL AS ItemImageLocation,
                        u.FirstName, u.LastName, u.Email, u.Phone, u.ZipCode, 
                        a.Id AS AccessoryId, a.Name AS AccessoryName, a.Details AS AccessoryDetails
                    FROM Item i
                    LEFT JOIN User u ON u.Id = i.UserId
                    LEFT JOIN Accessory a ON a.ItemId = i.Id
                             WHERE i.Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);

                    var reader = cmd.ExecuteReader();

                    Item item = null;

                    while (reader.Read())
                    {
                        item = new Item()
                        {
                            Id = DbUtils.GetInt(reader, "ItemId"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Description = DbUtils.GetString(reader, "Description"),
                            UserId = DbUtils.GetInt(reader, "UserId"),


                        };
                    }
                return item;
                reader.Close();
                }
            }
        }
 
        public void Add(Item item)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Item (UserId, Description, Name, ImageUrl)
                        OUTPUT INSERTED.ID
                                  VALUES (@UserId, @Description, @Name, @ImageUrl);";
                    DbUtils.AddParameter(cmd, "@UserId", item.UserId);
                    DbUtils.AddParameter(cmd, "@Description", item.Description);
                    DbUtils.AddParameter(cmd, "@Name", item.Name);
                    DbUtils.AddParameter(cmd, "@ImageUrl", item.ImageUrl);
                    DbUtils.AddParameter(cmd, "@Id", item.Id);

                    item.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Item item)
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
                                               ImageUrl = @ImageUrl,                                             
                                         WHERE Id = @Id;";

                    DbUtils.AddParameter(cmd, "@UserId", item.UserId);
                    DbUtils.AddParameter(cmd, "@Description", item.Description);
                    DbUtils.AddParameter(cmd, "@Name", item.Name);
                    DbUtils.AddParameter(cmd, "@ImageUrl", item.ImageUrl);
                    DbUtils.AddParameter(cmd, "@Id", item.Id);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
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
