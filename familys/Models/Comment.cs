using System;
using System.Collections.Generic;

namespace familys.Models
{
    public partial class Comment
    {
        public int Id { get; set; }
        public int? AccId { get; set; }
        public int? HomeId { get; set; }
        public string? Comment1 { get; set; }
        public Boolean? IsDelete { get; set; }  
        public string CreateBy { get; set; } = null!;
        public DateTime CreateTime { get; set; }
        public string UpdateBy { get; set; } = null!;
        public DateTime UpdateTime { get; set; }
        public string DeleteBy { get; set; } = null!;
        public DateTime DeleteTime { get; set; }

        public virtual Account? Acc { get; set; }
        public virtual Home? Home { get; set; }
    }
}
