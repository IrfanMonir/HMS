using HMS.Services;
using HMSNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMSNew.Controllers
{
    public class AccomodationsController : Controller
    {
        AccomodationTypeService accomodationTypeService = new AccomodationTypeService();
        AccomodationPackageService accomodationPackageService = new AccomodationPackageService();
        AccomodationService accomodationService = new AccomodationService();
        public ActionResult Index(int accomodationTypeId, int? accomodationPackageId)
        {
            AccomodationViewModel model = new AccomodationViewModel();
            model.AccomodationTypes = accomodationTypeService.GetAccomodationTypeId(accomodationTypeId);
            model.AccomodationPackages = accomodationPackageService.GetAllAccomodationPackageByAccomodationType(accomodationTypeId);
            model.selectedAccomodationPackageId = accomodationPackageId.HasValue ? accomodationPackageId.Value : model.AccomodationPackages.First().Id;
            model.Accomodations = accomodationService.GetAllAccomodationByAccomodationPackage(model.selectedAccomodationPackageId);
            return View(model);
        }

        public ActionResult Details(int accomodationPackageId)
        {
            AccomodationPackageDetailsViewModel model = new AccomodationPackageDetailsViewModel();
            model.AccomodationPackage = accomodationPackageService.GetAccomodationPackageId(accomodationPackageId);
            return View(model);
        }
    }
}