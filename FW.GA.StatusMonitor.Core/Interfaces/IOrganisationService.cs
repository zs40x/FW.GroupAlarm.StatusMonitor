using FW.GA.StatusMonitor.Core.ValueTypes.DTO.GroupAlarm;
using System.Collections.Generic;

namespace FW.GA.StatusMonitor.Core.Interfaces
{
    class IOrganizationService
    {
        public List<Organization> All { get; set; }
    }
}
