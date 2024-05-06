using System;
using System.Collections.Generic;

namespace familys.Models
{
    public partial class Account
    {
        public Account()
        {
            Avatars = new HashSet<Avatar>();
            Comments = new HashSet<Comment>();
            Histories = new HashSet<History>();
            Homes = new HashSet<Home>();
            ListfriendAccs = new HashSet<Listfriend>();
            ListfriendFriends = new HashSet<Listfriend>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Birthday { get; set; }
        public string? Password { get; set; }
        public string? Address { get; set; }
        public Boolean? IsDelete { get; set; }
        public string? Createby { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? Updateby { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? DeleteBy { get; set; }
        public DateTime? DeleteTime { get; set; }

        public virtual ICollection<Avatar> Avatars { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<History> Histories { get; set; }
        public virtual ICollection<Home> Homes { get; set; }
        public virtual ICollection<Listfriend> ListfriendAccs { get; set; }
        public virtual ICollection<Listfriend> ListfriendFriends { get; set; }
    }
}
