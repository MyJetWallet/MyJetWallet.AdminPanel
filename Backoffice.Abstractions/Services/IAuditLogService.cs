using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;

namespace Backoffice.Abstractions.Services
{
    public interface IAuditLogService
    {
        ValueTask<IReadOnlyList<IAuditLogModel>> GetByTraderId(string traderId, DateTime @from, DateTime to);
    }
}