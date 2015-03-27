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
        //
        // GET: /BasicAccount/
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /BasicAccount/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /BasicAccount/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /BasicAccount/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /BasicAccount/Edit/5
        public ActionResult Edit(int id)
        {
            var repo = new BankAccountRepo();
            AccountManager am = new AccountManager(repo);
            am.GetBankAccount(new UserProfile{ UserProfileID= 1});

            return View();
        }

        //
        // POST: /BasicAccount/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /BasicAccount/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /BasicAccount/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
