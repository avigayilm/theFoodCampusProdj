using Microsoft.AspNetCore.Mvc;
using theFoodCampus.Models;

namespace theFoodCampus.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
