using EnableBankingTest.Helper_Methods;
using EnableBankingTest.SwaggerResponses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace JwtValidatorApi
{
    [ApiController]
    [Route("api/[controller]")]
    public class JwtValidationController : ControllerBase
    {
        /// <summary>
        /// Validates the JWT token.
        /// </summary>
        /// <returns>Returns a response indicating the validity of the JWT token.</returns>
        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ValidationResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
        public async Task<IActionResult> ValidateJwt()
        {
            try
            {
                string authorizationHeader = Request.Headers[ApplicationConstants.Authorization];

                if (string.IsNullOrEmpty(authorizationHeader))
                {
                    return BadRequest(new ErrorResponse { Message = Messages.AuthRequired });
                }

                if (!authorizationHeader.StartsWith(ApplicationConstants.Bearer))
                {
                    return BadRequest(new ErrorResponse { Message = Messages.InvalidAuth });
                }

                var handler = new JwtSecurityTokenHandler();

                string token = authorizationHeader.Substring(ApplicationConstants.Bearer.Length).Trim();
                
                JwtSecurityToken jwtToken = handler.ReadToken(token) as JwtSecurityToken;

               
                string x5UUrl = jwtToken.Header[ApplicationConstants.PemFileConst]?.ToString();

                if (string.IsNullOrEmpty(x5UUrl))
                {
                    return BadRequest(new ErrorResponse { Message = Messages.x5UError });
                }

                var certificate = await FileContentOperations.GetCertificate(x5UUrl);

                var validationParameters = TokenOperations.TokenValidationParameters(certificate);

                try
                {
                    SecurityToken validatedToken;
                    var principal = handler.ValidateToken(token, validationParameters, out validatedToken);
                }
                catch (SecurityTokenExpiredException)
                {
                    return BadRequest(new ErrorResponse { Message = Messages.TokenExpired });
                }
                catch (SecurityTokenInvalidSignatureException)
                {
                    return BadRequest(new ErrorResponse { Message = Messages.SignatureInvalid });
                }
                catch (SecurityTokenException ex)
                {
                    return BadRequest(new ErrorResponse { Message = Messages.TokenInvalid });
                }

                return Ok(new { Valid = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, Messages.InteralServerError);
            }
        }

        
    }
}
