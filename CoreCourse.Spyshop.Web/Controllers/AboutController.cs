﻿using CoreCourse.Spyshop.Domain.Settings;
using CoreCourse.Spyshop.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace CoreCourse.Spyshop.Web.Controllers
{
    public class AboutController : Controller
    {
        //holds the options under the SpyShopConfig configurationnode
        private SpyShopConfig spyShopConfig;

        //constructor using dependency injection to get our specific options
        public AboutController(IOptionsSnapshot<SpyShopConfig> spyshopconfig)
        {
            spyShopConfig = spyshopconfig.Value;
        }

        public IActionResult Index()
        {
            //Create the View Model
            var aboutIndexViewModel = new AboutIndexViewModel();

            //Populate View Model
            aboutIndexViewModel.ContactEmail = spyShopConfig.MailSettings.PublicInfoAddress;
            aboutIndexViewModel.CompanyFullName = spyShopConfig.FullCompanyName;
            aboutIndexViewModel.AboutTitle = "Welcome to Spy Shop";
            aboutIndexViewModel.AboutContent = "<p>We deliver premium gadgets to help all Clouseaus and Bonds out there.<br />To start, have a look at the <a href=\"/\">homepage</a>!</p>";

            //return View Model to the Index.cshtml view, by using the overloaded View() method
            return View(aboutIndexViewModel);
        }
    }
}