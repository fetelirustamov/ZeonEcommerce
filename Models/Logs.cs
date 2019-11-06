using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ZeonEcommerce.Models
{
    public class Logs
    {
        [Key]
        public int LogsId { get; set; }
        public string ActionName { get; set; }
        public string ControllerName { get; set; }
        public DateTime Time { get; set; }
        public string Info { get; set; }
        public string UserIp { get; set; }


        public int? CustomersID { get; set; }
        public virtual Customers Customers { get; set; }

        public int? CategoriesID { get; set; }
        public virtual Categories Categories { get; set; }
    }
}