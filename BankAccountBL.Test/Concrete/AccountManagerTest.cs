using System;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using Moq;
using BankAccountDB.Abstract;
using BankAccountBL.Concrete;
using BankAccountDB.Concrete.Entities;
using TestUtilities.Helpers;
namespace BankAccountBL.Test.Concrete
{
    [TestFixture]
    public class AccountManagerTest
    {
        private AccountManager manager;
        private BasicAccount decoyAcct;
        private UserProfile decoyUser;
        private int nonExistentAccountID = -9;

        Mock<IBankAccountRepo> mockRepo;
        [SetUp]
        public void BankAccountTestSetup()
        {
            //Arrange
            decoyAcct = new BasicAccount
            {
                BasicAccountID = 28,
                Balance = 2000,
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
            }.ToList();

            decoyUser = new UserProfile
            {
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
            }.ToList();


            mockRepo = new Mock<IBankAccountRepo>();
            mockRepo.Setup(m => m.UserProfiles).Returns(
               decoyUsers
            );

            mockRepo.Setup(m => m.UserProfileFindByID(1)).Returns(decoyUser);
            mockRepo.Setup(m => m.BasicAccounts).Returns(decoyAccounts);

            manager = new AccountManager(mockRepo.Object);
        }

        [Test]
        public void GetUserProfile_GivenAUserProfileIDExists_CurrentUserSet()
        {
            /*
             * This demonstrates the focuse that we are not validating the result from the repo but what the manager does with it. 
             * Other than for demonstration, this particular implementation would be too trivial to test.
             */
            //Arrange
            var expected = decoyUser;
            //Act
            manager.GetUserProfile(decoyUser.UserProfileID);
            var actual = manager.CurrentUser;
            //Assert
            Assert.AreSame(expected, actual);
            mockRepo.Verify(db => db.UserProfileFindByID(decoyUser.UserProfileID));
        }

        [Test]
        public void GetUserProfile_GivenANonExistentUserProfileID_ExpectNull()
        {
            //Act
            manager.GetUserProfile(12);
            var actual = manager.CurrentUser;

            //Assert
            Assert.IsNull(actual);
            mockRepo.Verify(db => db.UserProfileFindByID(12));
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
            manager.GetUserProfile(decoyUser.UserProfileID);
            //alternatively the arrange setup could have been: manager.CurrentUser = decoyUser;

            //Act
            var actual = manager.CreateBankAccount();

            //Assert
            /* 
             * Problem: The line below will use the default Object.Equals implementation which compares if the references are the same, this is not what we intended as an equality check
             * Assert.AreSame(expected, actual);
             * 
             * Solution: Override the Equals for this class or base class. Otherwise build a helper method as in this example.
             */
            AssertEquals.PropertyValuesAreEquals(expected, actual);
        }

        [Test]
        public void GetBankAccount_GivenAnExistingAccountID_FetchAccount()
        {
            //Arrange
            manager.CurrentUser = decoyUser;
            var expected = decoyAcct;
            //Act
            var actual = manager.GetBankAccount(decoyAcct.BasicAccountID);

            //Assert
            Assert.AreSame(expected, actual);
        }

        [Test]
        public void GetBankAccount_GivenAnNonExistentAccountID_FetchAccount()
        {
            //Arrange
            manager.CurrentUser = decoyUser;
            var expected = decoyAcct;
            //Act
            var actual = manager.GetBankAccount(nonExistentAccountID);

            //Assert
            Assert.IsNull(actual);
        }

        [Test]
        public void Withdraw_GivenAnAmount_WithdrawAndUpdateTheBalance()
        {
            //Arrange
            manager.CurrentUser = decoyUser;
            var accountToManage = manager.GetBankAccount(decoyAcct.BasicAccountID);
            var expectedBalance = 1500m;
            //Act
            manager.Withdraw(500);

            //Assert
            Assert.AreEqual(expectedBalance, accountToManage.Balance);
        }

        [Test]
        public void Withdraw_GivenAnAmountGreaterThanTheBalance_ThrowAnExceptionForOverdraftProtection()
        {
            //Arrange
            /*
             * We could have setup the data via the typical repo setup, but technically we would no longer but unit testing. 
             * In this manager implementation we simply setup the current Account data directly.
             * var user = manager.GetUserProfile(decoyUser.UserProfileID);
             * var accountToManage = manager.GetBankAccount(decoyAcct.BasicAccountID);
             */
            manager.CurrentAccount = decoyAcct;
            //Act, Assert
            Exception ex = Assert.Throws<Exception>(() => manager.Withdraw(20000));

            Assert.That(ex.Message, Is.EqualTo("Overdraft protection enacted!"));
        }

        [Test]
        public void Deposit_GivenAnAmount_DepositAndUpdateTheBalance()
        {
            //Arrange

            /*
             * We could have setup the data via the typical repo setup, but technically we would no longer but unit testing. 
             * In this manager implementation we simply setup the current Account data directly.
             * var user = manager.GetUserProfile(decoyUser.UserProfileID);
             * var accountToManage = manager.GetBankAccount(decoyAcct.BasicAccountID);
             */
            manager.CurrentAccount = decoyAcct;
            var expectedBalance = 2500;
            //Act
            manager.Deposit(500);

            //Assert
            Assert.AreEqual(expectedBalance, manager.CurrentAccount.Balance);
        }
    }
}
