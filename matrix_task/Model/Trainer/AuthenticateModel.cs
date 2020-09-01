using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace matrix_task.Model.Trainer
{
    public class AuthenticateModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "The password must contain 8 digit that contain at least one capital letter, a number and one character: #?!@$%^&*- ")]
        public string Password { get; set; }
    }
}
