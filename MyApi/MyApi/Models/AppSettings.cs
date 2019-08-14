using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApi.Models
{
    public class AppSettings
    {
        public string JWT_Secret { get; set; }
        public string Client_URL { get; set; }
    }
}
