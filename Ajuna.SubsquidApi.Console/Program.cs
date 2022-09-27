using Ajuna.SubsquidApi.Console;
using Microsoft.Extensions.DependencyInjection;
using StrawberryShake;

namespace Ajuna.Subsquid.Demo.Console;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddBajunClient()
            .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://bajun.explorer.subsquid.io/graphql"));

        IServiceProvider services = serviceCollection.BuildServiceProvider();

        var client = services.GetRequiredService<IBajunClient>();

        // Get 10 latest Blocks
        var result = await client.GetFirstTenBlocks.ExecuteAsync();
        result.EnsureNoErrors();

        foreach (var session in result.Data.Blocks) System.Console.WriteLine(session.Id);

        // Get Block by Id
        var block = await client.GetBlockById.ExecuteAsync("0000000000-35a06");
        block.EnsureNoErrors();

        System.Console.WriteLine("Hash of Block is:  " + block.Data.BlockById.Hash);

        await GetEvents(client);
    }

    public static async Task GetEvents(IBajunClient client)
    {
        var result = await client.GetEventsByName.ExecuteAsync("Balances.Transfer", 100);
        result.EnsureNoErrors();


        foreach (var @event in result.Data.Events) System.Console.WriteLine(@event.Id);
    }
}