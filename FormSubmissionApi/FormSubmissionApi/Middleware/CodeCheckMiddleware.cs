using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using FormSubmissionApi.Models; // ✅ Adjust namespace if needed

namespace FormSubmissionApi.Middleware
{
    public class CodeCheckMiddleware
    {
        private readonly RequestDelegate _next;

        public CodeCheckMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, AppDbContext dbContext)
        {
            var code = context.Request.Query["code"].ToString();

            if (!string.IsNullOrEmpty(code))
            {
                var validCode = await dbContext.ValidCodes.FirstOrDefaultAsync(c => c.Value == code);
                if (validCode != null)
                {
                   
                    Console.WriteLine($"Code Found: ID={validCode.Id}, Value={validCode.Value}");

                    // Optionally, you can store it into HttpContext.Items
                    context.Items["RequestCodeId"] = validCode.Id;
                    context.Items["RequestCodeValue"] = validCode.Value;
                }
                else
                {
                    Console.WriteLine($"Code '{code}' not found in database.");
                }
            }

            // Call the next middleware in the pipeline
            await _next(context);
        }
    }
}
