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
            manager.GetUserProfile(defaultUserId);
        }
        //
        // GET: /BasicAccount/
        public ActionResult Index()
        {
            return View(manager.CurrentUser);
        }

        public ActionResult Create()
        {
            ViewBag.Title = "Create Account";

            var model = manager.CreateBankAccount();

            return View("Edit", model);
        }

        public JsonResult CreateJson()
        {
            ViewBag.Title = "Create Account";

            var model = manager.CreateBankAccount();

            return Json( new{ Success= true, Model= model});
        }

        [HttpPost]
        public ActionResult Edit(BasicAccount model)
        {
            ViewBag.Title = "Edit Account";
            if (model.UserProfileID <= 0) {
                this.ViewData.ModelState.AddModelError("UserProfileID", "No user profile associated to this account");
            }


            if (ModelState.IsValid)
            {
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

            var model = manager.GetBankAccount(id);
            return View(model);
        }

        // GET: /BasicAccount/Delete/5
        public ActionResult Delete(int id)
        {
            var model = manager.DeleteBankAccount(id);
            manager.Save();
            return RedirectToAction("Index");
        }

    }
}
