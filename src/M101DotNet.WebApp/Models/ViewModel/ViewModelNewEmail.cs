using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M101DotNet.WebApp.Models.ViewModel
{
    public class ViewModelNewEmail
    {
        [Required]
        public String Id { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public String Email { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public String NewEmail { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        [System.ComponentModel.DataAnnotations.Compare("NewEmail")]
        public String ConfirmEmail { get; set; }
    }
}