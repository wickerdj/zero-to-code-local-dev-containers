var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.MapGet("/e/{id}", (int id) =>
{
    Thread.Sleep(1000);
    return $"echo {id}";
});


app.Run();
