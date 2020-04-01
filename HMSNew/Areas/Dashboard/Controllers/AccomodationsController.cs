using HMS.Services;
using HMSEntities;
using HMSNew.Areas.Dashboard.ViewModel;
using HMSNew.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMSNew.Areas.Dashboard.Controllers
{
    public class AccomodationsController : Controller
    {
        AccomodationService accomodationService = new AccomodationService();
        AccomodationPackageService accomodationPackageService = new AccomodationPackageService();
        // GET: Dashboard/AccomodationTypes
        public ActionResult Index(string searchTerm, int? accomodationPackageId, int? page)
        {
            int recordSize = 3;
            page = page ?? 1;
            AccomodationsListingModel model = new AccomodationsListingModel();

            model.SearchTerm = searchTerm;
            model.AccomodationPackageId = accomodationPackageId;
            model.Accomodations = accomodationService.SearchAccomodation(searchTerm, accomodationPackageId, page.Value, recordSize);
            model.AccomodationPackages = accomodationPackageService.GetAllAccomodationPackage();

            var totalRecords = accomodationService.SearchAccomodationCount(searchTerm, accomodationPackageId);
            model.Pager = new Pager(totalRecords, page, recordSize);
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
            AccomodationActionModel model = new AccomodationActionModel();

            if (Id.HasValue)//editing a record
            {
                var accomodation = accomodationService.GetAccomodationId(Id.Value);
                model.Id = accomodation.Id;
                model.Name = accomodation.Name;
                model.Description = accomodation.Description;
             
                model.AccomodationPackageId = accomodation.AccomodationPackageId;
            }
            model.AccomodationPackages = accomodationPackageService.GetAllAccomodationPackage();
            return PartialView("_Action", model);
            //else//creating a new record
            //{

            //}


        }
        [HttpPost]

        public JsonResult Action(AccomodationActionModel model)
        {

            JsonResult json = new JsonResult();
            var result = false;
            if (model.Id > 0)//editing a record
            {
                var accomodation = accomodationService.GetAccomodationId(model.Id);
                accomodation.Name = model.Name;
                accomodation.Description = model.Description;
               
                accomodation.AccomodationPackageId = model.AccomodationPackageId;
                result = accomodationService.UpdateAccomodation(accomodation);
            }
            else//creating a new record
            {
                Accomodation accomodation = new Accomodation();
                accomodation.Name = model.Name;
                accomodation.Description = model.Description;

                accomodation.AccomodationPackageId = model.AccomodationPackageId;

                result = accomodationService.SaveAccomodation(accomodation);
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
            AccomodationActionModel model = new AccomodationActionModel();
            var accomodation = accomodationService.GetAccomodationId(Id);
            model.Id = accomodation.Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public JsonResult Delete(AccomodationActionModel model)
        {

            JsonResult json = new JsonResult();
            var result = false;

            var accomodation = accomodationService.GetAccomodationId(model.Id);

            result = accomodationService.DeleteAccomodation(accomodation);

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