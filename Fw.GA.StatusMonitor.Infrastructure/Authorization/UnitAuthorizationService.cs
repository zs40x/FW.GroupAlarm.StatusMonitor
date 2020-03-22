using FW.GA.StatusMonitor.Core.Interfaces;
using FW.GA.StatusMonitor.Core.ValueTypes.DTO.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace Fw.GA.StatusMonitor.Infrastructure.Authorization
{
    public class UnitAuthorizationService : IUnitAuthorizationService
    {
        private readonly List<AuthorizationMapping> _authorizationMappings;

        public UnitAuthorizationService(List<AuthorizationMapping> authorizationMappings)
        {
            _authorizationMappings = authorizationMappings ?? throw new ArgumentNullException(nameof(authorizationMappings));
        }

        public bool HasAccess(ClaimsPrincipal user, int unitId)
            => MappingAvailable(unitId) ? IsAuthorized(unitId, user) : false;
        
        private bool MappingAvailable(int unitId)
            => _authorizationMappings.Any(m => m.UnitId == unitId);

        private bool IsAuthorized(int unitId, ClaimsPrincipal user)
            =>  user.Claims.Any(c => c.Value.CompareTo(GetAdGroupObjectId(unitId)) == 0);

        private string GetAdGroupObjectId(int unitId)
            => _authorizationMappings.First(m => m.UnitId == unitId).AdGroupObjectId;
    }
}
