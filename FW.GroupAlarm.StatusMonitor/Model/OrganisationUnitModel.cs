﻿namespace FW.GroupAlarm.StatusMonitor.Model
{
    public class OrganisationUnitModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CountAvailable { get; set; }
        public int CountInEvent { get; set; }
        public int CountNotAvailable { get; set; }
    }
}