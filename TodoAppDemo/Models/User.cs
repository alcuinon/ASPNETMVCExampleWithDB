using System;
using System.Collections.Generic;

namespace TodoAppDemo.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public int Age { get; set; }
        public bool? IsActive { get; set; }

        public virtual Task? Task { get; set; }
    }
}
