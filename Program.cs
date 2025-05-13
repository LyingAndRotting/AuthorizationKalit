using Authorization.Infrastructure;

namespace Authorization
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllers();
            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            var app = builder.Build();
            app.UseStaticFiles();
            app.UseCors("AllowAll");
            app.MapControllers();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Use(async (context, next) =>
            {
                if (context.Request.Path == "/")
                {
                    context.Response.Redirect("/page.html");
                    return;
                }
                await next();
            });
            app.Run();
        }
    }
}
