namespace FW.GroupAlarm.StatusMonitor.Model
{
    public class OrganizationUnitUserModel
    {
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public bool IsInEvent { get; set; }
        public bool IsRegistered { get; set; }
    }
}
