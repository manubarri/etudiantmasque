using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace M101DotNet.WebApp.Models.ViewModel
{
    public class ViewModelNewName
    {
        [Required]
        public String Id { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public String Name { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public String NewName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public String Email { get; set; }
    }
}