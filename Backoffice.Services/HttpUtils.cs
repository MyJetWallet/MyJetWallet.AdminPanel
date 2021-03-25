using System;
using System.Linq;
using System.Threading.Tasks;
using Backoffice.Abstractions.Bo;
using Microsoft.AspNetCore.Http;

namespace Backoffice.Services
{
    public static class HttpUtils
    {
        public static IBoUsersService BoUsersService { get; set; }

        public static async Task<string> GetBoUserId(this HttpContext ctx)
        {
            string sslCn;
            if (!ctx.Request.Headers.TryGetValue("x-ssl-user", out var sslUser))
            {
                var userId = System.Environment.GetEnvironmentVariable("x-ssl-user");
                sslCn = userId;
            }
            else
            {
                Console.WriteLine($"x-ssl-user: {sslUser.ToString()}");
                sslCn = sslUser.ToString().Split("=").Last();
            }
            var targetUser = await BoUsersService.GetBoUserById(sslCn);

            if (targetUser != null)
                return targetUser.Id;
            
            var allUsers = await BoUsersService.GetBoUsersAsync();

            return allUsers.FirstOrDefault(itm => itm.CertAliases?.Contains(sslCn) ?? false)?.Id ?? sslCn;
        }
    }
}