using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using University_Registration_System_Current_.Controllers;
using University_Registration_System_Current_.Data_Access_Layer;
using UniversitySystemRegistration.Business_Logic;
using UniversitySystemRegistration.Data_Access_Layer;

namespace University_Registration_System_Current_
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<IUserDAL,UserDAL>();
            container.RegisterType<IPasswordHashing, PasswordHashing>();
            container.RegisterType<IStudentServices, StudentServices>();
            container.RegisterType<IUserServices, UserServices>();
            container.RegisterType<IDatabaseManipulation, DatabaseManipulation>();
            container.RegisterType<IUserDAL, UserDAL>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}