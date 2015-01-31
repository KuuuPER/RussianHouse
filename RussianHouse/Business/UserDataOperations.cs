using RussianHouse.Models;

namespace RussianHouse.Business
{
    public static class UserDataOperations
    {
        public static void SaveUserData(UserData data)
        {
            using (var db = new UserDatasContext())
            {
                db.UserDatas.Add(data);
                db.SaveChanges();
            }
        }
    }
}