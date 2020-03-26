using System.Collections.Generic;

namespace FW.GA.StatusMonitor.Core.ValueTypes.Model
{
    public class OrganizationStatus
    {
        public List<OrganizationUnitModel> UnitStatus { get; set; }
        public OrganizationTotalsModel Totals { get; set; }
    }
}
