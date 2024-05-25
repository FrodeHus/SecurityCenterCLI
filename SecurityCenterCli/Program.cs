using Cocona;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using SecurityCenterCli;
using SecurityCenterCli.Authentication;
using SecurityCenterCli.Command;
using SecurityCenterCli.Infrastructure;
using SecurityCenterCli.Service;

var builder = CoconaApp.CreateBuilder();
builder.Logging.AddFilter("System.Net.Http", LogLevel.Warning);
builder.Services.AddTransient(s => Configuration.Load());
builder.Services.AddTransient<TokenClient>();
builder.Services.AddTransient<DefenderApiService>();
builder.Services.AddTransient<GraphService>();
builder.Services.AddHttpClient();

var app = builder.Build();
app.AddCommands<AccountCommands>();
app.AddCommands<ConfigCommands>();
app.AddCommands<DefenderCommands>();
app.AddCommands<SecureScoreCommands>();
app.Run();