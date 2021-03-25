using System.Threading.Tasks;
using Backoffice.Tokens;
using Microsoft.AspNetCore.Mvc;
using MyCrm.TradersDocuments.Grpc;
using MyCrm.TradersDocuments.Grpc.Contracts;

namespace Backoffice.Controllers
{
    public class ShowDocumentController : Controller
    {
        private readonly IMyCrmTradersDocumentsGrpcService _tradersDocumentsGrpc;

        public ShowDocumentController(IMyCrmTradersDocumentsGrpcService tradersDocumentsGrpc)
        {
            _tradersDocumentsGrpc = tradersDocumentsGrpc;
        }
        
        [HttpGet("/Kyc/ShowDocument/{token}")]
        public async ValueTask<ActionResult> Index(string token)
        {
            var documentToken = token.ParseBackOfficeToken<ShowDocumentToken>();
            
            var response = await _tradersDocumentsGrpc.GetDocumentContentAsync(
                new GetDocumentContentGrpcRequest{
                    TraderId = documentToken.TraderId,
                    DocumentId = documentToken.DocumentId,
                    BackOfficeUserId = documentToken.UserId
                }
            );

            if (response.Content == null)
                return Content($"Document with ID: {documentToken.DocumentId} is not found for client {documentToken.TraderId}");
            
            return File(response.Content.Data, response.Content.Mime);
        }
    }
}