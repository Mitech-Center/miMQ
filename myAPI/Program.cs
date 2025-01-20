using Microsoft.AspNetCore.HttpOverrides;
using myAPI.Contracts;
using myAPI.Extensions;
using NLog;

var builder = WebApplication.CreateBuilder(args);
// Add Log
LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
// Add Repository Manager
builder.Services.ConfigureRepositoryManager();
// Add Service Manager
builder.Services.ConfigureServiceManager();
// Add SQL config
builder.Services.ConfigureSqlContext(builder.Configuration);
// Add Auto mapper
builder.Services.AddAutoMapper(typeof(Program));
// Add ApiVersioning
builder.Services.ConfigureVersioning();
// Add Swagger
builder.Services.ConfigureSwagger();
// Add services to the container.
builder.Services.AddControllers()
    .AddApplicationPart(typeof(myAPI.Presentation.AssemblyReference).Assembly);
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();


var app = builder.Build();
//if (app.Environment.IsDevelopment())
//    app.UseDeveloperExceptionPage();
//else
//    app.UseHsts();
// Configure the HTTP request pipeline.
var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);
if (app.Environment.IsProduction())
    app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseAuthorization();
// Test middleware.
// Using MapWhen if request contains query string
//app.MapWhen(context => context.Request.Query.ContainsKey("qr"), builder =>
//{
//    builder.Run(async context =>
//    {
//        await context.Response.WriteAsync("Hello from the MapWhen branch.");
//    });
//});
//app.Use(async (context, next) =>
//{
//    Console.WriteLine($"Logic before executing the next delegate in the Use method");
//    await next.Invoke();
//    Console.WriteLine($"Logic after executing the next delegate in the Use method");
//});
//app.Run(async context =>
//{
//    Console.WriteLine($"Writing the response to the client in the Run method");
//    await context.Response.WriteAsync("Hello from the middleware component.");
//});
app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi_API");
});

app.MapControllers();

app.Run();
