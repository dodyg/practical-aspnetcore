using System;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace CustomEncryptor.Extensions
{
    public static class DependencyInjection
    {
        public static IDataProtectionBuilder UseXmlEncryptor(
            this IDataProtectionBuilder builder,
            Func<IServiceProvider, IXmlEncryptor> factory)
        {
            builder.Services.AddSingleton<IConfigureOptions<KeyManagementOptions>>(serviceProvider =>
            {
                var instance = factory(serviceProvider);
                return new ConfigureOptions<KeyManagementOptions>(options =>
                {
                    options.XmlEncryptor = instance;
                });
            });

            return builder;
        }
    }
}
