using System.Collections.Generic;
using System.Threading.Tasks;
using MyCrm.Accounts.Grpc;
using MyCrm.Accounts.Grpc.Contracts;
using MyCrm.Accounts.Grpc.Models;

namespace Backoffice.Mocks
{
    public class MyCrmAccountsGrpcServiceMoq: IMyCrmAccountsGrpcService
    {
        public IAsyncEnumerable<CrmAccountGrpcModel> GetLiveAccountsAsync(GetCrmAccountsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<CrmAccountGrpcModel> GetDemoAccountsAsync(GetCrmAccountsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<CrmAccountGrpcModel> GetAccountAsync(GetCrmAccountsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public Task<TransferMoneyBetweenAccountsResponse> TransferMoneyBetweenAccounts(TransferMoneyBetweenAccountsRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<DealingInfoResponse> GetDealingInfoAsync(DealingInfoRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<BalanceCorrectionGrpcResponse> BalanceCorrectionAsync(BalanceCorrectionGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}