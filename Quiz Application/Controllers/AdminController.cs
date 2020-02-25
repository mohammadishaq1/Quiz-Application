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
        [HttpPost]
        public JsonResult Index(QuestionOptionViewModel QuestionOption)
        {
            Question objQuestion = new Question();

            objQuestion.QuestionName = QuestionOption.QuestionName;
            objQuestion.CategoryId = QuestionOption.CategoryId;
            objQuestion.isActive = true;
            objQuestion.isMultiple = false;
            ObjQuizDBEntities.Questions.Add(objQuestion);
            ObjQuizDBEntities.SaveChanges();

            int questionId = objQuestion.QuestionId;

            foreach(var item in QuestionOption.ListOfOptions)
            {
                Option objOption = new Option();
                objOption.OptionName = item;
                objOption.QuestionId = questionId;
                ObjQuizDBEntities.Options.Add(objOption);
                ObjQuizDBEntities.SaveChanges();

            }

            Answer objAnswer = new Answer();
            objAnswer.AnswerText = QuestionOption.AnswerText;
            objAnswer.QuestionId = questionId;
            ObjQuizDBEntities.Answers.Add(objAnswer);
            ObjQuizDBEntities.SaveChanges();

            return Json(new { message = "Data successfully added", success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}