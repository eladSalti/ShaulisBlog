using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ShaulisBlogMvc_3.Models.Blog;
using ShaulisBlogMvc_3.Models.Fans;

namespace ShaulisBlogMvc_3
{
    public class ShauliBlogContext : DbContext
    {   
        public DbSet<Post> Posts { get; set; }
        public DbSet<Fan> Fans { get; set; }

        public ShauliBlogContext() : base("FinalProject")
        {
        }
    }
}