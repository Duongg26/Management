using Management.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public AccountController(DataContext context)
        {
            _context = context;
        }

        public static class HashingHelper
        {
            public static string ComputeSha256Hash(string rawData)
            {



                using (SHA256 sha256Hash = SHA256.Create())
                {
                    byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
                    StringBuilder builder = new StringBuilder();

                    foreach (byte t in bytes)
                    {
                        builder.Append(t.ToString("x2"));
                    }

                    return builder.ToString();
                }
            }
        }

        public IActionResult Index()
        {
            var id = HttpContext.Session.GetInt32("Id");
            var name = HttpContext.Session.GetString("Name");
            ViewBag.Name = name;
            return View();
        }

        [HttpGet]
        public IActionResult GetAccount(int pageNumber = 1, int pageSize = 5, string search = null)
        {
            try
            {
                if (pageNumber < 1 || pageSize < 1)
                {
                    return BadRequest("Số trang và kích thước trang phải lớn hơn 0.");
                }

                var query = _context.Accounts.AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(a => a.Name.Contains(search));
                }

                var totalRecords = query.Count();
                var accounts = query
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return Json(new
                {
                    data = accounts,
                    totalRecords = totalRecords,
                    pageNumber = pageNumber,
                    pageSize = pageSize
                });
            }
            catch (Exception)
            {
                return StatusCode(500, "Lỗi khi lấy dữ liệu tài khoản.");
            }
        }


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
                    a.IsRead
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

        [HttpPost]
        public IActionResult DeleteById([FromBody] DeleteRequest request)
        {
            var acc = _context.Accounts.Find(request.Id);
            if (acc == null)
            {
                return BadRequest("Không tìm thấy tài khoản.");
            }

            _context.Accounts.Remove(acc);
            _context.SaveChanges();

            return Ok("Đã xóa thành công.");
        }

        [HttpPost]
        public IActionResult AddAccount([FromBody] AccountViewModel model)
        {
            var hashedPassword = HashingHelper.ComputeSha256Hash(model.pass);
            var hashedPassword2 = HashingHelper.ComputeSha256Hash(model.pass2);

            var acc = new Account
            {
                Uname = model.UName,
                Status = model.Status,
                Name = model.Name,
                Role = model.Role,
                Addr = model.Addr,
                pass = hashedPassword,
                pass2 = hashedPassword2
            };

            _context.Accounts.Add(acc);
            _context.SaveChanges();

            return Ok();
        }



        public class DeleteRequest
        {
            public int Id { get; set; }
        }

        [HttpGet]
        public IActionResult GetAllQuyen(int id)
        {
            var chucNangList = _context.ChucNang
                .Select(c => new
                {
                    c.Id,
                    c.Description,
                    PhanQuyen = _context.PhanQuyen
                        .Where(pq => pq.AccountId == id && pq.IdCn == c.Id)
                        .Select(pq => new
                        {
                            pq.Id,
                            pq.IsRead,
                            pq.IsAdd,
                            pq.IsEdit,
                            pq.IsDelete
                        })
                        .FirstOrDefault()
                })
                .ToList();

            var quyenList = chucNangList.Select(cn => new
            {
                cn.Id,
                cn.Description,
                PhanQuyenId = cn.PhanQuyen?.Id ?? 0,
                IsRead = cn.PhanQuyen?.IsRead ?? 0,
                IsAdd = cn.PhanQuyen?.IsAdd ?? 0,
                IsEdit = cn.PhanQuyen?.IsEdit ?? 0,
                IsDelete = cn.PhanQuyen?.IsDelete ?? 0
            }).ToList();

            if (quyenList.Count == 0)
            {
                return NotFound(new { message = "No permissions found." });
            }

            return Json(new { data = quyenList });
        }





        [HttpPost]
        public IActionResult UpdateQuyen([FromBody] List<QuyenViewModel> models)
        {
            
                foreach (var model in models)
                {
                    if (model.Id == 0)
                    {
                        if (model.IsRead == 1)
                        {
                            if (model.AccountId != 0)
                            {
                                var newpq = new PhanQuyen
                                {
                                    AccountId = model.AccountId,
                                    IdCn = model.IdCn,
                                    IsRead = model.IsRead,
                                    IsAdd = model.IsAdd,
                                    IsEdit = model.IsEdit,
                                    IsDelete = model.IsDelete
                                };

                                _context.PhanQuyen.Add(newpq);
                            }
                        }
                    }
                    else
                    {
                        var pq = _context.PhanQuyen.Find(model.Id);

                        if (pq != null)
                        {
                            if (model.IsRead == 1)
                            {
                                pq.IsRead = model.IsRead;
                                pq.IsAdd = model.IsAdd;
                                pq.IsEdit = model.IsEdit;
                                pq.IsDelete = model.IsDelete;

                                _context.PhanQuyen.Update(pq);
                            }
                            else
                            {
                                _context.PhanQuyen.Remove(pq);
                            }
                        }
                        else
                        {
                            return NotFound($"Không tìm thấy quyền với ID {model.Id}.");
                        }
                    }
                }

                _context.SaveChanges();
                return Ok("Cập nhật quyền thành công.");
            }
         
        









    }
}
