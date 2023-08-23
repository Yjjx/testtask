using CommandSystem;
using Exiled.API.Features.Pickups;
using Exiled.API.Features;
using System;
using UnityEngine;
using Exiled.Events.Handlers;

namespace secondtask
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class Delete
    {
        public string Command => "delete";
        public string[] Aliases => new string[] { "del", "deleteall" };
        public string Description => "Удаляет предметы и труппы в радиусе";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            var PlayerGet = Exiled.API.Features.Player.Get(sender);
            if (float.TryParse(string.Join("", arguments), out float radius))
            {
                Vector3 position = PlayerGet.Position;
                int rc = 0;
                int pc = 0;
                foreach (var doll in Ragdoll.List)
                {
                    if (Vector3.Distance(doll.Position, position) <= radius)
                    {
                        doll.Destroy();
                        rc++;
                    }
                }
                foreach (var item in Pickup.List)
                {
                    if (Vector3.Distance(item.Position, position) <= radius)
                    {
                        item.Destroy();
                        pc++;
                    }
                }
                response = $"Удалено {rc} трупов и {pc} предметов в {radius} радиусе";
                return true;
            }
            else
            {
                response = "delete [радиус]";
                return false;
            }
        }
    }
}
