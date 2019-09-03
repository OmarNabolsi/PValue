using System;
using System.Collections.Generic;
using System.Linq;
using Accord.Statistics;
using Accord.Statistics.Testing;
using PValue.Data;
using PValue.DTOs;
using PValue.Models;
using RDotNet;

namespace PValue.Repository
{
    public class RepoService : IRepoService
    {
        private readonly PValueDbContext _context;
        private readonly REngine _engine;
        public RepoService(PValueDbContext context)
        {
            _context = context;

            REngine.SetEnvironmentVariables();
            _engine = REngine.GetInstance();
            _engine.Initialize();
        }

        public IndicatorsInfo GetIndicatorById(int indicatorId)
        {
            return _context.IndicatorsInfo.SingleOrDefault(i => i.OppeIndicatorID == indicatorId);
        }

        public IEnumerable<Physician> GetPhysiciansList(int indicatorId)
        {
            IList<Physician> physiciansList = new List<Physician>();

            var subGroups = _context.IndicatorsData
                                // .Where(g => (g.OppeIndicatorID == indicatorId)
                                // && (g.PayrollID == "AB52356"))
                                .Where(g => g.OppeIndicatorID == indicatorId)
                                .GroupBy(g => new
                                {
                                    g.OppeCycleID,
                                    g.OppeIndicatorID,
                                    g.OppePhysicianSubGroupID
                                })
                                .Select(g => new
                                {
                                    subGroupId = g.Key.OppePhysicianSubGroupID,
                                    oppeCycleId = g.Key.OppeCycleID,
                                    oppeIndicatorId = g.Key.OppeIndicatorID,
                                    physicians = g.ToList()
                                });

            foreach (var grp in subGroups)
            {
                var physicians = grp.physicians
                                    .GroupBy(p => p.PayrollID)
                                    .Select(p => new
                                    {
                                        payrollId = p.Key
                                    });
                foreach (var phys in physicians)
                {
                    var physician = new Physician
                    {
                        ID = phys.payrollId,
                        SubGroupID = grp.subGroupId,
                        OppeCycleID = grp.oppeCycleId,
                        OppeIndicatorID = grp.oppeIndicatorId
                    };

                    physiciansList.Add(physician);
                }
            }

            return physiciansList;
        }

        public Statistics CalculateTTest(Physician phys)
        {
            bool isValid = false;
            double levenesValue = 0;
            double pValue_E = 0;
            double pValue_U = 0;
            double pValue = 0;

            var statistics = new Statistics
            {
                IsValid = false
            };

            var physData = _context.IndicatorsData
                .Where(p => p.PayrollID == phys.ID)
                .Select(p => new { value = p.NumeratorValue });

            if (physData.Count() >= 5)
            {
                #region Build Physician Array
                var physArray = Util.BuildArray(physData);
                #endregion

                #region Build Peers Array
                var peersData = _context.IndicatorsData
                    .Where(p => (p.PayrollID != phys.ID) && (p.OppePhysicianSubGroupID == phys.SubGroupID))
                    .Select(p => new { value = p.NumeratorValue });
                var peersArray = Util.BuildArray(peersData);

                if (peersData.Count() >= 5)
                {
                    statistics.IsValid = true;
                    isValid = true;
                }
                else
                {
                    statistics.IsValid = false;
                    isValid = false;
                }
                #endregion


                if (isValid)
                {
                    #region Levene's Test
                    double[][] doubleArray = new double[][]
                    {
                            physArray,
                            peersArray
                    };
                    var levenesTest = new LeveneTest(doubleArray);
                    levenesValue = levenesTest.PValue;
                    #endregion

                    #region T-Test Equal Variances assumed
                    var TTestEqualVariance = new TwoSampleTTest(physArray, peersArray, assumeEqualVariances: true);
                    pValue_E = TTestEqualVariance.PValue;
                    #endregion

                    #region T-Test UnEqual Variances assumed
                    var TTestUnEqualVariance = new TwoSampleTTest(physArray, peersArray, assumeEqualVariances: false);
                    pValue_U = TTestUnEqualVariance.PValue;
                    #endregion
                    pValue = levenesValue > 0.05 ? pValue_E : pValue_U;
                }

                #region Populating Statistics
                statistics.IsValid = true;
                statistics.PayrollID = phys.ID;
                statistics.NumeratorSum = physArray.Sum();
                statistics.DenominatorSum = physArray.Count();
                statistics.Mean = Measures.Mean(physArray);
                statistics.StandardDeviation = Measures.StandardDeviation(physArray);
                if (isValid)
                {
                    statistics.PeerNumeratorSum = peersArray.Sum();
                    statistics.PeerDenominatorSum = peersArray.Count();
                    statistics.PeerMean = Measures.Mean(peersArray);
                    statistics.PeerStandardDeviation = Measures.StandardDeviation(peersArray);
                    statistics.LevenesTest = levenesValue;
                    statistics.PValue_EqualVariances = pValue_E;
                    statistics.PValue_UnequalVariances = pValue_U;
                    statistics.PValue = pValue;
                }
                #endregion

            }

            return statistics;
        }

        public Statistics CalculateChiSquare(Physician phys)
        {
            bool isValid = false;
            var pValue = 0.0;
            var statistics = new Statistics
            {
                IsValid = false
            };

            var physData = _context.IndicatorsData
                .Where(p => (p.PayrollID == phys.ID)
                    && (p.OppeIndicatorID == phys.OppeIndicatorID)
                    && (p.OppeCycleID == phys.OppeCycleID))
                .Select(p => new { value = Int32.Parse(p.NumeratorValue.ToString()) });

            if (physData.Count() >= 5)
            {
                var physYes = physData.Count(y => y.value == 1);
                var physNo = physData.Count(n => n.value != 1);

                var peersData = _context.IndicatorsData
                        .Where(p => (p.PayrollID != phys.ID) && (p.OppePhysicianSubGroupID == phys.SubGroupID) &&
                        (p.OppeIndicatorID == phys.OppeIndicatorID) &&
                        (p.OppeCycleID == phys.OppeCycleID))
                        .Select(p => new { value = Int32.Parse(p.NumeratorValue.ToString()) });

                var peersYes = peersData.Count(y => y.value == 1);
                var peersNo = peersData.Count(n => n.value != 1);

                if (peersData.Count() >= 5)
                {
                    statistics.IsValid = true;
                    isValid = true;
                }
                else
                {
                    statistics.IsValid = false;
                    isValid = false;
                }

                if (isValid)
                {


                    var total = physYes + physNo + peersYes + peersNo;
                    var ratio = physYes / total;


                    if (ratio < 0.05)
                    {
                        var expr1 = $"fisher.test(rbind(c({physYes},{physNo}),c({peersYes},{peersNo})))$p.value";

                        var fisherP = _engine.Evaluate(expr1).AsNumeric().First();
                        pValue = fisherP;
                    }
                    else
                    {
                        var expr2 = $"chisq.test(rbind(c({physYes},{physNo}),c({peersYes},{peersNo})), correct = FALSE)$p.value";

                        var chiaqP = _engine.Evaluate(expr2).AsNumeric().First();
                        pValue = chiaqP;
                    }

                }

                #region Populating Statistics
                statistics.IsValid = true;
                statistics.PayrollID = phys.ID;
                statistics.NumeratorSum = physYes;
                statistics.DenominatorSum = physYes + physNo;
                statistics.Mean = statistics.NumeratorSum / statistics.DenominatorSum;
                statistics.StandardDeviation = 0;
                if (isValid)
                {
                    statistics.PeerNumeratorSum = peersYes;
                    statistics.PeerDenominatorSum = peersYes + peersNo;
                    statistics.PeerMean = statistics.PeerNumeratorSum / statistics.PeerDenominatorSum;
                    statistics.PeerStandardDeviation = 0;
                    statistics.LevenesTest = 0;
                    statistics.PValue_EqualVariances = 0;
                    statistics.PValue_UnequalVariances = 0;
                    statistics.PValue = pValue;
                }
                #endregion
            }
            return statistics;
        }
        public bool AddOrUpdateStatsTable(Statistics stats, Physician phys)
        {
            var statsQ = _context.StatsTable
                .Where(s => (s.PayrollID == stats.PayrollID)
                    && (s.OppeCycleID == phys.OppeCycleID)
                    && (s.OppeIndicatorID == phys.OppeIndicatorID)
                    && (s.OppePhysicianSubGroupID == phys.SubGroupID));
            var exists = statsQ.Count() > 0;

            if (exists)
            {
                var statsDB = statsQ.SingleOrDefault();
                statsDB.Count = stats.DenominatorSum;
                statsDB.PayrollID = stats.PayrollID;
                statsDB.Alpha = 0.05;
                statsDB.Mean = stats.Mean;
                statsDB.StandardDeviation = stats.StandardDeviation;
                statsDB.PeerCount = stats.PeerDenominatorSum;
                statsDB.Sum = stats.NumeratorSum;
                statsDB.PeerSum = stats.PeerNumeratorSum;
                statsDB.PeerMean = stats.PeerMean;
                statsDB.PeerStandardDeviation = stats.PeerStandardDeviation;
                statsDB.LevenesTest = stats.LevenesTest;
                statsDB.PValue_EqualVariances = stats.PValue_EqualVariances;
                statsDB.PValue_UnequalVariances = stats.PValue_UnequalVariances;
                statsDB.PValue = stats.PValue;
                statsDB.OppeCycleID = phys.OppeCycleID;
                statsDB.OppeIndicatorID = phys.OppeIndicatorID;
                statsDB.OppePhysicianSubGroupID = phys.SubGroupID;

                _context.StatsTable.Update(statsDB);
            }
            else
            {
                var statsTable = new StatsTable
                {
                    Count = stats.DenominatorSum,
                    PayrollID = stats.PayrollID,
                    Alpha = 0.05,
                    Mean = stats.Mean,
                    StandardDeviation = stats.StandardDeviation,
                    PeerCount = stats.PeerDenominatorSum,
                    Sum = stats.NumeratorSum,
                    PeerSum = stats.PeerNumeratorSum,
                    PeerMean = stats.PeerMean,
                    PeerStandardDeviation = stats.PeerStandardDeviation,
                    LevenesTest = stats.LevenesTest,
                    PValue_EqualVariances = stats.PValue_EqualVariances,
                    PValue_UnequalVariances = stats.PValue_UnequalVariances,
                    PValue = stats.PValue,
                    OppeCycleID = phys.OppeCycleID,
                    OppeIndicatorID = phys.OppeIndicatorID,
                    OppePhysicianSubGroupID = phys.SubGroupID
                };

                _context.StatsTable.Add(statsTable);
            }

            _context.SaveChanges();

            return exists;
        }

        public void Dispose()
        {
            _engine.Dispose();
        }
    }
}