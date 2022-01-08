using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T2PlanningApi.Models
{
    public class Table
    {
        public int tableId { get; set; }
        public string tableAdmin { get; set; }
        public string tableName { get; set; }
        public int? tableTeam { get; set; }
        public int? tablePermission { get; set; }
    }
}