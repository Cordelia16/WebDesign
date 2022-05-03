using Industry_4._0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Industry_4._0.Data;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace Industry_4._0.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Emerge()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public async Task<IActionResult> comAndOr()
        {
            if (!User.Identity.IsAuthenticated)
            {
                foreach (var post in _context.DiscussionForum)
                {
                    post.canIncreaseLike = true;
                    post.canIncreaseAgree = true;
                    post.canIncreaseDisagree = true;
                }
                await _context.SaveChangesAsync();
            }

            var allDiscussions = from result in _context.DiscussionForum
                                 orderby result.PostDate descending
                                 select result;

            return View(await allDiscussions.ToListAsync());
        }
        public async Task<IActionResult> Privacy()
        {
            if (!User.Identity.IsAuthenticated)
            {
                foreach (var post in _context.DiscussionForum)
                {
                    post.canIncreaseAgree = true;
                    post.canIncreaseDisagree = true;
                }
                await _context.SaveChangesAsync();
            }

            var allDiscussions = from result in _context.DiscussionForum
                                 orderby result.PostDate descending
                                 select result;

            return View(await allDiscussions.ToListAsync());
            //return View(await _context.DiscussionForum.ToListAsync());
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
