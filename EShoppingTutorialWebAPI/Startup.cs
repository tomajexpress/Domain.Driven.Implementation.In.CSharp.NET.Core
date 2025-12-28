namespace EShoppingTutorialWebAPI;
public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(options => options.Filters.Add(new AopExceptionHandlerFilter()));

        // This scans the Web API project for all AbstractValidator classes
        services.AddValidatorsFromAssemblyContaining<OrderSaveRequestModelValidator>();

        // Register AutoMapper: scans loaded assemblies for Profile implementations.

        services.AddAutoMapper(cfg =>
        {
            AutoMappingProfileConfigs.AddAutoMapperConfigs(cfg);
        });

        // Register the Swagger generator, defining 1 or more Swagger documents 
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "EShopping Tutorial WebAPI",
                Description = "ASP.NET Core Web API",
                Contact = new OpenApiContact
                {
                    Name = "EShopping Tutorial Web API",
                    Email = string.Empty,
                    Url = new Uri("https://github.com/tomajexpress/Domain.Driven.Implementation.In.CSharp.NET.Core"),
                },
                License = new OpenApiLicense
                {
                    Name = "Aman Toumaj",
                    Url = new Uri("https://www.linkedin.com/in/aman-toumaj-92114051/"),
                }
            });

            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                c.IncludeXmlComments(xmlPath);
            }
        });

        // Register the Swagger services
        services.AddSwaggerDocument();

        services.AddOpenApiDocument();

        services.AddDbContext<EShoppingTutorialDbContext>(opts => opts.UseSqlServer(Configuration["ConnectionStrings:EShoppingTutorialDB"]));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IOrderDomainService, OrderDomainService>();

        services.AddApplication();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        // Enable middleware to serve generated Swagger as a JSON endpoint.
        app.UseSwagger();

        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
        // specifying the Swagger JSON endpoint.
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            c.RoutePrefix = string.Empty;
        });

        app.UseStaticFiles();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
