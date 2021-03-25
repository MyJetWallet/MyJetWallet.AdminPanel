using System.Collections.Generic;
using System.Threading.Tasks;
using MyCrm.TradersDocuments.Grpc;
using MyCrm.TradersDocuments.Grpc.Contracts;
using MyCrm.TradersDocuments.Grpc.Models;

namespace Backoffice.Mocks
{
    public class MyCrmTradersDocumentsGrpcServiceMoq: IMyCrmTradersDocumentsGrpcService
    {
        public IAsyncEnumerable<CrmTraderDocumentGrpcModel> GetUploadedDocumentsAsync(GetUploadedTraderDocumentsGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask<DocumentContentGrpcResponse> GetDocumentContentAsync(GetDocumentContentGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask UploadDocumentAsync(UploadDocumentCrmGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask DeleteDocumentAsync(DeleteDocumentCrmGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }

        public ValueTask ChangeVisibilityAsync(ChangeDocumentVisibilityCrmGrpcRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}