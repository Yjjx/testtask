using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.EventArgs.Map;
using Exiled.Events.Handlers;
using Interactables.Interobjects;
using InventorySystem;
using PlayerRoles;
using System;
using System.Collections.Generic;

namespace secondtask
{
    public class Plugin: Plugin<Config>
    {
        public override string Name => "testtask";
        public override string Author => "yjjx";
        public override string Prefix => "ftt";
        public override void OnEnabled()
        {
            base.OnEnabled();
            Log.Info("secondtask is enabled");
            Exiled.Events.Handlers.Player.TriggeringTesla += Player_TriggeringTesla;
            Exiled.Events.Handlers.Map.ExplodingGrenade += Map_ExplodingGrenade;
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
