namespace FW.GroupAlarm.StatusMonitor.Model
{
    public class OrganizationUnitLabelModel
    {
        public string Name { get; set; }
        public int AssigneeCount { get; set; }
        public int AvailableCount { get; set; }
        public int NotAvailableCount => AssigneeCount - AvailableCount;
        public string RgbColorCode { get; set; }
    }
}
