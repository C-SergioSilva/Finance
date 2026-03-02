using System.Net;
using Newtonsoft.Json;

namespace Finance.Api.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Deixa a requisição seguir o caminho dela (passar pelas outras "estações")
                await _next(context);
            }
            catch (Exception ex)
            {
                // Se qualquer erro estourar em qualquer lugar, ele cai aqui!
                _logger.LogError(ex, "Ocorreu um erro não tratado.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            // personalização do Status Code baseado no tipo de erro
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

            var response = new
            {
                StatusCode = context.Response.StatusCode,
                Message = "Ocorreu um erro interno na operação.",
                Detailed = exception.Message // Em produção, omitir isso por segurança
            };

            return context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}