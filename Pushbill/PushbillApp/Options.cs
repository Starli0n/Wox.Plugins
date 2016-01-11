using CommandLine;

namespace Pushbill
{
    public class Options
    {
        [Option('t', "title", HelpText = "Set title")]
        public string Title { get; set; }

        [Option('m', "message", HelpText = "Set message")]
        public string Message { get; set; }

        [Option("nogui", HelpText = "Send a message without GUI")]
        public bool NoGui { get; set; }
    }
}
