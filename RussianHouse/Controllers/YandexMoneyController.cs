using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RussianHouse.Business;
using RussianHouse.Models;

namespace RussianHouse.Controllers
{
    public class YandexMoneyController : ApiController
    {
        [HttpPost]
        public void SendEmailToPayment(string email)
        {
            UserDataOperations.SaveUserData(new UserData {Email = email, IsMailWasSend = false});
        }
    }
}
