using System.Collections.Generic;
using MyCRM.AccountTransactions.Grpc;
using MyCRM.AccountTransactions.Grpc.Contracts;
using MyCRM.AccountTransactions.Grpc.Models;

namespace Backoffice.Mocks
{
    public class MyCrmAccountTransactionsGrpcServiceMoq: IMyCrmAccountTransactionsGrpcService
    {
        public IAsyncEnumerable<AccountTransactionGrpcModel> GetAsync(GetAccountTransactionsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}