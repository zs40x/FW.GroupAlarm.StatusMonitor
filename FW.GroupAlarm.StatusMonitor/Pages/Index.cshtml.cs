using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FW.GA.StatusMonitor.Core.Interfaces;
using FW.GroupAlarm.StatusMonitor.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

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
            OrganisationUnits = 
                _organizationService.Get()
                    .Childs?.Select(c => new OrganisationUnitModel
                    {
                        Name = c.Name,
                        Description = c.Description,
                        CountAvailable = c.AvailableUsers.CountAvailable,
                        CountInEvent = c.AvailableUsers.CountInEvent,
                        CountNotAvailable = c.AvailableUsers.CountNotAvailable
                    })
                    .ToList();
        }
    }
}
