using kartazdrowia.Data;
using kartazdrowia.Models;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace kartazdrowia.Controllers
{
    public class DiseaseController : Controller
    {
        public DiseaseController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager)

        {
            DbContext = dbContext;
            _userManager = userManager;        
        }

        public readonly ApplicationDbContext DbContext;
        public readonly UserManager<IdentityUser> _userManager;
        private string GetCurrentUserId()
        {
            ClaimsPrincipal currentUser = this.User;
            var userId = _userManager.GetUserId(currentUser);
            return userId;
        }

        public IActionResult Index() 
        {
            var diseases = DbContext.Diseases.Where(x => x.UserId == GetCurrentUserId()).ToList();
            return View(diseases);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Store(String name, String description, DateTime startdate)
        {
            var disease = new Disease(name, description, startdate);
            var userId = GetCurrentUserId();
            disease.UserId = userId;

            DbContext.Diseases.Add(disease);
            DbContext.SaveChanges();
            return Redirect("/Disease");
        }
        public IActionResult Show(int id)
        {
            var disease = DbContext.Diseases.FirstOrDefault(x => x.Id == id);
            return View(disease);
        }
        public IActionResult Edit(int id)
        {

            var disease = DbContext.Diseases.FirstOrDefault(x => x.Id == id);
            
            return View(disease);
        }
        [HttpPost]
        public IActionResult Save(String name, String description, DateTime startdate,int id)
        {
            var disease = DbContext.Diseases.FirstOrDefault(x => x.Id == id);
            if(name != null)
            {
                disease.Name = name;
            }
            
            disease.Description = description;
            DbContext.Diseases.Update(disease);
            DbContext.SaveChanges();

            return Redirect("/Disease");
        }
    }
}
