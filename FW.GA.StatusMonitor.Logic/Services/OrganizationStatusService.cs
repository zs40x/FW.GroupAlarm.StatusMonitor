using FW.GA.StatusMonitor.Core.Interfaces;
using FW.GA.StatusMonitor.Core.ValueTypes.DTO.GroupAlarm;
using FW.GA.StatusMonitor.Core.ValueTypes.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FW.GA.StatusMonitor.Logic.Services
{
    public class OrganizationStatusService : IOrganizationStatusService
    {
        private readonly IOrganizationDataService _organizationService;
        private readonly IUnitAuthorizationService _authorizationService;

        public OrganizationStatusService(IOrganizationDataService organizationService, IUnitAuthorizationService authorizationService)
        {
            _organizationService = organizationService;
            _authorizationService = authorizationService;
        }

        public OrganizationStatus CurrentStatus(ClaimsPrincipal user)
        {
            var organizationStatus =
                MakeOrganizationTotals(
                    RetrieveOrganizationUnits(user));

            return new OrganizationStatus
            {
                UnitStatus = organizationStatus.UnitStatus,
                Totals = organizationStatus.Totals
            };
        }

        private List<OrganizationUnitModel> RetrieveOrganizationUnits(ClaimsPrincipal user)
        {
            return _organizationService.Get()
                                        .Childs?
                                        .Where(c => string.Compare(c.Description, "- n/a -") != 0)
                                        .Where(c => _authorizationService.HasAccess(user, c.Id))
                                        .Select(c => new
                                        {
                                            Child = c,
                                            Users = _organizationService.UserInOrganization(c.Id)
                                        })
                                        .Select(c => new OrganizationUnitModel
                                        {
                                            Id = c.Child.Id,
                                            Name = c.Child.Name,
                                            Description = c.Child.Description,
                                            CountAvailable = c.Users.Count(u => u.AvailableStatus > 0),
                                            CountInEvent = c.Child.AvailableUsers.CountInEvent,
                                            CountNotAvailable = c.Users.Count(u => !u.Pending && u.AvailableStatus == 0),
                                            CountRegistrationPending = c.Users.Count(u => u.Pending),
                                            Labels = RetrieveOrganizationLabels(c.Child.Id, c.Users),
                                            Users = MakeUsers(c.Users),
                                            TotalStrength = c.Users.Count
                                        })
                                        .ToList();
        }

        public List<OrganizationUnitLabelModel> RetrieveOrganizationLabels(int organizationId, List<User> users)
        {
            return _organizationService.LabelsInOrganization(organizationId)
                                        .Where(l => l.Assignees?.Count > 0)
                                        .Select(l => new OrganizationUnitLabelModel
                                        {
                                            Name = l.Name,
                                            AssigneeCount = l.Assignees?.Count ?? 0,
                                            AvailableCount = l.Assignees?
                                                                .Select(a => users.FirstOrDefault(u => u.Id == a))
                                                                .Count(u => u?.AvailableStatus == 1) ?? 0,
                                            RgbColorCode = l.Color
                                        })
                                        .OrderByDescending(l => l.RgbColorCode).ThenBy(l => l.Name)
                                        .ToList();
        }

        private List<OrganizationUnitUserModel> MakeUsers(List<User> users)
        {
            return users
                .OrderBy(u => u.Pending).ThenBy(u => u.Surname)
                .Select(u => new OrganizationUnitUserModel
                {
                    Name = !string.IsNullOrEmpty(u.Surname)
                            ? $"{u.Surname}, {u.Name}"
                            : u.EMail.Split('@').First() + "@",
                    IsAvailable = u.AvailableStatus == 1,
                    IsInEvent = u.AvailableStatus == 2,
                    IsRegistered = !u.Pending
                })
                .ToList();
        }

        private (List<OrganizationUnitModel> UnitStatus, OrganizationTotalsModel Totals) MakeOrganizationTotals(List<OrganizationUnitModel> organizationUnits)
        {
            if (organizationUnits == null)
                throw new ArgumentNullException(nameof(organizationUnits));

            return 
                (UnitStatus: organizationUnits,
                 Totals: new OrganizationTotalsModel
                    {
                        TotalAvailable = organizationUnits.Sum(u => u.CountAvailable),
                        TotalInEvent = organizationUnits.Sum(u => u.CountInEvent),
                        TotalNotAvailable = organizationUnits.Sum(u => u.CountNotAvailable),
                        TotalRegistrationPending = organizationUnits.Sum(u => u.CountRegistrationPending),
                        LabelTotals = MakeOrganizationLabelTotals(organizationUnits)
                    });
        }

        private List<OrganizationUnitLabelModel> MakeOrganizationLabelTotals(List<OrganizationUnitModel> organizationUnits)
        {
            if (organizationUnits == null)
                throw new ArgumentNullException(nameof(organizationUnits));

            return organizationUnits
                    .SelectMany(u => u.Labels)
                    .Where(u => !u.Name.ToLower().StartsWith("lg"))
                    .GroupBy(l => l.Name)
                    .Select(g => new OrganizationUnitLabelModel
                    {
                        Name = g.Key,
                        RgbColorCode = g.FirstOrDefault()?.RgbColorCode,
                        AssigneeCount = g.Sum(l => l.AssigneeCount),
                        AvailableCount = g.Sum(l => l.AvailableCount)
                    })
                    .OrderByDescending(l => l.RgbColorCode).ThenBy(l => l.Name)
                    .ToList();
        }
    }
}
