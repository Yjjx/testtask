using Exiled.API.Features;
using Exiled.Events.EventArgs.Map;
using System;
using System.Collections.Generic;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using System.Runtime.InteropServices;

namespace secondtask
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "testtask";
        public override string Author => "yjjx";
        public override string Prefix => "ftt";
        public static Plugin Instance;
        public static Report report = new Report();

        public override void OnEnabled()
        {
            base.OnEnabled();
            Log.Info("secondtask is enabled");
            Exiled.Events.Handlers.Player.TriggeringTesla += Player_TriggeringTesla;
            Exiled.Events.Handlers.Map.ExplodingGrenade += Map_ExplodingGrenade;
            Exiled.Events.Handlers.Server.WaitingForPlayers += Server_WaitingForPlayers;
        }

        private void Server_WaitingForPlayers()
        {
            report.reports.Clear();
        }

        private void Map_ExplodingGrenade(ExplodingGrenadeEventArgs granate)
            {
            var lift = Exiled.API.Features.Lift.Get(granate.Position);
            if (lift != null)
            {
                int level = lift.CurrentLevel;
                if (level == 1)
                {
                    level = 0;
                }
                else
                {
                    level = 1;
                }
                if (UnityEngine.Random.Range(0, 101) <= 50)
                {
                    lift.TryStart(level, true);
                }
            }
        }
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

        private void Player_TriggeringTesla(Exiled.Events.EventArgs.Player.TriggeringTeslaEventArgs ev)
            {
                var curitem = ev.Player.CurrentItem.Type;
                if (ev.Player.CurrentItem != null && Config.Items.Contains(ev.Player.CurrentItem.Type))
                {
                    ev.IsTriggerable = false;
                }
            }
        }
    }
