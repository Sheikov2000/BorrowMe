using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using BorrowMe.Models;
using Microsoft.Data.SqlClient;
using BorrowMe.Utils;

namespace BorrowMe.Repositories
{
    public class ItemRepository : BaseRepository, IItemRepository
    {
        public ItemRepository(IConfiguration configuration) : base(configuration) { }

        public List<Item> GetAllItems()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT * FROM Item i
                                        JOIN Category c ON i.CategoryId = c.id
                                        JOIN [User] u ON i.UserId = u.Id";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var Items = new List<Item>();
                        while (reader.Read())
                        {
                            Items.Add(new Item()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                UserId = DbUtils.GetInt(reader, "UserId"),
                                Title = DbUtils.GetString(reader, "Title"),
                                Description = DbUtils.GetString(reader, "Description"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                                Category = new Category()
                                {
                                    Id = DbUtils.GetInt(reader, "CategoryId"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                },
                                User = new User()
                                {
                                    Id = DbUtils.GetInt(reader, "UserId"),
                                    FirebaseId = DbUtils.GetString(reader, "FirebaseId"),
                                    FirstName = DbUtils.GetString(reader, "FirstName"),
                                    LastName = DbUtils.GetString(reader, "LastName"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    Phone = DbUtils.GetString(reader, "Phone"),
                                    ZipCode = DbUtils.GetInt(reader, "ZipCode")
                                }


                            });
                        }
                        return Items;
                    }
                }
            }
        }


        public Item GetItemById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @" 
                                        SELECT * FROM Item i
                                        JOIN Category c ON i.CategoryId = c.id
                                        JOIN [User] u ON i.UserId = u.Id
                                        WHERE i.Id = @id";
                    DbUtils.AddParameter(cmd, "@id", id);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Item item = null;
                        if (reader.Read())
                        {
                            item = new Item()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                UserId = DbUtils.GetInt(reader, "UserId"),
                                Title = DbUtils.GetString(reader, "Title"),
                                Description = DbUtils.GetString(reader, "Description"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                                Category = new Category()
                                {
                                    Id = DbUtils.GetInt(reader, "CategoryId"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                },
                                User = new User()
                                {
                                    Id = DbUtils.GetInt(reader, "UserId"),
                                    FirebaseId = DbUtils.GetString(reader, "FirebaseId"),
                                    FirstName = DbUtils.GetString(reader, "FirstName"),
                                    LastName = DbUtils.GetString(reader, "LastName"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    Phone = DbUtils.GetString(reader, "Phone"),
                                    ZipCode = DbUtils.GetInt(reader, "ZipCode")
                                }
                            };
                        }
                        return item;
                    }
                }
            }
        }

        public List<Item> GetItemsByUserId(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        SELECT * FROM Item i
                                        JOIN Category c ON i.CategoryId = c.id
                                        JOIN [User] u ON i.UserId = u.Id
                                        WHERE i.UserId = @id";

                    DbUtils.AddParameter(cmd, "@id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var Items = new List<Item>();
                        while (reader.Read())
                        {
                            Items.Add(new Item()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                UserId = DbUtils.GetInt(reader, "UserId"),
                                Title = DbUtils.GetString(reader, "Title"),
                                Description = DbUtils.GetString(reader, "Description"),
                                ImageUrl = DbUtils.GetString(reader, "ImageUrl"),
                                CategoryId = DbUtils.GetInt(reader, "CategoryId"),
                                Category = new Category()
                                {
                                    Id = DbUtils.GetInt(reader, "CategoryId"),
                                    Name = DbUtils.GetString(reader, "Name"),
                                },
                                User = new User()
                                {
                                    Id = DbUtils.GetInt(reader, "UserId"),
                                    FirebaseId = DbUtils.GetString(reader, "FirebaseId"),
                                    FirstName = DbUtils.GetString(reader, "FirstName"),
                                    LastName = DbUtils.GetString(reader, "LastName"),
                                    Email = DbUtils.GetString(reader, "Email"),
                                    Phone = DbUtils.GetString(reader, "Phone"),
                                    ZipCode = DbUtils.GetInt(reader, "ZipCode")
                                }


                            });
                        }
                        return Items;
                    }
                }
            }
        }

        public void AddItem(Item item)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                        INSERT INTO Item (UserId, Title, Description, ImageUrl, CategoryId)
                                        OUTPUT INSERTED.Id
                                        VALUES (@UserId, @Title, @Description, @ImageUrl, @CategoryId)
                                       ";
                    DbUtils.AddParameter(cmd, "@UserId", item.UserId);
                    DbUtils.AddParameter(cmd, "@Title", item.Title);
                    DbUtils.AddParameter(cmd, "@Description", item.Description);
                    DbUtils.AddParameter(cmd, "@ImageUrl", item.ImageUrl);
                    DbUtils.AddParameter(cmd, "@CategoryId", item.CategoryId);

                    item.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void UpdateItem(Item item)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                                    UPDATE Item
                                    SET UserId = @UserId, Title = @Title, Description = @Description,
                                    ImageUrl = @ImageUrl, CategoryId = @CategoryId
                                    WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@UserId", item.UserId);
                    DbUtils.AddParameter(cmd, "@Title", item.Title);
                    DbUtils.AddParameter(cmd, "@Description", item.Description);
                    DbUtils.AddParameter(cmd, "@ImageUrl", item.ImageUrl);
                    DbUtils.AddParameter(cmd, "@CategoryId", item.CategoryId);
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
                    cmd.CommandText = "DELETE FROM Item WHERE Id = @Id";
                    DbUtils.AddParameter(cmd, "@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}












