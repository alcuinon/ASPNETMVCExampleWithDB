using System;
using System.Collections.Generic;

namespace TodoAppDemo.Models
{
    public partial class News
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Body { get; set; } = null!;
        public bool IsPublished { get; set; }
        public DateTime DateCreated { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
