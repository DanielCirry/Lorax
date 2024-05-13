using InterviewTestMid.Interfaces;
using InterviewTestMid.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddSingleton<ILogger, Logger>();
})
    .Build();



var logger = _host.Services.GetRequiredService<ILogger>();
logger.WriteLogMessage("Doing some JSON tasks...");
await logger.WriteCSV(new List<string>() { "abc",  "def", "ghi" });

var materials = logger.GetMaterialDetails("FOIL");
foreach (var item in materials)
{
    logger.WriteLogMessage($"LookId: {item.Material.LookId}");
    logger.WriteLogMessage($"LookNbr: {item.Material.LookNbr}");
    logger.WriteLogMessage($"LookDesc: {item.Material.LookDesc}");

    logger.WriteLogMessage($"Percentage: {item.Percentage}");
    logger.WriteLogMessage($"MatrIsBarrier: {item.MatrIsBarrier}");
    logger.WriteLogMessage($"MatrIsDensier: {item.MatrIsDensier}");
    logger.WriteLogMessage($"MatrIsOppacifier: {item.MatrIsOppacifier}");
}

var materialsWWithLinqQuery = logger.GetMaterialDetailsLinq("FOIL"); ;
foreach (var item in materialsWWithLinqQuery)
{
    logger.WriteLogMessage($"LookId: {item.Material.LookId}");
    logger.WriteLogMessage($"LookNbr: {item.Material.LookNbr}");
    logger.WriteLogMessage($"LookDesc: {item.Material.LookDesc}");

    logger.WriteLogMessage($"Percentage: {item.Percentage}");
    logger.WriteLogMessage($"MatrIsBarrier: {item.MatrIsBarrier}");
    logger.WriteLogMessage($"MatrIsDensier: {item.MatrIsDensier}");
    logger.WriteLogMessage($"MatrIsOppacifier: {item.MatrIsOppacifier}");
}

logger.ModifyPartWeight(2.00000M);

logger.WriteLogMessage("Finished doing some JSON tasks.");