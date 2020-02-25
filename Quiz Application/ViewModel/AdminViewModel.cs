using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Quiz_Application.ViewModel
{
    public class AdminViewModel
    {
        [Display(Name="Email")]
        [Required(ErrorMessage ="Required")]
        [EmailAddress(ErrorMessage ="Valid email required")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Required")]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }
    }
}