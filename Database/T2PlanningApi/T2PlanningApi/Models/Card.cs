using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace T2PlanningApi.Models
{
    public class Card
    {
        public int cardId { get; set; }
        public int? tableId { get; set; }
        public int? listCardId { get; set; }
        public string cardName { get; set; }
        public string cardDescription { get; set; }
        public DateTime? cardDeadline { get; set; }
    }
}