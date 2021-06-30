﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class ReactionController : Controller
    {
        private readonly IReactionRepository _reactionRepository;

        public ReactionController(IReactionRepository reactionRepository)
        {
            _reactionRepository = reactionRepository;
        }

        // GET: ReactionController
        public ActionResult Index()
        {
            List<Reaction> reactions = _reactionRepository.GetAllReactions();

            if (User.IsInRole("Admin"))
            {
                return View(reactions);
            }
            else
            {
                return Unauthorized();
            }
        }

        // GET: ReactionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReactionController/Create
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

        // POST: ReactionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Reaction reaction)
        {
            try
            {
                _reactionRepository.AddReaction(reaction);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ReactionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReactionController/Edit/5
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

        // GET: ReactionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReactionController/Delete/5
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
    }
}