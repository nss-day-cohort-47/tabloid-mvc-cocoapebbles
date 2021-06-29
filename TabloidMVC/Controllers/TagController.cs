using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;
using System.Security.Claims;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepo;
        private readonly IUserProfileRepository _userRepo;

        public TagController(ITagRepository tagRepository, IUserProfileRepository userRepo)
        {
            _tagRepo = tagRepository;
            _userRepo = userRepo;
        }

        // GET: TagController
        [Authorize]
        public ActionResult Index()
        {
            List<Tag> tags = _tagRepo.GetAll();

            if (User.IsInRole("Admin"))
            {
                return View(tags);
            }
            else
            {
                return Unauthorized();
            }
           
        }


        // GET: TagController/Create
        [Authorize]
        public ActionResult Create()
        {

            if (User.IsInRole("Admin"))
            {
                return View();
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST: TagController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tag tag)
        {

            if (User.IsInRole("Admin"))
            {
                try
                {
                    _tagRepo.AddTag(tag);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    return View(tag);
                }
            }
            else
            {
                return Unauthorized();
            }
           
        }

        // GET: TagController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            Tag tag = _tagRepo.GetTagById(id);
            if (User.IsInRole("Admin"))
            {
                return View(tag);
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST: TagController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Tag tag)
        {
            if (User.IsInRole("Admin"))
            {
                try
                {
                    _tagRepo.UpdateTag(tag);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(tag);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        // GET: TagController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            Tag tag = _tagRepo.GetTagById(id);

            if (User.IsInRole("Admin"))
            {
                return View(tag);
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST: TagController/Delete/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Tag tag)
        {
            if (User.IsInRole("Admin"))
            {
                try
                {
                    _tagRepo.Delete(id);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(tag);
                }
            }
            else
            {
                return Unauthorized();
            }
          
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
