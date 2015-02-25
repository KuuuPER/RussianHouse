using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Resources;
using System.Text;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using RussianHouse.Business;
using RussianHouse.Models;
using RussianHouse.Properties;
using RussianHouse.SectionsContent;
using RussianHouse.ViewModel;
using RussianHouse.YandexMoney;

namespace RussianHouse.Controllers
{
    public class HomeController : Controller
    {
        private static YaMoneyNet _yaMoneyNet;
        private EmailSmtpSender SmtpSender { get; set; }
        private UserDataLogic UserDataLogic { get; set; }
        private YandexMoneyHelper YandexMoneyHelper { get; set; }

        private Yandex Yandex { get; set; }

        public HomeController()
        {
            SmtpSender = new EmailSmtpSender();
            UserDataLogic = new UserDataLogic();
            Yandex = new Yandex();

            YaMoneyNet.ClientId = "7128E582421294D37F261631F6A33D2E56A6380ADCCFAE1996C94A5E30C0B293";
            YaMoneyNet.SecretId = "D5A0A207E10F0D81922E692600CC20A8EE4094F9A1507C463D4981D00DE5C421FDBCD17D44CE5D78F6D7FEDA3898B7F9697E66E2FD720A3A891B2AAECC1A8DC7";
            

            _yaMoneyNet = new YaMoneyNet();
            _yaMoneyNet.AccessToken = GetTokenFromSession();
            YandexMoneyHelper = new YandexMoneyHelper(_yaMoneyNet);
        }

        public ActionResult AboutProject(bool isPartial = false)
        {
            ViewBag.Message = "О проекте";
            ViewBag.SelectedSection = EnSection.AboutProject;

            var model = new UserDataViewModel { Content = SectionContentCreator.GetContentBySection(EnSection.AboutProject) };

            if (!isPartial)
            {
                return View("AboutProject", model);
            }

            return PartialView("AboutProject", model);
        }

        [ActionName("AboutProjectWithModel")]
        public ActionResult AboutProject(UserData data)
        {
            ViewBag.Message = "О проекте";
            
            return View("AboutProject", new UserDataViewModel{Email = data.Email, Content = SectionContentCreator.GetContentBySection(EnSection.AboutProject)});
        }

        public ActionResult FundRising()
        {
            ViewBag.Message = "Сбор средств";

            return View(SectionContentCreator.GetContentBySection(EnSection.FundRising));
        }

        [HttpGet]
        public ActionResult GetNewsContent(bool isPartial = false)
        {
            ViewBag.Message = "Новости";
            ViewBag.SelectedSection = EnSection.News;
            var model = SectionContentCreator.GetContentBySection(EnSection.News);

            if (!isPartial)
            {
                return View("News", model);
            }

            return PartialView("News", model);
        }

        [HttpGet]
        public ActionResult SendEmailToAll()
        {
            SmtpSender.SendToAll();
            return RedirectToAction("AboutProject");
        }

        public ActionResult GetContactsContent(bool isPartial = false)
        {
            ViewBag.Message = "Страница контактов";
            ViewBag.SelectedSection = EnSection.Partners;

            var model = SectionContentCreator.GetContentBySection(EnSection.Contact);

            if (!isPartial)
            {
                return View("Contact", model);
            }

            return PartialView("Contact", model);
        }

        public ActionResult GetPartnersContent(bool isPartial = false)
        {
            ViewBag.Message = "Наши партнёры";
            ViewBag.SelectedSection = EnSection.Partners;

            var model =  SectionContentCreator.GetContentBySection(EnSection.Partners);

            if (!isPartial)
            {
                return View("Partners", model);
            }

            return PartialView("Partners", model);
        }

        public ActionResult GetTeamContent(bool isPartial = false)
        {
            ViewBag.Message = "Наша команда";
            ViewBag.SelectedSection = EnSection.Team;

            var members = new[]
            {
                new TeamMember {Name = "Влад Логинов", Title = "Руководитель и автор идеи"},
                new TeamMember {Name = "Ольга Шеффер", Title = "Консультант"},
                new TeamMember {Name = "Любовь Копейкина", Title = "Дизайнер интерьера"},
                new TeamMember {Name = "Глеб Валевач", Title = "Дизайнер"},
                new TeamMember {Name = "Александр Куприенко", Title = "Веб-программист"},
            };

            var model = new TeamMemberViewModel() { Content = SectionContentCreator.GetContentBySection(EnSection.Team), Members = members };

            if (!isPartial)
            {
                return View("Team", model);
            }

            return PartialView("Team", model);
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

        [HttpGet]
        public ActionResult GetAboutProjectContent(bool isPartial = false)
        {
            ViewBag.Message = "О проекте";
            ViewBag.SelectedSection = EnSection.AboutProject;

            var model = new UserDataViewModel { Content = SectionContentCreator.GetContentBySection(EnSection.AboutProject) };

            if (!isPartial)
            {
                return View("AboutProject", model);
            }
            
            return PartialView("AboutProject", model);
        }

        [HttpGet]
        public ActionResult GetFundRisingContent(bool isPartial = false)
        {
            ViewBag.Message = "Сбор средств";
            var content = SectionContentCreator.GetContentBySection(EnSection.FundRising);
            var paymentsAndGifts = new[]
            {
                new PaymentsAndGiftsViewModel(){Gifts = new[] { Resources.FirstGift, Resources.SecondGift}, IsPopular = false, PaymentSum = 500},
                new PaymentsAndGiftsViewModel(){Gifts = new[] { Resources.FirstGift, Resources.SecondGift, Resources.ThirdGift }, IsPopular = true, PaymentSum = 1000},
            };

            var roadMaps = new[]
            {
                new RoadMapViewModel {TotalSum = 2000, Title = "Получен первый платёж!", Text = "Даже путь в тысячу ли начинается с первого шага..."},
                new RoadMapViewModel {TotalSum = 20000, Title = "Получен второй платёж!", Text = "Даже путь в тысячу ли начинается со второго шага..."}
            };

            var model = new FundRisingViewModel()
            {
                Content = content,
                PaymentsAndGifts = paymentsAndGifts,
                RoadMaps = roadMaps,
                Guid = Guid.NewGuid()
            };

            if (!isPartial)
            {
                return View("FundRising", model);
            }

            return PartialView("FundRising", model);
        }

        public string ConnectToYandexMoney()
        {
            var reqPost = WebRequest.Create(YandexMoneyHelper.GetTokenRequestUrl());

            var data = "client_id=" + YaMoneyNet.ClientId + "&response_type=code" +
                       "&redirect_uri=" + YaMoneyNet.RedirectUri + "&scope=" + YaMoneyNet.Scope;

            reqPost.Method = "POST"; // Устанавливаем метод передачи данных в POST
            reqPost.Timeout = 120000; // Устанавливаем таймаут соединения
            reqPost.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            ServicePointManager.Expect100Continue = false;

            var sentData = Encoding.UTF8.GetBytes(data);
            reqPost.ContentLength = sentData.Length;
            var sendStream = reqPost.GetRequestStream();

            // Выполняем запрос
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();

            // Получаем результат
            var result = reqPost.GetResponse();

            var resultString = "";

            using (var reader = new StreamReader(result.GetResponseStream()))
            {
                resultString = reader.ReadToEnd();
            }

            return resultString;
        }

        public ActionResult GetToken()
        {
            var temporaryToken = Request.Params["code"];

            var error = YandexMoneyHelper.GetAccessTokenFromTemporaryToken(temporaryToken);

            if (error != null)
            {
                throw new Exception(string.Format("Ошибка при получении токена: {0}", error));
            }

            Yandex.SaveAccesToken(_yaMoneyNet.AccessToken);
            Session.Add("accessToken", _yaMoneyNet.AccessToken);

            return RedirectToAction("AboutProject");
        }

        [RequireHttps]
        [HttpPost]
        public void SendEmailToPayment(string email)
        {
            email = Request.Form["email"];

            UserDataOperations.SaveUserData(new UserData { Email = email });
        }

        [HttpGet]
        public string GetBalance()
        {
            return GetBalanceValue();
        }

        [NonAction]
        public string GetBalanceValue()
        {
            if (_yaMoneyNet.AccessToken == null)
            {
                return "0";
            }

            var accountInfo = YandexMoneyHelper.GetAccountInfo();

            var o = JObject.Parse(accountInfo);
            var balance = (string)o.SelectToken("balance");

            balance = balance.Split('.')[0];

            return balance;
        }

        [NonAction]
        private string GetTokenFromSession()
        {
            if (Session == null)
            {
                return Yandex.GetLastToken();
            }

            var token = Session["accessToken"] ?? Yandex.GetLastToken();
            
            return token.ToString();
        }
    }
}
