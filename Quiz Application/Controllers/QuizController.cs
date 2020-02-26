using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quiz_Application.Models;
using Quiz_Application.ViewModel;
namespace Quiz_Application.Controllers
{
    public class QuizController : Controller
    {
        private QuizDBEntities objQuizDBEntities;
        // GET: Quiz

        public QuizController()
        {
            objQuizDBEntities = new QuizDBEntities();
        }
        public ActionResult Index()
        {
            QuizCategoryViewModel objQuizCategoryViewModel = new QuizCategoryViewModel();
            objQuizCategoryViewModel.ListofCategory = (from obj in objQuizDBEntities.Categories
                                                       select new SelectListItem()
                                                       {
                                                           Value = obj.CategoryId.ToString(),
                                                           Text = obj.CategoryName
                                                       }).ToList();

            return View(objQuizCategoryViewModel);
        }

        [HttpPost]
        public ActionResult Index(string CandidateName, int CategoryId)
        {
            User objUser = new User();
            objUser.UserName = CandidateName;
            objUser.LoginTime = DateTime.Now;
            objUser.CategoryId = CategoryId;
            objQuizDBEntities.Users.Add(objUser);
            objQuizDBEntities.SaveChanges();

            Session["CandidateName"] = CandidateName;
            Session["CategoryId"] = CategoryId;

            return View("QuestionIndex");
        }

        public PartialViewResult UserQuestionAnswer()
        {
            int pageSize = 1;
            int pageNumber = 0;
            int CategoryId = Convert.ToInt32(Session["CategoryId"]);
            if (Session["CadQuestionAnswer"] == null)
            {
                pageNumber = pageNumber + 1;
            } else
            {
                List<CandidateAnswer> canAnswer = Session["CadQuestionAnswer"] as List<CandidateAnswer>;
                pageNumber = canAnswer.Count+1;

            }
            List<Question> listOfQuestion = new List<Question>();
            listOfQuestion = objQuizDBEntities.Questions.Where(m => m.CategoryId == CategoryId).ToList();

            QuizQuestionAnswerViewModel objAnswerViewModel = new QuizQuestionAnswerViewModel();
            Question objQuestion = new Question();
            objQuestion = listOfQuestion.Skip((pageNumber - 1) * pageSize).Take(pageSize).FirstOrDefault();

            objAnswerViewModel.QuestionId = objQuestion.QuestionId;
            objAnswerViewModel.QuestionName = objQuestion.QuestionName;
            objAnswerViewModel.ListOfQuizOption = (from obj in objQuizDBEntities.Options
                                                   where obj.QuestionId == objQuestion.QuestionId
                                                   select new QuizOption()
                                                   { 
                                                       OptionName = obj.OptionName, 
                                                       OptionId = obj.OptionId 
                                                   }).ToList();
            return PartialView("QuizQuestionOption", objAnswerViewModel);
        }
    }
}