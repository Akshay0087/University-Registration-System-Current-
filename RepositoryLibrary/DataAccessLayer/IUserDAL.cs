using UniversitySystemRegistration.Models;

namespace UniversitySystemRegistration.Repository
{
    public interface IUserDAL
    {
        bool LoginCheck(User userData);
        bool UserInfoCheck(User userData);
        bool insertUserDataInDb(User user);

        User GetUserDataInDb(User user);
    }
}