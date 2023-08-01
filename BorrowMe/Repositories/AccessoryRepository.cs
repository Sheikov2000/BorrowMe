﻿using BorrowMe.Models;
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
                        SELECT Id, ItemId, Name, Details
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
                            Details = DbUtils.GetString(reader, "Details")
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
                        INSERT INTO Accessory (ItemId, Name, Details)
                        OUTPUT INSERTED.ID
                                  VALUES (@ItemId, @Name, @Details);";

                    DbUtils.AddParameter(cmd, "@ItemId", accessory.ItemId);
                    DbUtils.AddParameter(cmd, "@Name", accessory.Name);
                    DbUtils.AddParameter(cmd, "@Details", accessory.Details);

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
                                               Details = @Details,
                                               ItemId = @ItemId
                                         WHERE Id = @Id;";

                    DbUtils.AddParameter(cmd, "@Name", accessory.Name);
                    DbUtils.AddParameter(cmd, "@Details", accessory.Details);
                    DbUtils.AddParameter(cmd, "@ItemId", accessory.ItemId);
                    DbUtils.AddParameter(cmd, "@Id", accessory.Id);

                    cmd.ExecuteNonQuery();
                }
            }

        }
    }
}