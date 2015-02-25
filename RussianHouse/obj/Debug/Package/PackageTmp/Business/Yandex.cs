using System;
using System.Linq;
using RussianHouse.Models;

namespace RussianHouse.Business
{
    public class Yandex
    {
        public void SaveAccesToken(string accessToken)
        {
            using (var db = new YandexContext())
            {
                db.YandexToken.Add(new YandexToken { Token = accessToken, Date = DateTime.Now });
                db.SaveChanges();
            }
        }

        public string GetLastToken()
        {
            YandexToken token;

            using (var db = new YandexContext())
            {
                token = db.YandexToken.OrderByDescending(t => t.Date).FirstOrDefault();

                if (token == null)
                {
                    return null;
                }
            }

            return token.Token;
        }
    }
}