using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;

namespace RussianHouse.YandexMoney
{
    public class YandexMoneyHelper
    {
        private readonly YaMoneyNet _yaMoney;

        public YandexMoneyHelper(YaMoneyNet yaMoney)
        {
            _yaMoney = yaMoney;
        }

        public static string GetTokenRequestUrl()
        {
            return "https://sp-money.yandex.ru/oauth/authorize";
        }

        public string GetAccessTokenFromTemporaryToken(string temporaryToken)
        {

            var reqPost = System.Net.WebRequest.Create("https://sp-money.yandex.ru/oauth/token");
            reqPost.Method = "POST"; // Устанавливаем метод передачи данных в POST
            reqPost.Timeout = 120000; // Устанавливаем таймаут соединения
            reqPost.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";

            System.Net.ServicePointManager.Expect100Continue = false;
            // Формируем параметры запроса
            var data = "code=" + temporaryToken + "&client_id=" + YaMoneyNet.ClientId + "&client_secret=" +
                                                               YaMoneyNet.SecretId + "&grant_type=authorization_code&redirect_uri="
                                                               + System.Web.HttpUtility.UrlEncode(YaMoneyNet.RedirectUri, Encoding.UTF8);

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

            // Пытаемся разобрать
            var o = JObject.Parse(resultString);
            // и сохранить токен
            _yaMoney.AccessToken = (string)o.SelectToken("access_token");

            // Если есть ошибка - возвращаем ее
            return (string)o.SelectToken("error");
        }

        public string GetAccountInfo()
        {
            var resultText = "";

            try
            {

                var request = System.Net.WebRequest.Create("https://money.yandex.ru/api/account-info");
                request.Method = "POST"; // Устанавливаем метод передачи данных в POST
                request.Timeout = 120000; // Устанавливаем таймаут соединения
                request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
                request.Headers.Add("Authorization", "Bearer " + _yaMoney.AccessToken);
                request.ContentLength = 0;
                var result = request.GetResponse();


                using (var reader = new StreamReader(result.GetResponseStream()))
                {
                    resultText = reader.ReadToEnd();
                }
            }
            catch (System.Net.WebException ex)
            {
                resultText = "Ошибка: " + ex.Message;

            }
            return resultText;
        }
    }
}