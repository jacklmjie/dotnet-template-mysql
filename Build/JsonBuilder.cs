using dotnetmysql.Data;
using Newtonsoft.Json;

namespace dotnetmysql.Build
{
    public class JsonBuilder: ConfigBuilder
    {
        public JsonBuilder(string configPath) : base(configPath)
        {
        }

        protected override BuildConfig Deserialize(string content)
        {
            return JsonConvert.DeserializeObject<BuildConfig>(content);
        }
    }
}
