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
