using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Domain
{
    public class SignUpRequest
    {
        [Required, EmailAddress]
        public string EmailId { get; set; }

        [Required]
        public string Password { get; set; }
    }

}
