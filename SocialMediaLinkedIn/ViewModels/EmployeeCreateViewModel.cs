using Microsoft.AspNetCore.Http;
using SocialMediaLinkedIn.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialMediaLinkedIn.ViewModels
{
    public class EmployeeCreateViewModel
    {

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Office Mail")]
        public string Email { get; set; }
        [MaxLength(50, ErrorMessage = "Name Cannot excedd 50 cgharacters")]
        [Required]
        public string Name { get; set; }
        [Required]
        public Dept Department { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
