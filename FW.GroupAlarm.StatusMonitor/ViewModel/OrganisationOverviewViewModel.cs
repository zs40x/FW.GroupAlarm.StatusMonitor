using FW.GA.StatusMonitor.Core.Interfaces;

namespace FW.GroupAlarm.StatusMonitor.ViewModel
{
    public class OrganisationOverviewViewModel
    {
        private readonly IOrganizationService _organizationService;

        public OrganisationOverviewViewModel(IOrganizationService organizationService)
        {
            _organizationService = organizationService;
        }
    }
}
