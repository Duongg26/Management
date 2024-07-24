using Microsoft.AspNetCore.Mvc;
using QLNV.Data;
using QLNV.Models.ViewModel;
using System.Linq;

namespace QLNV.Controllers
{
    public class LoginController : Controller
    {
        private readonly DataContext _context;

        public LoginController(DataContext context)
        {
            _context = context;
        }

        // Hiển thị form đăng nhập
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // Xử lý đăng nhập
        [HttpPost]
        public IActionResult Login([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var account = _context.Accounts
                    .FirstOrDefault(a => a.Uname == model.Username && a.pass == model.Password);

                if (account != null)
                {
                
                    HttpContext.Session.SetString("Name", account.Name);
                    HttpContext.Session.SetInt32("Id", account.Id);


                    var redirectUrl = Url.Action("Index", "Home");
                    return Json(new { success = true, redirectUrl });
                }
                else
                {
                    return Json(new { success = false, message = "Tên người dùng hoặc mật khẩu không đúng." });
                }
            }

            return Json(new { success = false, message = "Thông tin đăng nhập không hợp lệ." });
        }
    }
}
