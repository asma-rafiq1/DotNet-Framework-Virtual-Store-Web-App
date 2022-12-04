using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Brandedkart.DTO.User
{
    public class AdminDTO
    {
 
        [Required(ErrorMessage = "The 'Name' field is required"), Display(Name = "User Name")]
        [StringLength(30, MinimumLength = 7, ErrorMessage = "Name must be at least 7-30 characters in length")]
        public string Admin_Name { get; set; }


        [Required(ErrorMessage = "Password must not be empty"),Display(Name = "Password")]
        //[RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{8,64})", ErrorMessage = "Enter a password with 1 uppercase & 1 lowercase letter, 1 digit and a special character")]
        [StringLength(50, ErrorMessage = "Password cannot exceed more than 50 characters")]
        public string Admin_Password { get; set; }
    }
}
