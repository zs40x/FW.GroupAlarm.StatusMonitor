using System;
using System.Collections.Generic;
using System.Text;

namespace FW.GA.StatusMonitor.Core.ValueTypes.DTO.GroupAlarm
{
    public class User
    {
        public int Id { get; set; }
        public string EMail { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int AvailableStatus { get; set; }
        public bool Pending { get; set; }
    }
}
