using System.Collections.Generic;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;

namespace TabloidMVC.Repositories
{
    public interface IPostRepository
    {
        void Add(Post post);
        void Delete(int postID);
        void UpdatePost(Post post);
        List<Post> GetAllPublishedPosts();
        List<Post> GetUserPosts(int userProfileId);
        Post GetPublishedPostById(int id);
        Post GetUserPostById(int id, int userProfileId);
        PostManageTagsViewModel GetUserPostByIdAndTags(int id);
    }
}