using Microsoft.AspNetCore.Mvc;
using theFoodCampus.Data;
using theFoodCampus.Models;

namespace theFoodCampus.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        // GET: CommentController
        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Check()
        {
            var recipes = _context.Recipes.ToList();
            return View(recipes);
        }

        public List<UserLogin> PutValue()
        {
            var users = new List<UserLogin>
            {
                new UserLogin {Id=1,UserName="Admin",Password="1234"}
            };
            return users;
        }

        [HttpPost]
        public IActionResult Verify(UserLogin usr)
        {
            var u = PutValue();

            var ue = u.Where(u => u.UserName.Equals(usr.UserName));

            var up = ue.Where(p => p.Password.Equals(usr.Password));

            if (up.Count() == 1)
            {
                ViewBag.message = "Login Success";
                return RedirectToAction("Index", "Recipe");
            }
            else
            {
                ViewBag.message = "Login Failed";
                return View("Index");
            }
        }
    }
}
