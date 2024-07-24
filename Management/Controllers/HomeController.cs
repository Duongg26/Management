using Management.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using QLNV.Data;
using QLNV.Models;
using System.Diagnostics;

namespace QLNV.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataContext _context;
        public HomeController(ILogger<HomeController> logger, DataContext context)
        {
            _logger = logger;
            _context = context;
        }



        public IActionResult Index()
        {

            var name = HttpContext.Session.GetString("Name");

            ViewBag.Name = name;

            return View();
        }



        public IActionResult GetFunctions()
        {
            var id = HttpContext.Session.GetInt32("Id");
            if (id == null)
            {
                return BadRequest("Error: No user ID in session");
            }

            var phanQuyen = _context.PhanQuyen
                .Where(pq => pq.AccountId == id)
                .ToList();

            if (!phanQuyen.Any())
            {
                return Json(new { data = new List<FunctionViewModel>(), message = "No permissions" });
            }

            var functionIds = phanQuyen.Select(pq => pq.FunctionsId).ToList();

            var functions = _context.Functions
                .Where(f => functionIds.Contains(f.Id))
                .ToList();

            if (!functions.Any())
            {
                return Json(new { data = new List<FunctionViewModel>(), message = "No functions found" });
            }

            var model = functions.Select(f => new FunctionViewModel
            {
                Description = f.Description,
                Link=f.Link
            }).ToList();

            return Json(new { data = model });
        }



    }
}


