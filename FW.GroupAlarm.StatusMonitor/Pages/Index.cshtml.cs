using FW.GA.StatusMonitor.Core.Interfaces;
using FW.GA.StatusMonitor.Core.ValueTypes.DTO.GroupAlarm;
using FW.GA.StatusMonitor.Core.ValueTypes.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FW.GroupAlarm.StatusMonitor.Pages
{
    //[Authorize(Policy = "DashboardAccess")]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly IOrganizationStatusService _organizationStatusService;

        public OrganizationStatus OrganizationStatus { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IOrganizationStatusService organizationStatusService)
        {
            _logger = logger;
            _organizationStatusService = organizationStatusService;
        }

        public void OnGet()
        {
            OrganizationStatus = _organizationStatusService.CurrentStatus(User);
        }
    }
}
