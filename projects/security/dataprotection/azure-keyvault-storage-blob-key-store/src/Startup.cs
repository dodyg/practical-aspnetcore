using System;
using Azure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace PANC.DataProtection.AzKeyVaultStorageBlobKeyStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var storageConnectionString = Configuration["DataProtection:StorageConnectionString"];
            var containerName = Configuration["DataProtection:ContainerName"];
            var blobName = Configuration["DataProtection:BlobName"];
            var keyId = Configuration["DataProtection:KeyId"];
            services.AddDataProtection()
                    .ProtectKeysWithAzureKeyVault(new Uri(keyId),new ChainedTokenCredential(new ManagedIdentityCredential(), new AzureCliCredential()))
                    .PersistKeysToAzureBlobStorage(storageConnectionString,containerName,blobName);
            services.AddRazorPages();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
