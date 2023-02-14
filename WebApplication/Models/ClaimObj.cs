using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
    public class ClaimObj
    {
        public string id { get; set; }
        public DateTime Date { get; set; }
        public string Type { get; set; }
        public string RValue { get; set; }
        public string CurrType { get; set; }
        public string Link { get; set; }
    }
}
