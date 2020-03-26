using FW.GA.StatusMonitor.Core.ValueTypes.DTO.GroupAlarm;
using System.Collections.Generic;

namespace FW.GA.StatusMonitor.Core.Interfaces
{
    public interface IOrganizationService
    {
        OrganizationStructure Get();
        List<Label> LabelsInOrganization(int organizationId);
        List<User> UserInOrganization(int organizationId);
    }
}
