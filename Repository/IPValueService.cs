using System.Collections.Generic;
using PValue.Data;
using PValue.DTOs;
using PValue.Models;

namespace PValue.Repository
{
    public interface IPValueService
    {
        IList<Physician> GetIndicatorList(PValueDbContext context);
        Statistics CalculateStats(PValueDbContext context, Physician item);
    }
}