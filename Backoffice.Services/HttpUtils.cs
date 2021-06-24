using System;
using System.Linq;
using System.Threading.Tasks;
using Backoffice.Abstractions.Bo;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace Backoffice.Services
{
    public static class HttpUtils
    {
        public static IBoUsersService BoUsersService { get; set; }

        public static async Task<string> GetBoUserId(this HttpContext ctx)
        {
            var sslUser = new StringValues();
            try
            {
                if (ctx?.Request?.Headers == null)
                {
                    Console.WriteLine("ctx?.Request?.Headers is null!!");
                    return "Ctx is null";
                }
                
                string sslCn;
                if (!ctx.Request.Headers.TryGetValue("x-ssl-user", out sslUser))
                {
                    Console.WriteLine($"Do not found header 'x-ssl-user' {ctx.Request.Method}|{ctx.Request.Scheme}|{ctx.Request.Protocol}");
                    var userId = System.Environment.GetEnvironmentVariable("x-ssl-user");
                    sslCn = userId;
                }
                else
                {
                    if (string.IsNullOrEmpty(sslUser.ToString()))
                    {
                        Console.WriteLine($"Receive empty x-ssl-user: {sslUser}");
                        sslCn = null;
                    }
                    else
                    {
                        sslCn = sslUser.ToString().Split("=").Last();
                    }
                }

                if (string.IsNullOrEmpty(sslCn))
                    return null;

                var targetUser = await BoUsersService.GetBoUserById(sslCn);

                if (targetUser != null)
                    return targetUser.Id;

                var allUsers = await BoUsersService.GetBoUsersAsync();

                return allUsers.FirstOrDefault(itm => itm.CertAliases?.Contains(sslCn) ?? false)?.Id ?? sslCn;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"x-ssl-user: {sslUser.ToString()}");
                throw;
            }
        }
    }
}