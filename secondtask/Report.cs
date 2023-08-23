using Exiled.API.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondtask
{
    public class Report
    {
        public List<string> reports = new List<string>();
        public void SendReport(Exiled.Events.EventArgs.Player.LocalReportingEventArgs report)
        {
            string reporting = $"Игрок [{report.Player.Id}] {report.Player.Nickname} отправил репорт на игрока [{report.Target.Id}] {report.Target.Nickname} Причина: {report.Reason}";
            foreach (var player in Player.List)
            {
                if (player.RemoteAdminAccess)
                {
                    player.Broadcast(7, reporting);
                }
            }
            reports.Add(reporting);
        }
    }
}
