using CoreCourse.Spyshop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoreCourse.Spyshop.Web.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            var accountLoginViewModel = new AccountLoginViewModel();
            return View(accountLoginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(AccountLoginViewModel accountLoginViewModel)
        {
            string validUser = "Joe";
            string validPass = "unsafe";

            if (ModelState.IsValid) //if form was filled in correctly
            {
                //check if provided credentials are valid (user: Joe, pas: unsafe)
                if (accountLoginViewModel.Username.Trim().Equals(validUser, StringComparison.InvariantCultureIgnoreCase)
                    &&
                    accountLoginViewModel.Password == validPass)
                {
                    //todo: add authentication code

                    return new RedirectToActionResult("Index", "Home", null);  //redirect to homepage.
                }
                else
                {
                    ModelState.AddModelError(string.Empty,
                           "The credentials you have provided are invalid. Please try again.");
                    return View(accountLoginViewModel);
                }
            }
            else
            {
                return View(accountLoginViewModel);
            }
        }

        public IActionResult Register()
        {
            var accountRegisterViewModel = new AccountRegisterViewModel();
            return View(accountRegisterViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(AccountRegisterViewModel accountRegisterViewModel)
        {
            if (ModelState.IsValid)
            {
                //todo: register user account
                //set tempdata 
                TempData[Constants.SuccessMessage] = $@"Welcome, <b>{accountRegisterViewModel.UserName}</b>.<br />
                    Your account has been registered succesfully. You may now log in.";
                return new RedirectToActionResult("Index", "Home", null);   //redirect to homepage on succesful registration.
            }
            else
            {
                return View(accountRegisterViewModel);
            }
        }
    }
}