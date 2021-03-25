using System.Collections.Generic;
using MyCRM.AccountTransactions.Grpc;
using MyCRM.AccountTransactions.Grpc.Contracts;
using MyCRM.AccountTransactions.Grpc.Models;

namespace Backoffice.Mocks
{
    public class MyCrmActiveDealsGrpcServiceMoq: IMyCrmActiveDealsGrpcService
    {
        public IAsyncEnumerable<ActiveDealGrpcModel> GetAsync(GetActiveDealsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}