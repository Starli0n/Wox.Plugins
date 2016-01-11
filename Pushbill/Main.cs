using Pushbill;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace Wox.Plugin.Pushbill
{
    public class Main : IPlugin
    {
        private const string SendCommand = "send";
        private const string ListCommand = "list";
        private const string PushbillApp = "PushbillApp.exe";
        private PluginInitContext m_context;

        public void Init(PluginInitContext context)
        {
            m_context = context;
        }

        public List<Result> Query(Query query)
        {
            List<Result> results = new List<Result>();

            if (string.IsNullOrEmpty(query.Search))
            {
                results.Add(ResultForSendCommandAutoComplete(query));
                results.Add(ResultForListCommandAutoComplete(query));
                return results;
            }

            string command = query.FirstSearch.ToLower();
            if (string.IsNullOrEmpty(command)) return results;

            if (command == SendCommand)
            {
                return ResultForSendMessage(query);
            }
            if (command == ListCommand)
            {
                return ResultForListMessages(query);
            }
            
            if (SendCommand.Contains(command))
            {
                results.Add(ResultForSendCommandAutoComplete(query));
            }
            if (ListCommand.Contains(command))
            {
                results.Add(ResultForListCommandAutoComplete(query));
            }

            return results;
        }

        private Result ResultForSendCommandAutoComplete(Query query)
        {
            string title = SendCommand;
            string subtitle = "send <Title> <Message>";
            return ResultForCommand(query, SendCommand, title, subtitle);
        }

        private Result ResultForListCommandAutoComplete(Query query)
        {
            string title = ListCommand;
            string subtitle = "list";
            return ResultForCommand(query, ListCommand, title, subtitle);
        }

        private Result ResultForCommand(Query query, string command, string title, string subtitle)
        {
            const string seperater = Plugin.Query.Seperater;
            var result = new Result
            {
                Title = title,
                IcoPath = "Images\\icon.png",
                SubTitle = subtitle,
                Action = e =>
                {
                    m_context.API.ChangeQuery($"{m_context.CurrentPluginMetadata.ActionKeyword}{seperater}{command}{seperater}");
                    return false;
                }
            };
            return result;
        }

        private List<Result> ResultForSendMessage(Query query)
        {
            List<Result> results = new List<Result>();

            string clipboard = GetClipboard();
            if (clipboard.StartsWith("http"))
                results.Add(ResultForSendMessage("Link", clipboard));

            if (!String.IsNullOrEmpty(query.ThirdSearch))
                results.Add(ResultForSendMessage(query.SecondSearch, query.ThirdSearch));
            else if (!String.IsNullOrEmpty(query.SecondSearch))
                results.Add(ResultForSendMessage("NoTitle", query.SecondSearch));

            else
                results.Add(new Result
                {
                    Title = SendCommand,
                    SubTitle = "send <Title> <Message>",
                    IcoPath = "Images\\icon.png",
                    Action = c =>
                    {
                        try
                        {
                            Process.Start(PushbillApp);
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

        private Result ResultForSendMessage(string sTitle, string sMessage)
        {
            return (new Result
            {
                Title = SendCommand,
                SubTitle = "send " + sTitle + " " + sMessage,
                IcoPath = "Images\\icon.png",
                Action = c =>
                {
                    try
                    {
                        PushApi.Send(sTitle, sMessage);
                        return true;
                    }
                    catch (Exception e)
                    {
                        m_context.API.ShowMsg("Error", e.Message, null);
                        return false;
                    }
                }
            });
        }

        private List<Result> ResultForListMessages(Query query)
        {
            List<Result> results = new List<Result>();
            results.Add(new Result
            {
                Title = ListCommand,
                SubTitle = "list",
                IcoPath = "Images\\icon.png",
                Action = c =>
                {
                    try
                    {
                        m_context.API.ShowMsg(ListCommand, "list", null);
                        return false;
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

        static public string GetClipboard()
        {
            string clipboard = "";
            Exception threadEx = null;
            Thread staThread = new Thread(
                delegate ()
                {
                    try
                    {
                        clipboard = Clipboard.GetText();
                    }
                    catch (Exception ex)
                    {
                        threadEx = ex;
                    }
                });
            staThread.SetApartmentState(ApartmentState.STA);
            staThread.Start();
            staThread.Join();
            return clipboard.Trim();
        }
    }
}
