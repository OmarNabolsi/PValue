using System;
using System.Collections.Generic;
using PValue.DTOs;
using PValue.Models;

namespace PValue.Repository
{
    public interface IRepoService : IDisposable
    {
        IndicatorsInfo GetIndicatorById(int indicatorId);
        IEnumerable<Physician> GetPhysiciansList(int indicatorId);
        Statistics CalculateTTest(Physician phys);
        Statistics CalculateChiSquare(Physician phys);
        bool AddOrUpdateStatsTable(Statistics stats, Physician phys);
    }
}