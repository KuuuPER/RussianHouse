namespace RussianHouse.YandexMoney
{
    public class YaMoneyNet
    {

        ///  Задаем константы для работы с API
        public static string ClientId = "";
        public static string SecretId = "";
        public static string RedirectUri = "http://rdom.azurewebsites.net/Home/GetToken";
        public static string Scope = "account-info";


        public string AccessToken { get; set; }
    }
}