using ShaulisBlogMvc_3.Models.Blog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace ShaulisBlogMvc_3.Controllers
{
    public class BlogController : Controller
    {
        public BlogController()
        {
            ViewBag.BlogSelected = "selected";
        }
        //
        // GET: /Blog/

        public ActionResult Index()
        {
            using(var context = new ShauliBlogContext())
            {
                //including comments list (Join in sql) in the post object
                var _posts = context.Posts.Include(a=>a.Comments).ToList();                         
                return View("Blog", _posts.OrderByDescending(a=>a.PostDate).ToList());
            }
        }


        [HttpPost]
        public ActionResult AddNewComment(int Id, Comment Comment)
        {
            using (var context = new ShauliBlogContext())
            {
                var _post = context.Posts.Where(a => a.PostId == Id).SingleOrDefault();
                // Comment.Post = _post;
                _post.Comments.Add(Comment);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Filter(FilterParams FilterParams)
        {
            //filtering blog entrees by the field below:
            using (var context = new ShauliBlogContext())
            {
                var _results = context.Posts.Select(a=> a);
                if (FilterParams.DateFrom!=null)
                {
                    _results = _results.Where(a => a.PostDate >= FilterParams.DateFrom && a.PostDate <= FilterParams.DateTo);
                }
                if (FilterParams.Author != null)
                {
                    _results = _results.Where(a => a.Author.Contains(FilterParams.Author));
                }
                if (FilterParams.Content != null)
                {
                    _results = _results.Where(a => a.PostContent.Contains(FilterParams.Content));
                }
                if (FilterParams.CommentContent!=null)
                {
                    _results = _results.Where(a => a.Comments
                    .Any(b => b.CommentContent.Contains(FilterParams.CommentContent)))
                    .Include(a=>a.Comments);
                }

                return View("Blog",_results.Include(a=>a.Comments).ToList());
            }
            
        }

    }
}
