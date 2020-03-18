using FW.GA.StatusMonitor.Core.ValueTypes.DTO.GroupAlarm;
using System.Collections.Generic;

namespace FW.GA.StatusMonitor.Core.Interfaces
{
    public interface IOrganizationService
    {
        OrganizationStructure Get();
        List<Label> LabelsInOrganisation(int organizationId);
        List<User> UserInOrganisation(int organizationId);
    }
}
