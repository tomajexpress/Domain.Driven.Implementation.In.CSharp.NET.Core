namespace EShoppingTutorialWebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // 1. Add Database Connection
        builder.Services.AddDbContext<EShoppingTutorialDbContext>(opts => opts.UseSqlServer(builder.Configuration["ConnectionStrings:EShoppingTutorialDB"]));

        // 2. Add MediatR (Assembly Scanning)
        builder.Services.AddCoreApplication();

        // 3. Add Controllers with our AOP Exception Filter
        builder.Services.AddControllers(options => options.Filters.Add<AopExceptionHandlerFilter>());

        // 4. Add Fluent Validation. This scans the Web API project for all AbstractValidator classes
        builder.Services.AddValidatorsFromAssemblyContaining<OrderSaveRequestModelValidator>();

        // 5. Add AutoMapper
        builder.Services.AddAutoMapper(AutoMappingProfileConfigs.AddAutoMapperConfigs);

        // 6. Add Swagger/OpenAPI for testing
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // 7. Register Application Services and Repositories
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

        // -------- Build the app --------

        var app = builder.Build();

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = string.Empty;
            });
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.UseStaticFiles();
        app.UseRouting();
        app.Run();
    }
}