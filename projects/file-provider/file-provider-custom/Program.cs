using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System.Collections;
using System.Text;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootFileProvider = new CustomFileProvider();
var app = builder.Build();

app.Run(async context =>
{
    context.Response.Headers.Add("content-type", "text/html");

    using (var stream = app.Environment.ContentRootFileProvider.GetFileInfo("").CreateReadStream())
    {
        var reader = new StreamReader(stream);
        await context.Response.WriteAsync(reader.ReadToEnd());
    }
});

app.Run();

public class CustomDirectoryContents : IDirectoryContents
{
    readonly IEnumerable<IFileInfo> _entries;

    public CustomDirectoryContents(IEnumerable<IFileInfo> files)
    {
        _entries = files;
    }

    public bool Exists => true;
    public IEnumerator<IFileInfo> GetEnumerator() => _entries.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _entries.GetEnumerator();
}

public class AlwaysTheSameFile : IFileInfo
{
    public bool Exists => true;
    public long Length { get; }
    public string PhysicalPath => null;
    public string Name { get; }
    public DateTimeOffset LastModified { get; }
    public bool IsDirectory => false;
    public Stream CreateReadStream()
    {
        var text = @"
            Dhritarashtra said: O Sanjaya, after my sons and the sons of Pandu assembled in the place of pilgrimage at Kurukshetra, desiring to fight, what did they do?
            ";

        return new MemoryStream(Encoding.UTF8.GetBytes(text));
    }
}

//https://docs.microsoft.com/en-us/aspnet/core/api/microsoft.extensions.fileproviders.ifileinfo#Microsoft_Extensions_FileProviders_IFileInfo
public class CustomDirectory : IFileInfo
{
    public bool Exists => true;
    public long Length => -1; // read the offical doc        
    public string PhysicalPath => null; // read the offical doc
    public string Name { get; }
    public DateTimeOffset LastModified { get; }
    public bool IsDirectory => true;

    public Stream CreateReadStream()
    {
        throw new InvalidOperationException("Create Stream is not applicable for directory");
    }
}

public class CustomFileProvider : IFileProvider
{
    public IDirectoryContents GetDirectoryContents(string subpath)
    {
        var list = new List<AlwaysTheSameFile>
            {
                new AlwaysTheSameFile()
            };

        var contents = new CustomDirectoryContents(list);
        return contents;
    }

    public IFileInfo GetFileInfo(string subpath) => new AlwaysTheSameFile();
    public IChangeToken Watch(string filter) => NullChangeToken.Singleton;
}
