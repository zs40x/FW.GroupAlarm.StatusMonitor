﻿namespace FW.GA.StatusMonitor.Core.ValueTypes.DTO.GroupAlarm
{
    public class OrganizationAvailability
    {
        public int CountInEvent { get; set; }
        public int CountAvailable { get; set; }
        public int CountNotAvailable { get; set; }
    }
}
