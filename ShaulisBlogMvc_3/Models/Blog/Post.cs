using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShaulisBlogMvc_3.Models.Blog
{
    public class Post
    {
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string Author { get; set; }
        public string AuthorSiteAddress { get; set; }
        public DateTime PostDate { get; set; }
        public string PostContent { get; set; }
        public string ImageUrl { get; set; }
        public string VideoUrl { get; set; }
        public virtual List<Comment> Comments { get; set; }

        public Post()
        {
            PostDate = DateTime.Now;
            Comments = new List<Comment>();
        }
    }
}