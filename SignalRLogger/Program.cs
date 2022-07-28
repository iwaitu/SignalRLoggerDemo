using IVilson.Utils.Logger.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllersWithViews();



builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("https://localhost:44480").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
}));



var app = builder.Build();
app.Services.AddSignalRLogger();
//var lf = app.Services.GetRequiredService<ILoggerFactory>();
//lf.AddProvider(new SignalRLoggerProvider(
//                    new SignalRLoggerConfiguration
//                    {
//                        ServiceProvider = app.Services,
//                        LogLevel = LogLevel.Information,
//                        GroupName = "LogMonitor"
//                    }));

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("corsapp");




app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.MapHub<SignalRLoggerHub>(SignalRLoggerHub.HubUrl);

app.Run();
