using System;

namespace PValue.Models
{
    public class IndicatorsData
    {
        public int ID { get; set; }
        public string NameReference { get; set; }
        public string DataSource { get; set; }
        public int OppeCycleID { get; set; }
        public int OppeIndicatorID { get; set; }
        public int OppePhysicianSubGroupID { get; set; }
        public string PayrollID { get; set; }
        public string AUBNetID { get; set; }
        public string CSNID { get; set; }
        public string PatientMRN { get; set; }
        public DateTime IndicatorDate { get; set; }
        public double NumeratorValue { get; set; }
        public double DenominatorValue { get; set; }
    }
}