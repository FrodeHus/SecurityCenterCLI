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

builder.Commands.Add<AccountCommands>().WithCommand("token", x => x.Login);
builder.Commands
    .WithGroup("account")
    .Add<TokenCommands>()
    .WithCommand("token", x => x.GetAccessToken)
    .WithCommand("set-credentials", x => x.SetCredentials);
var deviceGroup = builder.Commands
    .WithGroup("device");

deviceGroup.Add<DeviceCommands>()
    .WithCommand("list", x => x.DeviceList);

deviceGroup
    .WithGroup("vulnerability")
    .Add<DeviceCommands>()
    .WithCommand("list", x => x.VulnerabilityList);

deviceGroup
    .WithGroup("recommendation")
    .Add<DeviceCommands>()
    .WithCommand("list", x => x.RecommendationList);

builder
    .Commands
    .WithGroup("secure-score")
    .Add<SecureScoreCommands>()
    .WithCommand("list", x=> x.SecureScoreList)
    .WithCommand("profiles", x => x.GetSecurityControlProfiles);

var app = builder.Build();


app.Run();