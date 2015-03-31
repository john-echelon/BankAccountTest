# BankAccountTest
This is a simple sln that attempts to show by example unit testing concepts such as mocking and dependency injection.  

BankAccountBL.Test\Concrete\CalculatorTest.cs
---------------------------------------------
A trivial test class used as introductory. Covers the following:
* Test Method Naming Conventions
* Arrange Act Assert
* Examples of using Nunit's Assert class

BankAccountBL.Test\Concrete\AccountManagerTest.cs
---------------------------------------------
A less trivial example that attempts to show the use of a mock, to fake the dependency of a database. Specifically, the loose coupling between the *AccountManager* which (has-a) dependency on the repo class, *BankAccountRepo*. By exposing an interface of *IBankAccountRepo* we effectively can use a mock in its place during unit testing. Covers the following:

* Introduces mock objects using the Moq framework.
* Introduces dependency injection (e.g. constructor injection).
* Example on how to loosely-couple a class dependency.

BankAccountMVC
---------------------------------------------
This is a functional ASP.NET MVC project, *BankAccountMVC* that will be used in the future to demonstrate complete unit testing from the business layer to the presentation layer. Covers the following:

* [TODO] Introduces inversion of control via Ninject framework.
* [TODO] Examples on how to test the Controller and its actions.

# Setup
1. [Clone from a remote repository in Visual Studio.](https://msdn.microsoft.com/en-us/library/hh850445.aspx#remote)
2. Once the solution is loaded. From the Tools menu, select Library Package Manager and then click Package Manager Console.
3. Set the Default Project to BankAccountDB. 
4. In the console, type the following and hit enter: update-database
5. The previous step will create a database called *BankAccount* and schema based on EF Code First migrations. Add a user in the UserProfile table and you should now be able to run the MVC application.
