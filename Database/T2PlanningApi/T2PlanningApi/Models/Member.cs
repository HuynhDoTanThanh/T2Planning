using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T2PlanningApi.Models
{
    public class Member
    {
        public int memberId { get; set; }
        public int tableId { get; set; }
        public string Uid { get; set; }
    }
}