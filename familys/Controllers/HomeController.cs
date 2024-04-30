using familys.Models;
using familys.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;

namespace familys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly familysContext _context;

        public HomeController(familysContext context)
        {
            _context = context;
        }
        [HttpPost("addHome")]
        public async Task<IActionResult> addHome([FromBody] addHome addHome)
        {
            bool status = false;
            var title = "";
            try
            {
                Home home = new Home
                {
                    Title = addHome.Title,
                    AccId = addHome.AccId,
                    Avatar = addHome.Avatar,
                    ShareCount = addHome.Share == null ? 0 : addHome.Share,
                    LikeCount = addHome.Like == null ? 0 : addHome.Like,
                    IsDelete = false,
                    Createby = _context.Accounts.FirstOrDefault(x => x.IsDelete == false && x.Id == addHome.AccId).Name,
                    CreateTime = DateTime.Now,
                };
                _context.Homes.Add(home);
                _context.SaveChanges();
                status = false;
                title = "Đã đăng tải bài viết.";
            }
            catch (Exception ex)
            {
                status = true;
                title = ex.Message;
            }
            var result = new
            {
                status = status,
                title = title,
            };
            return Ok(result);
        }
        [HttpPost("listposts")]
        public async Task<IActionResult> listPosts([FromBody] AccId accId)
        {
            bool status = false;
            var title = "";
            try
            {
                var data = _context.Homes.Where(x => x.IsDelete == false && x.AccId == 1).Select(x => new
                {
                    x.Avatar,
                    x.Title,
                    x.LikeCount,
                    x.ShareCount,
                    ListComment = _context.Histories.Where(t=>t.IsDelete == false && t.AccId ==x.AccId).ToList(),
                });
                return Ok(data);
            }
            catch (Exception ex)
            {
                status = true;
                title = "Đã có lỗi";
            }
            var result = new
            {
                status = status,
                title = title,
            };
            return Ok(result);
        }
    }
}
