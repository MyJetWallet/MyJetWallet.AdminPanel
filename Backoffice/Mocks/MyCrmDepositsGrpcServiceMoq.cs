using System.Collections.Generic;
using System.Threading.Tasks;
using MyCrm.Deposits.Grpc;
using MyCrm.Deposits.Grpc.Contracts;
using MyCrm.Deposits.Grpc.Contracts.PaymentProviderSettings;
using MyCrm.Deposits.Grpc.Contracts.PaymentSystemSettings;
using MyCrm.Deposits.Grpc.Models;

namespace Backoffice.Mocks
{
    public class MyCrmDepositsGrpcServiceMoq: IMyCrmDepositsGrpcService
    {
        public IAsyncEnumerable<DepositGrpcModel> GetByTraderIdAsync(GetByTraderIdGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<DepositGrpcModel> GetByPeriodAsync(GetByPeriodGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<DepositGrpcModel> FindByTransactionIdAsync(GetByTransactionIdGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<DepositGrpcModel> GetByTransactionIdAsync(GetByTransactionIdGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<MakeSuccessGrpcResponse> MakeSuccessAsync(MakeSuccessGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<MakeDepositVoidResponse> MakeDepositVoidAsync(MakeDepositVoidRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask SetActivePaymentProviderAsync(SetPaymentProviderRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<GetPaymentProviderRequest> GetActivePaymentProvider()
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<GetAvailablePaymentProvidersGrpcResponse> GetAvailablePaymentProvidersAsync(GetAvailablePaymentProvidersGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<GetPaymentProvidersStrategySettingsGrpcResponse> GetPaymentProvidersStrategySettingsAsync(GetPaymentProvidersStrategySettingsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask SetPaymentProvidersStrategySettingsAsync(SetPaymentProvidersStrategySettingsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<GetSupportedBrandsGrpcResponse> GetSupportedBrandsAsync(GetSupportedBrandsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask RemovePaymentProvidersStrategySettingsAsync(RemovePaymentProvidersStrategySettingsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<GetAvailablePaymentSystemsGrpcResponse> GetAvailablePaymentSystemsAsync(GetAvailablePaymentSystemsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<GetPaymentSystemSettingsGrpcResponse> GetPaymentSystemSettingsAsync(GetPaymentSystemSettingsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask SetPaymentSystemSettingsAsync(SetPaymentSystemSettingsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask RemovePaymentSystemSettingsAsync(RemovePaymentSystemSettingsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}