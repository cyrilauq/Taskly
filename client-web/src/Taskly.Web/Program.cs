using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Taskly.Web.Application;
using Taskly.Web;
using Microsoft.AspNetCore.Components.Authorization;
using Taskly.Web.Provider;
using Taskly.Web.Infrastructure;
using Blazored.Toast;
using System.Net.Http;
using System.Text;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

IConfiguration configuration = builder.Configuration;

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.ConfigureApplicationServices(configuration)
    .AddApplicationInfrastructure();
builder.Services.AddBlazorBootstrap();
builder.Services.AddBlazoredToast();

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddSingleton<AuthenticationStateProvider, AuthStateProvider>();

var app = builder.Build();

await app.RunAsync();
