using FW.GA.StatusMonitor.Core.ValueTypes.Model;

namespace FW.GA.StatusMonitor.Core.Interfaces
{
    interface IOrganizationStatusService
    {
        OrganizationStatus CurrentStatus();
    }
}
