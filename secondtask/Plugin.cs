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
        private Handler handler;
        public static Report report = new Report();
        public override void OnEnabled()
        {
            base.OnEnabled();
            Log.Info("secondtask is enabled");
            Exiled.Events.Handlers.Player.TriggeringTesla += Player_TriggeringTesla;
            Exiled.Events.Handlers.Map.ExplodingGrenade += Map_ExplodingGrenade;
            Exiled.Events.Handlers.Player.Died += handler.OnDied;
            Exiled.Events.Handlers.Player.Spawned += handler.OnSpawned;
            Exiled.Events.Handlers.Server.WaitingForPlayers += handler.OnWaiting;
            Exiled.Events.Handlers.Server.RoundEnded += handler.OnRoundEnd;
        }
            private void Map_ExplodingGrenade(ExplodingGrenadeEventArgs ev)
            {
                foreach (Lift lift in Lift.List)
                {
                    if (!((ev.Position - lift.Position).sqrMagnitude < 13)) continue;
                    Random random = new Random();
                    var level = lift.CurrentLevel;
                    lift.TryStart(-level); break;

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
