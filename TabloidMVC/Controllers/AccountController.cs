﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserTypeRepository _userTypeRepository;

        public AccountController(IUserProfileRepository userProfileRepository,
                                    IUserTypeRepository userTypeRepository)
        {
            _userProfileRepository = userProfileRepository;
            _userTypeRepository = userTypeRepository;
        }

        // GET: Accounts
        public ActionResult Index()
        {
            List<UserProfile> users = _userProfileRepository.GetAllUsers();
            return View(users);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Credentials credentials)
        {
            var userProfile = _userProfileRepository.GetByEmail(credentials.Email);

            if (userProfile == null)
            {
                ModelState.AddModelError("Email", "Invalid email");
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userProfile.Id.ToString()),
                new Claim(ClaimTypes.Email, userProfile.Email),
                new Claim(ClaimTypes.Role, userProfile.UserType.Name),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Index", "Home");
        }

        //GET: Account/Create
        public ActionResult Create()
        {
            UserProfile user = new UserProfile();
            return View();

        }

        //POST: Account/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserProfile user)
        {
            try
            {
                _userProfileRepository.CreateUser(user);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                return View(user);
            }
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Edit/5
        public ActionResult Edit(int id)
        {
            UserProfile user = _userProfileRepository.GetById(id);
            List<UserType> types = _userTypeRepository.GetAllTypes();

            EditUserProfileViewModel vm = new EditUserProfileViewModel()
            {
                UserProfile = user,
                UserTypes = types
            };

            if (vm == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        // POST: Account/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EditUserProfileViewModel vm)
        {
            try
            {
                _userProfileRepository.UpdateUserProfile(id, vm.UserProfile);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(vm);
            }
        }
    }
}