using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace EtudiantMasque.Models
{
    public class Captcha
    {
        [JsonProperty("success")]
        private string Success { get; set; }

        [JsonProperty("error-codes")]
        private List<string> ErrorCodes { get; set; }

        public void Validate(string _request)
        {
            //secret that was generated in key value pair
            const string secret = "6LdSKioTAAAAAHfTOZeEPlNjRt8gJ_qcdp5T35Ao";

            string reply =
                new WebClient().DownloadString(
                    string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",
                secret, _request));

            Captcha response = JsonConvert.DeserializeObject<Captcha>(reply);

            if (response != null)
            {
                this.ErrorCodes = response.ErrorCodes;
                this.Success = response.Success;
            }
        }

        public bool BoolSuccess
        {
            get { return this.Success == true.ToString(); }
        }
    }
}