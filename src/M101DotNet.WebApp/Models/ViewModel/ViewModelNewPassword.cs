using EtudiantMasque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace M101DotNet.WebApp.Models.ViewModel
{
    public class ViewModelNewPassword
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Password { get; set; }
        public String OldPassword { get; set; }
        public String NewPassword { get; set; }
        public String ConfirmPassword { get; set; }
    }
}