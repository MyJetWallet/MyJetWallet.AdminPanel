using System.Collections.Generic;
using System.Threading.Tasks;
using MyCrm.Comments.GrpcContracts;
using MyCrm.Comments.GrpcContracts.Contracts;
using MyCrm.Comments.GrpcContracts.Models;

namespace Backoffice.Mocks
{
    public class MyCrmCommentsGrpcServiceMoq: IMyCrmCommentsGrpcService
    {
        public ValueTask PostAsync(CommentGrpcModel comment)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<CommentGrpcModel> GetDataAsync(GetCommentsByCategory request)
        {
            throw new System.NotImplementedException();
        }

        public IAsyncEnumerable<CommentGrpcModel> GetDataByCategoriesAsync(GetCommentsByCategories categories)
        {
            throw new System.NotImplementedException();
        }
    }
}