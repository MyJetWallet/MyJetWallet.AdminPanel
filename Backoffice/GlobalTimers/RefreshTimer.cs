using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MyJetWallet.Sdk.Service.Tools;

namespace Backoffice.GlobalTimers
{
    public static class RefreshTimer
    {
        private static MyTaskTimer _timer;

        private static ILogger _logger;

        public static void SetupTimer(ILogger logger, TimeSpan interval)
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
            }

            _timer = new MyTaskTimer(nameof(RefreshTimer), interval, logger, DoRefresh);
            _logger = logger;
            
            _timer.Start();
        }

        public static void StopTimer()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                _timer = null;
            }
        }

        public static void RegisterCallback(string name, Func<Task> callback)
        {
            lock (_calbacks)
            {
                _calbacks[name] = callback;
            }
        }
        
        public static void RemoveCallback(string name)
        {
            lock (_calbacks)
            {
                _calbacks.Remove(name);
            }
        }

        private static Dictionary<string, Func<Task>> _calbacks = new();
            
        private static async Task DoRefresh()
        {
            List<KeyValuePair<string, Func<Task>>> callbacks = null;
            lock (_calbacks)
            {
                callbacks = _calbacks.ToList();
            }

            foreach (var callback in callbacks)
            {
                try
                {
                    await callback.Value.Invoke();
                }
                catch (Exception ex)
                {
                    _logger?.LogError(ex, "Error on callback {name}", callback.Key);
                    RemoveCallback(callback.Key);
                }
            }
        }
    }
}