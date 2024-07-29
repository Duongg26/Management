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
        public IActionResult AddWork([FromBody] WorkViewModel model)
        {
            var id = HttpContext.Session.GetInt32("Id");
            var work = new Work
            {
                AccountId = id.Value,
                NoiDung = model.NoiDung,
                NgayXong=model.NgayXong,
                TepDinhKemPath=model.TepDinhKemPath,
                Status = model.Status


            };
            _context.Works.Add(work);
            _context.SaveChanges();
            return Ok();
        }
        public IActionResult ListTask()
        {
            var id = HttpContext.Session.GetInt32("Id");

            if (!id.HasValue)
            {
                return BadRequest("User ID not found in session.");
            }

            // Lấy tất cả các công việc cho người dùng hiện tại
            var tasks = _context.Works
                .Where(w => w.AccountId == id.Value)
                .Select(w => new
                {
                    w.Id,
                    w.NoiDung,
                    w.NgayGiao,
                    w.NgayXong,
                    w.TepDinhKemPath,
                    w.Status,
                    AssigneeIds = _context.NguoiLam
                        .Where(nl => nl.WorkId == w.Id)
                        .Select(nl => nl.IdNguoiLam)
                        .ToList()
                })
                .ToList(); 

            var result = tasks.Select(w => new
            {
                w.Id,
                w.NoiDung,
                w.NgayGiao,
                w.NgayXong,
                w.TepDinhKemPath,
                w.Status,
                AssigneeNames = _context.Accounts
                    .Where(a => w.AssigneeIds.Contains(a.Id))
                    .Select(a => a.Name)
                    .ToList()
                    .DefaultIfEmpty() 
                    .Aggregate(
                        (current, next) => string.IsNullOrEmpty(current) ? next : current + ", " + next 
                    )
            }).ToList();

            return Ok(result);
        }
        public IActionResult GetNguoiLam()
        {
            var id = HttpContext.Session.GetInt32("Id");
            var acc = _context.Accounts
                .Where(a => a.Id != id)
                .Select(a => new
                {
                    a.Id,
                    a.Name
                })
                .ToList();
            return Ok(acc);
        }





    }
}
