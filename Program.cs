using System;
using Microsoft.Extensions.DependencyInjection;
using PValue.Repository;
using PValue.Data;
using System.Linq;
using System.Collections.Generic;
using PValue.Models;
using PValue.DTOs;

namespace PValue
{
    public class Program
    {
        static void Main(string[] args)
        {
            #region Register Services
                var serviceProvider = new ServiceCollection()
                    .AddDbContext<PValueDbContext>()
                    .AddSingleton<IRepoService, RepoService>()
                    .BuildServiceProvider();
            #endregion

            #region GetIndicatorInfo
                Console.WriteLine("Which indicator would you like to calculate the statistics for?");
                var indicatorId = 0;
                Int32.TryParse(Console.ReadLine(), out indicatorId);

                var repo = serviceProvider.GetService<IRepoService>();

                var indicatorInfo = repo.GetIndicatorById(indicatorId);
                if(indicatorInfo == null)
                {
                    Console.WriteLine($"Indicator {indicatorId} not found.");
                }
                else
                {
                    Console.WriteLine("ID = {0}, IndicatorId = {1}, HypothesisTest = {2}", indicatorInfo.ID, indicatorInfo.OppeIndicatorID, indicatorInfo.HypothesisTest);
                    #endregion


                    #region GetPhysicianList
                        var physiciansList = repo.GetPhysiciansList(indicatorId);
                        var addedCount = 0;
                        var updatedCount = 0;

                        foreach (var phys in physiciansList)
                        {
                            Statistics stats= new Statistics();
                            switch (indicatorInfo.HypothesisTest)
                            {
                                case 1:
                                    #region T-Test
                                        stats = repo.CalculateTTest(phys);
                                    #endregion
                                    break;
                                case 2:
                                    stats = repo.CalculateChiSquare(phys);
                                    break;
                                default:
                                    break;
                            }
                            #region Update Database    
                                if(stats.IsValid)
                                {
                                    Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14}, {15}",
                                    stats.DenominatorSum
                                    ,stats.PayrollID
                                    ,stats.Mean
                                    ,stats.StandardDeviation
                                    ,stats.PeerDenominatorSum
                                    ,stats.NumeratorSum
                                    ,stats.PeerNumeratorSum
                                    ,stats.PeerMean
                                    ,stats.PeerStandardDeviation
                                    ,stats.LevenesTest
                                    ,stats.PValue_EqualVariances
                                    ,stats.PValue_UnequalVariances
                                    ,stats.PValue
                                    ,phys.OppeCycleID
                                    ,phys.OppeIndicatorID
                                    ,phys.SubGroupID);
                                    var result = repo.AddOrUpdateStatsTable(stats, phys);

                                    if(result)
                                    {
                                        updatedCount++;
                                    }
                                    else
                                    {
                                        addedCount++;
                                    }
                                }
                            #endregion
                        }
                    #endregion

                    Console.WriteLine("Added {0} records, Updated {1} records.", addedCount, updatedCount);
                }
                
            
            Console.WriteLine("");

            Console.WriteLine("Press any key...");
            Console.ReadKey();
        }
    }
}
