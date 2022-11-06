using RepositoryLibrary.DataAccessLayer;
using ServiceLibrary.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;
using UniversitySystemRegistration;
using UniversitySystemRegistration.Repository;
using UniversitySystemRegistration.Services;

namespace University_Registration_System_Current_
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            container.RegisterType<IUserDAL,UserDAL>();
            container.RegisterType<IStudentServices, StudentServices>();
            container.RegisterType<IUserServices, UserServices>();
            container.RegisterType<IDatabaseConnection, DatabaseConnection>();
            container.RegisterType<IStudentDAL, StudentDAL>();
            container.RegisterType<IAdminDAL, AdminDAL>();
            container.RegisterType<IAdminServices, AdminServices>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}