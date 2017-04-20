using Microsoft.ServiceFabric.Services.Remoting.Client;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var t1 = Task.Run(async () =>
            {
                await UpdateLoop();
            });

            var t2 = Task.Run(async () =>
            {
                await UpdateLoop();
            });

            Task.WhenAll(t1, t2).GetAwaiter().GetResult();
        }

        static async Task UpdateLoop()
        {
            var uri = new Uri("fabric:/SFLockRepo/Stateful1");
            ITestService service = ServiceProxy.Create<ITestService>(uri, new Microsoft.ServiceFabric.Services.Client.ServicePartitionKey(0));

            while (true)
            {
                try
                {
                    Console.WriteLine("Calling update...");
                    await service.Update();
                    Console.WriteLine("Finished updating");
                }
                catch(Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
