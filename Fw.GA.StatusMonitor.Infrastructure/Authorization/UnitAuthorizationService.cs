using FW.GA.StatusMonitor.Core.Interfaces;
using FW.GA.StatusMonitor.Core.ValueTypes.DTO.Authorization;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Fw.GA.StatusMonitor.Infrastructure.Authorization
{
    public class UnitAuthorizationService : IUnitAuthorizationService
    {
        private readonly List<AuthorizationMapping> authorizationMappings;

        public UnitAuthorizationService(List<AuthorizationMapping> authorizationMappings)
        {
            this.authorizationMappings = authorizationMappings ?? throw new ArgumentNullException(nameof(authorizationMappings));
        }

        public bool HasAccess(ClaimsPrincipal user, int unitId)
        {
            return false;
        }
    }
}
