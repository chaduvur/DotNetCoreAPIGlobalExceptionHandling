using GlobalExceptionHandlerMiddleware.DAO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace GlobalExceptionHandlerMiddleware.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public GlobalExceptionMiddleware(RequestDelegate next) {

            _next = next;
        }

        public async Task InvokeAsync(HttpContext context) {

            try
            {
                await _next(context);
            }            
            catch (InvalidOperationException exInvalidOp)
            {
                await HandleExceptionAsync(context, exInvalidOp);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            string message = string.Empty;
            if (ex.GetType() == typeof(InvalidOperationException)) {
                message = "Invalid operation exception from custom middleware.";
            }
            else
               message = "Internal Server error from custome middleware";

            byte[] error = Encoding.ASCII.GetBytes(new ErrorDetails() {statusCode=context.Response.StatusCode,message = message }.ToString());
            await context.Response.Body.WriteAsync(error,0, error.Length);
        }
    }
}
