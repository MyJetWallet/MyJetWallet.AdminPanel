using Backoffice.Abstractions.Models;
using System;
using System.Threading.Tasks;

namespace Backoffice.Extensions
{
    public static class LoaderExtensions
    {
        public static async Task<T> WithLoaderAsync<T>(this Task<T> task, ILoaderConfiguration loaderConfiguration)
        {
            try
            {
                loaderConfiguration ??= new LoaderConfiguration();
                loaderConfiguration.Enable();
                return await task;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                loaderConfiguration.Disable();
            }
        }

        public static async Task WithLoaderAsync(this Task task, ILoaderConfiguration loaderConfiguration)
        {
            try
            {
                loaderConfiguration ??= new LoaderConfiguration();
                loaderConfiguration.Enable();
                await task;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                loaderConfiguration.Disable();
            }
        }

        public static async ValueTask WithLoaderAsync(this ValueTask task, ILoaderConfiguration loaderConfiguration)
        {
            try
            {
                loaderConfiguration ??= new LoaderConfiguration();
                loaderConfiguration.Enable();
                await task.Preserve();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                loaderConfiguration.Disable();
            }
        }

        public static async ValueTask<T> WithLoaderAsync<T>(this ValueTask<T> task, ILoaderConfiguration loaderConfiguration)
        {
            try
            {
                loaderConfiguration ??= new LoaderConfiguration();
                loaderConfiguration.Enable();
                return await task.Preserve();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                loaderConfiguration.Disable();
            }
        }
    }
}
