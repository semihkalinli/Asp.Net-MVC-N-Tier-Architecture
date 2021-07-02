using System;

namespace Product.Management.Data.Models
{
    public class LogInfo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string IpAdress { get; set; }
    }
}
