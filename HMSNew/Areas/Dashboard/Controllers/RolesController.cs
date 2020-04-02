using HMS.Services;
using HMSNew.Areas.Dashboard.ViewModel;
using HMSNew.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HMSNew.Areas.Dashboard.Controllers
{
    public class RolesController : Controller
    {
        private HMSSignInManager _signInManager;
        private HMSUserManager _userManager;
        private HMSRoleManager _roleManager;

        public RolesController()
        {
        }

        public RolesController(HMSUserManager userManager, HMSSignInManager signInManager,HMSRoleManager roleManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            RoleManager = roleManager;
        }

        public HMSSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<HMSSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public HMSUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<HMSUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public HMSRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<HMSRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }



        // GET: Dashboard/AccomodationTypes
        public ActionResult Index(string searchTerm,  int? page)
        {
            int recordSize = 5;
            page = page ?? 1;
            RolesListingModel model = new RolesListingModel();

            model.SearchTerm = searchTerm;
           // model.ro = roleId;
            model.Roles = SearchRoles(searchTerm, page.Value, recordSize);


            var totalRecords = SearchRolesCount(searchTerm);
            model.Pager = new Pager(totalRecords, page, recordSize);
            return View(model);
        }
        public IEnumerable<IdentityRole> SearchRoles(string searchTerm, int page, int recordSize)
        {
            var roles = RoleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }
            
            var skip = (page - 1) * recordSize;
            return roles.OrderBy(x => x.Name).Skip(skip).Take(recordSize).ToList();
            
        }

        public int SearchRolesCount(string searchTerm)
        {
            var roles = RoleManager.Roles.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                roles = roles.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            return roles.Count();
        }
        //we used it for listing before and later updated in index
        //public ActionResult Listing()
        //{
        //    AccomodationTypeListingModel model = new AccomodationTypeListingModel();
        //    model.AccomodationTypes = accomodationTypeService.GetAllAccomodationType();
        //    return PartialView("_Listing", model);
        //}
        [HttpGet]
        public async Task<ActionResult> Action(string Id)
        {
            RolesActionModel model = new RolesActionModel();

            if (!string.IsNullOrEmpty(Id))//editing a record
            {
                var role = await RoleManager.FindByIdAsync(Id);
                model.Id = role.Id;
                model.Name = role.Name;
                

            }
            // model.AccomodationPackages = accomodationPackageService.GetAllAccomodationPackage();
            return PartialView("_Action", model);
            //else//creating a new record
            //{

            //}


        }
        [HttpPost]

        public async Task<JsonResult> Action(RolesActionModel model)
        {

            JsonResult json = new JsonResult();

            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.Id))//editing a record
            {
                var role = await RoleManager.FindByIdAsync(model.Id);
                role.Name = model.Name;
                result = await RoleManager.UpdateAsync(role);
            }
            else//creating a new record
            {
                var role = new IdentityRole();
                role.Name = model.Name;
               
                result = await RoleManager.CreateAsync(role);
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            return json;
        }
        [HttpGet]
        public async Task<ActionResult> Delete(string Id)
        {
            RolesActionModel model = new RolesActionModel();
            var role = await RoleManager.FindByIdAsync(Id);
            model.Id = role.Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(RolesActionModel model)
        {

            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.Id))//deleting a record
            {
                var role = await RoleManager.FindByIdAsync(model.Id);

                result = await RoleManager.DeleteAsync(role);

                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid Role" };
            }


            return json;
        }
    }
}