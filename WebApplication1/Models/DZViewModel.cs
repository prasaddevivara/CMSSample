using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DZViewModel
    {
        public int DZId { get; set; }

        [Required]
        [Display(Name = "DZ Name")]       
        public string DZName { get; set; }
    }
}