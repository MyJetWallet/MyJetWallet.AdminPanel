using System;
using MyTokenGeneratorUtils;

namespace Backoffice
{
    public static class TokensUtils
    {
        public static T ParseBackOfficeToken<T>(this string token) where T:class, ITokenBase
        {
            var tokenResult = token.ParseHexToken<T>(TokenStore.Token);
            
            if (tokenResult.result != TokenParseResult.Ok)
                throw new Exception("Token is "+tokenResult);

            return tokenResult.token;
        }
        
    }
}