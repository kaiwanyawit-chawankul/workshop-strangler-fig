var app = WebApplication.CreateBuilder(args).Build();
var sent = new List<object>();

app.MapPost("/send", (dynamic payload) => { sent.Add(payload); return Results.Accepted(); });
app.MapGet("/sent", () => sent);

app.Run();
