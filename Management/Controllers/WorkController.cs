using Management.Models.Entities;
using Management.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using QLNV.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            if (id == null)
            {
                return BadRequest("Session ID not found.");
            }

            var work = new Work
            {
                AccountId = id.Value,
                TenCV = model.TenCV,
                NoiDung = model.NoiDung,
                NgayXong = model.NgayXong,
                DsNguoiLam = model.DSNguoiLam,
                DsTep = model.DsTep,
                Status = model.Status
            };

            _context.Works.Add(work);
            _context.SaveChanges();
           
            var CvMoiNhat = _context.Works
                .OrderByDescending(w => w.Id) 
                .First();

            // Lấy ID của bản ghi mới nhất
            var IdMoiNhat = CvMoiNhat.Id;
            string[] Tep = model.DsTep.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var tep in Tep)
            {
                var ThemTep = new Tep
                {
                    WorkId = IdMoiNhat,
                    Link = tep

                };
                _context.Teps.Add(ThemTep);
                _context.SaveChanges() ;
            }
            string[] NguoiLam=model.DSNguoiLam.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach(var IdNl in NguoiLam)
            {
                var ThemNguoi = new NguoiLam
                {
                    WorkId = IdMoiNhat,
                    IdNguoiLam=int.Parse(IdNl)

                };
                _context.NguoiLam.Add(ThemNguoi);
                _context.SaveChanges();
            }
            return Ok();
        }
        public IActionResult UpdateWork([FromBody] WorkViewModel model)
        {
            var id = model.Id;

            // Tìm công việc theo ID
            var congviec = _context.Works.Find(id);
            if (congviec == null)
            {
                return BadRequest("Không tìm thấy công việc");
            }

            congviec.DsNguoiLam = model.DSNguoiLam;
            congviec.DsTep = model.DsTep;
            congviec.TenCV = model.TenCV;
            congviec.NoiDung = model.NoiDung;
            _context.SaveChanges();

            var nguoiLam = _context.NguoiLam.Where(w => w.WorkId == id).ToList();
            var IdNguoiLam = new HashSet<int>(nguoiLam.Select(nl => nl.IdNguoiLam));

            var IdNguoiLamUpdate = new HashSet<int>(model.DSNguoiLam.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));

            var idsToDelete = IdNguoiLam.Except(IdNguoiLamUpdate).ToList();
            foreach (var idToDelete in idsToDelete)
            {
                var nguoiLamToDelete = _context.NguoiLam.FirstOrDefault(nl => nl.IdNguoiLam == idToDelete && nl.WorkId == id);
                if (nguoiLamToDelete != null)
                {
                    _context.NguoiLam.Remove(nguoiLamToDelete);
                }
            }

            var idsToAdd = IdNguoiLamUpdate.Except(IdNguoiLam).ToList();
            foreach (var idToAdd in idsToAdd)
            {
                _context.NguoiLam.Add(new NguoiLam { IdNguoiLam = idToAdd, WorkId = id });
            }
            //Update Tệp
            var tep = _context.Teps.Where(t => t.WorkId==id).ToList();
            var Linktep = new HashSet<string>(tep.Select(t => t.Link));
            var LinktepUpdate = new HashSet<string>(model.DsTep.Split(',', StringSplitOptions.RemoveEmptyEntries));
            var filesToRemove = new HashSet<string>(Linktep);
            filesToRemove.ExceptWith(LinktepUpdate);
            foreach (var fileToRemove in filesToRemove)
            {
                var tepToRemove = _context.Teps.FirstOrDefault(t => t.Link == fileToRemove && t.WorkId == id);
                if (tepToRemove != null)
                {
                    _context.Teps.Remove(tepToRemove);
                }
            }

            var filesToAdd = new HashSet<string>(LinktepUpdate);
            filesToAdd.ExceptWith(Linktep);
            foreach (var fileToAdd in filesToAdd)
            {
                _context.Teps.Add(new Tep { Link = fileToAdd, WorkId = id });
            }

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
            //Viet hàm SQL thay hàm này

            // Lấy tất cả các công việc cho người dùng hiện tại
            var tasks = _context.Works
                .Where(w => w.AccountId == id.Value)
                .Select(w => new
                {
                    w.Id,
                    w.TenCV,
                    w.NoiDung,
                    w.NgayGiao,
                    w.NgayXong,
                    w.DsTep,
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
                w.TenCV,
                w.NoiDung,
                w.NgayGiao,
                w.NgayXong,
                w.DsTep,
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
        public IActionResult GetNguoiLam(int idTask)
        {            
            var accounts = _context.Accounts
                .Select(a => new
                {
                    a.Id,
                    a.Name
                })
                .ToList();
                
            var assignedIds = _context.NguoiLam
                 .Where(w => w.WorkId == idTask)
                 .Select(w => w.IdNguoiLam)
                 .ToList();
            var result = new
            {
                AssignedIds = assignedIds,
                Accounts = accounts
            };
            
            return Ok(result);
        }
        public IActionResult KiemTra(int id)
        {
            var IdNL = _context.NguoiLam
                .Where(idnl => idnl.WorkId == id)
                .Select(idnl => new { idnl.IdNguoiLam, idnl.Status })
                .ToList();

            var NL = _context.Accounts
                .Where(nl => IdNL.Select(x => x.IdNguoiLam).Contains(nl.Id))
                .Select(nl => new { nl.Id, nl.Name })
                .ToList();

            var result = IdNL
                .Select(idnl => new
                {
                    Name = NL.FirstOrDefault(nl => nl.Id == idnl.IdNguoiLam)?.Name,
                    idnl.Status
                })
                .ToList();

            return Ok(result);
        }

        ///////////// Cần làm
        public IActionResult CanLam()
        {
            var id = HttpContext.Session.GetInt32("Id");
            if (id == null)
            {
                return BadRequest("Id không hợp lệ.");
            }

            var workIds = _context.NguoiLam
                .Where(nl => nl.IdNguoiLam == id)
                .Select(nl => nl.WorkId)
                .ToList();

            if (workIds.Count == 0)
            {
                return Ok(new { Message = "Không có công việc" });
            }

            // Lấy danh sách công việc dựa trên WorkId
            var cv = _context.Works
                .Where(w => workIds.Contains(w.Id))
                .ToList();

            return Ok(cv);
        }












    }
}
