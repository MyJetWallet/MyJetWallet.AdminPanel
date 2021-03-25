using System;
using Backoffice.Abstractions.Models;
using Backoffice.Services.Transactions;
using MyCRM.AccountTransactions.Grpc.Models;

namespace Backoffice.Services.ActiveDeals
{
    public static class ModelsUtils
    {
        public static IActiveDealModel ToDomain(this ActiveDealGrpcModel src)
        {
            return new ActiveDealModel
            {
                DateTime = src.DateTime,
                Id = src.Id,
                Instrument = src.Instrument,
                Operation = src.Operation.ToDomain(),
                Volume = src.Volume,
                Pl = src.Pl,
                Swaps = src.Swaps,
                Commissions = src.Commissions,
                AccountId = src.AccountId,
                OpenPrice = src.OpenPrice,
                OpenDuration = DateTime.UtcNow - src.DateTime
            };
        }
    }
}