using Management.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using QLNV.Data;
using QLNV.Models;
using QLNV.Models.Entities;
using System.Security.Cryptography;
using System.Text;

namespace Management.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _context;
        public static class HashingHelper
        {
            public static string ComputeSha256Hash(string rawData)
            {
                if (string.IsNullOrEmpty(rawData))
                {
                    throw new ArgumentNullException(nameof(rawData), "Input data cannot be null or empty.");
                }

                using (SHA256 sha256Hash = SHA256.Create())
                {
                    // Chuyển đổi rawData thành mảng byte và tính toán hash
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                    // Chuyển đổi mảng byte thành chuỗi hex để hiển thị
                    StringBuilder builder = new StringBuilder();
                    foreach (byte t in bytes)
                    {
                        builder.Append(t.ToString("x2"));
                    }
                    return builder.ToString();
                }
            }
        }
        public AccountController(DataContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var id = HttpContext.Session.GetInt32("Id");
            var name = HttpContext.Session.GetString("Name");
            ViewBag.Name = name;
            return View();
        }
       
        

        [HttpGet]
        public IActionResult GetAllAccount()
        {
            var accounts = _context.Accounts.ToList();
            return Json(new { data = accounts });
        }
        [HttpGet]
        public IActionResult GetChucNang()
        {
            var x = HttpContext.Session.GetInt32("Id");

            var chucnang = _context.PhanQuyen
                .Where(a => a.AccountId == x)
                .Select(a => new
                {
                    a.IsAdd,
                    a.IsEdit,
                    a.IsDelete,
                    a.IsUpdate
                })
                .FirstOrDefault();

            return Json(new { data = chucnang });
        }
        [HttpGet]
        public IActionResult GetAccById(int id)
        {
            var acc = _context.Accounts.Find(id);

            return Json(new { data = acc });

        }
        [HttpPost]
        public IActionResult AddAccount(AccountViewModel model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.pass) || string.IsNullOrEmpty(model.pass2))
                {
                    return BadRequest("Mật khẩu không được để trống.");
                }

                

                var acc = new Account
                {
                    Uname = model.UName,
                    Status = model.Status,
                    Name = model.Name,
                    Role = model.Role,
                    Addr = model.Addr,
                    pass = HashingHelper.ComputeSha256Hash(model.pass),
                    pass2 = HashingHelper.ComputeSha256Hash(model.pass2),
                };

                _context.Accounts.Add(acc);
                _context.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Lỗi khi thêm tài khoản.");
            }
        }

        [HttpPost]
        public IActionResult UpdateAccById([FromBody] AccountViewModel model)
        {
            var acc = _context.Accounts.Find(model.Id);
            if (acc == null)
            {
                return BadRequest("Không tìm thấy tài khoản");
            }

            acc.Uname = model.UName;
            acc.Name = model.Name;
            acc.Role = model.Role;
            acc.Status = model.Status;
            acc.Addr = model.Addr;

            _context.SaveChanges();
            return Ok();
        }

    }
}
