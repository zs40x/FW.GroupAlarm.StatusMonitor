namespace FW.GroupAlarm.StatusMonitor.Model
{
    public class OrganizationTotalsModel
    {
        public int TotalAvailable { get; set; }
        public int TotalInEvent { get; set; }
        public int TotalNotAvailable { get; set; }
        public int TotalRegistrationPending { get; set; }
    }
}
