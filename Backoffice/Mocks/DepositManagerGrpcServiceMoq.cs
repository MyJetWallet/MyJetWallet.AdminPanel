using System.Threading.Tasks;
using SimpleTrading.Deposit.Grpc;
using SimpleTrading.Deposit.Grpc.Contracts;

namespace Backoffice.Mocks
{
    public class DepositManagerGrpcServiceMoq: IDepositManagerGrpcService
    {
        public ValueTask<CreatePaymentInvoiceGrpcResponse> CreatePaymentInvoiceAsync(CreatePaymentInvoiceGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<GetPaymentMethodsResponse> GetPaymentMethodsAsync()
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<ProcessDepositResponse> ProcessDepositAsync(ProcessDepositRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<ProcessDepositResponse> HandleCryptoDepositCallback(HandleCryptoDepositCallbackRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<GetCryptoWalletAddressResponse> GetCryptoWalletForClient(GetCryptoWalletAddressRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<GetTraderResponse> GetTraderByAccountId(GetTraderRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<GetPaymentSystemsResponse> GetPaymentSystemsAsync(GetPaymentSystemsRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}