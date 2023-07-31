using BorrowMe.Models;
using BorrowMe.Repositories;
using BorrowMe.Utils;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BorrowMe.Repositories
{
    public class AccessoryRepository : BaseRepository, IAccessoryRepository
    {
        public AccessoryRepository(IConfiguration configuration) : base(configuration) { }


        public Accessory GetById(int id)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT Id, ItemId, Name, Description
                          FROM Accessory";

                    var reader = cmd.ExecuteReader();

                    Accessory accessory = null;

                    if (reader.Read())
                    {
                        accessory = new Accessory()
                        {
                            Id = DbUtils.GetInt(reader, "Id"),
                            ItemId = DbUtils.GetInt(reader, "ItemId"),
                            Name = DbUtils.GetString(reader, "Name"),
                            Note = DbUtils.GetString(reader, "Note")
                        };
                    }

                    reader.Close();
                    return accessory;
                }
            }
        }

        public void Add(Accessory accessory)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        INSERT INTO Accessory (ItemId, Name, Note)
                        OUTPUT INSERTED.ID
                                  VALUES (@ItemId, @Name, @Note);";

                    DbUtils.AddParameter(cmd, "@ItemId", accessory.ItemId);
                    DbUtils.AddParameter(cmd, "@Name", accessory.Name);
                    DbUtils.AddParameter(cmd, "@Note", accessory.Note);

                    accessory.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        public void Update(Accessory accessory)
        {
            using (var conn = Connection)
            {
                conn.Open();

                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Accessory 
                                           SET Name = @Name,
                                               Note = @Note,
                                               ItemId = @ItemId
                                         WHERE Id = @Id;";

                    DbUtils.AddParameter(cmd, "@Name", accessory.Name);
                    DbUtils.AddParameter(cmd, "@Note", accessory.Note);
                    DbUtils.AddParameter(cmd, "@ItemId", accessory.ItemId);
                    DbUtils.AddParameter(cmd, "@Id", accessory.Id);

                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}