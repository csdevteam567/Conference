using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Conference.Models;

namespace Conference.Controllers
{
    public class RegistrationController : Controller
    {
        private ConferenceModel db;
        public RegistrationController()
        {
            db = new ConferenceModel();
        }

        private bool InitRegisterForm()
        {
            try
            {
                List<City> cities = db.Cities.ToList();


                List<SelectListItem> items = new List<SelectListItem>();

                foreach (var city in cities)
                {
                    if (city.Id == cities.First().Id)
                    {
                        items.Add(new SelectListItem { Text = city.Name, Value = city.Id.ToString(), Selected = true });
                    }
                    else
                    {
                        items.Add(new SelectListItem { Text = city.Name, Value = city.Id.ToString() });
                    }
                }

                ViewBag.StateCapital = items;
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public ActionResult Register()
        {
            if (!InitRegisterForm())
            {
                return new HttpNotFoundResult();
            }
            return View();
        }

        [HttpPost]
        public ActionResult Registered(FormCollection form)
        {
            if (!InitRegisterForm())
            {
                return new HttpNotFoundResult();
            }
            try
            {
                db.Users.Add(new User()
                {
                    Name = form["Name"],
                    CityId = int.Parse(form["StateCapital"]),
                    Age = int.Parse(form["Age"]),
                    Email = form["Email"],
                    Phone = form["Phone"],
                    Password = form["Password"]
                });
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewData["Message"] = "Проверьте правильность введенных данных";
                return View("Register");
            }
            ViewData["Message"] = "";
            return View("Register");
        }
    }
}