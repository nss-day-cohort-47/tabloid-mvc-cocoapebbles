using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostRepository _postRepository;

        public HomeController(ILogger<HomeController> logger, IPostRepository postRepo)
        {
            _logger = logger;
            _postRepository = postRepo;
        }

        public IActionResult Index()
        {
            int currentUserId = GetCurrentUserId();

            List<Post> posts = _postRepository.GetAllSubscribedPosts(currentUserId);

            return View(posts); 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private int GetCurrentUserId()
        {
            try
            {

            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
            }
            catch
            {
                return 0;
            }
        }
    }
}
