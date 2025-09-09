using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using Presentation;

using Application;
using Infrastructure;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5000/") });
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:5287/") });

// ...existing code...

await builder.Build().RunAsync();
