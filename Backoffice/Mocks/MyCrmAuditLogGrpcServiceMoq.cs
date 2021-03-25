using System.Collections.Generic;
using System.Threading.Tasks;
using MyCrm.AuditLog.Grpc;
using MyCrm.AuditLog.Grpc.Contracts;
using MyCrm.AuditLog.Grpc.Models;

namespace Backoffice.Mocks
{
    public class MyCrmAuditLogGrpcServiceMoq: IMyCrmAuditLogGrpcService
    {
        public ValueTask SaveAsync(AuditLogEventGrpcModel request)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<AuditLogEventGrpcModel> GetAsync(GetAuditLogEventsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<AuditLogEventGrpcModel> GetAllByTraderAsync(GetAuditLogByTraderIdRequest request)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<AuditLogEventGrpcModel> GetAllByActionAsync(GetAuditLogByActionRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}