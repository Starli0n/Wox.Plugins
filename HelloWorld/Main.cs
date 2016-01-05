using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Wox.Plugin;
using System.Windows.Forms;

namespace HelloWorld
{
    public class Main : IPlugin
    {
        private PluginInitContext m_context;

        public void Init(PluginInitContext context)
        {
            m_context = context;
        }

        public List<Result> Query(Query query)
        {
            var results = new List<Result>();
            results.Add(new Result
            {
                Title = "Hello World",
                IcoPath = "Images\\pic.png",
                SubTitle = "Simple plugin test",
                Action = c =>
                {
                    try
                    {
                        MessageBox.Show("Hello World !!");
                        m_context.API.HideApp();
                        return true;
                    }
                    catch (Exception e)
                    {
                        m_context.API.ShowMsg("Error", e.Message, null);
                        return false;
                    }
                }
            });
            return results;
        }
    }
}
