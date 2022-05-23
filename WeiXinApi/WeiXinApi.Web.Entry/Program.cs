var builder = WebApplication.CreateBuilder(args).Inject();
builder.WebHost.UseUrls("http://*:5000");
var app = builder.Build();
app.Run();