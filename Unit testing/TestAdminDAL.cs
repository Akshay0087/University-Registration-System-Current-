using Moq;
using NUnit.Framework;
using RepositoryLibrary.DataAccessLayer;
using University_Registration_System_Current_.Controllers;
using UniversitySystemRegistration.Models;

namespace Unit_testing
{
    public class Tests
    {


        public Mock<IAdminDAL> mock = new Mock<IAdminDAL>();


        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SetStudentStatus()
        {

            mock.Setup(p => p.SetStudentStatus()).Returns(true);
            AdminController studentController = new AdminController(mock.Object);
            string result = await emp.GetEmployeeById(1);
            Assert.Equal("JK", result);
        }
    }
}