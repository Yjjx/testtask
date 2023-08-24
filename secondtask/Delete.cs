using CommandSystem;
using Exiled.API.Features.Pickups;
using Exiled.API.Features;
using System;
using UnityEngine;
using Exiled.Events.Handlers;

namespace secondtask
{
    [CommandHandler(typeof(RemoteAdminCommandHandler))]
    internal class Delete : ICommand
    {
        public string Command => "delete";
        public string[] Aliases => new string[] { "del", "deleteall" };
        public string Description => "Удаляет предметы и труппы в радиусе";
        public bool Execute(ArraySegment<string> arguments, ICommandSender sender, out string response)
        {
            if (arguments.Count == 1 && int.TryParse(arguments.At(0), out var radius))
            {
                var PlayerGet = Exiled.API.Features.Player.Get(sender);
                Vector3 position = PlayerGet.Position;
                int rdcount = 0;
                int itemcount = 0;
                foreach (var doll in Ragdoll.List)
                {
                    if (Vector3.Distance(doll.Position, position) <= radius)
                    {
                        doll.Destroy();
                        rdcount++;
                    }
                }
                foreach (var item in Pickup.List)
                {
                    if (Vector3.Distance(item.Position, position) <= radius)
                    {
                        item.Destroy();
                        itemcount++;
                    }
                }
                response = $"Удалено {rdcount} трупов и {itemcount} предметов в {radius} радиусе";
                return true;
            }
            else
            {
                response = "Введите число";
                return false;
            }
            }
        }
}
