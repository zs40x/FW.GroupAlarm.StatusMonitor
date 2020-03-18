using FW.GA.StatusMonitor.Core.Interfaces;
using FW.GA.StatusMonitor.Core.ValueTypes.DTO.GroupAlarm;
using System.Collections.Generic;

namespace Fw.GA.StatusMonitor.Infrastructure.GroupAlarmApi
{
    public class OrganisationService : IOrganizationService
    {
        public List<Organization> All()
        {
            return new List<Organization>();
        }
    }
}
