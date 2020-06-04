using Microsoft.Azure.KeyVault;
using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;

namespace Api.Configuration
{
    public static class AzureKeyVaultConfigHostBuilderExtension
    {
        public static IHostBuilder ConfigureAzureKeyVault(this IHostBuilder hostBuilder)
        {
            hostBuilder.ConfigureAppConfiguration((context, config) =>
            {
                if (context.HostingEnvironment.IsProduction())
                    config.ConfigureProductionAzureKeyVault();
            });

            return hostBuilder;
        }

        private static void ConfigureProductionAzureKeyVault(this IConfigurationBuilder config)
        {
            var builtConfig = config.Build();
            
            // Ignore if no Key Vault is configured
            var keyVault = builtConfig.GetValue<string>("KeyVault");
            if (keyVault is null)
                return;

            var tokenProvider = new AzureServiceTokenProvider();
            var authCallback = new KeyVaultClient.AuthenticationCallback(tokenProvider.KeyVaultTokenCallback);
            var keyVaultClient = new KeyVaultClient(authCallback);

            config.AddAzureKeyVault(keyVault, keyVaultClient, new DefaultKeyVaultSecretManager());
        }
    }
}
