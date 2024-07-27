using Management.Models.Entities;
using Management.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using QLNV.Data;

namespace Management.Controllers
{
    public class WorkController : Controller
    {
        private readonly DataContext _context;
        public WorkController(DataContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var name = HttpContext.Session.GetString("Name");
            var id = HttpContext.Session.GetInt32("Id");
            ViewBag.Name = name;
            return View();
          
        }
        public IActionResult AddWork( [FromBody] WorkViewModel model)
        {
            var id = HttpContext.Session.GetInt32("Id");
            var work = new Work
            {
                AccountId = model.AccountId,
                PerId = id.Value,
                Description = model.Description,
                Status = model.Status

            };
            _context.Works.Add(work);
            _context.SaveChanges();
            return Ok();
        }
        public IActionResult ViewWork([FromBody] WorkViewModel model) {
            var id = HttpContext.Session.GetInt32("Id");
            var work = _context.Works
         .Where(p => p.AccountId == model.AccountId && p.PerId == id.Value)
         .ToList();

            return Ok(work);
        }
        public IActionResult Display()
        {
            return View();
        }
       
    }
}
