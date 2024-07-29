using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
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
            if (!ModelState.IsValid)
            {
                return Json(new { success = false, message = "Thông tin đăng nhập không hợp lệ." });
            }

            // Tìm tài khoản trong cơ sở dữ liệu
            var account = _context.Accounts
                .FirstOrDefault(a => a.Uname == model.Username && a.pass == model.Password);

            if (account != null)
            {
                // Lưu thông tin phiên làm việc vào Session
                HttpContext.Session.SetString("Name", account.Name);
                HttpContext.Session.SetInt32("Id", account.Id);
                HttpContext.Session.SetString("Addr", account.Addr);


                string roleName="Không thấy chức vụ";
                if (account.Role.HasValue)
                {
                    var role = _context.Roles
                        .FirstOrDefault(r => r.Id == account.Role.Value);

                    if (role != null)
                    {
                        roleName = role.Name;
                    }
                }

                HttpContext.Session.SetString("RoleName", roleName);

                var redirectUrl = Url.Action("Index", "Home");

                return Json(new { success = true, redirectUrl });
            }
            else
            {
                return Json(new { success = false, message = "Tên người dùng hoặc mật khẩu không đúng." });
            }
        }

        // Xử lý đăng xuất
        public IActionResult Logout()
        {
            // Xóa tất cả dữ liệu trong Session
            HttpContext.Session.Clear();

            // Chuyển hướng về trang đăng nhập
            return RedirectToAction("Index", "Login");
        }
    }
}
