using AbhijeetMvcTestBlogApplication.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AbhijeetMvcTestBlogApplication.Models
{
    public class Blog
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Guid IdUser { get; set; }
        public bool IsEnabled { get; set; }
        public virtual User Users { get; set; }
    }
}
