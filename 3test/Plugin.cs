using Exiled.API.Features;
using Hazards;
using MEC;
using PlayerRoles;
using PluginAPI.Roles;
using Respawning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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
            Exiled.Events.Handlers.Scp914.UpgradingPlayer += Scp914_UpgradingPlayer;
            Exiled.Events.Handlers.Player.ChangingRole += Player_ChangingRole;
            Exiled.Events.Handlers.Warhead.ChangingLeverStatus += Warhead_ChangingLeverStatus;
        }

        private void Warhead_ChangingLeverStatus(Exiled.Events.EventArgs.Warhead.ChangingLeverStatusEventArgs ev)
        {
            if (Warhead.CanBeStarted)
            {
                Status = "Рычаг on";
            }
            else
            {
                Status = "Рычаг off";
            }
        }

        private void Player_ChangingRole(Exiled.Events.EventArgs.Player.ChangingRoleEventArgs ev)
        {
            switch (RespawnTokensManager.DominatingTeam)
            {
                case SpawnableTeamType.NineTailedFox:
                    Team = "<color=blue>MTF";
                    break;
                case SpawnableTeamType.ChaosInsurgency:
                    Team = "<color=green>CI";
                    break;
            }

            if (ev.NewRole == RoleTypeId.Spectator || ev.NewRole == RoleTypeId.Overwatch)
            {
                Spectators.Add(ev.Player);
            }
            else
            {
                Spectators.Remove(ev.Player);
            }

            Timing.RunCoroutine(MyCoroutine());
        }

        private void Scp914_UpgradingPlayer(Exiled.Events.EventArgs.Scp914.UpgradingPlayerEventArgs ev)
        {
            var dmg = Config.CDamage;
            var efct = Config.CEffect;
            var tp = Config.CTp;
            var PlayerGet = ev.Player;
            if (UnityEngine.Random.Range(1, 101) < dmg)
            {
                int dmgam = UnityEngine.Random.Range(1, 150);
                PlayerGet.Hurt(dmgam);
            }
            if (UnityEngine.Random.Range(1, 101) < tp)
            {
                ev.IsAllowed = false;
                PlayerGet.RandomTeleport(typeof(Room));
            }
            if (UnityEngine.Random.Range(1,101) < efct)
            {
                PlayerGet.EnableEffect(Config.NEffect.RandomItem());
            }
        }
        public static List<Player> Spectators { get; } = new List<Player>() { };
        public static string Team = "<color=blue>MTF";
        public static string Status = "Рычаг off";
        public IEnumerator<float> MyCoroutine()
        {
            for (; ; )
            {
                var roundtime = $"{Round.ElapsedTime.Hours}:{Round.ElapsedTime.Minutes}:{Round.ElapsedTime.Seconds}";
                var timespawn = $"{Respawn.TimeUntilSpawnWave.Minutes}:{Respawn.TimeUntilSpawnWave.Seconds} до спавна {Team}";

                foreach (Player player in Spectators)
                {
                    player.ShowHint($"<align=right>Время раунда: {roundtime}\nСтатус рычага: {Status}\n{timespawn}</align>", 1);
                }
                yield return Timing.WaitForSeconds(1f);
            }
        }
        private void Player_EnteringEnvironmentalHazard(Exiled.Events.EventArgs.Player.EnteringEnvironmentalHazardEventArgs ev)
        {
            if (ev.EnvironmentalHazard is SinkholeEnvironmentalHazard && !ev.Player.IsScp)
            {
                ev.Player.Teleport(Room.Random().Position);
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
