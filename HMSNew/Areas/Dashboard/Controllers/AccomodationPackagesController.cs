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
    public class AccomodationPackagesController : Controller
    {
        AccomodationPackageService accomodationPackageService = new AccomodationPackageService();
        AccomodationTypeService accomodationTypeService = new AccomodationTypeService();
        DashboardService dashboardService = new DashboardService();
        // GET: Dashboard/AccomodationTypes
        public ActionResult Index(string searchTerm,int? accomodationTypeId, int? page)
        {
            int recordSize = 5;
            page = page ?? 1;
            AccomodationPackageListingModel model = new AccomodationPackageListingModel();

            model.SearchTerm = searchTerm;
            model.AccomodationTypeId = accomodationTypeId;
            model.AccomodationPackages = accomodationPackageService.SearchAccomodationPackage(searchTerm, accomodationTypeId,page.Value,recordSize);
            model.AccomodationTypes = accomodationTypeService.GetAllAccomodationType();

            var totalRecords= accomodationPackageService.SearchAccomodationPackageCount(searchTerm, accomodationTypeId);
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
            AccomodationPackageActionModel model = new AccomodationPackageActionModel();

            if (Id.HasValue)//editing a record
            {
                var accomodationPackage = accomodationPackageService.GetAccomodationPackageId(Id.Value);
                model.Id = accomodationPackage.Id;
                model.AccomodationTypeId = accomodationPackage.AccomodationTypeId;
                model.Name = accomodationPackage.Name;
                model.NoOfRoom = accomodationPackage.NoOfRoom;
                model.FeePerNight = accomodationPackage.FeePerNight;
                model.AccomodationPackagePictures = accomodationPackageService.GetPictureByAccomodationPackageId(accomodationPackage.Id);
               
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
            //this part is for picture upload
            List<int> picturesIds =!string.IsNullOrEmpty(model.PictureIds) ?model.PictureIds.Split(',').Select(x => int.Parse(x)).ToList():new List<int>();
            var pictures = dashboardService.GetPictureByIds(picturesIds);

            if (model.Id > 0)//editing a record
            {
                var accomodationPackage = accomodationPackageService.GetAccomodationPackageId(model.Id);
                accomodationPackage.Name = model.Name;
                accomodationPackage.NoOfRoom = model.NoOfRoom;
                accomodationPackage.FeePerNight = model.FeePerNight;
                accomodationPackage.Description = model.Description;
                accomodationPackage.AccomodationTypeId = model.AccomodationTypeId;
                result = accomodationPackageService.UpdateAccomodationPackage(accomodationPackage);
                //for picture edit
                accomodationPackage.AccomodationPackagePictures.Clear();
                accomodationPackage.AccomodationPackagePictures.AddRange(pictures.Select(x => new AccomodationPackagePicture() {AccomodationPackageId=accomodationPackage.Id, PictureId = x.Id }));
            }
            else//creating a new record
            {
                AccomodationPackage accomodationPackage = new AccomodationPackage();
                accomodationPackage.Name = model.Name;
                accomodationPackage.NoOfRoom = model.NoOfRoom;
                accomodationPackage.FeePerNight = model.FeePerNight;
                accomodationPackage.Description = model.Description;
                accomodationPackage.AccomodationTypeId = model.AccomodationTypeId;
              
                accomodationPackage.AccomodationPackagePictures = new List<AccomodationPackagePicture>();
                accomodationPackage.AccomodationPackagePictures.AddRange(pictures.Select(x => new AccomodationPackagePicture() { PictureId = x.Id }));

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