using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebApplication.Models
{
    public class Data
    {
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Name can't be empty")]
        [MinLength(3, ErrorMessage = "Name too short")]
        public string Name { get; set; }
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "E-mail can't be empty")]
        [EmailAddress(ErrorMessage = "E-mail is not valid")]
        public string Email { get; set; }
        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Phone number can't be empty")]
        [Phone(ErrorMessage = "Phone number is not valid")]
        public string Phone { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password can't be empty")]
        [MinLength(3, ErrorMessage = "Password too short")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Approval is required")]
        public bool Approval { get; set; }
        [Required(ErrorMessage = "Select gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Select city")]
        [Range(0, 3, ErrorMessage = "Invalid choice")]
        public int? City { get; set; }
        public List<SelectListItem> CityList { get; set; }
        [Required(ErrorMessage = "Select city")]
        public List<int?> Cities { get; set; }
        public MultiSelectList CitiesList { get; set; }
        [Required(ErrorMessage = "Select age")]
        [Range(0, 130, ErrorMessage = "Invalid age")]
        public uint? Age { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
        public Data()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "0", Text = "Lviv" });
            list.Add(new SelectListItem() { Value = "1", Text = "Lwow" });
            list.Add(new SelectListItem() { Value = "2", Text = "Lemberg" });
            list.Add(new SelectListItem() { Value = "3", Text = "Banderstadt" });
            this.CityList = list;
            this.CitiesList = new MultiSelectList(list, "Value", "Text");
            
        }
    }
}