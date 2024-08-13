using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;

class Program
{
    private static readonly string echoServiceUrl = "http://localhost:5059/e";
    private static readonly HttpClient client = new();

    static async Task Main(string[] args)
    {
        Console.WriteLine("shouting...");

        var i = 0;
        while (true)
        {
            i++;
            var cts = new CancellationTokenSource();
            var progressTask = Task.Run(() => ShowProgress(cts.Token), cts.Token);
            var path = $"{echoServiceUrl}/{i}";
            Console.WriteLine($"requesting {path}");
            var response = await client.GetAsync($"{path}", cts.Token);
            cts.Cancel();
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            progressTask.Wait();
        }

    }
    private static void ShowProgress(CancellationToken token)
    {
        while (!token.IsCancellationRequested)
        {
            Console.Write(".");
            Thread.Sleep(TimeSpan.FromSeconds(1));
        }
    }
}