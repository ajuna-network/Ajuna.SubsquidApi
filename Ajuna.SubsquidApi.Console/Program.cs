using Ajuna.SubsquidApi.Console;
using Microsoft.Extensions.DependencyInjection;
using StrawberryShake;

namespace Ajuna.Subsquid.Demo.Console
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            
            serviceCollection.AddBajunClient()
                .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://bajun.explorer.subsquid.io/graphql"));
            
            IServiceProvider services = serviceCollection.BuildServiceProvider();
            
            IBajunClient client = services.GetRequiredService<IBajunClient>();
            
            // Get 10 latest Blocks
            IOperationResult<IGetFirstTenBlocksResult> result = await client.GetFirstTenBlocks.ExecuteAsync();
            result.EnsureNoErrors();
            
            foreach (var session in result.Data.Blocks)
            {
                System.Console.WriteLine(session.Id);
            }
            
        }



    }

}