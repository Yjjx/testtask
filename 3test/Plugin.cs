using Exiled.API.Features;
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
        }

        private void Player_Died(Exiled.Events.EventArgs.Player.DiedEventArgs ev)
        {
            foreach (Player player in Player.List)
            {
                if (player.Role == RoleTypeId.FacilityGuard || player.Role == RoleTypeId.Scientist)
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
