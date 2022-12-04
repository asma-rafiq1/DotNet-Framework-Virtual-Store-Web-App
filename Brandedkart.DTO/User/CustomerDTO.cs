using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC = System.Web.Mvc;
using System.Xml.Linq;
using System.IO;
using System.ComponentModel;
using System.Web;
using Microsoft.SqlServer.Server;
using System.Web.Mvc;
using Brandedkart.DTO.User;

namespace Brandedkart.DTO.Customer
{
    public class CustomerDTO
    {
        [HiddenInput(DisplayValue = false)]
        public int Customer_ID { get; set; }

        [Required(ErrorMessage = "The 'Name' field is Required")]
        [StringLength(30, MinimumLength = 7, ErrorMessage = "Name must be at least 7-30 characters in length")]
        [Display(Name = "Full Name")]
        [RegularExpression(@"[a-zA-Z+\s+]*", ErrorMessage = "Please enter a Valid Name")]
        public string Full_Name { get; set; }


        [Required(ErrorMessage = "The Email Address must not be empty")]
        [Display(Name = "Email Address"), DataType(DataType.EmailAddress)]
        [RegularExpression(@"^[a-z0-9][-a-z0-9._]+@([-a-z0-9]+.)+[a-z]{2,5}$", ErrorMessage = "Please enter a valid email")]
        [MVC.Remote("IsUserEmailAvailable", "Validate", "Customer", ErrorMessage = "The Email is already in use. Please Try another one")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password must not be empty"), Display(Name = "Password"), DataType(DataType.Password)]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W]).{8,64})", ErrorMessage = "Enter a password with 1 uppercase & 1 lowercase letter, 1 digit and a special character")]
        [StringLength(30, ErrorMessage = "Password cannot exceed more than 30 characters")]
        public string Customer_Password { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Customer_Password", ErrorMessage = "The Password doesn't Match")]
        [Display(Name = "Confirm password"), DataType(DataType.Password)]
        public string confirmPassword { get; set; }


        [Required(ErrorMessage = "The phone number must not be empty"), Display(Name = "Phone Number"), DataType(DataType.PhoneNumber)]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "This entry can only contain numbers")]
        public string Phone_Number { get; set; }

        [Display(Name = "Choose Gender"), DisplayFormat(NullDisplayText = "Gender not specified")]
        public bool Gender { get; set; }

        [Required(ErrorMessage = "The Date of Birth is Required")]
        // [DataType(DataType.Date)]
        [Display(Name = "Date of Birth")]
        // [DisplayFormat(DataFormatString ="{0:d}")]
        //[Range(typeof(DateTime),"01/01/1950","01/01/2005",ErrorMessage ="Age must be greater than 18")]
        public System.DateTime DOB { get; set; }

        [Display(Name = "Choose a Profile Image")]
        public HttpPostedFileBase UserImage { get; set; }
        public string Image_Path { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public string error { get; set; }


    }


    public enum CustomerGender
    {
        [Display(Name = "Not Specified")]
        NotSpecified = 0,
        Male = 1,
        Female = 2
    }

}
