using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public class ReactionRepository : BaseRepository, IReactionRepository
    {
        public ReactionRepository(IConfiguration config) : base(config) { }

        //Add a new reaction
        public void AddReaction(Reaction reaction)
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"Insert Into Reaction(Name, ImageLocation) 
                                        OUTPUT INSERTED.ID 
                                        Values(@name, @imageLocation)";
                    cmd.Parameters.AddWithValue("@name", reaction.Name);
                    cmd.Parameters.AddWithValue("@imageLocation", reaction.ImageLocation);

                    reaction.Id = (int)cmd.ExecuteScalar();
                }
            }
        }

        //Get all the reactions
        public List<Reaction> GetAllReactions()
        {
            using (var conn = Connection)
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name, ImageLocation FROM Reaction ORDER BY Name ASC";
                    var reader = cmd.ExecuteReader();

                    List<Reaction> reactions = new List<Reaction>();

                    while (reader.Read())
                    {
                        reactions.Add(new Reaction()
                        {
                            Id = reader.GetInt32(reader.GetOrdinal("Id")),
                            Name = reader.GetString(reader.GetOrdinal("Name")),
                            ImageLocation = reader.GetString(reader.GetOrdinal("ImageLocation")),
                        });
                    }
                    reader.Close();
                    return reactions;
                }
            }
        }

        public List<Reaction> GetReactionsByPostId(int id)
        {
            throw new NotImplementedException();
        }
    }

}
