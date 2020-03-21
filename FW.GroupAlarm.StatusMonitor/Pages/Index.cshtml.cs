using FW.GA.StatusMonitor.Core.Interfaces;
using FW.GA.StatusMonitor.Core.ValueTypes.DTO.GroupAlarm;
using FW.GroupAlarm.StatusMonitor.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FW.GroupAlarm.StatusMonitor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IOrganizationService _organizationService;

        public List<OrganisationUnitModel> OrganisationUnits { get; set; }
        public OrganizationTotalsModel OrganisationTotals { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IOrganizationService organizationService)
        {
            _logger = logger;
            _organizationService = organizationService;
        }

        public void OnGet()
        {
            // ToDo: Warning: Temporal coupling!
            OrganisationUnits = RetrieveOrganisationUnits();
            OrganisationTotals = MakeOrganizationTotals();
        }

        private List<OrganisationUnitModel> RetrieveOrganisationUnits()
        {
            return _organizationService.Get()
                                        .Childs?
                                        .Where(c => string.Compare(c.Description, "- n/a -") != 0)
                                        .Select(c => new
                                        {
                                            Child = c,
                                            Users = _organizationService.UserInOrganisation(c.Id)
                                        })
                                        .Select(c => new OrganisationUnitModel
                                        {
                                            Id = c.Child.Id,
                                            Name = c.Child.Name,
                                            Description = c.Child.Description,
                                            CountAvailable = c.Child.AvailableUsers.CountAvailable,
                                            CountInEvent = c.Child.AvailableUsers.CountInEvent,
                                            CountNotAvailable = c.Child.AvailableUsers.CountNotAvailable,
                                            CountRegistrationPending = c.Users.Count(c => c.Pending),
                                            Labels = RetrieveOrganizationLabels(c.Child.Id, c.Users),
                                            Users = MakeUsers(c.Users),
                                            TotalStrength = c.Users.Count
                                        })
                                        .ToList();
        }

        public List<OrganisationUnitLabelModel> RetrieveOrganizationLabels(int organisationId, List<User> users)
        {
            return _organizationService.LabelsInOrganisation(organisationId)
                                        .Where(l => l.Assignees?.Count > 0)
                                        .Select(l => new OrganisationUnitLabelModel
                                        {
                                            Name = l.Name,
                                            AssigneeCount = l.Assignees?.Count ?? 0,
                                            AvailableCount = l.Assignees?
                                                                .Select(a => users.FirstOrDefault(u => u.Id == a))
                                                                .Count(u => u.AvailableStatus == 1) ?? 0,
                                            RgbColorCode = l.Color
                                        })
                                        .ToList();
        }

        private List<OrganisationUnitUserModel> MakeUsers(List<User> users)
        {
            return users
                .Where(u => !u.Pending)
                .OrderBy(u => u.Surname)
                .Select(u => new OrganisationUnitUserModel
                {
                    Name = $"{u.Surname}, {u.Name}",
                    IsAvailable = u.AvailableStatus == 1
                })
                .ToList();
        }

        private OrganizationTotalsModel MakeOrganizationTotals()
        {
            if (OrganisationUnits == null)
                throw new ArgumentNullException(nameof(OrganisationUnits));

            return new OrganizationTotalsModel
            {
                TotalAvailable = OrganisationUnits.Sum(u => u.CountAvailable),
                TotalInEvent = OrganisationUnits.Sum(u => u.CountInEvent),
                TotalNotAvailable = OrganisationUnits.Sum(u => u.CountNotAvailable),
                TotalRegistrationPending = OrganisationUnits.Sum(u => u.CountRegistrationPending),
                LabelTotals = MakeOrganizationLabelTotals()
            };
        }

        private List<OrganisationUnitLabelModel> MakeOrganizationLabelTotals()
        {
            if (OrganisationUnits == null)
                throw new ArgumentNullException(nameof(OrganisationUnits));

            return OrganisationUnits
                    .SelectMany(u => u.Labels)
                    .GroupBy(l => l.Name)
                    .Select(g => new OrganisationUnitLabelModel
                    {
                        Name = g.Key,
                        RgbColorCode = g.FirstOrDefault()?.RgbColorCode,
                        AssigneeCount = g.Sum(l => l.AssigneeCount),
                        AvailableCount = g.Sum(l => l.AvailableCount)
                    })
                    .ToList();
        }
    }
}
