﻿using System;
using System.Windows.Forms;

namespace Pushbill
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                var options = new Options();
                CommandLine.Parser.Default.ParseArguments(args, options);

                if (options.NoGui)
                {
                    PushApi.Send(options.Title, options.Message);
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new PushbillApp(options));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }
            
        }
    }
}
