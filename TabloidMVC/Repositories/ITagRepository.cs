using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;

namespace TabloidMVC.Repositories
{
    public interface ITagRepository
    {
        List<Tag> GetAll();
        List<Tag> GetTagsByPostId(int id);
        Tag GetTagByPostId(int id);

        void AddTag(Tag tag);
        //void UpdateTag(Tag tag);
        void Delete(int tagId);
    }
}
