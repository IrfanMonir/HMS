using HMS.Services;
using HMSEntities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HMSNew.Areas.Dashboard.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard/Dashboard
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UploadPictures()
        {
            JsonResult result = new JsonResult();
            var picList = new List<Picture>();
            var files = Request.Files;
            for (int i = 0; i <files.Count;i++)
            {
                var picture = files[i];              
                var fileName = Guid.NewGuid() + Path.GetExtension(picture.FileName);
                var filePath = Server.MapPath("~/images/site/") + fileName;
                picture.SaveAs(filePath);

                var dbPicture = new Picture();
                dbPicture.PictureUrl = fileName;
                var dashboardService = new DashboardService();
                if (dashboardService.SavePicture(dbPicture))
                {
                    picList.Add(dbPicture);
                }
            }
            result.Data = picList;
            return result;
        }
    }
}