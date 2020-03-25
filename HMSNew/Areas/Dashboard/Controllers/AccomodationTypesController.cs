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
        public ActionResult Action()
        {
            AccomodationTypeActionModel model = new AccomodationTypeActionModel();
            return PartialView("_Action",model);
        }
        [HttpPost]
        public JsonResult Action(AccomodationTypeActionModel model)
        {
            JsonResult json = new JsonResult();
            AccomodationType accomodationType = new AccomodationType();
            accomodationType.Name = model.Name;
            accomodationType.Description = model.Description;

            var result = accomodationTypeService.SaveAccomodationType(accomodationType);
            if (result)
            {
                json.Data = new { Success = true };
            }
            else
            {
                json.Data = new { Success = false, Message = "Unable to add Accomodation Type" };
            }
            return json;
        }
    }
}