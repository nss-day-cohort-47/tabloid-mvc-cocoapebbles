using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly ITagRepository _tagRepository;

        public PostController(IPostRepository postRepository, ICategoryRepository categoryRepository, ITagRepository tagRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
        }

        public IActionResult Index()
        {
            int userId = GetCurrentUserProfileId();
            var posts = _postRepository.GetAllPublishedPosts();
            foreach(Post post in posts)
            {
                if (post.UserProfileId==userId)
                {
                    post.isOwner = true;
                }
                else
                {
                    post.isOwner = false;
                }
            }
               
            return View(posts);
        }
        public IActionResult MyPosts()
        {
            int userId = GetCurrentUserProfileId();
            var posts = _postRepository.GetUserPosts(userId);
            return View(posts);
        }

        public IActionResult Details(int id)
        {
            var post = _postRepository.GetPublishedPostById(id);
            if (post == null)
            {
                int userId = GetCurrentUserProfileId();
                post = _postRepository.GetUserPostById(id, userId);
                if (post == null)
                {
                    return NotFound();
                }
            }
            return View(post);
        }
        public IActionResult ManageTags(int id)
        {
            PostManageTagsViewModel vm = _postRepository.GetUserPostByIdAndTags(id);
            vm.PostTags = _tagRepository.GetAllByPost(id);
            return View(vm);
        }

        public IActionResult AddPostTag(int postId)
        {
            List<Tag> tags = _tagRepository.GetAll();
            PostManageTagsViewModel vm = new();
            vm.PostId = postId;
            vm.PostTags = tags;
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddPostTag(PostManageTagsViewModel vm)
        {
            try
            {
                _tagRepository.AddPostTag(vm.TagId, vm.PostId);
                return RedirectToAction("ManageTags", new { id = vm.PostId });
            }
            catch
            {
                return RedirectToAction("ManageTags", new { id = vm.PostId });
            }
        }
        public IActionResult DeletePostTag(int Id, int PostId)
        {
            _tagRepository.DeletePostTag(Id, PostId);
            return RedirectToAction("ManageTags", new { id = PostId });
        }
        public IActionResult Create()
        {
            var vm = new PostCreateViewModel();
            vm.CategoryOptions = _categoryRepository.GetAll();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel vm)
        {
            try
            {
                vm.Post.CreateDateTime = DateAndTime.Now;
                vm.Post.IsApproved = true;
                vm.Post.UserProfileId = GetCurrentUserProfileId();

                _postRepository.Add(vm.Post);

                return RedirectToAction("Details", new { id = vm.Post.Id });
            } 
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAll();
                return View(vm);
            }
        }

        // GET
        public IActionResult Delete(int id)
        {
               int userId = GetCurrentUserProfileId();
               Post post = _postRepository.GetPublishedPostById(id);
            if (post == null)
            {
                return NotFound();
            }
            else if (post.UserProfileId == userId || User.IsInRole("Admin"))
            {
                return View(post);
            }
            else return Unauthorized();


        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Post post)
        {
                try
                {
                    _postRepository.Delete(id);
                    return RedirectToAction("MyPosts");
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Details", new { id = post.Id });
                }
        }

        public ActionResult Edit(int id)
        {
            Post post = _postRepository.GetPublishedPostById(id);
            List<Category> categories = _categoryRepository.GetAll();

            PostCreateViewModel pcvm = new PostCreateViewModel()
            {
                Post = post,
                CategoryOptions = categories
            };
            int UserID = GetCurrentUserProfileId();
            if (User.IsInRole("Admin") | post.UserProfileId == UserID)
            {
                return View(pcvm);
            }
            else return Unauthorized();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Post post)
        {
            int UserID = GetCurrentUserProfileId();
            if (User.IsInRole("Admin") | post.UserProfileId == UserID)
            {
                try
                {
                    _postRepository.UpdatePost(post);

                    return RedirectToAction("Details", new { id = id });
                }
                catch (Exception ex)
                {
                    return RedirectToAction("Details", new { id = id });
                }
            }
            else return Unauthorized();
        }
        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
