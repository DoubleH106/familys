using familys.Models;
using familys.ModelView;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;

namespace familys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly familysContext _context;

        public AccountsController(familysContext context)
        {
            _context = context;
        }
        [HttpPost("addAccount")]
        public async Task<IActionResult> addAccount([FromBody] addAccount account)
        {
            Boolean status = false;
            var title = "";
            var Email = _context.Accounts.FirstOrDefault(x => x.IsDelete == false && x.Email == account.Email);
            var Phone = _context.Accounts.FirstOrDefault(x => x.IsDelete == false && x.Phone == account.Phone);
            if (Email != null)
            {
                status = true;
                title = "Email đẫ tồn tại.";
            }
            else if (Phone != null)
            {
                status = true;
                title = "Phone đẫ tồn tại.";
            }
            else
            {
                var pass = account.Password;
                using (MD5 md5 = MD5.Create())
                {
                    // Chuyển đổi chuỗi thành mảng byte và tính toán giá trị băm (hash)
                    byte[] inputBytes = Encoding.ASCII.GetBytes(pass);
                    byte[] hashBytes = md5.ComputeHash(inputBytes);

                    // Chuyển đổi mảng byte thành chuỗi hex
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0; i < hashBytes.Length; i++)
                    {
                        sb.Append(hashBytes[i].ToString("x2"));
                    }
                    pass = sb.ToString();
                }
                Account account1 = new Account
                {
                    Email = account.Email,
                    Phone = account.Phone,
                    Name = account.Name,
                    Birthday = account.BirthDay,
                    //Avatar = account.Avatar,
                    Address = account.Address,
                    Password = pass,
                    IsDelete = false,
                    Createby = account.Name,
                    CreateTime = DateTime.Now,
                    Updateby="",
                    DeleteBy="",
                };
                _context.Accounts.Add(account1);
                _context.SaveChanges();
                status = false;
                title = "Tạo tài khoản thành công.";
            }
            var result = new
            {
                title = title,
                status = status,
            };
            return Ok(result);
        }
        [HttpPost("CheckAcc")]
        public async Task<IActionResult> CheckAcc([FromBody] MD5Mk mD5Mk)
        {
            string inputPassword = mD5Mk.Name;
            bool login = false;
            bool isAdmin = false;
            string code = "";

            using (MD5 md5 = MD5.Create())
            {
                // Chuyển đổi mật khẩu nhập vào thành giá trị băm
                byte[] inputBytes = Encoding.ASCII.GetBytes(inputPassword);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Chuyển đổi giá trị băm thành chuỗi hex
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }
                string inputHash = sb.ToString();
                return Ok(inputHash);
            }
        }
    }
}
