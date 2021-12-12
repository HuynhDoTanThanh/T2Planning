using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace T2Planning.Models
{
    public class ListCard
    {
        [PrimaryKey, AutoIncrement]
        public int listCardId { get; set; }
        public string listCardName { get; set; }
        public int tableId { get; set; }
    }
}
