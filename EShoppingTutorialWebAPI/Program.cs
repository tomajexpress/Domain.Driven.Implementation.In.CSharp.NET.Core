namespace EShoppingTutorialWebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add Database Connection
        // Use GetConnectionString which is safer for environment variable overrides
        var connectionString = builder.Configuration.GetConnectionString("EShoppingTutorialDB");

        if (string.IsNullOrEmpty(connectionString))
        {
            // LOG it instead of THROWING it
            Console.WriteLine("CRITICAL ERROR: Connection string 'EShoppingTutorialDB' was not found!");
        }

        builder.Services.AddDbContext<EShoppingTutorialDbContext>(opts => opts.UseSqlServer(connectionString));

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

        // ---- Database Migration Logic Necessary for Docker/Kubernetes----
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<EShoppingTutorialDbContext>();
                // This is the "Magic" line: It creates the DB and tables if they don't exist
                // and applies any pending migrations.
                context.Database.Migrate();
                Console.WriteLine("Database migration completed successfully.");
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while migrating the database.");
            }
        }
        // ----------------------------------

        app.Run();
    }
}