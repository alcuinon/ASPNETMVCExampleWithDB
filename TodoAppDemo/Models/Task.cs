using System;
using System.Collections.Generic;

namespace TodoAppDemo.Models
{
    public partial class Task
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime DateCreated { get; set; }
        public string Status { get; set; } = null!;
        public int UserId { get; set; }

        public virtual User IdNavigation { get; set; } = null!;
    }
}
