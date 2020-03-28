using HMS.Services;
using HMSEntities;
using HMSNew.Areas.Dashboard.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMSNew.Areas.Dashboard.Controllers
{
    public class AccomodationPackagesController : Controller
    {
        AccomodationPackageService accomodationPackageService = new AccomodationPackageService();
        AccomodationTypeService accomodationTypeService = new AccomodationTypeService();
        // GET: Dashboard/AccomodationTypes
        public ActionResult Index(string searchTerm)
        {

            AccomodationPackageListingModel model = new AccomodationPackageListingModel();

            model.SearchTerm = searchTerm;
            model.AccomodationPackages = accomodationPackageService.SearchAccomodationPackage(searchTerm);
            return View(model);
        }
        //we used it for listing before and later updated in index
        //public ActionResult Listing()
        //{
        //    AccomodationTypeListingModel model = new AccomodationTypeListingModel();
        //    model.AccomodationTypes = accomodationTypeService.GetAllAccomodationType();
        //    return PartialView("_Listing", model);
        //}
        [HttpGet]
        public ActionResult Action(int? Id)
        {
            AccomodationPackageActionModel model = new AccomodationPackageActionModel();

            if (Id.HasValue)//editing a record
            {
                var accomodationPackage = accomodationPackageService.GetAccomodationPackageId(Id.Value);
                model.Id = accomodationPackage.Id;
                model.Name = accomodationPackage.Name;
                model.NoOfRoom = accomodationPackage.NoOfRoom;
                model.FeePerNight = accomodationPackage.FeePerNight;
                model.AccomodationTypeId = accomodationPackage.AccomodationTypeId;
            }
            model.AccomodationTypes = accomodationTypeService.GetAllAccomodationType();
            return PartialView("_Action", model);
            //else//creating a new record
            //{

            //}


        }
        [HttpPost]

        public JsonResult Action(AccomodationPackageActionModel model)
        {

            JsonResult json = new JsonResult();
            var result = false;
            if (model.Id > 0)//editing a record
            {
                var accomodationPackage = accomodationPackageService.GetAccomodationPackageId(model.Id);
                accomodationPackage.Name = model.Name;
                accomodationPackage.NoOfRoom = model.NoOfRoom;
                accomodationPackage.FeePerNight = model.FeePerNight;
                accomodationPackage.AccomodationTypeId = model.AccomodationTypeId;
                result = accomodationPackageService.UpdateAccomodationPackage(accomodationPackage);
            }
            else//creating a new record
            {
                AccomodationPackage accomodationPackage = new AccomodationPackage();
                accomodationPackage.Name = model.Name;
                accomodationPackage.NoOfRoom = model.NoOfRoom;
                accomodationPackage.FeePerNight = model.FeePerNight;
                accomodationPackage.AccomodationTypeId = model.AccomodationTypeId;

                result = accomodationPackageService.SaveAccomodationPackage(accomodationPackage);
            }

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation Type" };
            }
            return json;
        }
        [HttpGet]
        public ActionResult Delete(int Id)
        {
            AccomodationPackageActionModel model = new AccomodationPackageActionModel();
            var accomodationPackage = accomodationPackageService.GetAccomodationPackageId(Id);
            model.Id = accomodationPackage.Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccomodationPackageActionModel model)
        {

            JsonResult json = new JsonResult();
            var result = false;

            var accomodationPackage = accomodationPackageService.GetAccomodationPackageId(model.Id);

            result = accomodationPackageService.DeleteAccomodationPackage(accomodationPackage);

            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to perform action on Accomodation Type" };
            }
            return json;
        }
    }
}