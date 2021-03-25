using System;
using System.Collections.Generic;

namespace Backoffice.Services
{
    public interface ILastSearchUserCache
    {
        void Add(string userId, string newCachedItem);
        IEnumerable<string> GetUserCachedData(string userId);
    }

    public class LastSearchUserCache : ILastSearchUserCache
    {
        private readonly object _lockObject = new object();

        private readonly Dictionary<string, List<string>> _usersSearchCache = new Dictionary<string, List<string>>();

        public void Add(string userId, string newCacheItem)
        {
            lock (_lockObject)
            {
                if (!_usersSearchCache.ContainsKey(userId))
                {
                    _usersSearchCache.Add(userId, new List<string>());
                }

                _usersSearchCache[userId].Add(newCacheItem);

                GarbageCollectUserItems(userId);
            }
        }

        public IEnumerable<string> GetUserCachedData(string userId)
        {
            lock (_lockObject)
            {
                if (!_usersSearchCache.ContainsKey(userId))
                {
                    return Array.Empty<string>();
                }
                
                return _usersSearchCache[userId];
            }
        }

        private void GarbageCollectUserItems(string userId)
        {
            lock (_lockObject)
            {
                if (_usersSearchCache[userId].Count > 10)
                    _usersSearchCache[userId].RemoveAt(0);
            }
        }
    }
}