using dotnetmysql.Data;
using Microsoft.Extensions.Configuration;
using System;

namespace dotnetmysql.Build
{
    public class DefaultBuilder : ConfigBuilder
    {
        private string APP_SETTINGS_PATH = "appsettings.json";
        private string AppDirectory => AppDomain.CurrentDomain.BaseDirectory;

        public DefaultBuilder(string configPath) : base(configPath)
        {
        }

        public override void Build()
        {
            var appSettingsbuilder = new ConfigurationBuilder()
                   .SetBasePath(AppDirectory)
                   .AddJsonFile(APP_SETTINGS_PATH, false, true);
            var configuration = appSettingsbuilder.Build();
            Config = configuration.Get<BuildConfig>();
            InitDefault();
        }

        protected override BuildConfig Deserialize(string content)
        {
            throw new NotImplementedException();
        }
    }
}
