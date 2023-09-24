namespace FMS._23.V1.REPOSITORY
{
    public static class StaticConfig
    {
        public static string GetConnectionString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Specify the base path for your appsettings.json
                .AddJsonFile("appsettings.json") // Specify the name of your configuration file
                .Build();

            string connectionString = configuration.GetConnectionString("Conn");

            return connectionString;
        }
    }
}
