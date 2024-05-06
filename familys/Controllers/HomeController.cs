﻿using familys.Models;
using familys.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System;

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
        static string GetElapsedTime(DateTime postTime)
        {
            TimeSpan timeDifference = DateTime.Now - postTime;
            if (postTime == null)
            {
                return "Không có thời gian đăng";
            }
            else if (timeDifference.TotalMinutes < 1)
            {
                return "Vài giây trước";
            }
            else if (timeDifference.TotalHours < 1)
            {
                return $"{(int)timeDifference.TotalMinutes} phút trước";
            }
            else if (timeDifference.TotalDays < 1)
            {
                return $"{(int)timeDifference.TotalHours} giờ trước";
            }
            else
            {
                return $"{(int)timeDifference.TotalDays} ngày trước";
            }
        }

        [HttpPost("listposts")]
        public async Task<IActionResult> listPosts([FromBody] AccId accId)
        {
            bool status = false;
            var title = "";
            try
            {
                var homePosts = from a in _context.Homes.Where(x => x.AccId == accId.accId ||
                                                _context.Listfriends.Any(a => a.AccId == accId.accId && a.FriendId == x.AccId) ||
                                                _context.Listfriends.Any(b => b.FriendId == accId.accId && b.AccId == x.AccId)).OrderByDescending(post => post.CreateTime)
                                select new
                                {
                                    homeId = a.Id,
                                    UserName = _context.Accounts.FirstOrDefault(x => x.Id == a.AccId && x.IsDelete == false).Name,
                                    AvatarUser = _context.Avatars.FirstOrDefault(x => x.Id == a.AccId && x.IsDelete == false).Avatars,
                                    a.Title,
                                    a.Avatar,
                                    //likepost = _context.Histories.Where(x => x.IsDelete == false && x.AccId == accId.accId && x.Likes == true),
                                    ListComment = _context.Histories.Where(t => t.IsDelete == false && t.AccId == a.AccId && t.HomeId == a.Id).ToList(),
                                    create = GetElapsedTime(a.CreateTime.Value),
                                };
                return Ok(homePosts);
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

        [HttpPost("likepost")]
        public async Task<IActionResult> likePost([FromBody] likePost likePost)
        {
            bool status = false;
            var title = "";
            try
            {
                History history = new History
                {
                    AccId = likePost.accId,
                    HomeId = likePost.homeId,
                    Likes = true,
                    Share = false,
                    IsDelete = false,
                    Createby = _context.Accounts.FirstOrDefault(x => x.IsDelete == false && x.Id == likePost.accId).Name,
                    CreateTime = DateTime.Now,
                };
                _context.Histories.Add(history);
                _context.SaveChanges();
                status = false;
                title = "Thành công";
            }
            catch
            {
                status = true;
                title = "Lỗi";
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
