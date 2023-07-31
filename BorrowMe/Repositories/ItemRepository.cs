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
                                   a.Id AS AccessoryId, a.Name AS AccessoryName, a.Note AS AccessoryNote
                              FROM Item i
                         LEFT JOIN User u ON u.Id = i.UserId
                         LEFT JOIN Accessory a ON a.ItemId = i.Id
                             WHERE i.Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);

                    var reader = cmd.ExecuteReader();

                    Item item = null;

                    while (reader.Read())
                    {
                        var gearId = DbUtils.GetInt(reader, "GearId");

                        if (item == null)
                        {
                            item = ItemFromDb(reader);
                            item.Accessory = new List<Accessory>();
                        }

                        if (DbUtils.IsNotDbNull(reader, "AccessoryId"))
                        {
                            item.Accessories.Add(new Accessory()
                            {
                                Id = DbUtils.GetInt(reader, "AccessoryId"),
                                Name = DbUtils.GetString(reader, "AccessoryName"),
                                Description = DbUtils.GetString(reader, "AccessoryDescription"),
                                GearId = gear.Id
                            });
                        }
                    }

       

                    reader.Close();
                    return item;
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
