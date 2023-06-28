using FagestProAdmin;
using FagestProAdmin.Models;
using FagestProAdmin.Models.General;
using FagestProAdmin.Services.Interfaces.Login;
using FagestProAdmin.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using FagestProAdmin.Services.Interfaces.ILicence;
using FagestProAdmin.Services.Interfaces.ICustomer;
using FFagestProAdmin.Services.Implementation;
using FagestProAdmin.Services.Implementation;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
var services = builder.Services;
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

AppGeneral.loginResponse = new LoginResponse
{
    userId = 1,
    activationCode = "379299",
    token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImN2YXJlbGEiLCJjdmFyZWxhIjoiQWRkcmVzc2VzLENyZWF0ZSIsIkNvbXBhbnlJZCI6IjAiLCJpZCI6IjEiLCJuYmYiOjE2ODc5NTE4OTgsImV4cCI6MTY4Nzk1NTQ5OCwiaWF0IjoxNjg3OTUxODk4LCJpc3MiOiJPbmlyaWNvQ3JlYXRpdmUiLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0NDQ1In0.LwPXNyus0T97nq1JZ8NdT3UXb2XfRz7lHwLFfOD3gp4",
    refreshToken = "JJhdP0lD3AkoCBqR27YFVyNgXV3bX7RMRUi3kCCsx/0="
};
services.AddOptions();
services.AddAuthorizationCore();
services.AddScoped<IAuthService, AuthService>();
services.AddScoped<IKeyManager, KeyManagerService>();
services.AddScoped<ICustomerService, CustomerService>();

services.AddScoped<CustomStateProvider>();
services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());

services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
