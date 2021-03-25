using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.Services;
using DotNetCoreDecorators;
using MyCrm.PaymentReport.GrpcContracts;
using MyCrm.PaymentReport.GrpcContracts.Contracts;

namespace Backoffice.Services.Deposits
{
    public class PaymentReportService : IPaymentServiceReport
    {
        private IMyCrmPaymentsReportGrpcService _crmDatasource;

        public PaymentReportService(IMyCrmPaymentsReportGrpcService crmDatasource)
        {
            _crmDatasource = crmDatasource;
        }

        public async IAsyncEnumerable<IPaymentReportModel> GetByDateRange(DateTime @from, DateTime to)
        {
            var request = new GetPaymentsGrpcRequest {From = from, To = to};
            var payments = _crmDatasource.GetPaymentsAsync(request);

            await foreach (var payment in payments)
                yield return payment.ToDomain();
        }
    }
}