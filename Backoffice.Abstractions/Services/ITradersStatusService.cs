using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;

namespace Backoffice.Abstractions.Services
{
    public interface ITradersStatusService
    {
        ValueTask UpdateCrmStatus();
        ValueTask UpdateTradingStatus();
        ValueTask<IEnumerable<IStatusModel>> GetCrmStatuses();
        ValueTask<IEnumerable<IStatusModel>> GetTradingStatusesAsync();
    }
}