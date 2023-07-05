using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FormAuthenticationCustom.Models
{
    public class Membership
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}