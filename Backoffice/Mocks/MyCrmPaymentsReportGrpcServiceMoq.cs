using System.Collections.Generic;
using System.Threading.Tasks;
using MyCRM.Logs.GrpcContracts.Models;
using MyCrm.PaymentReport.GrpcContracts;
using MyCrm.PaymentReport.GrpcContracts.Contracts;
using MyCrm.PaymentReport.GrpcContracts.Models;

namespace Backoffice.Mocks
{
    public class MyCrmPaymentsReportGrpcServiceMoq: IMyCrmPaymentsReportGrpcService
    {
        public IAsyncEnumerable<PaymentGrpcModel> GetPaymentsAsync(GetPaymentsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<PaginatedPaymentsGrpcModel> GetPaginatedPaymentsAsync(GetPaginatedPaymentsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<(string value, string displayValue)>> GetPossibleValuesAsync(GetPossibleValuesGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}