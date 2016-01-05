using System;
using System.Collections.Generic;

namespace Wox.Plugin.Pushbill
{
    public class Main : IPlugin
    {
        private const string SendCommand = "send";
        private const string ListCommand = "list";
        private PluginInitContext m_context;

        public void Init(PluginInitContext context)
        {
            m_context = context;
        }

        public List<Result> Query(Query query)
        {
            //m_context.API.ShowMsg("Hello", "World", null);
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
            /*

            results.Add(new Result
            {
                Title = m_context.CurrentPluginMetadata.ActionKeyword + " send",
                SubTitle = "Send a message",
                IcoPath = "Images\\icon.png",
                Action = c =>
                {
                    try
                    {
                        m_context.API.ChangeQuery(m_context.CurrentPluginMetadata.ActionKeyword + " send ");
                        return false;
                    }
                    catch (Exception e)
                    {
                        m_context.API.ShowMsg("Error", e.Message, null);
                        return false;
                    }
                }
            });
            results.Add(new Result
            {
                Title = m_context.CurrentPluginMetadata.ActionKeyword + " list",
                SubTitle = "List last messages",
                IcoPath = "Images\\icon.png",
                Action = c =>
                {
                    try
                    {
                        m_context.API.ChangeQuery(m_context.CurrentPluginMetadata.ActionKeyword + " list ");
                        return false;
                    }
                    catch (Exception e)
                    {
                        m_context.API.ShowMsg("Error", e.Message, null);
                        return false;
                    }
                }
            });
            return results;*/
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
            results.Add(new Result
            {
                Title = SendCommand,
                SubTitle = "send <Title> <Message>",
                IcoPath = "Images\\icon.png",
                Action = c =>
                {
                    try
                    {
                        m_context.API.ShowMsg(SendCommand, "send <Title> <Message>", null);
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
    }
}
