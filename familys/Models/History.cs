using System;
using System.Collections.Generic;

namespace familys.Models
{
    public partial class History
    {
        public int Id { get; set; }
        public int? AccId { get; set; }
        public int? HomeId { get; set; }
        public string? Comment { get; set; }
        public Boolean? IsDelete { get; set; }
        public string? Createby { get; set; }
        public DateTime? CreateTime { get; set; }
        public string? Updateby { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string? DeleteBy { get; set; }
        public DateTime? DeleteTime { get; set; }

        public virtual Account? Acc { get; set; }
        public virtual Home? Home { get; set; }
    }
}
