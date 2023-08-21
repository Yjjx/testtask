using Exiled.API.Features;
using Exiled.API.Features.Items;
using Exiled.Events.Handlers;
using InventorySystem;
using PlayerRoles;
using System;

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
