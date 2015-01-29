using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RussianHouse.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult AboutProject()
        {
            ViewBag.Message = "Опроекте";

            return View();
        }

        public ActionResult FundRising()
        {
            ViewBag.Message = "Сбор средств";

            return View();
        }

        public ActionResult News()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Страница контактов";

            return View();
        }

        public ActionResult Partners()
        {
            ViewBag.Message = "Наши партнёры";

            return View();
        }

        public ActionResult Team()
        {
            ViewBag.Message = "Наша команда";

            return View();
        }
    }
}
