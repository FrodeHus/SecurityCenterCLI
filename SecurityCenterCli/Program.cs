using Microsoft.Extensions.DependencyInjection;

using QuiCLI;

using SecurityCenterCli.Authentication;
using SecurityCenterCli.Command;
using SecurityCenterCli.Infrastructure;
using SecurityCenterCli.Service;

var builder = QuicApp.CreateBuilder();

builder.Services.AddTransient(s => Configuration.Load());
builder.Services.AddTransient<TokenClient>();
builder.Services.AddTransient<DefenderApiService>();
builder.Services.AddTransient<GraphService>();
builder.Services.AddHttpClient();

var app = builder.Build();

app.AddCommand(s => new AccountCommands(s.GetRequiredService<TokenClient>()));

app.AddCommandGroup("account")
        .AddCommand(s => new TokenCommands(s.GetRequiredService<TokenClient>(), s.GetRequiredService<Configuration>()));

app.AddCommand(s => new ConfigCommands());

app.AddCommandGroup("device")
    .AddCommand(s => new DeviceCommands(s.GetRequiredService<DefenderApiService>()));

app.AddCommandGroup("secure-score")
    .AddCommand(s => new SecureScoreCommands(s.GetRequiredService<GraphService>()));

app.Run();