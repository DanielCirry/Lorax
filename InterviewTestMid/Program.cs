using InterviewTestMid.Interfaces;
using InterviewTestMid.Models;
using InterviewTestMid.Repositories;
using InterviewTestMid.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

IHost _host = Host.CreateDefaultBuilder().ConfigureServices(services =>
{
    services.AddSingleton<ILogger, Logger>();
    services.AddSingleton<IPartRepository, PartRepository>();
})
    .Build();

ILogger logger = _host.Services.GetRequiredService<ILogger>();
logger.WriteLogMessage("Doing some JSON tasks...");
await logger.WriteCSV(new List<string>() { "abc",  "def", "ghi" });

List<MaterialDetails>? materialsWWithLinqQuery = logger.GetMaterialDetailsLinq("FOIL");

if (materialsWWithLinqQuery != null)
{ 
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
}

logger.ModifyPartWeightValue("BLUE TRAY", 2.00000M, "ModifiedSampleData.json");

logger.WriteLogMessage("Finished doing some JSON tasks.");