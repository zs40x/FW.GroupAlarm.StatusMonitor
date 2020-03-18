using FW.GA.StatusMonitor.Core.ValueTypes.DTO.GroupAlarm;
using System.Collections.Generic;

namespace FW.GA.StatusMonitor.Core.Interfaces
{
    public interface IOrganizationService
    {
        IEnumerable<Organization> All();
    }
}
