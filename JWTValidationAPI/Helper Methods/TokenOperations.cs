using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;

namespace EnableBankingTest.SwaggerResponses
{
    public class TokenOperations
    {
        public static TokenValidationParameters TokenValidationParameters(X509Certificate2 certificate)
        {
            // Create token validation parameters
            var validationParameters = new TokenValidationParameters
            {
                RequireExpirationTime = true,
                ValidateIssuer = false, // You may want to validate issuer as well
                ValidateAudience = false, // You may want to validate audience as well
                ValidateLifetime = true,
                IssuerSigningKey = new X509SecurityKey(certificate)
            };
            return validationParameters;
        }
    }
}
