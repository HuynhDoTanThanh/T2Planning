using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace T2Planning.Models
{
    public class ShowMember
    {
        public string Uid { get; set; }
        public string userName { get; set; }
        public string userAvatar { get; set; }
        public ICommand delete { get; set; }
    }
}
