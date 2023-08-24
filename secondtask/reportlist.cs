using CommandSystem;
using Exiled.API.Features;
using System;

namespace secondtask
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class reportlist : ICommand
    {
        public string Command => "reportlist";

        public string[] Aliases => new string[] { "rl" };
        public string Description => "Показывает список репортов";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            response = string.Join("\n", Plugin.report.reports);
            return true;
        }
    }
}
