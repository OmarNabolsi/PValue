using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Statistics;
using Accord.Statistics.Testing;
using PValue.Data;
using PValue.DTOs;
using PValue.Models;

namespace PValue.Repository
{
    public class PValueService : IPValueService
    {
        public IList<Physician> GetIndicatorList(PValueDbContext context)
        {
            var subGrps = context.Indicator_12
                    .GroupBy(i => i.OppePhysicianSubGroupID)
                    // .Where(i => i.Key == 27)
                    .ToList();

            IList<Physician> indList = new List<Physician>();
            
            foreach (var subGrp in subGrps)
            {
                var grps = context.Indicator_12
                    .Where(g => g.OppePhysicianSubGroupID == subGrp.Key)
                    .GroupBy(i => i.PayrollID);
                foreach (var item in grps)
                {
                    var phys = context.Indicator_12
                        .First(i => i.PayrollID == item.Key);

                    var grp = new Physician
                    {
                        ID = phys.PayrollID,
                        SubGroupID = phys.OppePhysicianSubGroupID,
                        OppeCycleID = phys.OppeCycleID,
                        OppeIndicatorID = phys.OppeIndicatorID
                    };
                    
                    indList.Add(grp);
                }
            }
            
            return indList;
        }
        public Statistics CalculateStats(PValueDbContext context, Physician item)
        {
            var phys = context.Indicator_12
                .Where(p => p.PayrollID == item.ID)
                .Select(p => new { num = p.NumeratorValue });
   
            var physCount = phys.Count();
            // string physNum = "";
            double[] x1 = new double[physCount];
            double physNumVal = 0;
            double physDenVal = 0;
            double physMean = 0;
            int i = 0;
            
            foreach (var phy in phys)
            {
                x1[i] = phy.num;
                // physNum += physNum == "" ? phy.num.ToString() : " " + phy.num.ToString();
                physNumVal += phy.num;
                physDenVal++;
                i++;
            }
            if (physDenVal != 0)
                physMean = physNumVal / physDenVal;

            var peers = context.Indicator_12
                .Where(p => p.OppePhysicianSubGroupID == item.SubGroupID)
                .Where(p => p.PayrollID != item.ID)
                .Select(p => new
                {
                    num = p.NumeratorValue
                });

            var peersCount = peers.Count();
            double[] x2 = new double[peersCount];
            // string peersNum = "";
            double peersNumVal = 0;
            double peersDenVal = 0;
            double peersMean = 0;


            double levenesTest = 0;
            double pValueA = 0;
            double pValueNA = 0;

            if (peers.Count() >= 5)
            {
                int j = 0;
                foreach (var peer in peers)
                {
                    x2[j] = peer.num;
                    peersNumVal += peer.num;
                    peersDenVal++;
                    j++;
                }
                if (peersDenVal != 0)
                {
                    peersMean = peersNumVal / peersDenVal;
                }

                #region Levene's Test
                double[][] X = new double[][]
                {
                    x1,
                    x2
                };

                var levenes = new LeveneTest(X);
                levenesTest = levenes.PValue;
                #endregion

                Console.WriteLine($"Grp = {item.SubGroupID} - Phys = {item.ID}");

                #region T-Test Equal variances assumed
                var ta = new TwoSampleTTest(x1, x2, assumeEqualVariances: true);
                pValueA = ta.PValue;
                #endregion

                #region T-Test Equal variances not assumed
                var tna = new TwoSampleTTest(x1, x2, assumeEqualVariances: false);
                pValueNA = tna.PValue;
                #endregion
            }

            var stats = new Statistics
            {
                Count = physCount,
                PayrollID = item.ID,
                NumeratorSum = physNumVal,
                DenominatorSum = physDenVal,
                Mean = physMean,
                StandardDeviation = Measures.StandardDeviation(x1),
                PeerCount = peersCount,
                PeerNumeratorSum = peersNumVal,
                PeerDenominatorSum = peersDenVal,
                PeerMean = peersMean,
                PeerStandardDeviation = Measures.StandardDeviation(x2),
                LevenesTest = levenesTest,
                PValue_EqualVariances = pValueA,
                PValue_UnequalVariances = pValueNA,
                PValue = levenesTest > 0.05 ? pValueA : pValueNA
            };
            
            return stats;
        }

    }
}