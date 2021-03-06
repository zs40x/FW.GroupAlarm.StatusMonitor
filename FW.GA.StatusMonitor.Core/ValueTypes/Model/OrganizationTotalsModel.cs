﻿using System.Collections.Generic;

namespace FW.GA.StatusMonitor.Core.ValueTypes.Model
{
    public class OrganizationTotalsModel
    {
        public int TotalAvailable { get; set; }
        public int TotalInEvent { get; set; }
        public int TotalNotAvailable { get; set; }
        public int TotalRegistrationPending { get; set; }
        public List<OrganizationUnitLabelModel> LabelTotals { get; set; }
    }
}
