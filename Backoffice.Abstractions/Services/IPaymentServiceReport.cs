using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;

namespace Backoffice.Abstractions.Services
{
    public interface IPaymentServiceReport
    {
        IAsyncEnumerable<IPaymentReportModel> GetByDateRange(DateTime @from, DateTime to);
    }
}