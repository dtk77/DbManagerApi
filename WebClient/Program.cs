using Microsoft.Net.Http.Headers;
using WebClient;
using WebClient.Contract;
using WebClient.Services;

var builder = WebApplication.CreateBuilder(args);

var conf = builder.Configuration;

builder.Services.Configure<WebApiConfig>
    (conf.GetSection("ApiSettings"));

builder.Services.AddHttpClient("ApiClient", client =>
{
    string baseUrl = conf.GetValue<string>("ApiSettings:BaseUrl");
    client.BaseAddress = new Uri(baseUrl);
    client.DefaultRequestHeaders.Clear();
    client.DefaultRequestHeaders.Add(HeaderNames.Accept, "application/json");
});

builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllersWithViews();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
