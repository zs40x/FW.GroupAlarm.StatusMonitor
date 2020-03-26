using FW.GA.StatusMonitor.Core.Interfaces;
using FW.GA.StatusMonitor.Core.ValueTypes.Model;
using Microsoft.AspNetCore.Mvc;

namespace FW.GroupAlarm.StatusMonitor.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationStatusController : ControllerBase
    {
        private readonly IOrganizationStatusService _organizationStatusService;

        public OrganizationStatusController(IOrganizationStatusService organizationStatusService)
        {
            _organizationStatusService = organizationStatusService;
        }

        [HttpGet]
        public ActionResult<OrganizationStatus> Get()
        {
            return _organizationStatusService.CurrentStatus(User);
        }
    }
}