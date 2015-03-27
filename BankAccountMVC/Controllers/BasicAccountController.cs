using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankAccountDB.Concrete;
using BankAccountDB.Concrete.Entities;
using BankAccountBL.Concrete;
namespace BankAccountMVC.Controllers
{
    public class BasicAccountController : Controller
    {
        private BankAccountRepo repo = new BankAccountRepo();
        public BasicAccountController() {
        }
        //
        // GET: /BasicAccount/
        public ActionResult Index()
        {
            AccountManager am = new AccountManager(repo, 1);

            return View(am.CurrentUser);
        }

        public ActionResult Create()
        {
            var repo = new BankAccountRepo();
            AccountManager am = new AccountManager(repo, 1);

            var model = am.CreateBankAccount();

            return RedirectToAction("Edit", model);
        }

        //
        // GET: /BasicAccount/Edit/5
        public ActionResult Edit(BasicAccount model)
        {
            if (model.BasicAccountID != 0) {
                AccountManager am = new AccountManager(repo, 1);
                am.CurrentAccount = model;
                am.Save();
                RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int basicAccountID)
        {
            AccountManager am = new AccountManager(repo, 1);
            var model = am.GetBankAccount(basicAccountID);
            return View(model);
        }
        //
        // GET: /BasicAccount/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

    }
}
