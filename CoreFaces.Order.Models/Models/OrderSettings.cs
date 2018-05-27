using System;
using System.Collections.Generic;
using System.Text;

namespace CoreFaces.Order.Models.Models
{
    public class OrderSettings
    {
        public string FileUploadFolderPath { get; set; } = "";
        public string OrderLicenseDomain { get; set; } = "";
        public string OrderLicenseKey { get; set; } = "";
    }
}
