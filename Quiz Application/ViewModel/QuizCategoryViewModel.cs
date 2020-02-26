using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quiz_Application.ViewModel
{
    public class QuizCategoryViewModel
    {
        [Display(Name ="Category")]
        [Required(ErrorMessage ="Required")]
        public int CategoryId { get; set; }


        [Display(Name = "Candidate")]
        [Required(ErrorMessage = "Required")]
        public string CandidateName { get; set; }


        public IEnumerable<SelectListItem> ListofCategory { get; set; }
    }
}