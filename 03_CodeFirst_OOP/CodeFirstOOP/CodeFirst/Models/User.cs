using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CodeFirst.Models
{
    public class User : IValidatableObject
    {
        [Key]
        public int Id { get; set; }

        [StringLength(30, MinimumLength = 4),
            Required]
        public string Username { get; set; }

        [StringLength(50, MinimumLength = 6),
            Required]
        public string Password { get; set; }

        public byte[] ProfilePicture { get; set; }

        public DateTime? RegisteredOn { get; set; }

        public DateTime? LastTimeLoggedIn { get; set; }

        [Range(1,120)]
        public int Age { get; set; }

        public bool IsDeleted { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            Regex pwdRegex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&]+", RegexOptions.IgnorePatternWhitespace);

            if(pwdRegex.IsMatch(Password) == false)
            {
                yield return new ValidationResult
                    ("Password must contain one upper, one lower alphabet, one digit and one special symbol.");
            }
        }
    }
}
