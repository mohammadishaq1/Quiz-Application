using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quiz_Application.Models;
using Quiz_Application.ViewModel;

namespace Quiz_Application.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private QuizDBEntities ObjQuizDBEntities;

        public AdminController()
        {
            ObjQuizDBEntities = new QuizDBEntities();
        }
        // GET: Admin
        public ActionResult Index()
        {
            CategoryViewModel objCategoryViewModel = new CategoryViewModel();
            objCategoryViewModel.ListofCategory = (from objCat in ObjQuizDBEntities.Categories
                                                   select new SelectListItem()
                                                   {
                                                       Value = objCat.CategoryId.ToString(),
                                                       Text = objCat.CategoryName
                                                   }).ToList();
            return View(objCategoryViewModel);
        }
    }
}