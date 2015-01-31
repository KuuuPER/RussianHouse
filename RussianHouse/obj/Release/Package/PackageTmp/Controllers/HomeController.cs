using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RussianHouse.Business;
using RussianHouse.Models;
using RussianHouse.SectionsContent;
using RussianHouse.ViewModel;

namespace RussianHouse.Controllers
{
    public class HomeController : Controller
    {
        private EmailSmtpSender SmtpSender { get; set; }
        private UserDataLogic UserDataLogic { get; set; }

        public HomeController()
        {
            SmtpSender = new EmailSmtpSender();
            UserDataLogic = new UserDataLogic();
        }

        public ActionResult AboutProject()
        {
            ViewBag.Message = "Опроекте";
            ViewBag.SelectedSection = EnSection.AboutProject;
            return View(new UserDataViewModel { Content = SectionContentCreator.GetContentBySection(EnSection.AboutProject)});
        }

        [ActionName("AboutProjectWithModel")]
        public ActionResult AboutProject(UserData data)
        {
            ViewBag.Message = "Опроекте";

            return View("AboutProject", new UserDataViewModel{Email = data.Email, Content = SectionContentCreator.GetContentBySection(EnSection.AboutProject)});
        }

        public ActionResult FundRising()
        {
            ViewBag.Message = "Сбор средств";

            return View(SectionContentCreator.GetContentBySection(EnSection.FundRising));
        }

        public ActionResult News()
        {
            var model = SectionContentCreator.GetContentBySection(EnSection.News);
            return View(model);
        }

        [HttpGet]
        public ActionResult SendEmailToAll()
        {
            SmtpSender.SendToAll();
            return RedirectToAction("AboutProject");
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

        [HttpPost]
        public ActionResult AddUserData(UserDataViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserDataLogic.AddUserData(model.Email);
            }

            return RedirectToAction("AboutProject");
       }

        [RequireHttps]
        [HttpPost]
        public void SendEmailToPayment(string email)
        {
            email = Request.Form["email"];

            UserDataOperations.SaveUserData(new UserData { Email = email });
        }
    }
}
