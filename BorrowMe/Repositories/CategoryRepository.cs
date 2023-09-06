using System.Collections.Generic;
using System.Runtime.InteropServices;
using BorrowMe.Models;
using BorrowMe.Utils;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace BorrowMe.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {
        public CategoryRepository(IConfiguration configuration) : base(configuration) { }

        public List<Category> GetAllCategories()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT * FROM Category";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        var categories = new List<Category>();
                        while (reader.Read())
                        {
                            categories.Add(new Category()
                            {
                                Id = DbUtils.GetInt(reader, "Id"),
                                Name = DbUtils.GetString(reader, "Name")

                            });
                        }
                        return categories;
                    }
                }
            }
        }
    }
}
