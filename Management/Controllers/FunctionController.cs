using Management.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using QLNV.Data;
using QLNV.Models.Entities;

namespace Management.Controllers
{
    public class FunctionController : Controller
    {
        private readonly DataContext _context;
        public FunctionController(DataContext context)
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
        public IActionResult GetAllFunction()
        {
            var Func= _context.ChucNang.ToList();
            return Json(Func);
        }
        [HttpPost]

        public IActionResult AddFunction([FromBody] FunctionViewModel model)
        {
            if (model == null)
            {
                return BadRequest("Dữ liệu đầu vào không hợp lệ.");
            }

            Console.WriteLine($"Checking for IdCn: {model.IdCn}, AccountId: {model.AccountId}");

            var func = _context.Functions
                .FirstOrDefault(f => f.IdCn == model.IdCn && f.AccountId == model.AccountId);

            if (func != null)
            {
                Console.WriteLine("Bản ghi đã tồn tại.");
                return Conflict("Bản ghi với IdCn và AccountId giống nhau đã tồn tại.");
            }

            var function = new Functions
            {
                IdCn = model.IdCn,
                AccountId = model.AccountId,
                Name = model.Name,
                Link = model.Link,
                Description = model.Description
            };

            _context.Functions.Add(function);
            _context.SaveChanges();

            Console.WriteLine("Thêm chức năng thành công.");
            return Ok("Thêm chức năng thành công.");
        }


        [HttpPost]
        public IActionResult DeleteFunction([FromBody] int id)
        {
            if (id == 0)
            {
                return BadRequest("Invalid ID.");
            }

            var func = _context.Functions
                .FirstOrDefault(f => f.Id == id);

            if (func == null)
            {
                return NotFound("Function not found.");
            }

            try
            {
                _context.Functions.Remove(func);
                _context.SaveChanges();
                return Ok("Function deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }






    }
}
