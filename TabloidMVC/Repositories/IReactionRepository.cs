using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface IReactionRepository
    {
        void AddReaction(Reaction reaction);
        List<Reaction> GetAllReactions();
        List<Reaction> GetReactionsByPostId(int id);
    }
}
