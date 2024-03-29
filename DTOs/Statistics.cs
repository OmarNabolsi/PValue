namespace PValue.DTOs
{
    public class Statistics
    {
        public int Count { get; set; }
        public string PayrollID { get; set; }
        public double[] NumeratorArray { get; set; }
        public double NumeratorSum { get; set; }
        public double DenominatorSum { get; set; }
        public double Mean { get; set; }
        public double StandardDeviation { get; set; }
        public int PeerCount { get; set; }
        public double[] PeerNumeratorArray { get; set; }
        public double PeerNumeratorSum { get; set; }
        public double PeerDenominatorSum { get; set; }
        public double PeerMean { get; set; }
        public double PeerStandardDeviation { get; set; }
        public double LevenesTest { get; set; }
        public double PValue_EqualVariances { get; set; }
        public double PValue_UnequalVariances { get; set; }
        public double PValue { get; set; }
        public bool IsValid { get; set; }
    }
}