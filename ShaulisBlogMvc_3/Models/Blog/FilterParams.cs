using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShaulisBlogMvc_3.Models.Blog
{
    public class FilterParams
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
        public string CommentContent { get; set; }
    }
}