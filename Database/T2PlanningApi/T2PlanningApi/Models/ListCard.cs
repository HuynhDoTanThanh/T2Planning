using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T2PlanningApi.Models
{
    public class ListCard
    {
        public string listCardId { get; set; }
        public string listCardName { get; set; }
        public int? tableId { get; set; }
    }
}