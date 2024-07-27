using Management.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using QLNV.Data;
using QLNV.Models;
using QLNV.Models.Entities;
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
            var addr = HttpContext.Session.GetString("Addr");
            var role = HttpContext.Session.GetString("RoleName");

            ViewBag.Name = name;
            ViewBag.Addr = addr;
            ViewBag.Role = role;
            return View();
        }



        public IActionResult GetFunctions()
        {
            var accountId = HttpContext.Session.GetInt32("Id");

            if (accountId == null)
            {
                return BadRequest("Error: No account ID in session");
            }

            
            var roleIds = _context.PhanQuyen
                .Where(pq => pq.AccountId == accountId&&pq.IsRead==1)
                .Select(pq => pq.IdCn)
                .ToList();

            if (!roleIds.Any())
            {
                return Json(new { data = new List<FunctionViewModel>(), message = "No functions found" });
            }

            // Lấy các chức năng từ bảng ChucNang dựa trên danh sách IdCn
            var functions = _context.ChucNang
                .Where(cn => roleIds.Contains(cn.Id))
                .ToList();

            if (!functions.Any())
            {
                return Json(new { data = new List<FunctionViewModel>(), message = "No functions found" });
            }

            // Map dữ liệu sang ViewModel
            var model = functions.Select(f => new FunctionViewModel
            {
                Description = f.Description,
                Link = f.Link
            }).ToList();

            return Json(new { data = model });
        }




    }
}


