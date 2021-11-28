using Grpc.Net.Client;
using Grpc.Core;

var app = WebApplication.Create();

app.UseStaticFiles();

//Make sure that the grpc-server is run 
app.Run(async context =>
{
    var channel = GrpcChannel.ForAddress("https://localhost:5500"); //check the values at /server project
    var client = new Billboard.Board.BoardClient(channel);
    var result = client.ShowMessage(new Google.Protobuf.WellKnownTypes.Empty());

    context.Response.Headers["Content-Type"] = "text/html";

    await context.Response.WriteAsync("<html><body><h1>Kitty Streaming</h1>");

    using var tokenSource = new CancellationTokenSource();
    CancellationToken token = tokenSource.Token;

    var streamReader = result.ResponseStream;

    //get metdata
    string path = null;
    await streamReader.MoveNext(token);
    var initial = streamReader.Current;
    if (initial.ImageCase == Billboard.MessageReply.ImageOneofCase.MetaData)
    {
        var env = context.RequestServices.GetService<IWebHostEnvironment>();
        path = Path.Combine(env.WebRootPath, initial.MetaData.FileName);
    }

    if (path == null)
        throw new ApplicationException("Metadata is missing from the server");

    using FileStream file = new FileStream(path, FileMode.Create);

    int position = 0;
    await foreach (var data in streamReader.ReadAllAsync(token))
    {
        var chunk = data.Chunk;
        await file.WriteAsync(chunk.Data.ToByteArray(), 0, chunk.Length);
        position += chunk.Length;
        await context.Response.WriteAsync(position + " ");
    }

    file.Close();

    await context.Response.WriteAsync($"<img src=\"{initial.MetaData.FileName}\"/>");

    await context.Response.WriteAsync("</body></html>");
});

app.Run();