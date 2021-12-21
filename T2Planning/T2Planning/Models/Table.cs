using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace T2Planning.Models
{
    public class Table
    {
        [PrimaryKey]

        public int tableId { get; set; }
        public string tableAdmin { get; set; }
        public string tableName { get; set; }
        public int tableTeam { get; set; }
        public int tablePermission { get; set; }
    }
}
