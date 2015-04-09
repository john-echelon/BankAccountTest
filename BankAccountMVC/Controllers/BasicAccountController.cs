using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BankAccountDB.Abstract;
using BankAccountDB.Concrete;
using BankAccountDB.Concrete.Entities;
using BankAccountBL.Abstract;
using BankAccountBL.Concrete;
namespace BankAccountMVC.Controllers
{
    public class BasicAccountController : Controller
    {
        private IAccountManager manager;
        private readonly int defaultUserId = 1;
        public BasicAccountController(IAccountManager manager)
        {
            this.manager = manager;
        }
        //
        // GET: /BasicAccount/
        public ActionResult Index(int id = 1)
        {
            manager.GetUserProfile(id);
            return View(manager.CurrentUser);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Create Account";
            manager.GetUserProfile(defaultUserId);

            var model = manager.CreateBankAccount();

            return View("Edit", model);
        }

        public JsonResult CreateJson()
        {
            ViewBag.Title = "Create Account";
            manager.GetUserProfile(defaultUserId);

            var model = manager.CreateBankAccount();

            return Json( new{ Success= true, Model= model});
        }

        [HttpPost]
        public ActionResult Edit(BasicAccount model)
        {
            ViewBag.Title = "Edit Account";
            if (ModelState.IsValid)
            {
                manager.GetUserProfile(defaultUserId);
                manager.CurrentAccount = model;

                //Save and Redirect to Index
                manager.UpdateBankAccount();
                manager.Save();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        // GET: /BasicAccount/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.Title = "Edit Account";

            manager.GetUserProfile(defaultUserId);
            var model = manager.GetBankAccount(id);
            return View(model);
        }

        // GET: /BasicAccount/Delete/5
        public ActionResult Delete(int id)
        {
            manager.GetUserProfile(defaultUserId);
            var model = manager.DeleteBankAccount(id);
            manager.Save();
            return RedirectToAction("Index");
        }

    }
}
