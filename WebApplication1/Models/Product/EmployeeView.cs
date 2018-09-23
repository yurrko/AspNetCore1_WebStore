using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebStore.Models
{
    public class EmployeeView
    {
        public int id { get; set; }

        [Required( AllowEmptyStrings = false , ErrorMessageResourceType = typeof( Resources.Resource ) , ErrorMessageResourceName = "RequiredFieldErrorMessage" )]
        //[Required( AllowEmptyStrings = false , ErrorMessage = "Имя является обязательным" )]
        [Display( Name = "Имя" )]
        [StringLength( maximumLength: 200 , MinimumLength = 2 , ErrorMessage = "В имени должно быть не менее 2х и не более 200 символов" )]
        public string FirstName { get; set; }

        [Required( AllowEmptyStrings = false , ErrorMessageResourceType = typeof( Resources.Resource ) , ErrorMessageResourceName = "RequiredFieldErrorMessage" )]
        [Display( Name = "Фамилия" )]
        public string SurName { get; set; }

        [Display( Name = "Отчество" )]
        public string Patronymic { get; set; }

        [Required( AllowEmptyStrings = false , ErrorMessageResourceType = typeof( Resources.Resource ) , ErrorMessageResourceName = "RequiredFieldErrorMessage" )]
        [Display( Name = "Возраст" )]
        public int Age { get; set; }

        [Display( Name = "Дата трудоустройства" )]
        public DateTime DateOfEmploy { get; set; }

        [Required( AllowEmptyStrings = false , ErrorMessage = "Зарплата должна быть > 0" )]
        public double Salary { get; set; }
    }
}
