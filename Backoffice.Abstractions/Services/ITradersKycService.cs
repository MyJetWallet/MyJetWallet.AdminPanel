using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;

namespace Backoffice.Abstractions.Services
{
    public interface ITradersKycService
    {
        public ValueTask<IReadOnlyList<ITraderToCheckKyc>> GetTradersToCheckAsync();

        public ValueTask<IReadOnlyList<ITraderToCheckKyc>> GetTradersAsync(DateTime fromDate,
            DateTime toDate);

        public ValueTask UpdateStateAsync(string userAgent, string ip, string traderId,
            BoKycState kycState);

        public ValueTask<BoKycState> GetKycStateAsync(string traderId);
    }
}