using HMS.Services;
using HMSEntities;
using HMSNew.Areas.Dashboard.ViewModel;
using HMSNew.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace HMSNew.Areas.Dashboard.Controllers
{
    public class UsersController : Controller
    {
        private HMSSignInManager _signInManager;
        private HMSUserManager _userManager;
        private HMSRoleManager _roleManager;

        public UsersController()
        {
        }

        public UsersController(HMSUserManager userManager, HMSSignInManager signInManager,HMSRoleManager roleManager)
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


        AccomodationService accomodationService = new AccomodationService();
        AccomodationPackageService accomodationPackageService = new AccomodationPackageService();
        // GET: Dashboard/AccomodationTypes
        public ActionResult Index(string searchTerm, string roleId, int? page)
        {
            int recordSize = 1;
            page = page ?? 1;
            UsersListingModel model = new UsersListingModel();

            model.SearchTerm = searchTerm;
            model.RoleId = roleId;
            model.Users = SearchUsers(searchTerm,roleId,page.Value,recordSize);
            model.Roles = RoleManager.Roles.ToList();

            var totalRecords = SearchUsersCount(searchTerm, roleId);
            model.Pager = new Pager(totalRecords, page, recordSize);
            return View(model);
        }
        public IEnumerable<HMSUser> SearchUsers(string searchTerm, string roleId, int page, int recordSize)
        {
            var users = UserManager.Users.AsQueryable();
            
            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(roleId))
            {
                //accomodations = accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            var skip = (page - 1) * recordSize;
            return users.OrderBy(x => x.Email).Skip(skip).Take(recordSize).ToList();
        }

        public  int SearchUsersCount(string searchTerm, string roleId)
        {
            var users = UserManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(a => a.Email.ToLower().Contains(searchTerm.ToLower()));
            }
            if (!string.IsNullOrEmpty(roleId))
            {
                //accomodations = accomodations.Where(a => a.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            
            return users.Count();
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
            UsersActionModel model = new UsersActionModel();

            if (!string.IsNullOrEmpty(Id))//editing a record
            {
                var user = await UserManager.FindByIdAsync(Id);
                model.Id = user.Id;
                model.Email = user.Email;
                model.FullName = user.FullName;
                model.UserName = user.UserName;
                model.Country = user.Country;
                model.City = user.City;
                model.Address = user.Address;
                
            }
           // model.AccomodationPackages = accomodationPackageService.GetAllAccomodationPackage();
            return PartialView("_Action", model);
            //else//creating a new record
            //{

            //}


        }
        [HttpPost]

        public async Task<JsonResult> Action(UsersActionModel model)
        {

            JsonResult json = new JsonResult();
            
            IdentityResult result =null;
            if (!string.IsNullOrEmpty(model.Id))//editing a record
            {
                var user = await UserManager.FindByIdAsync(model.Id);
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;
                result = await UserManager.UpdateAsync(user);
            }
            else//creating a new record
            {
                var user = new HMSUser();
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;
                result = await UserManager.CreateAsync(user);
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(",",result.Errors) };
            return json;
        }

        [HttpGet]
        public async Task<ActionResult> UserRoles(string Id)
        {
            UserRoleModel model = new UserRoleModel();
            model.Roles = RoleManager.Roles.ToList();
            var user = await UserManager.FindByIdAsync(Id);
            var userRoleIds = user.Roles.Select(x => x.RoleId).ToList();
            model.UserRoles = RoleManager.Roles.Where(x => userRoleIds.Contains(x.Id)).ToList();
           
            return PartialView("_UserRoles", model);
            //else//creating a new record
            //{

            //}


        }
        [HttpPost]

        public async Task<JsonResult> UserRoles(UsersActionModel model)
        {

            JsonResult json = new JsonResult();

            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.Id))//editing a record
            {
                var user = await UserManager.FindByIdAsync(model.Id);
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;
                result = await UserManager.UpdateAsync(user);
            }
            else//creating a new record
            {
                var user = new HMSUser();
                user.FullName = model.FullName;
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.Country = model.Country;
                user.City = model.City;
                user.Address = model.Address;
                result = await UserManager.CreateAsync(user);
            }

            json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            return json;
        }
        [HttpGet]
        public async Task<ActionResult> Delete(string Id)
        {
            UsersActionModel model = new UsersActionModel();
            var user = await UserManager.FindByIdAsync(Id);
            model.Id = user.Id;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(UsersActionModel model)
        {

            JsonResult json = new JsonResult();
            IdentityResult result = null;
            if (!string.IsNullOrEmpty(model.Id))//deleting a record
            {
                var user = await UserManager.FindByIdAsync(model.Id);
              
                result = await UserManager.DeleteAsync(user);

                json.Data = new { Success = result.Succeeded, Message = string.Join(",", result.Errors) };
            }
            else
            {
                json.Data = new { Success = false, Message = "Invalid User" };
            }
            

            return json;
        }
    }
}