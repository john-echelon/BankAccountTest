# Setup
1. [Clone from a remote repository in Visual Studio.](https://msdn.microsoft.com/en-us/library/hh850445.aspx#remote)
2. Once the solution is loaded. From the Tools menu, select Library Package Manager and then click Package Manager Console.
3. Set the Default Project to BankAccountDB. 
4. In the console, type the following and hit enter: update-database
5. The previous step will create a database called *BankAccount* and schema based on EF Code First migrations. A user should automatically be seeded in the UserProfile table and you should now be able to run the MVC application.
6. 
# BankAccountTest
This is a simple Bank Account Manage ASP.NET MVC application to demonstrate coverage of unit tests from the repository layer, the business layer to the presentation layer, introducing concepts such as mocking and dependency injection.  Upon studying this project you will learn that the goals of EF6 and ASP.NET MVC was designed to be written for unit testable solutions.

BankAccountBL.Test\Concrete\CalculatorTest.cs
---------------------------------------------
Tests the BankAccountBL\Concrete\Calculator.cs

For a starters we have, a trivial test class used as introductory. Covers the following:
* Test Method Naming Conventions
* Arrange Act Assert
* Examples of using Nunit's Assert class

BankAccountBL.Test\Concrete\AccountManagerTest.cs
---------------------------------------------
Tests the BankAccountBL\Concrete\AccountManager.cs

A less trivial example that attempts to show the use of a mock to fake the dependency of a database. Specifically, the association between the *AccountManager* now (has-a) loose dependency on the repo class, *BankAccountRepo*. By exposing an interface of *IBankAccountRepo* to the *AccountManager* we effectively can use a mock in its place when we are unit testing. Covers the following:

* Introduces mock objects using the Moq framework.
* [Quickstart to Moq.](https://github.com/Moq/moq4/wiki/Quickstart)
* Introduces dependency injection (via constructor injection).
* Example on how to loosely-couple a class dependency.

BankAccountDB.Test\Concrete\BankAccountRepoTest.cs
---------------------------------------------
Tests the BankAccountDB\Concrete\BankAccountRepo.cs

This class demonstrates how to write your own in memory representation of the EF context/DbSet<T> resulting in near complete mocking of the Entity Framework. This allows you to fully test your repository/domain layer without dependency of an actual database to tie into your unit testing. Covers the following:
* Demonstrates how to use a mocking framework (such as Moq) to place in-memory implementations of your context and sets created dynamically at runtime for you.
* [Testing with a mocking framework (EF6 onwards).](https://msdn.microsoft.com/en-us/data/dn314429.aspx)

BankAccountMVC.Test\Controllers\BasicAccountControllers\BasicAccountControllerTest.cs
---------------------------------------------
Tests BankAccountMVC\Controllers\BasicAccountControllers\BasicAccountController.cs

An example of testing the Controller and its actions. Covers the following:
* Introduces inversion of control via Ninject framework (BankAccountMVC\App_Start\NinjectWebCommon.cs)
* [NinjectWebCommon Setup, also applicable to Ninject.MVC5] (https://github.com/ninject/Ninject.Web.Mvc/wiki/Setting-up-an-MVC3-application)
* Testing Actions: the ViewResult, asserting the ViewName and the Model that is passed to the ViewResult.
* Asserting against ModelValidations.
* Using mock Verification.
* 

BankAccountMVC.Test\RouteTests.cs
---------------------------------------------
Tests BankAccountMVC\App_Start\RouteConfig.cs

Tests incoming route urls based on RouteConfig settings.
* Demonstrates how to test for matching and non matching segments defined by the RouteConfig.
