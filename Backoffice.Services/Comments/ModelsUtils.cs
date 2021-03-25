using Backoffice.Abstractions.Models;
using MyCrm.Comments.GrpcContracts.Models;

namespace Backoffice.Services.Comments
{
    public static class ModelsUtils
    {
        public static ICommentModel ToDomain(this CommentGrpcModel src)
        {
            return new CommentModel
            {
                Id = src.Id,
                DateTime = src.DateTime,
                Category = src.Category,
                Author = src.Author,
                Comment = src.Comment
            };
        }
        
        public static CommentGrpcModel ToGrpc(this ICommentModel src)
        {
            return new CommentGrpcModel
            {
                Id = src.Id,
                DateTime = src.DateTime,
                Category = src.Category,
                Author = src.Category,
                Comment = src.Comment,
                Type = TraderCommentType.Comment
            };
        }
    }
}