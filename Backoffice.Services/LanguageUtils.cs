using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Backoffice.Abstractions.Models;

namespace Backoffice.Services
{
    public static class LanguageUtils
    {
        public static async IAsyncEnumerable<TOut> SelectAsync<TIn, TOut>(this IAsyncEnumerable<TIn> src,
            Func<TIn, TOut> selectCallback)
        {
            await foreach (var itm in src)
                yield return selectCallback(itm);
        }

        public static async ValueTask<T> FirstOrDefaultAsync<T>(this IAsyncEnumerable<T> src, Func<T, bool> callback)
        {
            await foreach (var itm in src)
            {
                if (callback(itm))
                    return itm;
            }

            return default;
        }

        public static async ValueTask<T> FirstOrDefaultAsync<T>(this IAsyncEnumerable<T> src)
        {
            await foreach (var itm in src)
            {
                return itm;
            }

            return default;
        }
        
        public static async IAsyncEnumerable<T> WhereAsync<T>(this IAsyncEnumerable<T> src, Func<T, bool> callback)
        {
            await foreach (var itm in src)
            {
                if (callback(itm))
                    yield return itm;
            }
        }
    }
}