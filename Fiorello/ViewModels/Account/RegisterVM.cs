using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fiorello.ViewModels.Account
{
    public class RegisterVM
    {
        [Required]
        public string Fullname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required,DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
        [Required,DataType(DataType.Password),Compare(nameof(Password),ErrorMessage ="Password is unmatched")]
        public string CheckPassword { get; set; }
    }
}
