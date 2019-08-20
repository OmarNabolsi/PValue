using System;
using System.Collections;
using System.Collections.Generic;
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
            Console.WriteLine("Hello World!");

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
                    var stats = pService.CalculateStats(context, item);
                    
                    Console.WriteLine($"{stats.PayrollID} - Physician - {stats.Count} - {stats.Mean} - {stats.StandardDeviation} - {stats.LevenesTest} - {stats.PValue_EqualVariances}");
                    Console.WriteLine($"        - Peers     - {stats.PeerCount} - {stats.PeerMean} - {stats.PeerStandardDeviation} -  - {stats.PValue_UnequalVariances} ---> {stats.PValue}");
                }
            }
        }
    }
}
