using System.Windows.Forms;

namespace PushbillApp
{
    public class PushApi
    {
        static public void Send(string sTitle, string sMessage)
        {
            MessageBox.Show(sMessage, sTitle);
        }
    }
}
