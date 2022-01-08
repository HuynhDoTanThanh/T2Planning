using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T2PlanningApi.Models
{
    public class User
    {
        public string Uid { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string userAvatar { get; set; }
    }
}