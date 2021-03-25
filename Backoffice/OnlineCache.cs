using System.Collections.Generic;

namespace Backoffice
{
    public static class OnlineCache
    {
        private static readonly object LockObject = new();
        private static readonly HashSet<string> OnlineUsersIds = new();

        public static void HandleOnline(string userId)
        {
            lock (LockObject)
                OnlineUsersIds.Add(userId);
        }

        public static void HandleOffline(string userId)
        {
            lock (LockObject)
                OnlineUsersIds.Remove(userId);
        }

        public static bool IsOnline(string userId)
        {
            lock (LockObject)
                return OnlineUsersIds.Contains(userId);
        }
    }
}