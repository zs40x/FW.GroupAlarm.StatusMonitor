using System.Security.Claims;

namespace FW.GA.StatusMonitor.Core.Interfaces
{
    public interface IUnitAuthorizationService
    {
        bool HasAccess(ClaimsPrincipal user, int unitId);
    }
}
