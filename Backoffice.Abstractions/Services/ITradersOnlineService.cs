using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;
using Backoffice.Datatypes;

namespace Backoffice.Abstractions.Services
{
    public interface ITradersOnlineService
    {
        ValueTask<IEnumerable<ITraderOnline>> GetAsync(int page);
    }
}