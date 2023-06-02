using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Localization;
using ToolsBazaar.Domain.CustomerAggregate;
using ToolsBazaar.Domain.OrderAggregate;
using ToolsBazaar.Domain.ProductAggregate;
using ToolsBazaar.Persistence;
using ToolsBazaar.Web.Handlers;
using ToolsBazaar.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddAuthentication("ApiKey")
    .AddScheme<AuthenticationSchemeOptions, ApiKeyAuthenticationHandler>("ApiKey", options => { });

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

var requestCulture = new RequestCulture("en-US");
requestCulture.Culture.DateTimeFormat.ShortDatePattern = "MM-dd-yyyy";
app.UseRequestLocalization(new RequestLocalizationOptions
                           {
                               DefaultRequestCulture = requestCulture
                           });

app.MapControllerRoute("default",
                       "{controller}/{action=Index}/{id?}");

app.Run();

// Make the implicit Program class public so test projects can access it
public partial class Program { }
