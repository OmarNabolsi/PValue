using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using PValue.Data;
using PValue.Models;
using PValue.Repository;

namespace PValue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Calculating Stats...");

            using (var context = new PValueDbContext())
            {
                var serviceProvider = new ServiceCollection()
                    .AddSingleton<IPValueService, PValueService>()
                    .BuildServiceProvider();

                //----//
                var pService = serviceProvider.GetService<IPValueService>();
                var indList = pService.GetIndicatorList(context);

                foreach (var item in indList)
                {
                    //----//
                    var phys = context.Indicator_12
                        .Where(p => p.PayrollID == item.ID);

                    if (phys.Count() >= 5)
                    {
                        var stats = pService.CalculateStats(context, item);
                    
                        #region Insert Stats
                        var statsTable = new StatsTable()
                        {
                            PayrollID = stats.PayrollID,
                            OppeCycleID = item.OppeCycleID,
                            OppeIndicatorID = item.OppeIndicatorID,
                            OppePhysicianSubGroupID = item.SubGroupID,
                            Count = stats.Count,
                            NumeratorSum = stats.NumeratorSum,
                            DenominatorSum = stats.DenominatorSum,
                            Mean = stats.Mean,
                            StandardDeviation = stats.StandardDeviation,
                            PeerCount = stats.PeerCount,
                            PeerNumeratorSum = stats.PeerNumeratorSum,
                            PeerDenominatorSum = stats.PeerDenominatorSum,
                            PeerMean = stats.PeerMean,
                            PeerStandardDeviation = stats.PeerStandardDeviation,
                            LevenesTest = stats.LevenesTest,
                            PValue_EqualVariances = stats.PValue_EqualVariances,
                            PValue_UnequalVariances = stats.PValue_UnequalVariances,
                            PValue = stats.PValue
                        };
                        context.StatsTable.Add(statsTable);
                        context.SaveChanges();
                        #endregion
                    }
                    
                    // Console.WriteLine($"{stats.PayrollID} - Physician - {stats.Count} - {stats.Mean} - {stats.StandardDeviation} - {stats.LevenesTest} - {stats.PValue_EqualVariances}");
                    // Console.WriteLine($"        - Peers     - {stats.PeerCount} - {stats.PeerMean} - {stats.PeerStandardDeviation} -  - {stats.PValue_UnequalVariances} ---> {stats.PValue}");
                }
                Console.WriteLine("Done!");
                Console.WriteLine("Press Enter Key...");
                Console.Read();
            }
        }
    }
}
