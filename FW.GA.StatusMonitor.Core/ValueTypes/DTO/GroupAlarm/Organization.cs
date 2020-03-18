namespace FW.GA.StatusMonitor.Core.ValueTypes.DTO.GroupAlarm
{
    public class Organization
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public OrganizationAvailability AvailableUsers { get; set; }
    }
}
