using System.Collections.Generic;

namespace FW.GA.StatusMonitor.Core.ValueTypes.DTO.GroupAlarm
{
    public class OrganizationStructure
    {
        public IEnumerable<Organization> Path { get; set; }
        public IEnumerable<Organization> Children { get; set; }
    }
}
