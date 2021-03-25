using System.Collections.Generic;
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
    }
}