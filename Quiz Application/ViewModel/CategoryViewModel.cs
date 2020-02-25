using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quiz_Application.ViewModel
{
    public class CategoryViewModel
    {
        [Display(Name ="Category")]
        [Required(ErrorMessage ="Required")]
        public int CategoryId { get; set; }
        [Required(ErrorMessage = "Required")]
        [Display(Name = "Question")]
        public string QuestionName { get; set; }
        public string OptionName { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<SelectListItem> ListofCategory { get; set; }
    }
}