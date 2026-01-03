namespace EShoppingTutorialWebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add Database Connection
        builder.Services.AddDbContext<EShoppingTutorialDbContext>(opts => opts.UseSqlServer(builder.Configuration["ConnectionStrings:EShoppingTutorialDB"]));

        // Add Core Application (Assembly Scanning)
        builder.Services.AddCoreApplicationConfigs();

        // Presentation Layer Configs 
        builder.Services.AddControllers();
        builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
        builder.Services.AddProblemDetails();

        // Add Swagger/OpenAPI for testing
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Register Application Services and Repositories
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddTransient<ITaxCalculationService, ExternalTaxProvider>();

        // -------- Build the app --------

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.MapOrderEndpoints();
        app.UseExceptionHandler();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.UseStaticFiles();
        app.UseRouting();
        app.Run();
    }
}