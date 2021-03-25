using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;

namespace Backoffice.Abstractions.Services
{
    public interface ILogService
    {
        IAsyncEnumerable<IRegisterLogModel> GetRegistrationLogsAsync(DateTime dateFrom, DateTime dateTo);
        ValueTask<IReadOnlyList<IAuthLogModel>> GetAuthLogsAsync(string traderId, DateTime dateFrom, DateTime dateTo);
    }
}