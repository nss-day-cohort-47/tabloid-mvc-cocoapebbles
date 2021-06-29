﻿using Microsoft.AspNetCore.Authorization;
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
            int userId = GetCurrentUserId();
            UserProfile user = _userRepo.GetUserById(userId);

            if (user.UserTypeId == 1)
            {
                return View(tags);
            }
            else
            {
                return Unauthorized();
            }
           
        }

        //// GET: TagController/Details/5
        //[Authorize]
        //public ActionResult Details(int id)
        //{
        //    int userId = GetCurrentUserId();
        //    UserProfile user = _userRepo.GetUserById(userId);

        //    Tag tag = _tagRepo.GetTagById(id);

        //    if (user.UserTypeId == 1)
        //    {
        //        return View();
        //    }
        //    else
        //    {
        //        return Unauthorized();
        //    }
        //}

        // GET: TagController/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: TagController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TagController/Edit/5
        [Authorize]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TagController/Edit/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TagController/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TagController/Delete/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }
    }
}
