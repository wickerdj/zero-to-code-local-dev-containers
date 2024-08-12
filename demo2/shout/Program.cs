using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;

class Program
{
    private static readonly string echoServiceUrl = Environment.GetEnvironmentVariable("ECHO_SERVICE_URL") ??
        // throw new InvalidOperationException("ECHO_SERVICE_URL environment variable is not set");
        "http://localhost:5059/e";
    private static readonly HttpClient client = new();

    static async Task Main(string[] args)
    {
        Console.WriteLine("shouting...");

        foreach (var i in Enumerable.Range(1, 10))
        {
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