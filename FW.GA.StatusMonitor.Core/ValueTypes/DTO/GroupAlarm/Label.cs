using System.Collections.Generic;

namespace FW.GA.StatusMonitor.Core.ValueTypes.DTO.GroupAlarm
{
    public class Label
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public List<int> Assignees { get; set; }
    }
}
