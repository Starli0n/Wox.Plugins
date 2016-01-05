using System;
using System.Windows.Forms;

namespace PushbillApp
{
    public partial class PushbillApp : Form
    {
        public PushbillApp(Options options)
        {
            InitializeComponent();
            tbTitle.Text = options.Title;
            rtbMessage.Text = options.Message;
        }

        private void bSend_Click(object sender, EventArgs e)
        {
            PushApi.Send(tbTitle.Text, rtbMessage.Text);
            Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
