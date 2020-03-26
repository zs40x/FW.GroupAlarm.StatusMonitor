using FW.GA.StatusMonitor.Core.ValueTypes.Model;
using System.Security.Claims;

namespace FW.GA.StatusMonitor.Core.Interfaces
{
    public interface IOrganizationStatusService
    {
        OrganizationStatus CurrentStatus(ClaimsPrincipal user);
    }
}
