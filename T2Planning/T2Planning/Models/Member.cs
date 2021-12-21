using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace T2Planning.Models
{
    public class Member
    {
        [PrimaryKey]
        public int memberId { get; set; }
        public int tableId { get; set; }
        public string Uid { get; set; }
    }
}
