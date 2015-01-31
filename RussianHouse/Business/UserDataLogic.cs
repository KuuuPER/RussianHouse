using RussianHouse.Models;

namespace RussianHouse.Business
{
    public class UserDataLogic
    {
        public void AddUserData(string email)
        {
            using (var db = new UserDatasContext())
            {
                db.UserDatas.Add(new UserData {Email = email});
                db.SaveChanges();
            }
        }
    }
}