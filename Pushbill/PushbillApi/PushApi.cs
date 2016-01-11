using Newtonsoft.Json;
using System.IO;
using System.Windows.Forms;

namespace Pushbill
{
    public class PushApi
    {
        private const string c_sConfig = "plugin.json";

        private class Config
        {
            public string ApiUrl { get; set; }
            public string ApiKey { get; set; }
        }
        static private Config config = null;
        
        static private void Initialize()
        {
            if (config != null) return;
            string json = File.ReadAllText(c_sConfig);
            config = JsonConvert.DeserializeObject<Config>(json);
        }

        static public void Send(string sTitle, string sMessage)
        {
            Initialize();
            MessageBox.Show(sMessage, sTitle);
        }
    }
}
