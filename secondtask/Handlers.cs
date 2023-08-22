using Exiled.API.Enums;
using Exiled.API.Features;
using Exiled.Events.EventArgs.Player;
using Exiled.Events.EventArgs.Server;
using PlayerRoles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace secondtask
{
    public sealed class Handler
    {
        public string HumanKillerText { get; set; } = "Игрок %name% убил %kills% игроков";

        private Dictionary<Exiled.API.Features.Player, int> humanKills = new();
        private Dictionary<Player, RoleTypeId> escapes = new();

        public void OnWaiting()
        {
            humanKills.Clear();
        }
        public void OnSpawned(SpawnedEventArgs ev)
        {
            if (ev.Reason != SpawnReason.Escaped || escapes.ContainsKey(ev.Player)) return;
            escapes.Add(ev.Player, ev.OldRole);
        }
        public void OnDied(DiedEventArgs ev)
        {
            if (ev.Attacker == null) return;
            else
            {
                if (!humanKills.ContainsKey(ev.Attacker)) humanKills.Add(ev.Attacker, 0);
                else humanKills[ev.Attacker]++;
            }
        }
        public void OnRoundEnd(RoundEndedEventArgs ev)
        {
            string text = "";

            Player? humanKiller = GetTopHumanKills();
            text += $"{HumanKillerText.Replace("%killer%", humanKiller.Nickname).Replace("%kills%", humanKills[humanKiller].ToString())}\n";
            foreach (Player ployer in Player.List)
            {
                ployer.Broadcast(20, text);
            }
        }
        private Player? GetTopHumanKills()
        {
            Player? player = null;
            int max = 0;

            foreach (KeyValuePair<Player, int> kvp in humanKills)
            {
                if (kvp.Value > max) { max = kvp.Value; player = kvp.Key; }
            }

            return player;
        }
    }
}
