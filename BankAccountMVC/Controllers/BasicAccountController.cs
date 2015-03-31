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
        private AccountManager am;
        public BasicAccountController() {
            am = new AccountManager(repo);
        }
        //
        // GET: /BasicAccount/
        public ActionResult Index(int id = 1)
        {
            am.GetUserProfile(id);
            return View(am.CurrentUser);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Create Account";
            am.GetUserProfile(1);

            var model = am.CreateBankAccount();

            return View("Edit", model);
        }

        public JsonResult CreateJson()
        {
            ViewBag.Title = "Create Account";
            am.GetUserProfile(1);

            var model = am.CreateBankAccount();

            return Json( new{ Success= true, Model= model});
        }

        [HttpPost]
        public ActionResult Edit(BasicAccount model)
        {
            ViewBag.Title = "Edit Account";
            if (ModelState.IsValid)
            {
                am.GetUserProfile(1);
                am.CurrentAccount = model;

                //Save and Redirect to Index
                am.UpdateBankAccount();
                am.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: /BasicAccount/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit Account";

            am.GetUserProfile(1);
            var model = am.GetBankAccount(id);
            return View(model);
        }

        // GET: /BasicAccount/Delete/5
        public ActionResult Delete(int id)
        {
            am.GetUserProfile(1);
            var model = am.DeleteBankAccount(id);
            am.Save();
            return RedirectToAction("Index");
        }

    }
}
