using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace M101DotNet.WebApp.Infrastructure
{
    public static class Convertions
    {
        public static string InputStreamToB64String(Stream _inputStream)
        {
            MemoryStream memoryStream = _inputStream as MemoryStream ?? new MemoryStream();
            _inputStream.CopyTo(memoryStream);
            byte[] imgB = memoryStream.ToArray();
            return Convert.ToBase64String(imgB);
        }
    }
}