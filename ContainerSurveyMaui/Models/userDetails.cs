using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContainerSurveyMaui.Models
{
    public class userDetails
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Please enter the password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])[a-zA-Z\d]{8,}$", ErrorMessage = "A minimum 8-character password should contain a combination of uppercase and lowercase letters.")]

        public string? Password { get; set; }
        public string? Phone_number { get; set; }

        public string? Email { get; set; }

        public int? IsActive { get; set; }

        public DateTime? CreatedOn { get; set; }

        public int? IsFirstuser { get; set; }

    }
}
