using Exiled.API.Features;
using Hazards;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3test
{
    public class Plugin : Plugin<Config>
    {
        public override string Name => "3testtask";
        public override string Author => "yjjx";
        public override string Prefix => "ttt";
        public override void OnEnabled()
        {
            base.OnEnabled();
            Log.Info("ttt is enabled");
            Exiled.Events.Handlers.Player.Died += Player_Died;
            Exiled.Events.Handlers.Player.EnteringEnvironmentalHazard += Player_EnteringEnvironmentalHazard;
        }

        private void Player_EnteringEnvironmentalHazard(Exiled.Events.EventArgs.Player.EnteringEnvironmentalHazardEventArgs ev)
        {
            if (ev.EnvironmentalHazard is SinkholeEnvironmentalHazard)
            {
                ev.Player.RandomTeleport(typeof(Room));
            }
        }

        private void Player_Died(Exiled.Events.EventArgs.Player.DiedEventArgs ev)
        {
            foreach (Player player in Player.List)
            {
                if (player.Role == RoleTypeId.FacilityGuard || player.Role == RoleTypeId.Scientist)
                {
                    break;
                }
                if (Warhead.IsDetonated)
                {
                    break;
                }
                else
                {
                    Warhead.Start();
                }
            }
        }
    }
}
