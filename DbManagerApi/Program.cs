using DbManagerApi.ServiceExtensions;
using Microsoft.AspNetCore.HttpOverrides;
using NLog;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(),
    "/nlog.config"));

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();

builder.Services.AddControllers();


var app = builder.Build();

if(app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else 
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
