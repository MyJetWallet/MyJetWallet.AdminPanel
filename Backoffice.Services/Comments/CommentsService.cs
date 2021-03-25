using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;
using Backoffice.Abstractions.Services;
using DotNetCoreDecorators;
using MyCrm.Comments.GrpcContracts;
using MyCrm.Comments.GrpcContracts.Contracts;

namespace Backoffice.Services.Comments
{
    public class CommentsService : ICommentsService
    {
        private readonly IMyCrmCommentsGrpcService _crmDatasource;

        public CommentsService(IMyCrmCommentsGrpcService src)
        {
            _crmDatasource = src;
        }
        
        public async ValueTask PostCommentAsync(ICommentModel model)
        {
            await _crmDatasource.PostAsync(model.ToGrpc());
        }

        public async ValueTask<IEnumerable<ICommentModel>> GetDataAsync(string categoryId)
        {
            return await _crmDatasource.GetDataAsync(GetCommentsByCategory.Create(categoryId))
                .SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }

        public async ValueTask<IEnumerable<ICommentModel>> GetDataAsync(IEnumerable<string> categories)
        {
            return await _crmDatasource.GetDataByCategoriesAsync(GetCommentsByCategories.Create(categories))
                .SelectAsync(itm => itm.ToDomain())
                .AsListAsync();
        }
    }
}