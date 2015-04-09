using System;
using NUnit.Framework;
using Moq;
using TestUtilities.Helpers;
using System.Linq;
using BankAccountDB.Concrete;
using BankAccountDB.Concrete.Entities;
using System.Collections.Generic;
namespace BankAccountDB.Test
{
    [TestFixture]
    public class BankAccountRepoTest
    {
        private BankAccountRepo repo;
        IQueryable<BasicAccount> basicAccountData;
        [SetUp]
        public void SetupDbContext()
        {
            var mockDbContext = new Mock<EFDbContext>();

            basicAccountData = new List<BasicAccount> 
            { 
              new BasicAccount { BasicAccountID= 1, UserProfileID= 1, StatusOfAccount = BasicAccount.AccountStatus.Open },
              new BasicAccount { BasicAccountID= 2, UserProfileID= 1, StatusOfAccount = BasicAccount.AccountStatus.Open },
              new BasicAccount { BasicAccountID= 3,  UserProfileID= 1, StatusOfAccount = BasicAccount.AccountStatus.Open }
            }.AsQueryable();

            var basicAccountSet = DbContextHelper.SetupDbSet(basicAccountData);

            mockDbContext.Setup(m => m.BasicAccounts).Returns(basicAccountSet.Object);

            var userProfilesData = new List<UserProfile> 
            { 
              new UserProfile { UserProfileID= 1, UserName = "Tobias", Accounts = basicAccountData.ToList() },
              new UserProfile { UserProfileID= 2, UserName = "Ubuntu" },
              new UserProfile { UserProfileID= 3, UserName = "Debian" }
            }.AsQueryable();

            var userProfileSet = DbContextHelper.SetupDbSet(userProfilesData);

            userProfileSet.Setup(dbSet => dbSet.Include("Accounts")).Returns(userProfileSet.Object);

            mockDbContext.Setup(m => m.UserProfiles).Returns(userProfileSet.Object);
            
            repo = new BankAccountRepo(mockDbContext.Object);
        }

        [Test]
        public void UserProfiles_GivenUserProfilesExists_GetAllUsers()
        {
            //Arrange
            var expected = new List<UserProfile> 
            { 
              new UserProfile { UserProfileID= 1, UserName = "Tobias", Accounts = basicAccountData.ToList() },
              new UserProfile { UserProfileID= 2, UserName = "Ubuntu" },
              new UserProfile { UserProfileID= 3, UserName = "Debian" }
            }.ToList();

            //Act

            var actual = repo.UserProfiles;
            //Assert
            Assert.AreEqual(actual.Count(), expected.Count());
            AssertEquals.PropertyValuesAreEquals(actual[0], expected[0]);
            AssertEquals.PropertyValuesAreEquals(actual[1], expected[1]);
            AssertEquals.PropertyValuesAreEquals(actual[2], expected[2]);
          
        }

        [Test]
        public void UserProfileFindByID_GivenAUserProfileID_GetUser()
        {
            //Arrange
            var expected = new UserProfile { UserProfileID = 2, UserName = "Ubuntu" };

            //Act
            var actual = repo.UserProfileFindByID(2);

            //Assert
            AssertEquals.PropertyValuesAreEquals(actual, expected);
        }

        [Test]
        public void UserProfileFindByID_GivenAnNonExistentUserProfileID_GetNil()
        {
            //Act
            var actual = repo.UserProfileFindByID(-99);

            //Assert
            Assert.IsNull(actual);
        }
    }

    [TestFixture]
    public class EmptyRepoTest
    {
        private BankAccountRepo repo;
        private IQueryable<UserProfile> userProfilesData;
        [SetUp]
        public void SetupDbContext()
        {
            userProfilesData = new List<UserProfile> { }.AsQueryable();

            var userProfileSet = DbContextHelper.SetupDbSet(userProfilesData);

            userProfileSet.Setup(dbSet => dbSet.Include("Accounts")).Returns(userProfileSet.Object);

            var mockDbContext = new Mock<EFDbContext>();

            mockDbContext.Setup(m => m.UserProfiles).Returns(userProfileSet.Object);

            repo = new BankAccountRepo(mockDbContext.Object);
        }

        [Test]
        public void UserProfiles_GivenNoUserProfilesExists_GetEmptyList()
        {
            //Arrange
            var expected = new List<UserProfile>();

            //Act
            var actual = repo.UserProfiles.ToList();

            //Assert
            Assert.AreEqual(actual.Count(), expected.Count());
        }
    }
}
