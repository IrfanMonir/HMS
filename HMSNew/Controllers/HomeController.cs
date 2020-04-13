using HMS.Services;
using HMSNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMSNew.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel();
            AccomodationTypeService service = new AccomodationTypeService();
            AccomodationPackageService accomodationPackageService = new AccomodationPackageService();
            model.AccomodationTypes = service.GetAllAccomodationType();
            model.AccomodationPackages = accomodationPackageService.GetAllAccomodationPackage();
            return View(model);
        }

      
    }
}