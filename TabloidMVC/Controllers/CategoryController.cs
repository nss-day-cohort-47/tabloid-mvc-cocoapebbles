using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepo;

        public CategoryController(ICategoryRepository categoryRepo)
        {
            _categoryRepo = categoryRepo;
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            var cats = _categoryRepo.GetAll();

            if (User.IsInRole("Admin"))
            {
                return View(cats);
            }
            else
            {
                return Unauthorized();
            }
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CategoryController/Create
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

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(Category category)
        {
            try
            {
                _categoryRepo.Add(category);

                return RedirectToAction("Index");
            }
            catch
            {
                return View(category);
            }

        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var cat = _categoryRepo.GetCategoryById(id);
            if (User.IsInRole("Admin"))
            {
                return View(cat);
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Category category)
        {
            if (User.IsInRole("Admin"))
            {

                try
                {
                    _categoryRepo.Update(category);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(category);
                }
            }
            else
            {
                return Unauthorized();
            }
        }

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var cats = _categoryRepo.GetAll();
            Category cat = _categoryRepo.GetCategoryById(id);

            if (User.IsInRole("Admin"))
            {
                return View(cat);
            }
            else
            {
                return Unauthorized();
            }
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                _categoryRepo.Delete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
