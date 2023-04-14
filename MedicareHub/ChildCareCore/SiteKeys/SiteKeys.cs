using Microsoft.Extensions.Configuration;

namespace ChildCareCore.SiteKeys
{
    public static class SiteKeys
    {

        private static IConfigurationSection configuration;

        public static void Configure(IConfigurationSection _configuration)
        {
            configuration = _configuration;
        }



        public static string Domain => configuration["Domain"];




    }
}
