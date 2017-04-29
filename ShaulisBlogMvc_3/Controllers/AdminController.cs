using Newtonsoft.Json.Linq;
using ShaulisBlogMvc_3.Models.Blog;
using ShaulisBlogMvc_3.Models.Fans;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShaulisBlogMvc_3.Controllers
{
    public class AdminController : Controller
    {
        public AdminController()
        {
            int i = 0;
        }

        #region Blog
        #region ViewFunctions


        public ActionResult CheckLoggedIn(string ViewName)
        {
            return CheckLoggedIn(ViewName, null);
        }
        public ActionResult CheckLoggedIn(string ViewName, object Model)
        {
            //check if user is admin and already logged in
            if (Session["LoggedIn"] == null || !(bool)Session["LoggedIn"])
            {
                //not logged in
                return RedirectToAction("Login");
            }
            else
            {
                //logged in - redirecting to admin home page
                return View(ViewName, Model);
            }
        }
        public ActionResult Index()
        {
            return CheckLoggedIn("Home");

        }

        [Route("admin/blog/addnewpost")]
        public ActionResult AddNewPost()
        {
            return CheckLoggedIn("Blog/AddNewPost");
           // return View("Blog/AddNewPost");
        }

        [Route("admin/blog/manageposts")]
        public ActionResult ManagePosts()
        {
            using (var context = new ShauliBlogContext())
            {
                return CheckLoggedIn("Blog/Posts", context.Posts.OrderByDescending(a => a.PostDate)
                    .Include(b => b.Comments)
                    .ToList());

                //return View("Blog/Posts", context.Posts.OrderByDescending(a => a.PostDate)
                //    .Include(b => b.Comments)
                //    .ToList());
            }
        }

        [Route("admin/blog/posts/{id}/delete")]
        public ActionResult DeletePost(int Id)
        {
            using (var context = new ShauliBlogContext())
            {
                var _post = context.Posts.SingleOrDefault(a => a.PostId == Id);
                return CheckLoggedIn("Blog/Posts/DeletePost", _post);
                //return View("Blog/Posts/DeletePost", _post);
            }
        }


        [Route("admin/blog/posts/{id}/update")]
        public ActionResult EditPost(int Id)
        {
            using (var context = new ShauliBlogContext())
            {
                var _post = context.Posts.SingleOrDefault(a => a.PostId == Id);
                return CheckLoggedIn("Blog/Posts/EditPost", _post);
              //  return View("Blog/Posts/EditPost", _post);
            }
        }

        [Route("admin/blog/posts/{id}/comments")]
        public ActionResult ShowComments(int Id)
        {
            using (var context = new ShauliBlogContext())
            {
                var _post = context.Posts.Include(a => a.Comments).SingleOrDefault(a => a.PostId == Id);
                return CheckLoggedIn("Blog/Posts/DisplayComments", _post);
             //   return View("Blog/Posts/DisplayComments", _post);
            }
        }
        #endregion

        #region ActionFunctions
        [HttpPost]
        [Route("admin/blog/addnewpost")]
        public ActionResult AddNewPost(Post Post)
        {
            using (var context = new ShauliBlogContext())
            {
                context.Posts.Add(Post);
                context.SaveChanges();
                return RedirectToAction("ManagePosts");
            }
        }

        [Route("admin/blog/posts/{id}/delete")]
        [HttpPost]
        public ActionResult DeletePost(int Id, Post Post)
        {
            using (var context = new ShauliBlogContext())
            {
                var _post = context.Posts.SingleOrDefault(a => a.PostId == Id);

                //first remove comments of the selected post
                _post.Comments.RemoveRange(0, _post.Comments.Count);

                //then remove post it self
                context.Posts.Remove(_post);
                context.SaveChanges();
                return RedirectToAction("ManagePosts");
            }
        }



        [Route("admin/blog/posts/{id}/update")]
        [HttpPost]
        public ActionResult EditPost(int Id, Post Post)
        {
            using (var context = new ShauliBlogContext())
            {
                var _post = context.Posts.SingleOrDefault(a => a.PostId == Id);
                _post.ImageUrl = Post.ImageUrl;
                _post.VideoUrl = Post.VideoUrl;
                _post.Author = Post.Author;
                _post.AuthorSiteAddress = Post.AuthorSiteAddress;
                _post.PostContent = Post.PostContent;
                _post.PostTitle = Post.PostTitle;
                context.SaveChanges();
                return RedirectToAction("ManagePosts");
            }
        }


        [Route("admin/blog/posts/{postId}/comments/{commentId}/delete")]
        public ActionResult DeleteComment(int PostId, int CommentId)
        {
            using (var context = new ShauliBlogContext())
            {
                var _post = context.Posts.SingleOrDefault(a => (a.PostId == PostId));
                var _comment = _post.Comments.SingleOrDefault(a => a.CommentId == CommentId);
                _post.Comments.Remove(_comment);
                context.SaveChanges();
                return RedirectToAction(String.Format("Blog/Posts/{0}/comments", PostId));
            }
        }
        #endregion
        #endregion

        #region Fans

        #region ViewFunctions
        [HttpPost]
        [Route("admin/fans/fanslist/filter")]        
        public ActionResult Filter(Fan Fan)
        {
            using (var context = new ShauliBlogContext())
            {
                var _results = context.Fans.Select(a => a);
                if (Fan.FirstName != null)
                {
                    _results = _results.Where(a => a.FirstName.Contains(Fan.FirstName));
                }
                if (Fan.LastName != null)
                {
                    _results = _results.Where(a => a.LastName.Contains(Fan.LastName));
                }
                if (Fan.Gender != "All")
                {
                    _results = _results.Where(a => a.Gender ==  Fan.Gender);
                }
                if (Fan.YearsInClub != 0)
                {
                    _results = _results.Where(a => a.YearsInClub == Fan.YearsInClub);
                }
                return CheckLoggedIn("Fans/FanList", _results.ToList());
               // return View("Fans/FanList", _results.ToList());
            }

        }

        [Route("admin/fans/fanslist")]
        public ActionResult FansList()
        {
            try
            {
                using (var context = new ShauliBlogContext())
                {
                    List<Fan> _list = context.Fans.ToList();
                    return CheckLoggedIn("Fans/FanList", _list);
                  //  return View("Fans/FanList", _list);

                }
            }
            catch (Exception e)
            {

                throw;
            }


            // return View("FanList", GenerateDummyFans());
        }

        [Route("admin/fans/{id}/update")]
        public ActionResult Update(int Id)
        {
            using (var context = new ShauliBlogContext())
            {
                var _fan = context.Fans.Where(a => a.Id == Id).Include(b=>b.FanLocation).SingleOrDefault();
                if (_fan == null)
                {
                    return HttpNotFound("Id does not exist");
                }
                return CheckLoggedIn("Fans/UpdateFan", _fan);
                //return View("Fans/UpdateFan", _fan);

            }
        }

        [Route("admin/fans/{id}/delete")]
        public ActionResult Delete(int Id)
        {
            using (var context = new ShauliBlogContext())
            {
                var _fan = context.Fans.Where(a => a.Id == Id).SingleOrDefault();
                if (_fan == null)
                {
                    return HttpNotFound("Id does not exist");
                }
                return CheckLoggedIn("Fans/DeleteFan", _fan);
               // return View("Fans/DeleteFan", _fan);
            }
        }

        [Route("admin/fans/map")]
        public ActionResult Map()
        {
            using (var context = new ShauliBlogContext())
            {
                
                var _fans = context.Fans.ToList();
                var _grouped_fans = context.Fans.GroupBy(a => new { a.FanLocation.Country, a.FanLocation.City })
                    .ToList();
                var _dict = new Dictionary<string, List<Fan>>();

                foreach (var item in _grouped_fans)
                {
                    var _list = new List<Fan>();
                    _list.AddRange(item.ToList());
                    
                    _dict.Add(item.Key.Country + ", " + item.Key.City, _list);
                }
                
                var _json_arr = new JArray();
                foreach (var fan in _fans)
                {
                    var _obj = new JObject();
                    _obj.Add("FanName", fan.FirstName + " " + fan.LastName);
                    _obj.Add("PlaceId", fan.FanLocation.PlaceId);
                    _json_arr.Add(_obj);
                }

                var _tuple = new Tuple<string, Dictionary<string, List<Fan>>>(_json_arr.ToString(Newtonsoft.Json.Formatting.None),
                    _dict);
                return CheckLoggedIn("Fans/Map", _tuple);
                //return View("Fans/Map", _tuple);
            }

        }

        public ActionResult Login()
        {
            //first time logging in so there can't be an 'invalid user or...' message
            var _invalid_credentials = false;
            return View("Login", _invalid_credentials);
        }

        [HttpPost]
        public ActionResult Login(string User, string Password)
        {
            if (User == ConfigurationManager.AppSettings["AdminUserName"] && Password == ConfigurationManager.AppSettings["AdminPassword"])
            {
                Session["LoggedIn"] = true;
                return RedirectToAction("Index");
            }
            else
            {
                //if user entered invlaid user name or password, we set Model in the view to be true so we can show an 'invalid user or password' message
                var _invalid_credentials = true;
                return View("Login", _invalid_credentials);
            }
        }
        #endregion

        #region ActionFunctions
        [HttpPost]
        [Route("admin/fans/{id}/update")]
        public ActionResult Update(int Id, Fan Fan)
        {
            using (var context = new ShauliBlogContext())
            {
                var _fan = context.Fans.Where(a => a.Id == Id).SingleOrDefault();
                _fan.FirstName = Fan.FirstName;
                _fan.LastName = Fan.LastName;
                _fan.Birthday = Fan.Birthday;
                _fan.Gender = Fan.Gender;
                _fan.YearsInClub = Fan.YearsInClub;
                _fan.FanLocation = Fan.FanLocation;
                context.SaveChanges();
                return RedirectToAction("FansList");
            }
        }

        [HttpPost]
        [Route("admin/fans/{id}/delete")]
        public ActionResult Delete(int Id, Fan Fan)
        {
            using (var context = new ShauliBlogContext())
            {
                var _fan = context.Fans.Where(a => a.Id == Id).SingleOrDefault();
                context.Fans.Remove(_fan);
                context.SaveChanges();
                return RedirectToAction("FansList");
            }
        }
        #endregion

        #endregion

    }
}