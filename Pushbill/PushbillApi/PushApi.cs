using System.Windows.Forms;

namespace Pushbill
{
    public class PushApi
    {
        static public void Send(string sTitle, string sMessage)
        {
            MessageBox.Show(sMessage, sTitle);
        }
    }
}
