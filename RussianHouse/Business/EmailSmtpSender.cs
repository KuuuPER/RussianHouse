using System.Net;
using System.Net.Mail;
using RussianHouse.Models;

namespace RussianHouse.Business
{
    public class EmailSmtpSender
    {
        public void SendToAll()
        {
            using (var db = new UserDatasContext())
            {
                var userDatas = db.UserDatas;

                foreach (var userData in userDatas)
                {
                    if (!userData.IsMailWasSend)
                    {
                        var message = new MailMessage();
                        message.To.Add(new MailAddress(userData.Email));
                        message.Subject = "Русский Дом";
                        message.Body = "приглашаем Вас в наше уникальное заведение! Русский Дом! Он Дом и он Русский. Он русский и он Дом!";
                        var client = new SmtpClient("smtp.mail.ru");
                        client.Port = 25;
                        client.EnableSsl = true;

                        client.Credentials =
                            new NetworkCredential("kuuuper@mail.ru", "22Ptvkzybrb");

                        client.Send(message);
                    }

                    userData.IsMailWasSend = true;
                }

                db.SaveChanges();
            }
        }
    }
}