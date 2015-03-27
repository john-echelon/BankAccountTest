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

            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(BasicAccount model)
        {
            if (ModelState.IsValid)
            {
                AccountManager am = new AccountManager(repo, 1);
                am.CurrentAccount = model;
                am.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            AccountManager am = new AccountManager(repo, 1);
            var model = am.GetBankAccount(id);
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
