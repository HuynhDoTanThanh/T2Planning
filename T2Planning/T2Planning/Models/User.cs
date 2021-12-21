using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace T2Planning.Models
{
    public class User
    {
        [PrimaryKey]
        public string Uid { get; set; }
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string userAvatar { get; set; }
    }
}
