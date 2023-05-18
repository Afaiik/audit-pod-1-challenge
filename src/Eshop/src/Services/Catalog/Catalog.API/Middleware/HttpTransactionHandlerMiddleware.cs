using Core.Exceptions.Base;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace Catalog.API.Middleware
{
    public class Response
    {
        public int Severity { get; set; }
        public int StatusCode { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class HttpTransactionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<HttpTransactionHandlerMiddleware> _logger;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="next"></param>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        public HttpTransactionHandlerMiddleware(
            RequestDelegate next,
            ILogger<HttpTransactionHandlerMiddleware> logger,
            IConfiguration configuration)
        {
            _next = next;
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task InvokeAsync(HttpContext context)
        {
            //Autenticacion
            //context.Request.Headers
            var jwtToken = DecodeNextSessionToken(context.Request.Headers["Authorization"]);

            var validationResult = ValidateGoogleJwt(jwtToken);

            try
            {
                //TODO: implement global validator
                //_validator.Validate();
                await HandleValidation(context).ConfigureAwait(false);
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleException(context, e).ConfigureAwait(false);
            }
        }
        /*
         * ojota con esto eee
         
         public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
    {
        // Invoke the validators
        var failures = _validators
            .Select(validator => validator.Validate(request))
            .SelectMany(result => result.Errors)
            .ToArray();

        if (failures.Length > 0)
        {
            // Map the validation failures and throw an error,
            // this stops the execution of the request
            var errors = failures
                .GroupBy(x => x.PropertyName)
                .ToDictionary(k => k.Key, v => v.Select(x => x.ErrorMessage).ToArray());
            throw new InputValidationException(errors);
        }

        // Invoke the next handler
        // (can be another pipeline behavior or the request handler)
        return next();
    }
}
         */

        private async Task HandleValidation(HttpContext context)
        {
            //TODO: IValidator resolver from url

            //TODO: invoke validation

        }

        private async Task HandleException(HttpContext context, Exception exception)
        {
            // search for static
            context.Response.ContentType = "application/problem+json";

            var result = JsonSerializer.Serialize(new Response
            {
                StatusCode = exception switch
                {
                    CatalogApiBaseException => StatusCodes.Status401Unauthorized,
                    _ => StatusCodes.Status500InternalServerError,
                }
            });

            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;

            _logger.LogError(result);
            await context.Response.WriteAsync(result);
        }

        private string DecodeNextSessionToken(string nextSessionToken)
        {

            // Configura la clave secreta para la firma del JWT (puedes usar tu propia clave)
            string secretKey = _configuration.GetSection("Jwt:SecretKey").Value;
            if (string.IsNullOrWhiteSpace(secretKey)) throw new ArgumentNullException("Secret Key Not Defined");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            // Configura la firma y la configuración del token JWT
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("sessionToken", nextSessionToken) }),
                Expires = DateTime.UtcNow.AddDays(7), // Configura la fecha de expiración del JWT como desees
                SigningCredentials = signingCredentials
            };

            // Crea el token JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);
            var jwt = tokenHandler.WriteToken(jwtToken);

            // Utiliza el token JWT como desees
            return jwt;
        }

        public bool ValidateGoogleJwt(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Obtén la clave pública de Google para verificar la firma del token
            var googlePublicKeys = GetGooglePublicKeys(); // Implementa tu método para obtener las claves públicas de Google

            // Define la configuración de validación del token
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = "https://accounts.google.com", // Emisor del token JWT de Google
                ValidateAudience = true,
                ValidAudience = "310780174873-hnkbfbglr76ekpdnlehsq57qkv11hfuo.apps.googleusercontent.com", // El ID de cliente de tu aplicación en Google
                ValidateLifetime = true,
                IssuerSigningKeys = googlePublicKeys // Claves públicas de Google para verificar la firma
            };

            try
            {
                // Valida el token
                var claimsPrincipal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken result);

                // Aquí puedes realizar acciones adicionales con los claims si lo deseas

                return result != null; // El token es válido
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false; // El token no es válido
            }
        }

        private IEnumerable<SecurityKey> GetGooglePublicKeys()
        {
            // Obtiene las claves públicas de Google desde su servicio de descubrimiento de claves JSON Web
            // Puedes utilizar la URL: "https://www.googleapis.com/oauth2/v3/certs"
            // para obtener las claves públicas en formato JSON Web Key Set (JWKS)
            // Luego, puedes utilizar la clase JsonWebKeySet para deserializar las claves públicas

            // Ejemplo de cómo deserializar las claves públicas desde un archivo local en formato JWKS
            string jwksJson = File.ReadAllText("google-public-keys.json");
            var jsonWebKeySet = new JsonWebKeySet(jwksJson);

            return jsonWebKeySet.GetSigningKeys();
        }

    }
}
