using System.Collections.Generic;

namespace FW.GA.StatusMonitor.Core.ValueTypes.Model
{
    public class OrganizationUnitModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountAvailable { get; set; }
        public int CountInEvent { get; set; }
        public int CountNotAvailable { get; set; }
        public int CountRegistrationPending { get; set; }
        public List<OrganizationUnitLabelModel> Labels { get; set; }
        public List<OrganizationUnitUserModel> Users { get; set; }
        public int TotalStrength { get; set; }

        public string CollapseId => $"unitUsers{Id}";
    }
}
