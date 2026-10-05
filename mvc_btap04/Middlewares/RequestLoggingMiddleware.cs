using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace mvc_btap04.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Đo thời gian xử lý theo mili-giây (ms)
            var stopwatch = Stopwatch.StartNew();
            var time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            var method = context.Request.Method;
            var path = context.Request.Path.Value ?? string.Empty;

            // Chức năng 1: Ghi log thời gian, Method và Path
            Console.WriteLine($"[{time}] Method: {method} - Path: {path}");

            // Chức năng 3: Chặn truy cập ID = 0 hoặc ID = -1 (Cho cả /SinhVien/Detail và /Student/Detail)
            if (path.Equals("/SinhVien/Detail/0", StringComparison.OrdinalIgnoreCase) ||
                path.Equals("/SinhVien/Detail/-1", StringComparison.OrdinalIgnoreCase) ||
                path.Equals("/Student/Detail/0", StringComparison.OrdinalIgnoreCase) ||
                path.Equals("/Student/Detail/-1", StringComparison.OrdinalIgnoreCase))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "text/plain; charset=utf-8";
                await context.Response.WriteAsync("Student id không hợp lệ");
                return; // Ngắt luồng, không chuyển tiếp tới Controller
            }

            // Chuyển request tới Middleware / Controller tiếp theo
            await _next(context);

            stopwatch.Stop();
            // Chức năng 2: Ghi log Status Code và thời gian thực thi (ms) sau khi xử lý xong
            Console.WriteLine($"Status Code: {context.Response.StatusCode} - Elapsed: {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}