Azure Key Vault Encryption & Azure Storage Blob - Key Store
========

This sample showcases data protection with keys encrypted using Azure Key Vault and stored in Azure Blob Storage.

* Azure Storage connection string is set up in appsettings.
    ```
    "DataProtection": 
    {
        ...
        "StorageConnectionString": "<storage connection string>",
        "ContainerName": "<container name>",
        "BlobName": "<blob name e.g. dpkeys.xml>"
        ...
    }
    ```
* Azure Key Vault URI is set up in appsettings.
    ```
    "DataProtection": 
    {
        ...
        "KeyId": "https://<your key vault name>.vault.azure.net/keys/<your key name>/<your key version>"        
        ...
    }
    ```
* Key encryption & persistence is set up in StartUp ConfigureServices().
    ```
    public void ConfigureServices(IServiceCollection services)
        {
            ...
            var storageConnectionString = Configuration["DataProtection:StorageConnectionString"];
            var containerName = Configuration["DataProtection:ContainerName"];
            var blobName = Configuration["DataProtection:BlobName"];
            var keyId = Configuration["DataProtection:KeyId"];
            services.AddDataProtection()
                    //Key Encryption
                    .ProtectKeysWithAzureKeyVault(new Uri(keyId),new ChainedTokenCredential(new ManagedIdentityCredential(), new AzureCliCredential()))
                    //Key Persistence
                    .PersistKeysToAzureBlobStorage(storageConnectionString,containerName,blobName);
            ...
        }
    ```

## Reference
[Data Protection Key Encryption using Azure Key Vault](https://github.com/Azure/azure-sdk-for-net/blob/Azure.Extensions.AspNetCore.DataProtection.Keys_1.0.3/sdk/extensions/Azure.Extensions.AspNetCore.DataProtection.Keys/README.md)

[Data Protection Key Persistence using Azure Storage Blob](https://github.com/Azure/azure-sdk-for-net/blob/Azure.Extensions.AspNetCore.DataProtection.Blobs_1.2.1/sdk/extensions/Azure.Extensions.AspNetCore.DataProtection.Blobs/README.md)

## Screenshot
<img src="assets/main-page.png">

## Credits
[Lohith GN](https://github.com/lohithgn)