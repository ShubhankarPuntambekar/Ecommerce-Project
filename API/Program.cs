using Microsoft.EntityFrameworkCore;
using INFRASTRUCTURE.Data;
using CORE.Interfaces;
using API.Middleware;
using StackExchange.Redis;
using INFRASTRUCTURE.Services;
namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            builder.Services.AddDbContext<StoreContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            builder.Services.AddCors();
            builder.Services.AddSingleton<IConnectionMultiplexer>(config =>
            {
                var connectionString = builder.Configuration.GetConnectionString("Redis");
                if (connectionString == null) throw new Exception("Connection string not found(Redis)");
                var configuration = ConfigurationOptions.Parse(connectionString, true);
                return ConnectionMultiplexer.Connect(configuration);
            });

            builder.Services.AddSingleton<ICartServices,CartService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionMiddleware>();

            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200", "https://localhost:4200"));

            app.MapControllers();

            
            try
            {
                using var scope = app.Services.CreateScope();
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<StoreContext>();
                await context.Database.MigrateAsync();
                await StoreContextSeed.SeedAsync(context);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                throw; 
            }

            app.Run();
        }
    }
}
