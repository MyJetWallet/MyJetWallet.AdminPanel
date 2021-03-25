using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;

namespace Backoffice.Abstractions.Services
{
    public interface ICommentsService
    {
        ValueTask PostCommentAsync(ICommentModel model);
        ValueTask<IEnumerable<ICommentModel>> GetDataAsync(string categoryId);
        ValueTask<IEnumerable<ICommentModel>> GetDataAsync(IEnumerable<string> categories);
    }
}