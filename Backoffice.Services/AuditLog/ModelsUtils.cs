using Backoffice.Abstractions.Models;
using MyCrm.AuditLog.Grpc.Models;

namespace Backoffice.Services.AuditLog
{
    public static class ModelsUtils
    {
        public static IAuditLogModel ToDomain(this AuditLogEventGrpcModel src)
        {
            return new AuditLogModel
            {
                TraderId = src.TraderId,
                Action = src.Action,
                ActionId = src.ActionId,
                DateTime = src.DateTime,
                Message = src.Message,
                TechData = src.TechData,
                Author = src.Author
            };
        }
    }
}