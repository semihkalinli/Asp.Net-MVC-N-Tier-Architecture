using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Product.Management.UI.Models
{
    public class ErrorModel
    {
        public string ErrorTitle { get; set; }
        public Exception ExceptionDetail { get; set; }
    }
}