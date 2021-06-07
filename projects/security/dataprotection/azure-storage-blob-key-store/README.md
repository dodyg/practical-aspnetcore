Azure Storage Blob Key Store
========

This sample showcases data protection with keys stored in Azure Blob Storage.

* Azure Storage connection string is set up in appsettings.
    ```
    "DataProtection": 
    {
        "StorageConnectionString": "<storage connection string>",
        "ContainerName": "<container name>",
        "BlobName": "<blob name e.g. dpkeys.xml>"
    }
    ```
* Key persistence is set up in StartUp ConfigureServices().
    ```
    public void ConfigureServices(IServiceCollection services)
        {
            var storageConnectionString = Configuration["DataProtection:StorageConnectionString"];
            var containerName = Configuration["DataProtection:ContainerName"];
            var blobName = Configuration["DataProtection:BlobName"];
            services.AddDataProtection()
                    .PersistKeysToAzureBlobStorage(storageConnectionString,containerName,blobName);
            ...
        }
    ```

## Screenshot
<img src="assets/main-page.png">

## Credits
[Lohith GN](https://github.com/lohithgn)