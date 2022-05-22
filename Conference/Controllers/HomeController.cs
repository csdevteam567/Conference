using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Conference.Models;

namespace Conference.Controllers
{
    public class HomeController : Controller
    {
        private ConferenceModel db;
        public HomeController()
        {
            db = new ConferenceModel();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View("Login");
        }

        [Authorize]
        public ActionResult Participants()
        {
            List<UsersViewModel> users = (from u in db.Users
                                         join c in db.Cities on u.CityId equals c.Id
                                         select new UsersViewModel()
                                         {
                                             Name = u.Name,
                                             Age = u.Age,
                                             Email = u.Email,
                                             Phone = u.Phone,
                                             StateCapital = c.Name
                                         }).ToList();

            return View(users);
        }

        [Authorize]
        public ActionResult SortParticipants(FormCollection form)
        {
            List<UsersViewModel> users;
            int sortBy = int.Parse(form["SortBy"]);

            var usersQuery = from u in db.Users
                             join c in db.Cities on u.CityId equals c.Id
                             select new UsersViewModel()
                             {
                                 Name = u.Name,
                                 Age = u.Age,
                                 Email = u.Email,
                                 Phone = u.Phone,
                                 StateCapital = c.Name
                             };

            switch (sortBy)
            {
                case 0: usersQuery = usersQuery.OrderBy(u => u.Name ); break;
                case 1: usersQuery = usersQuery.OrderBy(u => u.Age); break;
                case 2: usersQuery = usersQuery.OrderBy(u => u.StateCapital); break;
            }

            users = usersQuery.ToList();

            return View("Participants", users);
        }

        [HttpPost]
        public ActionResult LoggedIn(FormCollection form)
        {
            try
            {
                string email = form["Email"];
                string password = form["password"];

                if(db.Users.Where(u => u.Email == email && u.Password == password).Count() == 0)
                {
                    ViewData["Message"] = "Проверьте правильность введенных данных";
                    return View("Login");
                } else
                {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.SetAuthCookie(email, true);
                }

            } catch(Exception ex)
            {
                return new HttpNotFoundResult();
            }
            ViewData["Message"] = "";
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}