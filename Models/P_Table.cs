namespace PValue.Models
{
    public class P_Table
    {
        public int ID { get; set; }
        public int OppeCycleID { get; set; }
        public int OppeIndicatorID { get; set; }
        public int OppePhysicianSubGroupID { get; set; }
        public string PhysicianID { get; set; }
        public double Numerator { get; set; }
        public double Denominator { get; set; }
        public double Mean { get; set; }
        public double PeerNumerator { get; set; }
        public double PeerDenominator { get; set; }
        public double PeerMean { get; set; }
        public double P_Value { get; set; }
    }
}