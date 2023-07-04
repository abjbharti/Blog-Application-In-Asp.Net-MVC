using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AbhijeetMvcTestBlogApplication.Models
{
    public class User
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Role { get; set; }
        public ICollection<Blog> Blogs { get; set; }

    }
}