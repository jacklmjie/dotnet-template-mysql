using dotnetmysql.Data;
using dotnetmysql.RazorTemplates;
using System.IO;

namespace dotnetmysql.Build
{
    public abstract class ConfigBuilder : IConfigBuilder
    {
        private string APP_SETTINGS_PATH = "appsettings.json";
        private string ConfigPath { get; }
        public BuildConfig Config { get; set; }
        public ITemplateEngine TemplateEngine { get; set; }

        protected ConfigBuilder(string configPath)
        {
            if (!configPath.Contains(APP_SETTINGS_PATH))
            {
                configPath = Path.Combine(configPath, APP_SETTINGS_PATH);
            }
            ConfigPath = configPath;
            Build();
        }

        public virtual void Build()
        {
            using (StreamReader configStream = new StreamReader(ConfigPath))
            {
                var jsonConfigStr = configStream.ReadToEnd();
                Config = Deserialize(jsonConfigStr);
            }
            InitDefault();
        }

        protected abstract BuildConfig Deserialize(string content);

        protected void InitDefault()
        {
            if (!Directory.Exists(Config.Output.Path))
            {
                Directory.CreateDirectory(Config.Output.Path);
            }
            TemplateEngine = new OfficialRazorTemplateEngine();
            TemplateEngine.Initialize();
        }
    }
}
