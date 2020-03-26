using FW.GA.StatusMonitor.Core.ValueTypes.Model;

namespace FW.GA.StatusMonitor.Core.Interfaces
{
    public interface IOrganizationStatusService
    {
        OrganizationStatus CurrentStatus();
    }
}
