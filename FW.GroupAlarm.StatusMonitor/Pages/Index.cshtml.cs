using FW.GA.StatusMonitor.Core.Interfaces;
using FW.GroupAlarm.StatusMonitor.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace FW.GroupAlarm.StatusMonitor.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IOrganizationService _organizationService;

        public List<OrganisationUnitModel> OrganisationUnits { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IOrganizationService organizationService)
        {
            _logger = logger;
            _organizationService = organizationService;
        }

        public void OnGet()
        {
            OrganisationUnits = RetrieveOrganisationUnits();
        }

        private List<OrganisationUnitModel> RetrieveOrganisationUnits()
        {
            return _organizationService.Get()
                                        .Childs?.Select(c => new OrganisationUnitModel
                                        {
                                            Name = c.Name,
                                            Description = c.Description,
                                            CountAvailable = c.AvailableUsers.CountAvailable,
                                            CountInEvent = c.AvailableUsers.CountInEvent,
                                            CountNotAvailable = c.AvailableUsers.CountNotAvailable,
                                            Labels = RetrieveOrganizationLabels(c.Id)
                                        })
                                        .ToList();
        }

        public List<OrganisationUnitLabelModel> RetrieveOrganizationLabels(int organisationId)
        {
            var users = _organizationService.UserInOrganisation(organisationId);

            return _organizationService.LabelsInOrganisation(organisationId)
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
    }
}
