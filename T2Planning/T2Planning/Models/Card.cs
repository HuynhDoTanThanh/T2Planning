using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace T2Planning.Models
{
    public class Card
    {
        [PrimaryKey, AutoIncrement]
        public int cardId { get; set; }
        public int tableId { get; set; }
        public int listCardId { get; set; }
        public string cardName { get; set; }
        public string cardDescription { get; set; }
        public DateTime cardDeadline { get; set; }
    }
}
