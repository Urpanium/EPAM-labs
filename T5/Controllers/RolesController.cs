using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using T5.Models;

namespace T5.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationRoleManager RoleManager
        {
            get { return HttpContext.GetOwinContext().GetUserManager<ApplicationRoleManager>(); }
        }


        public ActionResult Index()
        {
            return View(RoleManager.Roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(CreateRoleModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await RoleManager.CreateAsync(new ApplicationRole
                {
                    Name = model.Name
                });
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                ModelState.AddModelError("", "Something went wrong");
            }

            return View(model);
        }

        public async Task<ActionResult> Edit(EditRoleModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationRole role = await RoleManager.FindByIdAsync(model.Id);
                if (role != null)
                {
                    role.Name = model.Name;
                    IdentityResult result = await RoleManager.UpdateAsync(role);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index");
                    }

                    ModelState.AddModelError("", "Something went wrong");
                }
            }

            return View(model);
        }

        public async Task<ActionResult> Delete(string id)
        {
            ApplicationRole role = await RoleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await RoleManager.DeleteAsync(role);
                //TODO: check result
            }

            return RedirectToAction("Index");
        }
    }
}