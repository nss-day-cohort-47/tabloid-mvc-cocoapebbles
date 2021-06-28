using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class CommentController : Controller
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;
        private readonly IUserProfileRepository _userProfileRepository;

        public CommentController(ICommentRepository commentRepository, IPostRepository postRepository, IUserProfileRepository userProfileRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
            _userProfileRepository = userProfileRepository;
        }


        // GET: CommentController

        public ActionResult Index(int id)
        {
            List<Comment> comments = _commentRepository.GetAllCommentsByPostId(id);
            Post post = _postRepository.GetPublishedPostById(id);


            CommentViewModel vm = new CommentViewModel()
            {
                comments = comments,
                post = post,


            };

            return View("Index", vm);

        }

        // GET: CommentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CommentController/Create
        public ActionResult Create(int id)
        {
            var vm = new CommentViewModel();
            vm.post = _postRepository.GetPublishedPostById(id);
            return View(vm);
        }

        // POST: CommentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CommentViewModel vm, int id)
        {
            try
            {
                vm.post = _postRepository.GetPublishedPostById(id);
                vm.comment.UserProfileId = GetCurrentUserProfileId();
                vm.comment.CreateDateTime = DateAndTime.Now;
                _commentRepository.AddComment(vm);
                return RedirectToAction("Details", "Post", new { id = id });
            }
            catch (Exception EX)
            {
                return View(vm);
            }
        }

        // GET: CommentController/Edit/5
        public ActionResult Edit(int id)
        {
            Comment comment = _commentRepository.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }


            return View(comment);
        }

        // POST: CommentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comment comment)
        {
            try
            {
                _commentRepository.EditComment(comment);

                return RedirectToAction("Index", "Comment", new { id = comment.PostId });
            }
            catch
            {
                return View(comment);
            }
        }

        // GET: CommentController/Delete/5
        public ActionResult Delete(int id)
        {
            Comment comment = _commentRepository.GetCommentById(id);

            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: CommentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Comment comment, int id)
        {
            try
            {
                var thisComment = _commentRepository.GetCommentById(id);
                _commentRepository.DeleteComment(comment);

                return RedirectToAction("Index", "Comment", new { id = thisComment.PostId });
            }
            catch (Exception)
            {
                return View(comment);
            }
        }
        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }


    }

}