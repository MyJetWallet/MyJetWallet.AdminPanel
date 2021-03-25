using System.Collections.Generic;
using System.Threading.Tasks;
using MyCrm.Withdrawals.Grpc;
using MyCrm.Withdrawals.Grpc.Contracts;
using MyCrm.Withdrawals.Grpc.Models;

namespace Backoffice.Mocks
{
    public class MyCrmWithdrawalsGrpcServiceMoq: IMyCrmWithdrawalsGrpcService
    {
        public ValueTask<PendingWithdrawalsAmountGrpcResponse> GetPendingWithdrawalsAmountAsync(GetPendingWithdrawalsAmountGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<MyCrmWithdrawGrpcModel> GetPendingAsync(GetPendingWithdrawsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<MyCrmWithdrawGrpcModel> GetByPeriodAsync(GetWithdrawsByPeriodGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<MyCrmWithdrawGrpcModel> GetByIdAsync(GetWithdrawByIdGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask UpdateWithdrawDetailsAsync(UpdateWithdrawDetailsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask ProcessWithdrawAsync(ProcessWithdrawGrpcContracts request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask CancelWithdrawAsync(ProcessWithdrawGrpcContracts request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<ReserveWithdrawMoneyResponse> ReserveWithdrawMoneyAsync(ProcessWithdrawGrpcContracts request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask CancelWithReservationAsync(ProcessWithdrawGrpcContracts request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask AddWithdrawalAsync(AddWithdrawalRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}