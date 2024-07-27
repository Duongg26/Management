using Management.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using QLNV.Data;

namespace Management.Controllers
{
    public class DisplayController : Controller
    {
        private readonly DataContext _context;
        public DisplayController(DataContext context)
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
        public IActionResult GetWork()
        {
         
            var id = HttpContext.Session.GetInt32("Id");

            if (id == null)
            {
                return BadRequest("ID không hợp lệ.");
            }

            var work = _context.Works
                .Where(w => w.AccountId == id.Value)
                .OrderByDescending(w => w.DateWork)
                .Select(w => new
                {
                    w.Id,
                    w.PerId,
                    w.Description,
                    w.DateWork,
                    w.Status
                })
                .ToList();

         
            var workWithNames = work.Select(work => new
            {
                work.Id,
                work.PerId,
                work.Description,
                work.DateWork,
                work.Status,
                Name = _context.Accounts
                    .Where(a => a.Id == work.PerId)
                    .Select(a => a.Name)
                    .FirstOrDefault()
            });

            return Ok(workWithNames);
        }
        public IActionResult UpdateStatus([FromBody ]DisplayWorkViewModel model)
        {
            var work = _context.Works.Find(model.Id);
            work.Status = model.Status;
            _context.SaveChanges();
            return Ok();
        }

    }
}
