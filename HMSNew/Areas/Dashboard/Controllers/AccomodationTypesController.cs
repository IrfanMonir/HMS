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
    public class AccomodationTypesController : Controller
    {
        AccomodationTypeService accomodationTypeService = new AccomodationTypeService();
        // GET: Dashboard/AccomodationTypes
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Listing()
        {
            AccomodationTypeListingModel model = new AccomodationTypeListingModel();
            model.AccomodationTypes = accomodationTypeService.GetAllAccomodationType();
            return PartialView("_Listing", model);
        }
        [HttpGet]
        public ActionResult Action(int? Id)
        {
            AccomodationTypeActionModel model = new AccomodationTypeActionModel();

            if (Id.HasValue)//editing a record
            {
                var accomodationType = accomodationTypeService.GetAccomodationTypeId(Id.Value);
                model.Id = accomodationType.Id;
                model.Name = accomodationType.Name;
                model.Description = accomodationType.Description;
            }
            else//creating a new record
            {

            }
           
            return PartialView("_Action",model);
        }
        [HttpPost]
       
        public JsonResult Action(AccomodationTypeActionModel model)
        {
           
            JsonResult json = new JsonResult();
            var result = false;
            if (model.Id>0)//editing a record
            {
                var accomodationType = accomodationTypeService.GetAccomodationTypeId(model.Id);
                accomodationType.Name = model.Name;
                accomodationType.Description = model.Description;
                result = accomodationTypeService.UpdateAccomodationType(accomodationType);
            }
            else//creating a new record
            {
                AccomodationType accomodationType = new AccomodationType();
                accomodationType.Name = model.Name;
                accomodationType.Description = model.Description;

                result = accomodationTypeService.SaveAccomodationType(accomodationType);
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
            AccomodationTypeActionModel model = new AccomodationTypeActionModel();
            var accomodationType = accomodationTypeService.GetAccomodationTypeId(Id);
            model.Id=accomodationType.Id;
            return PartialView("_Delete",model);
        }

        [HttpPost]
        public JsonResult Delete(AccomodationTypeActionModel model)
        {

            JsonResult json = new JsonResult();
            var result = false;
        
                var accomodationType = accomodationTypeService.GetAccomodationTypeId(model.Id);
             
                result = accomodationTypeService.DeleteAccomodationType(accomodationType);
         
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