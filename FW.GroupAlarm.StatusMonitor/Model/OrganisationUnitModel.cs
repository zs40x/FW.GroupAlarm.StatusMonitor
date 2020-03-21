using System.Collections.Generic;

namespace FW.GroupAlarm.StatusMonitor.Model
{
    public class OrganisationUnitModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountAvailable { get; set; }
        public int CountInEvent { get; set; }
        public int CountNotAvailable { get; set; }
        public List<OrganisationUnitLabelModel> Labels { get; set; }
        public List<OrganisationUnitUserModel> Users { get; set; }
        public int TotalStrength { get; set; }
    }
}
