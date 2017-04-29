using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShaulisBlogMvc_3.Models.Blog
{
    public class Comment
    {
        public int CommentId { get; set; }
        public string CommentTitle { get; set; }
        public string CommentAuthor { get; set; }
        public string CommentAuthorWebsite { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentDate { get; set; }
        public virtual Post Post { get; set; }
        public Comment()
        {
            CommentDate = DateTime.Now;
        }
    }
}