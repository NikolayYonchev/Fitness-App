//using System.Net;
//using System.Text.Json;

//namespace FitnessApp.Middleware
//{
//    public class ErrorHandlingMiddleware
//    {
//        private readonly RequestDelegate _next;

//        public ErrorHandlingMiddleware(RequestDelegate next)
//        {
//            _next = next;
//        }

//        public async Task Invoke(HttpContext context)
//        {
//            try
//            {
//                await _next(context);
//            }
//            catch (Exception ex)
//            {
//                await HandleExceptionAsync(context, ex);
//            }
//        }

//        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
//        {
//            var response = context.Response;
//            response.ContentType = "application/json";

//            var statusCode = exception switch
//            {
//                ArgumentException => HttpStatusCode.BadRequest,
//                KeyNotFoundException => HttpStatusCode.NotFound,
//                UnauthorizedAccessException => HttpStatusCode.Unauthorized,
//                _ => HttpStatusCode.InternalServerError
//            };

//            response.StatusCode = (int)statusCode;

//            var errorResponse = new { message = exception.Message, status = response.StatusCode };

//            return response.WriteAsync(JsonSerializer.Serialize(errorResponse));
//        }
//    }
//}
