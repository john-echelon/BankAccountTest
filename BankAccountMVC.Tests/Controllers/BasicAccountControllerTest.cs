using System;
using NUnit.Framework;
using Moq;
using BankAccountMVC.Controllers;
using BankAccountBL.Abstract;
using BankAccountDB.Concrete.Entities;
using System.Web.Mvc;
using TestUtilities.Helpers;
namespace BankAccountMVC.Test.Controllers
{
    [TestFixture]
    public class BasicAccountControllerTest
    {
        BasicAccountController controller;
        Mock<IAccountManager> mockManager;
        UserProfile testUser;
        [SetUp]
        public void Setup() { 

            testUser = new UserProfile
            {
                UserProfileID = 1,
                UserName = "AnnieUser",
                Accounts = null
            };
            mockManager = new Mock<IAccountManager>();
            mockManager.Setup(m => m.GetUserProfile(It.IsAny<int>())).Returns(testUser);
            mockManager.Setup(m => m.CurrentUser).Returns(testUser);
            controller = new BasicAccountController(mockManager.Object);
        }
        [Test]
        public void Create_GivenAUserProfile_NewBankAccountPassedToView()
        {
            //Arrange
            var expectedViewName = "Edit";
            //Act
            var result = controller.Create() as ViewResult;
            //Assert
            AssertEquals.Equals(result.Model, testUser);
            Assert.AreEqual(expectedViewName, result.ViewName);
        }

        [Test]
        public void Edit_BankAccountInfoSubmitted_UpdateBasicAccount()
        {
            //Arrange
            var model = new BasicAccount
            {
                BasicAccountID = 1,
                UserProfileID = testUser.UserProfileID,
                StatusOfAccount = BasicAccount.AccountStatus.Open,
                Balance = 90000
            };

            //Act
            var result = controller.Edit(model) as ViewResult;

            //Assert
            mockManager.Verify(m => m.UpdateBankAccount());
            mockManager.Verify(m => m.Save());
        }

        [Test]
        public void Edit_BankAccountInfoSubmitted_RedirectsToIndex()
        {
            //Arrange
            var expectedViewName = "Index";

            var model = new BasicAccount
            {
                BasicAccountID = 1,
                UserProfileID = testUser.UserProfileID,
                StatusOfAccount = BasicAccount.AccountStatus.Open,
                Balance = 90000
            };

            //Act
            var result = controller.Edit(model) as RedirectToRouteResult;

            //Assert
            Assert.AreEqual(expectedViewName, result.RouteValues["Action"]);
        }


        [Test]
        public void Edit_InvalidBankAccountInfoSubmitted_RemainOnEditPage()
        {
            //Arrange
            var model = new BasicAccount
            {
                BasicAccountID = 1,
                UserProfileID = -9,
                StatusOfAccount = BasicAccount.AccountStatus.Open,
                Balance = 90000
            };

            //Act
            var result = controller.Edit(model) as ViewResult;

            //Assert
            Assert.NotNull(result, "Not a redirect result");
        }
    }
}
