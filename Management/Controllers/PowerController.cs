using Management.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLNV.Data;
using QLNV.Models.Entities;
using System.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Management.Controllers
{
    public class PowerController : Controller
    {
        private readonly DataContext _context;
        public PowerController(DataContext context)
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
        public IActionResult GetFunction()
        {
            var accounts = _context.Accounts
                .Select(a => new { a.Id, a.Name })
                .ToList();

            var functions = _context.Functions
                .Where(f => accounts.Select(a => a.Id).Contains(f.AccountId.Value))
                .Select(f => new
                {
                    f.Id,
                    f.Description,
                    f.AccountId,
                    f.IdCn
                })
                .ToList();

            var result = accounts.Select(account => new
            {
                UserId = account.Id,
                UserName = account.Name,
                Functions = functions
                    .Where(func => func.AccountId == account.Id)
                    .Select(func => new
                    {
                        FunctionId = func.Id,
                        FunctionName = func.Description,
                        Idcn=func.IdCn
                    })
                    .ToList()
            }).ToList();

            // Trả về dữ liệu dưới dạng JSON
            return Json(result);
        }
        public IActionResult CheckQuyen(int Uid, int Fid)
        { 
            var permissions = _context.PhanQuyen
                .Where(p => p.AccountId == Uid && p.IdCn == Fid)
                .Select(p => new
                { 
                    idcn=p.IdCn,
                    id= p.Id,
                    Add = p.IsAdd,
                    Edit = p.IsEdit,
                    Delete = p.IsDelete
                })
                .ToList();

            return Json(permissions);
        }
        public IActionResult UpdateQuyen([FromBody] QuyenViewModel model)
        {

            var quyen = _context.PhanQuyen.FirstOrDefault(p => p.Id == model.Id);

            if (quyen == null)
            {
                return NotFound("Quyền không tìm thấy.");
            }

            quyen.IsAdd = model.IsAdd;
            quyen.IsEdit = model.IsEdit ;
            quyen.IsDelete = model.IsDelete;

            _context.SaveChanges();

            return Ok("Cập nhật quyền thành công.");
        }
        //public IActionResult AddQuyen([FromBody] QuyenViewModel model)
        //{
            
        //    var quyen = new PhanQuyen
        //    {
        //        IdCn = model.FunctionsId,
        //        AccountId = model.AccountId,
        //        IsAdd = model.IsAdd,
        //        IsEdit = model.Edit,
        //        IsDelete = model.Delete
        //    };

        //    _context.PhanQuyen.Add(quyen);
        //    _context.SaveChanges();

        //    return Ok();
        //}





    }
}
