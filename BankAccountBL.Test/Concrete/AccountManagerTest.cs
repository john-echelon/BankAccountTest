using System;
using System.Linq;
using System.Collections.Generic;
using BankAccountBL.Concrete;
using NUnit.Framework;
using Moq;
using BankAccountDB.Abstract;
using BankAccountBL.Concrete;
using BankAccountDB.Concrete.Entities;
namespace BankAccountBL.Test.Concrete
{
    [TestFixture]
    public class AccountManagerTest
    {
        AccountManager manager;
        BasicAccount decoyAcct;
        UserProfile decoyUser;

        [SetUp]
        public void BankAccountTestSetup()
        {
            //Arrange
            decoyAcct = new BasicAccount
            {
                BasicAccountID = 28,
                Balance = 1800,
                AccountType = BasicAccount.BankType.RegularChecking,
                StatusOfAccount = BasicAccount.AccountStatus.Open
            };

            var decoyAccounts = new BasicAccount[]{ 
               
                new BasicAccount
                {
                    BasicAccountID = 26,
                    Balance = 19000,
                    AccountType = BasicAccount.BankType.RegularSavings,
                    StatusOfAccount = BasicAccount.AccountStatus.Open
                },
                new BasicAccount
                {
                    BasicAccountID = 27,
                    Balance = 50000,
                    AccountType = BasicAccount.BankType.PremiumSavings,
                    StatusOfAccount = BasicAccount.AccountStatus.Open
                },
                decoyAcct
            }.AsQueryable();

            decoyUser = new UserProfile {
                    UserProfileID = 1,
                    UserName = "AnnieUser",
                    Accounts = decoyAccounts.ToList()
            };

            var decoyUsers = new UserProfile[]{
                decoyUser,
                new UserProfile {                               
                    UserProfileID = 2,
                    UserName = "DecoyOctopus",
                    Accounts = null
                }
            }.AsQueryable();


            Mock<IBankAccountRepo> mockRepo = new Mock<IBankAccountRepo>();
            mockRepo.Setup(m => m.UserProfiles).Returns(
               decoyUsers
            );
            //mockRepo.Setup(m => m.BasicAccounts).Returns(decoyAccounts);

            manager = new AccountManager(mockRepo.Object);
        }

        [Test]
        public void GetUserProfile_GivenAUserProfileIDExists_FetchAUser()
        {
            //Arrange
            var expected = decoyUser;
            //Act
            var actual = manager.GetUserProfile(1);

            //Assert
            Assert.AreSame(expected, actual);
        }

        [Test]
        public void GetUserProfile_GivenANonExistentUserProfileID_ExpectNull()
        {
            //Act
            var actual = manager.GetUserProfile(12);

            //Assert
            Assert.IsNull(actual);
        }


        [Test]
        public void CreateBankAccount_OnCreation_CreateNewAccount()
        {
            //Arrange 
            var expected = new BasicAccount
            {
                UserProfileID = decoyUser.UserProfileID,
                Balance = 0.0m,
                InterestRate = 0,
                StatusOfAccount = BasicAccount.AccountStatus.Open
            };

            //Note this statement is now part of the arrange;
            manager.GetUserProfile(1);
            //alternatively the arrange setup could have been: manager.CurrentUser = decoyUser;

            //Act
            var actual = manager.CreateBankAccount();
        }

        [Test]
        public void GetBankAccount_GivenAnExistingAccountID_FetchAccount()
        {
            //Arrange
            var user = manager.GetUserProfile(1);
            var expected = decoyAcct;
            //Act
            var actual = manager.GetBankAccount(28);

            //Assert
            Assert.AreSame(expected, actual);
        }

        [Test]
        public void GetBankAccount_GivenAnNonExistentAccountID_FetchAccount()
        {
            //Arrange
            var user = manager.GetUserProfile(1);
            var expected = decoyAcct;
            //Act
            var actual = manager.GetBankAccount(29);

            //Assert
            Assert.IsNull(actual);
        }

        [Test]
        public void Withdraw_GivenAnAmount_WithdrawAndUpdateTheBalance() { 
        }

        [Test]
        public void Withdraw_GivenAnAmountGreaterThanTheBalance_ThrowAnExceptionForOverdraftProtection() { }

        [Test]
        public void Withdraw_GivenAnAmount_DepositAndUpdateTheBalance() { }
    }
}
